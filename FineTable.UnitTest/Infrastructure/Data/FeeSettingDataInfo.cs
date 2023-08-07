using FineTable.Application.DTO.Response;
using FineTable.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineTable.UnitTest.Infrastructure.Data
{
	public class FeeSettingDataInfo
	{
		public static void Init()
		{
			FeeSetting = new List<EFine>()
			{
				new EFine()
				{
					Id=1,
					MemberType=0,
					Amount=0,
				},
			};
			SuccessFineSetting = new EFine()
			{
				Id = 1,
				MemberType = 0,
				Amount = 0,
			};
			FineResponse = new List<FineResponse>()
			{
				new FineResponse()
				{
					Amount = 0,
					MemberType=0	

				}
			};
		}
		public static List<EFine> FeeSetting { get; set; }
        public static EFine SuccessFineSetting { get; private set; } = new EFine();
        public static List<FineResponse> FineResponse { get; private set; } = new List<FineResponse>();
    }
}
