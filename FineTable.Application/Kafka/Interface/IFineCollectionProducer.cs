using FineTable.Application.DTO.Request;
using FineTable.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FineTable.Infrastructure.Service.Common;

namespace FineTable.Application.Kafka.Interface
{
    public interface IFineCollectionProducer
    {
        Task AddFineCollection(FineCollectionRequest fineCollectionRequest);
        Task UpdateFineCollection(FineCollectionUpdateRequest fineCollectionRequest);
        Task DeleteFineCollection(int id);

        Task GetFineCollections();
        Task GetFineCollectionById(int id);
    }
}
