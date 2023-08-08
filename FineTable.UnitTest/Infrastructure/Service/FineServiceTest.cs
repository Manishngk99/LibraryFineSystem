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
	public class FineServiceTest
	{
		[Fact]
		public async Task UpdateFineStatus_OnSuccess_ReturnsTrue()
		{
			DatabaseFixture _fixture = new DatabaseFixture();
			using(var factory = new ServiceFactory(_fixture.mockDbContext, true))
			{
				var service = new FineService(factory);

				//arrange
				var requestresult = FeeSettingDataInfo.SuccessFineSetting;
				bool EXPECTED_RESULT = true;

				//act
				var result = await service.UpdateFineStatus(requestresult);

				//assert
				Assert.Equal(EXPECTED_RESULT, result);
			}
		}
		[Fact]
		public async Task GetFineByID_OnSuccess_ReturnsData()
		{
			DatabaseFixture _fixture = new DatabaseFixture();
			using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
			{
				var service = new FineService(factory);
				var fineId = 1;


				//act
				var result = await service.GetFineByFineId(fineId);

				//assert
				Assert.NotNull(result);
				Assert.Equal(fineId, result.Id);
			}
		}
		[Fact]
		public async Task GetFine_ReturnsFineInfo()
		{
			//Arrange
			DatabaseFixture _fixture = new DatabaseFixture();
			using(var factory = new ServiceFactory(_fixture.mockDbContext, true))
			{
				var service = new FineService(factory);


				//Act
				var result = await service.GetFine();

				//Assert
				Assert.NotNull(result);
			}
		}
		[Fact]
		public async Task GetFineById_ReturnsError_WhenIdNotFound()
		{
			DatabaseFixture _fixture = new DatabaseFixture();
			using (var factory = new ServiceFactory(_fixture.mockDbContext,true))
			{
				//arrange
				var service = new FineService(factory);
				var fineId = 0;

				//act
				var result = await service.GetFineByFineId(fineId);

				//assert
				Assert.Null(result);
			}
		}
		[Fact]
		public async Task UpdateFine_ReturnsFalse_WhenIncorrectIdProvided()
		{
			DatabaseFixture _fixture = new DatabaseFixture();
			using (var factory= new ServiceFactory(_fixture.mockDbContext, true))
			{
				//arrange
				var service = new FineService(factory);
				
                var requestresult = FeeSettingDataInfo.FailureFineSetting;

                //act
                var result = await service.UpdateFineStatus(requestresult);
				Assert.False(result);
			}
		}
	}
}
