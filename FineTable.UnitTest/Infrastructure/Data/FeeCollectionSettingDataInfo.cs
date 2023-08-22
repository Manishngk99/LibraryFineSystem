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
			fineCollection = new FineCollection()
			{
				MemberId = 1,
				BookId = 1,
				IssuedDate = DateTime.Today,
				ReturnDate = DateTime.Today,
				FineAmount = 100,
				FineRate = 50,
				Days = 2,
				IsDeleted = false,
			};
			fineCollectionUpdateRequest = new FineCollectionUpdateRequest()
			{
				Id = 1,
				FineStatus = Domain.Enum.FineStatus.Active,
				MemberType = Domain.Enum.MemberType.Student,
				CreatedDate = DateTime.Today,
				MemberID = 1,
				Amount = 100,
				ReturnDate = DateTime.Today,
				Days = 2

			};
			fineCollectionResponseList = new List<FineCollectionResponse>()
			{
				new FineCollectionResponse()
				{
				Id = 1,
				MemberType = Domain.Enum.MemberType.Student,
				CreatedDate = DateTime.Today,
				MemberID = 1,
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
				MemberID = 1,
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
					MemberID = 1,
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
				MemberID = 1,
				ReturnDate = DateTime.Today,
				Amount = 100,
				Days = 2
			};
			eFineCollectionRequest = new EFineCollection()
			{
				MemberType = Domain.Enum.MemberType.Student,
				CreatedDate = DateTime.Today,
				MemberID = 1,
				ReturnDate = DateTime.Today,
				Amount = 100,
				Days = 2
			};
		}

		public static FineCollection fineCollection { get; set; } = new FineCollection();
		public static FineCollectionUpdateRequest fineCollectionUpdateRequest { get; set; } = new FineCollectionUpdateRequest();
		public static List<FineCollectionResponse> fineCollectionResponseList { get; set; } = new List<FineCollectionResponse>();
		public static FineCollectionResponse fineCollectionResponse { get; set; } = new FineCollectionResponse();
		public static List<EFineCollection> eFineCollectionList { get; set; } = new List<EFineCollection>();
		public static EFineCollection eFineCollection { get; set; } = new EFineCollection();
		public static EFineCollection eFineCollectionRequest { get; set; } = new EFineCollection();
	}
}

