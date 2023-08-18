using AutoMapper;
using FineTable.Application.DTO.Request;
using FineTable.Application.DTO.Response;
using FineTable.Application.Manager.Interface;
using FineTable.Domain.Enum;
using FineTable.Domain.Interface;
using FineTable.Infrastructure.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FineTable.Infrastructure.Service.Common;

namespace FineTable.Application.Manager.Implementation
{
    public class FineManager : IFineManager
    {
        private readonly IFineService _service;
        private readonly IMapper _mapper;
        public FineManager(IFineService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }    

        public async Task<ServiceResult<List<FineResponse>>> GetFine()
        {
            var serviceResult = new ServiceResult<List<FineResponse>>();
            try
            {
                var fine = await _service.GetFine();
                var result = (from s in fine
                              select new FineResponse()
                              {
                                  Id = s.Id,
                                  Amount = s.Amount,
                                  MemberType=s.MemberType,
                              }).ToList();
                serviceResult.Status = StatusType.Success;
                serviceResult.Message = "Fine Status Found";
                serviceResult.Data = result;
                return serviceResult;
            }
            catch(Exception ex)
            {
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "An error occurred while retrieving the Fine Status";
                serviceResult.Data = null;

                return serviceResult;
            }
        }

        public async Task<ServiceResult<FineResponse>> GetFineById(int id)
        {
            var serviceResult = new ServiceResult<FineResponse>();

            try
            {
                var fine = await _service.GetFineByFineId(id);
                if (fine == null)
                {
                    serviceResult.Status = StatusType.Failure;
                    serviceResult.Message = "Fine not present";
                    serviceResult.Data = null;

                    return serviceResult;
                }

                var fineResponse = _mapper.Map<FineResponse>(fine);

                serviceResult.Status = StatusType.Success;
                serviceResult.Message = "Fine Found";
                serviceResult.Data = fineResponse;

                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "An error occurred while retrieving the fine";
                serviceResult.Data = null;

                return serviceResult;
            }
        }

        public async Task<ServiceResult<int>> GetRateByMemberType(MemberType memberType)
        {
			var fineList = await _service.GetFine();
			var rate = fineList.Where(x => x.MemberType == memberType).Select(x => x.Amount).FirstOrDefault();
            return new ServiceResult<int>() { 
                Data = rate,
                Message = $"Rate of {memberType}",
                Status = StatusType.Success,    
            };

			//Alternative if parameter is int

			//string studentValue = "Student";
			//string StaffValue = "Staff";

			//MemberType enumStudentValue = (MemberType)Enum.Parse(typeof(MemberType), studentValue);
			//int intStaffValue = (int)enumStudentValue;

			//MemberType enumStaffValue = (MemberType)Enum.Parse(typeof(MemberType), StaffValue);
			//int intStudentValue = (int)enumStaffValue;

			//if (fineCollectionRequest.Id == intStaffValue)
			//{
			//    var rateStaff = fineList.Where(x => x.MemberType == MemberType.Staff).Select(x => x.Amount).FirstOrDefault();
			//    //return rateStaff;
			//}

			//if (fineCollectionRequest.Id == intStudentValue)
			//{
			//    var rateStaff = fineList.Where(x => x.MemberType == MemberType.Student).Select(x => x.Amount).FirstOrDefault();
			//    //return rateStaff;
			//}

		}


		public async Task<ServiceResult<bool>> UpdateFineStatus(FineRequest finerequest)
        {
            var serviceResult = new ServiceResult<bool>();

            try
            {
                var fine = await _service.GetFineByFineId(finerequest.Id);
                if (fine == null)
                {
                    serviceResult.Status = StatusType.Failure;
                    serviceResult.Message = "Fine status not found";
                    serviceResult.Data = false;

                    return serviceResult;
                }
                fine.Amount = finerequest.Amount;
                fine.MemberType = finerequest.MemberType;
               

                var result = await _service.UpdateFineStatus(fine);

                serviceResult.Status = result ? StatusType.Success : StatusType.Failure;
                serviceResult.Message = result ? "Fine Updated Successfully" : "Failed to update fine";
                serviceResult.Data = result;

                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "An error occurred while updating the fine";
                serviceResult.Data = false;

                return serviceResult;
            }
        }
    }
}
