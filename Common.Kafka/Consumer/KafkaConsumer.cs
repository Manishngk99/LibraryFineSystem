using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Kafka.Interfaces;
using Common.Kafka.Model;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Kafka.Consumer
{
    /// <summary>
    /// Base class for implementing Kafka Consumer.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class KafkaConsumer<TKey, TValue> : IKafkaConsumer<TKey, TValue> where TValue : class
    {
        private readonly ConsumerConfig _config;
        private IKafkaHandler<TKey, TValue> _handler;
        private IConsumer<TKey, TValue> _consumer;
        private string _topic;
        private List<string> _topics;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// Indicates constructor to initialize the serviceScopeFactory and ConsumerConfig
        /// </summary>
        /// <param name="config">Indicates the consumer configuration</param>
        /// <param name="serviceScopeFactory">Indicates the instance for serviceScopeFactory</param>
        public KafkaConsumer(ConsumerConfig config, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _config = config;
        }

        /// <summary>
        /// Triggered when the service is ready to consume the Kafka topic.
        /// </summary>
        /// <param name="topic">Indicates Kafka Topic</param>
        /// <param name="stoppingToken">Indicates stopping token</param>
        /// <returns></returns>
        public async Task Consume(string topic, CancellationToken stoppingToken, ConsumerGroupIdModel groupModel)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            _config.GroupId = groupModel.GroupId;
            _config.EnableAutoCommit = groupModel.EnableAutoCommit;
            _config.AutoOffsetReset = groupModel.AutoOffsetReset;
            _config.StatisticsIntervalMs = groupModel.StatisticsIntervalMs;
            _config.SessionTimeoutMs = groupModel.SessionTimeoutMs;
            _handler = scope.ServiceProvider.GetRequiredService<IKafkaHandler<TKey, TValue>>();
            if (groupModel.IsEvent)
            {
                _consumer = new ConsumerBuilder<TKey, TValue>(_config).Build();
            }
            else
            {
                _consumer = new ConsumerBuilder<TKey, TValue>(_config).SetValueDeserializer(new KafkaDeserializer<TValue>()).Build();

            }
            if (!topic.Contains(","))
            {
                _topic = topic;
            }
            else
            {
                _topics = topic.Split(",").ToList();
            }

            await Task.Run(() => StartConsumerLoop(stoppingToken), stoppingToken);
        }

        /// <summary>
        /// This will close the consumer, commit offsets and leave the group cleanly.
        /// </summary>
        public void Close()
        {
            _consumer.Close();
        }

        /// <summary>
        /// Releases all resources used by the current instance of the consumer
        /// </summary>
        public void Dispose()
        {
            _consumer.Dispose();
        }

        private async Task StartConsumerLoop(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(_topic))
            {
                _topics = new List<string>();
                _topics.Add(_topic);
                _consumer.Subscribe(_topics);

            }
            else
            {
                _consumer.Subscribe(_topics);
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(cancellationToken);

                    if (result != null)
                    {
                        await _handler.HandleAsync(result.Message.Key, result.Message.Value);
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ConsumeException e)
                {
                    // Consumer errors should generally be ignored (or logged) unless fatal.
                    Console.WriteLine($"Consume error: {e.Error.Reason}");

                    if (e.Error.IsFatal)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unexpected error: {e}");
                    break;
                }
            }
        }

    }
}
