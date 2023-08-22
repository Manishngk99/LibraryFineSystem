using AutoMapper;
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
		private readonly IMapper _mapper;

		public FineCollectionManager(IFineCollectionService fineCollectionService, IFineService serviceFine,
			IMapper mapper)
        {
            _service = fineCollectionService;
            _serviceFine = serviceFine;
            _mapper = mapper;
        }
        public async Task<ServiceResult<bool>> AddFineCollection(FineCollection request)
        {
            try
            {
				var parse = new EFineCollection()
                {
                    ReturnDate = request.ReturnDate,
                    Amount =request.FineAmount,
                    CreatedDate= request.IssuedDate,
                    MemberID = request.MemberId,
                    Days = request.Days
                };

                var result = await _service.AddFineCollection(parse);
             
                return new ServiceResult<bool>()
                {
                    Data = true,
                    Message = "FineCollection Added!",
                    Status = StatusType.Success
                };
            }
            catch (Exception ex) {
				return new ServiceResult<bool>()
				{
					Data = false,
					Message = "Something went wrong",
					Status = StatusType.Failure
				};
			}
        }

        public async Task<ServiceResult<bool>> UpdateFineCollection(FineCollectionUpdateRequest fineCollectionRequest)
        {
            try
            {
                var mapper = _mapper.Map<EFineCollection>(fineCollectionRequest);
                var result = await _service.UpdateFineCollection(mapper);
                return new ServiceResult<bool>()
                {
                    Data = true,
                    Message = "FineCollection Updated!",
                    Status = StatusType.Success
                };
            }
            catch (Exception ex) {
				return new ServiceResult<bool>()
				{
					Data = false,
					Message = "Something went wrong",
					Status = StatusType.Failure
				};
			}

        }
        public async Task<ServiceResult<bool>> DeleteFineCollection(int id)
        {
            try
            {
                var fineCollection = await _service.GetFineCollectionById(id);
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
                              Amount = s.Amount,
                              Days = s.Days,
                              CreatedDate = s.CreatedDate,
                              ReturnDate = s.ReturnDate
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
                Amount = fine.Amount,
                Days = fine.Days,
                CreatedDate = fine.CreatedDate,
				ReturnDate = fine.CreatedDate
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


