using FineTable.Application.DTO.Request;
using FineTable.Application.DTO.Response;
using FineTable.Domain.Entities;
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
			fineCollectionResponseList = new List<FineCollectionResponse>()
			{
				new FineCollectionResponse()
				{
				Id = 1,
				MemberType = Domain.Enum.MemberType.Student,
				CreatedDate = DateTime.Today,
				MemberID = 0,
				ReturnDate = DateTime.Today,
				Amount = 100,
				Days = 2
				}
			};
			fineCollectionResponse = new FineCollectionResponse()
			{
				Id = 1,
				MemberType = Domain.Enum.MemberType.Student,
				CreatedDate = DateTime.Today,
				MemberID = 0,
				ReturnDate = DateTime.Today,
				Amount = 100,
				Days = 2
			};
			eFineCollectionList = new List<EFineCollection>()
			{
				new EFineCollection()
				{
					Id = 1,
					MemberType = Domain.Enum.MemberType.Student,
					CreatedDate = DateTime.Today,
					MemberID = 0,
					ReturnDate = DateTime.Today,
					Amount = 100,
					Days = 2
				}
			};
			eFineCollection = new EFineCollection()
			{
				Id = 1,
				MemberType = Domain.Enum.MemberType.Student,
				CreatedDate = DateTime.Today,
				MemberID = 0,
				ReturnDate = DateTime.Today,
				Amount = 100,
				Days = 2
			};

		}

		public static FineCollectionRequest fineCollectionRequest { get; set; } = new FineCollectionRequest();
		public static FineCollectionUpdateRequest fineCollectionUpdateRequest { get; set; } = new FineCollectionUpdateRequest();
		public static List<FineCollectionResponse> fineCollectionResponseList { get; set; } = new List<FineCollectionResponse>();
		public static FineCollectionResponse fineCollectionResponse { get; set; } = new FineCollectionResponse();
		public static List<EFineCollection> eFineCollectionList { get; set; } = new List<EFineCollection>();
		public static EFineCollection eFineCollection { get; set; } = new EFineCollection();
	}
}

