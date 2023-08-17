using Common.Kafka.Interfaces;
using FineTable.Application.DTO.Request;
using FineTable.Application.DTO.Response;
using FineTable.Application.Kafka.Interface;
using FineTable.Application.Kafka.Topic;
using FineTable.Application.Manager.Interface;
using FineTable.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FineTable.Infrastructure.Service.Common;

namespace FineTable.Application.Kafka.Producer
{
    public class FineCollectionProducer : IFineCollectionProducer

    {
        private readonly IKafkaProducer<string, FineCollectionRequest> _kafkaProducer;
        private readonly IKafkaProducer<string, ServiceResult<List<FineCollectionResponseProducer>>> _kafkaProducer1;
        private readonly IFineCollectionManager _manager;
        
        public FineCollectionProducer(IKafkaProducer<string, FineCollectionRequest> kafkaProducer, IFineCollectionManager manager)
        {
            _kafkaProducer = kafkaProducer;
            _manager = manager;
        }

        public async Task AddFineCollection(FineCollectionRequest fineCollectionRequest)
        {
            await _kafkaProducer.ProduceAsync(KafkaTopic.FineCollectionDetails, null, fineCollectionRequest);
        }

        public Task DeleteFineCollection(int id)
        {
            throw new NotImplementedException();
        }

        public async Task GetFineCollectionById(int id)
        {
            var result = await _manager.GetFineCollectionById(id);
            await _kafkaProducer.ProduceAsync(KafkaTopic.FineCollectionDetails, null, result);
        }

        public async Task GetFineCollections()
        {
            var result = await _manager.GetFineCollectionProducer();
            //var request = new List<FineCollectionRequest>()
            //{
            //    new FineCollectionRequest()
            //    {

            //    }
            //};
            await _kafkaProducer1.ProduceAsync(KafkaTopic.FineCollectionDetails, null, result );
        }

        public Task UpdateFineCollection(FineCollectionUpdateRequest fineCollectionRequest)
        {
            throw new NotImplementedException();
        }
    }
}
