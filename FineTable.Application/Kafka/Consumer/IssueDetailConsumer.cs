using Common.Kafka.Interfaces;
using Common.Kafka.Model;
using Confluent.Kafka;
using FineTable.Application.DTO.Request;
using FineTable.Application.Kafka.Topic;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FineTable.Application.Kafka.Consumer
{
	public class IssueDetailConsumer : BackgroundService
	{
		private readonly IKafkaConsumer<string, FineCollectionDetailRequest> _consumer;

		public IssueDetailConsumer(IKafkaConsumer<string, FineCollectionDetailRequest> kafkaConsumer)
		{
			_consumer = kafkaConsumer;

		}
		ConsumerGroupIdModel model = new ConsumerGroupIdModel()
		{
			GroupId = "business-group",
			EnableAutoCommit = true,
			AutoOffsetReset = AutoOffsetReset.Earliest,
			StatisticsIntervalMs = 5000,
			SessionTimeoutMs = 6000
		};
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			try
			{
				await _consumer.Consume(KafkaTopic.FineCollectionDetails, stoppingToken, model);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"{(int)HttpStatusCode.InternalServerError} ConsumeFailedOnTopic - {KafkaTopic.FineCollectionDetails}, {ex}");
			}
		}

		public override void Dispose()
		{
			_consumer.Close();
			_consumer.Dispose();

			base.Dispose();
		}
	}
}
