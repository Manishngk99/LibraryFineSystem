using FineTable.API.Controllers;
using FineTable.Application.DTO.Request;
using FineTable.Application.DTO.Response;
using FineTable.Application.Manager.Interface;
using FineTable.Infrastructure.Service;
using FineTable.UnitTest.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static FineTable.Infrastructure.Service.Common;

namespace FineTable.UnitTest.Api.Controller
{
	public class FineControllerTest : IClassFixture<DatabaseFixture>
	{
		private readonly FineController _fineController = null;
		private readonly Mock<IFineManager> _fineManager = new Mock<IFineManager>();
		public FineControllerTest()
		{
			_fineController = new FineController(_fineManager.Object);
		}
		[Fact]
		public async Task GetFines_ReturnsOkResultWithFineList()
		{
			//Arrange
			var expectedFines = new List<FineResponse>
			{
				new FineResponse { Id = 1, Amount = 10 },
				new FineResponse { Id = 2, Amount = 50 },
			};

			var expectedResult = new ServiceResult<List<FineResponse>>
			{
				Data = expectedFines,
				Message = "Fine retrieved successfully",
				Status = StatusType.Success
			};

			_fineManager.Setup(service=>service.GetFine()).ReturnsAsync(expectedResult);

			//Act
			var result = await _fineController.GetFine() as OkObjectResult;

			//Assert
			Assert.NotNull(result);
			Assert.Equal(200,result.StatusCode);
			var fineList = Assert.IsAssignableFrom<List<FineResponse>>(result.Value);
			Assert.Equal(expectedFines.Count, fineList.Count);
		}

		[Fact]
		public async Task UpdateFine_ReturnsUpdatedFineList()
		{
			//Arrange
			FeeSettingDataInfo.Init();
			var IdToUpdate = 1;
			var updatedFineRequest = new FineRequest
			{
				Id = IdToUpdate,
				Amount = 10,
			};

			var expectedResult = new ServiceResult<bool>
			{
				Data = true,
				Message = "Fine Updated Successfully",
				Status = StatusType.Success
			};
			_fineManager.Setup(service=>service.UpdateFineStatus(updatedFineRequest)).ReturnsAsync(expectedResult);

			//Act
			var result = await _fineController.UpdateFineStatus(IdToUpdate, updatedFineRequest) as OkObjectResult;

			//Assert
			Assert.NotNull(result);
			Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
			Assert.Equal(expectedResult.Message, result.Value);
		}
		[Fact]
		public async Task GetFineById_ReturnsFine()
		{
			//Arrange
			FeeSettingDataInfo.Init();
			int fineId = 1;
			var response = new FineResponse
			{
				Id = 1,
				Amount = 10,
			};

			var expectedResult = new ServiceResult<FineResponse>
			{
				Data = response
			};

            //Act
            _fineManager.Setup(x => x.GetFineById(It.IsAny<int>()))
             .ReturnsAsync(expectedResult);
			var ActualResult = await _fineController.GetFineById(fineId) as OkObjectResult;

			//Assert
			Assert.Equal(expectedResult.Data, ActualResult.Value);

        }
	}
}
