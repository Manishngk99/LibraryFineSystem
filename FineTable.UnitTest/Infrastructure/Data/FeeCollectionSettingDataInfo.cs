using FineTable.Application.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineTable.UnitTest.Infrastructure.Data
{
	public static class FeeCollectionSettingDataInfo
	{
		public static void init()
		{
			fineCollectionRequest = new FineCollectionRequest()
			{
				FineStatus = Domain.Enum.FineStatus.Active,
				MemberType = Domain.Enum.MemberType.Student,
				CreatedDate = DateTime.Now,
				MemberID = 0
			};
			fineCollectionUpdateRequest = new FineCollectionUpdateRequest()
			{
				Id = 1,
				FineStatus = Domain.Enum.FineStatus.Active,
				MemberType = Domain.Enum.MemberType.Student,
				CreatedDate = DateTime.Now,
				MemberID = 0,
				ReturnDate = DateTime.Now,

			};	
		}

		public static FineCollectionRequest fineCollectionRequest { get; set; } = new FineCollectionRequest();
		public static FineCollectionUpdateRequest fineCollectionUpdateRequest { get; set; } = new FineCollectionUpdateRequest();

	}
}

