using Common.Kafka.Consumer;
using Common.Kafka.Interfaces;
using Common.Kafka.Producer;
using Confluent.Kafka;
using FineTable.Application.DTO.Request;
using FineTable.Application.Kafka.Consumer;
using FineTable.Application.Kafka.Handler;
using FineTable.Application.Manager.Implementation;
using FineTable.Application.Manager.Interface;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineTable.Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddInApplicationServices(this IServiceCollection services, IConfiguration Configuration)
        {
			var clientConfig = new ClientConfig()
			{
				SaslUsername = Configuration["KafkaConfig:SaslUsername"],
				BootstrapServers = Configuration["KafkaConfig:BootstrapServers"],
				SaslPassword = Configuration["KafkaConfig:SaslPassword"],
				SaslMechanism = SaslMechanism.Plain,
				SecurityProtocol = SecurityProtocol.SaslSsl,                                                                                                                             
				EnableSslCertificateVerification = false // to de force ssl in local 
			};

			var consumerConfig = new ConsumerConfig(clientConfig);

			services.AddSingleton(consumerConfig);

			services.AddSingleton(typeof(IKafkaConsumer<,>), typeof(KafkaConsumer<,>));

			services.AddScoped<IFineManager, FineManager>();
            services.AddScoped<IFineCollectionManager, FineCollectionManager>();

			services.AddHostedService<IssueDetailConsumer>();
			services.AddScoped<IssueHandler>();
			services.AddScoped<IKafkaHandler<string, FineCollectionDetailRequest>, IssueHandler>();

			return services;
        }
    }
}
