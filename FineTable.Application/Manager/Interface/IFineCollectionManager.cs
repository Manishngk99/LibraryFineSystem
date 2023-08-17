using FineTable.Application.DTO.Request;
using FineTable.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FineTable.Infrastructure.Service.Common;

namespace FineTable.Application.Manager.Interface
{
    public interface IFineCollectionManager
    {

        Task<ServiceResult<bool>>AddFineCollection(FineCollectionRequest fineCollectionRequest);
        Task<ServiceResult<bool>> UpdateFineCollection(FineCollectionUpdateRequest fineCollectionRequest);
        Task<ServiceResult<bool>> DeleteFineCollection(int id);

        Task<ServiceResult<List<FineCollectionResponse>>> GetFineCollections();
        Task<ServiceResult<FineCollectionResponse>> GetFineCollectionById(int id);

        Task<ServiceResult<List<FineCollectionResponseProducer>>> GetFineCollectionProducer();


    }
}
