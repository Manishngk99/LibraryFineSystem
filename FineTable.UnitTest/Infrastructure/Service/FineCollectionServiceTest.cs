using FineTable.Infrastructure.Repository;
using FineTable.Infrastructure.Service;
using FineTable.UnitTest.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FineTable.UnitTest.Infrastructure.Service
{
	public class FineCollectionServiceTest : IClassFixture<DatabaseFixture>
	{
		[Fact]
		public async Task GetFineCollection_OnSuccess_ReturnResponse()
		{
			DatabaseFixture _fixture = new DatabaseFixture();
			using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
			{
				var service = new FineCollectionService(factory);
				//Arrange
				FeeCollectionSettingDataInfo.init();
				var eResponseList = FeeCollectionSettingDataInfo.eFineCollectionList;
				var Expected_Result = eResponseList;
				//Act
				var Actual_Result = await service.GetFineCollections();
				//Assert
				Assert.Equivalent(Expected_Result, Actual_Result);
			}
		}

		[Fact]
		public async Task GetFineCollectionByID_OnSuccess_ReturnResponse()
		{
			DatabaseFixture _fixture = new DatabaseFixture();
			using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
			{
				var service = new FineCollectionService(factory);
				//Arrange
				int id = 1;
				FeeCollectionSettingDataInfo.init();
				var eResponse = FeeCollectionSettingDataInfo.eFineCollection;
				var Expected_Result = eResponse;
				//Act
				var Actual_Result = await service.GetFineCollectionById(id);
				//Assert
				Assert.Equivalent(Expected_Result, Actual_Result);
			}
		}

		[Fact]
		public async Task AddFineCollection_OnSuccess_ReturnTrue()
		{
			DatabaseFixture _fixture = new DatabaseFixture();
			using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
			{
				var service = new FineCollectionService(factory);
				//Arrange
				FeeCollectionSettingDataInfo.init();
				var eRequest = FeeCollectionSettingDataInfo.eFineCollectionRequest;
				var Expected_Result = true;
				//Act
				var Actual_Result = await service.AddFineCollection(eRequest);
				//Assert
				Assert.Equivalent(Expected_Result, Actual_Result);
			}
		}

		[Fact]
		public async Task DeleteFineCollection_OnSuccess_ReturnTrue()
		{
			DatabaseFixture _fixture = new DatabaseFixture();
			using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
			{
				var service = new FineCollectionService(factory);
				//Arrange
				int id = 1;
				var Expected_Result = true;
				//Act
				var Actual_Result = await service.DeleteFineCollection(id);
				//Assert
				Assert.Equivalent(Expected_Result, Actual_Result);
			}
		}

		[Fact]
		public async Task DeleteFineCollection_OnFailure_FineIdNotFound_ReturnTrue()
		{
			DatabaseFixture _fixture = new DatabaseFixture();
			using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
			{
				var service = new FineCollectionService(factory);
				//Arrange
				int id = 0;
				var Expected_Result = false;
				//Act
				var Actual_Result = await service.DeleteFineCollection(id);
				//Assert
				Assert.Equivalent(Expected_Result, Actual_Result);
			}
		}

		[Fact]
		public async Task UpdateFineCollection_OnSuccess_ReturnTrue()
		{
			DatabaseFixture _fixture = new DatabaseFixture();
			using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
			{
				var service = new FineCollectionService(factory);
				//Arrange
				FeeCollectionSettingDataInfo.init();
				var eRequest = FeeCollectionSettingDataInfo.eFineCollection;
				var Expected_Result = true;
				//Act
				var Actual_Result = await service.UpdateFineCollection(eRequest);
				//Assert
				Assert.Equivalent(Expected_Result, Actual_Result);
			}
		}

		[Fact]
		public async Task UpdateFineCollection_OnFailure_IdNotFound_ReturnFalse()
		{
			DatabaseFixture _fixture = new DatabaseFixture();
			using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
			{
				var service = new FineCollectionService(factory);
				//Arrange
				FeeCollectionSettingDataInfo.init();
				var eRequest = FeeCollectionSettingDataInfo.eFineCollection;
				eRequest.Id = 0;
				var Expected_Result = false;
				//Act
				var Actual_Result = await service.UpdateFineCollection(eRequest);
				//Assert
				Assert.Equivalent(Expected_Result, Actual_Result);
			}
		}

	}
}
