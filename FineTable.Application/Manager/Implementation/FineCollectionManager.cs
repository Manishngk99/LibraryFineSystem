using FineTable.Application.DTO.Request;
using FineTable.Application.DTO.Response;
using FineTable.Application.Kafka.Interface;
using FineTable.Application.Manager.Interface;
using FineTable.Domain.Entities;
using FineTable.Domain.Enum;
using FineTable.Domain.Interface;
using FineTable.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FineTable.Infrastructure.Service.Common;

namespace FineTable.Application.Manager.Implementation
{
    public class FineCollectionManager : IFineCollectionManager
    {
        private readonly IFineCollectionService _service;
        private readonly IFineService _serviceFine;
        public FineCollectionManager(IFineCollectionService fineCollectionService, IFineService serviceFine )
        {
            _service = fineCollectionService;
            _serviceFine = serviceFine;
              
        }
        public async Task<ServiceResult<bool>> AddFineCollection(FineCollectionRequest fineCollectionRequest)
        {
            try
            {
                var parse = new EFineCollection()
                {
                    CreatedDate = fineCollectionRequest.CreatedDate,
                    MemberID = fineCollectionRequest.MemberID,
                    MemberType = fineCollectionRequest.MemberType,
                };

                var result = await _service.AddFineCollection(parse);
                return new ServiceResult<bool>()
                {
                    Data = true,
                    Message = "FineCollection Added!",
                    Status = StatusType.Success
                };
            }
            catch (Exception ex) { throw; }
        }

        public async Task<ServiceResult<bool>> UpdateFineCollection(FineCollectionDetailRequest fineCollectionRequest)
        {
            try
            {

                var fineList = await _serviceFine.GetFine();
                var rate = fineList.Where(x => x.MemberType == fineCollectionRequest.MemberType).Select(x => x.Amount).FirstOrDefault();

                var parse = new EFineCollection()
                {
                    Amount = fineCollectionRequest.Amount,  
                    FineStatus = fineCollectionRequest.FineStatus,
                    CreatedDate = fineCollectionRequest.CreatedDate,
                    MemberID = fineCollectionRequest.MemberID,
                    ReturnDate = fineCollectionRequest.ReturnDate,
                    MemberType = fineCollectionRequest.MemberType,
                    Days = 2//fineCollectionRequest.CreatedDate.Day - fineCollectionRequest.ReturnDate.Day,
                            //  Amount = Days * rate,
                };
                parse.Amount = parse.Days * rate;


                var result = await _service.UpdateFineCollection(parse);
                return new ServiceResult<bool>()
                {
                    Data = true,
                    Message = "FineCollection Updated!",
                    Status = StatusType.Success
                };
            }
            catch (Exception ex) { throw; }

        }
        public async Task<ServiceResult<bool>> DeleteFineCollection(int id)
        {
            try
            {
                var fineCollection = await _serviceFine.GetFineByFineId(id);
                var result = await _service.DeleteFineCollection(fineCollection.Id);
                return new ServiceResult<bool>()
                {
                    Data = true,
                    Message = "FineCollection Deleted!",
                    Status = StatusType.Success
                };
            }
            catch (Exception ex) { throw; }
        }

        public async Task<ServiceResult<List<FineCollectionResponse>>> GetFineCollections()
        {
            var fineCollection = await _service.GetFineCollections();
            
            var result = (from s in fineCollection
                          where s.FineStatus == Domain.Enum.FineStatus.Active
                          select new FineCollectionResponse()
                          {
                              Id = s.Id,
                              MemberID = s.MemberID,
                              MemberType = s.MemberType,
                              CreatedDate = s.CreatedDate,
                          }).ToList();

            return new ServiceResult<List<FineCollectionResponse>>()
            {

                Data = result,
                Message = "FineCollection Retrieved!",
                Status = StatusType.Success,
            };
        }
       

        public async Task<ServiceResult<FineCollectionResponse>> GetFineCollectionById(int id)
        {
            var fine = await _service.GetFineCollectionById(id);
            if (fine == null)
            {
                return new ServiceResult<FineCollectionResponse>()
                {
                    Data = null,
                    Message = "Fine not found",
                    Status = StatusType.Failure
                };
            }

            var result = new FineCollectionResponse()
            {
                Id = fine.Id,
                MemberID = fine.MemberID,
                MemberType = fine.MemberType,
                CreatedDate = fine.CreatedDate,

            };
            return new ServiceResult<FineCollectionResponse>()
            {
                Data = result,
                Message = "Fine retrieved!",
                Status = StatusType.Success,
            };
        }


    }


}


