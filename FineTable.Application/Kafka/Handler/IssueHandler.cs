using Common.Kafka.Interfaces;
using FineTable.Application.DTO.Request;
using FineTable.Application.Manager.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineTable.Application.Kafka.Handler
{
    public class IssueHandler : IKafkaHandler<string, FineCollection>
    {
        private readonly IFineCollectionManager _manager;


        public IssueHandler(IFineCollectionManager manager)
        {
            _manager = manager;
        }
        public Task HandleAsync(string key, FineCollection value)
        {
            // Here we can actually write the  

             _manager.AddFineCollection(value);

            //After successful operation, suppose if the registered user has User Id as 1 the we can produce message for other service's consumption

            return Task.CompletedTask;
        }
    }
}
