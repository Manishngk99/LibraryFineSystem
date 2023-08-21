using FineTable.Application.DTO.Request;
using FineTable.Application.DTO.Response;
using FineTable.Application.Manager.Interface;
using FineTable.Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static FineTable.Infrastructure.Service.Common;

namespace FineTable.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FineCollectionController : ControllerBase
    {
        private readonly IFineCollectionManager _manager;

        public FineCollectionController(IFineCollectionManager manager)
        { 
            _manager = manager; 
        }

        [HttpGet("GetAllFineCollections")]
        public async Task<ServiceResult<List<FineCollectionResponse>>> GetFineCollections()
        {
            var result = await _manager.GetFineCollections();
            return new ServiceResult<List<FineCollectionResponse>>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status,
            };

        }

        [HttpGet("GetFineCollectionById")]
        public async Task<ServiceResult<FineCollectionResponse>> GetFineCollectionById(int id)
        {
            var result=await _manager.GetFineCollectionById(id);
            return new ServiceResult<FineCollectionResponse>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status,
            };
        }

        [HttpPost]
        public async Task<ServiceResult<bool>> AddFineCollection(FineCollection request)
        { 
            var result = await _manager.AddFineCollection(request);
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpPut]
        public async Task<ServiceResult<bool>> UpdateFineCollection(FineCollectionDetailRequest fineCollectionRequest)
        {
            var result = await _manager.UpdateFineCollection(fineCollectionRequest);
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpDelete]
        public async Task<ServiceResult<bool>> DeleteFineCollection(int id)
        {
            var result = await _manager.DeleteFineCollection(id);
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }


    }
}
