using FineTable.API.Controllers;
using FineTable.Application.Manager.Interface;
using FineTable.Infrastructure.Service;
using FineTable.UnitTest.Infrastructure.Data;
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
	public class FineCollectionControllerTest
	{
		private readonly FineCollectionController _controller; 
		private readonly Mock<IFineCollectionManager> _mockManager = new Mock<IFineCollectionManager>();	

		public FineCollectionControllerTest() 
		{
			_controller = new FineCollectionController(_mockManager.Object);
		}

		[Fact]
		public async Task AddFineCollection_OnSuccess_ReturnTrue()
		{
			//Arrange
			FeeCollectionSettingDataInfo.init();
			var request = FeeCollectionSettingDataInfo.fineCollectionRequest;
			var Expected_Result = new ServiceResult<bool>()
			{
				Data = true,
				Message = "FineCollection Added!",
				Status = StatusType.Success
			};

			//Act
			_mockManager.Setup(x => x.AddFineCollection(request)).ReturnsAsync(Expected_Result);
			var Actual_Result = await _controller.AddFineCollection(request) as OkObjectResult;

			//Assert
			Assert.NotNull(Actual_Result);
			Assert.Equal(200, Actual_Result.StatusCode);
			Assert.Equal(Expected_Result.Data, Actual_Result.Value);
		}

		//[Fact]
		//public async Task AddFineCollection_OnFailure_ReturnErrorMessage()
		//{
		//	//Arrange
		//	FeeCollectionSettingDataInfo.init();
		//	var request = FeeCollectionSettingDataInfo.fineCollectionRequest;
		//	var Expected_Result = new ServiceResult<bool>()
		//	{
		//		Data = true,
		//		Message = "FineCollection Added!",
		//		Status = StatusType.Success
		//	};

		//	//Act
		//	_mockManager.Setup(x => x.AddFineCollection(request)).ReturnsAsync(Expected_Result);
		//	var Actual_Result = await _controller.AddFineCollection(request) as OkObjectResult;

		//	//Assert
		//	Assert.NotNull(Actual_Result);
		//	Assert.Equal(200, Actual_Result.StatusCode);
		//	Assert.Equal(Expected_Result.Data, Actual_Result.Value);
		//}

		[Fact]
		public async Task UpdateFineCollection_OnSuccess_ReturnTrue()
		{
			//Arrange
			FeeCollectionSettingDataInfo.init();
			var request = FeeCollectionSettingDataInfo.fineCollectionUpdateRequest;
			var Expected_Result = new ServiceResult<bool>()
			{
				Data = true,
				Message = "FineCollection Updated!",
				Status = StatusType.Success
			};

			//Act
			_mockManager.Setup(x => x.UpdateFineCollection(request)).ReturnsAsync(Expected_Result);
			var Actual_Result = await _controller.UpdateFineCollection(request) as OkObjectResult;

			//Assert
			Assert.NotNull(Actual_Result);
			Assert.Equal(200, Actual_Result.StatusCode);
			Assert.Equal(Expected_Result.Data, Actual_Result.Value);
		}

		[Fact]
		public async Task DeleteFineCollection_OnSuccess_ReturnTrue()
		{
			//Arrange
			int id = 1;
			FeeCollectionSettingDataInfo.init();
			var request = FeeCollectionSettingDataInfo.fineCollectionUpdateRequest;
			var Expected_Result = new ServiceResult<bool>()
			{
				Data = true,
				Message = "FineCollection Deleted!",
				Status = StatusType.Success
			};

			//Act
			_mockManager.Setup(x => x.DeleteFineCollection(id)).ReturnsAsync(Expected_Result);
			var Actual_Result = await _controller.DeleteFineCollection(id) as OkObjectResult;

			//Assert
			Assert.NotNull(Actual_Result);
			Assert.Equal(200, Actual_Result.StatusCode);
			Assert.Equal(Expected_Result.Data, Actual_Result.Value);
		}
	}
}
