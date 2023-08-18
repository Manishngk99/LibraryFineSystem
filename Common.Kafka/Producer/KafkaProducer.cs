using System;
using System.Threading.Tasks;
using Common.Kafka.Interfaces;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace Common.Kafka.Producer
{
    /// <summary>
    /// Base class for implementing Kafka Producer.
    /// </summary>
    /// <typeparam name="TKey">Indicates message's key in Kafka topic</typeparam>
    /// <typeparam name="TValue">Indicates message's value in Kafka topic</typeparam>
    public class KafkaProducer<TKey, TValue> : IDisposable, IKafkaProducer<TKey, TValue> where TValue : class
    {
        private readonly IProducer<TKey, TValue> _producer;
        private readonly ProducerConfig _config;
        /// <summary>
        /// Initializes the producer
        /// </summary>
        /// <param name="config"></param>
        public KafkaProducer(ProducerConfig config)
        {
            _producer = new ProducerBuilder<TKey, TValue>(config).SetValueSerializer(new KafkaSerializer<TValue>()).Build();
            _config = config;
        }

        /// <summary>
        /// Triggered when the service creates Kafka topic.
        /// </summary>
        /// <param name="topic">Indicates topic name</param>
        /// <param name="key">Indicates message's key in Kafka topic</param>
        /// <param name="value">Indicates message's value in Kafka topic</param>
        /// <returns></returns>
        public async Task ProduceAsync(string topic, TKey key, TValue value)
        {
            using (var adminClient = new AdminClientBuilder(_config).Build())
            {
                var setData = adminClient.GetMetadata(TimeSpan.FromSeconds(15));
                if (setData.Topics.All(x => x.Topic != topic))
                {
                    var topicSpec = new TopicSpecification
                    {
                        Name = topic,
                        NumPartitions = 1,
                        ReplicationFactor = 3
                    };
                    await adminClient.CreateTopicsAsync(new[] { topicSpec });
                }

            }

            await _producer.ProduceAsync(topic, new Message<TKey, TValue> { Key = key, Value = value });
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _producer.Flush();
            _producer.Dispose();
        }
    }
}
