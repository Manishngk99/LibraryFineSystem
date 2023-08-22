using AutoMapper;
using FineTable.API.Controllers;
using FineTable.Application.DTO.Request;
using FineTable.Application.DTO.Response;
using FineTable.Application.Manager.Implementation;
using FineTable.Application.Manager.Interface;
using FineTable.Domain.Entities;
using FineTable.Domain.Interface;
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

namespace FineTable.UnitTest.Application.Manager
{
	public class FineCollectionManagerTest
	{
		private readonly FineCollectionManager _manager;
		private readonly Mock<IFineCollectionService> _mockFineCollectionService = new Mock<IFineCollectionService>();
		private readonly Mock<IFineService> _mockFineService= new Mock<IFineService>();
		private readonly Mock<IMapper> _mapper= new Mock<IMapper>();


		public FineCollectionManagerTest()
		{
			_manager = new FineCollectionManager(_mockFineCollectionService.Object, _mockFineService.Object,
				_mapper.Object);
		}

		[Fact]
		public async Task AddFineCollection_OnSuccess_ReturnTrue()
		{
			//Arrange
			FeeCollectionSettingDataInfo.init();
			var request = FeeCollectionSettingDataInfo.fineCollection;
			var response = FeeCollectionSettingDataInfo.eFineCollection;
			var Expected_Result = new ServiceResult<bool>()
			{
				Data = true,
				Message = "FineCollection Added!",
				Status = StatusType.Success
			};

			//Act
			_mockFineCollectionService.Setup(x => x.AddFineCollection(response)).ReturnsAsync(true);
			var Actual_Result = await _manager.AddFineCollection(request);

			//Assert
			Assert.Equivalent(Expected_Result, Actual_Result);
		}

		[Fact]
		public async Task UpdateFineCollection_OnSuccess_ReturnTrue()
		{
			//Arrange
			FeeCollectionSettingDataInfo.init();
			var request = FeeCollectionSettingDataInfo.fineCollectionUpdateRequest;
			var response = FeeCollectionSettingDataInfo.eFineCollection;
			var Expected_Result = new ServiceResult<bool>()
			{
				Data = true,
				Message = "FineCollection Updated!",
				Status = StatusType.Success
			};

			//Act
			_mapper.Setup(x => x.Map<EFineCollection>(It.IsAny<FineCollectionUpdateRequest>)).Returns(response);
			_mockFineCollectionService.Setup(x => x.UpdateFineCollection(response)).ReturnsAsync(true);
			var Actual_Result = await _manager.UpdateFineCollection(request);

			//Assert
			Assert.Equivalent(Expected_Result, Actual_Result);
		}

		[Fact]
		public async Task DeleteFineCollection_OnSuccess_ReturnTrue()
		{
			//Arrange
			int id = 1;
			FeeCollectionSettingDataInfo.init();
			var eResponse = FeeCollectionSettingDataInfo.eFineCollection;
			var Expected_Result = new ServiceResult<bool>()
			{
				Data = true,
				Message = "FineCollection Deleted!",
				Status = StatusType.Success
			};

			//Act
			_mockFineCollectionService.Setup(x => x.GetFineCollectionById(id)).ReturnsAsync(eResponse);
			_mockFineCollectionService.Setup(x => x.DeleteFineCollection(id)).ReturnsAsync(true);
			var Actual_Result = await _manager.DeleteFineCollection(id);

			//Assert
			Assert.Equivalent(Expected_Result, Actual_Result);
		}

		//Revisit
		[Fact]
		public async Task GetFineCollection_OnSuccess_ReturnResponseInList()
		{
			//Arrange
			FeeCollectionSettingDataInfo.init();
			var eResponse = FeeCollectionSettingDataInfo.eFineCollectionList;
			var response = FeeCollectionSettingDataInfo.fineCollectionResponseList;
			var Expected_Result = new ServiceResult<List<FineCollectionResponse>>()
			{
				Data = response,
				Message = "FineCollection Retrieved!",
				Status = StatusType.Success,
			};

			//Act
			_mockFineCollectionService.Setup(x => x.GetFineCollections()).ReturnsAsync(eResponse);
			var Actual_Result = await _manager.GetFineCollections();

			//Assert
			Assert.Equivalent(Expected_Result.Data, Actual_Result.Data);
		}

		[Fact]
		public async Task GetFineCollectionByID_OnSuccess_ReturnResponse()
		{
			//Arrange
			int id = 1;
			FeeCollectionSettingDataInfo.init();
			var eResponse = FeeCollectionSettingDataInfo.eFineCollection;
			var response = FeeCollectionSettingDataInfo.fineCollectionResponse;

			var Expected_Result = new ServiceResult<FineCollectionResponse>()
			{
				Data = response,
				Message = "Fine retrieved!",
				Status = StatusType.Success,
			};

			//Act
			_mockFineCollectionService.Setup(x => x.GetFineCollectionById(id)).ReturnsAsync(eResponse);
			var Actual_Result = await _manager.GetFineCollectionById(id);

			//Assert
			Assert.Equivalent(Expected_Result, Actual_Result);
		}


		//Revisit ThrowAsync did not work

		[Fact]
		public async Task AddFineCollection_OnFailure_ThrowException()
		{
			//Arrange
			FeeCollectionSettingDataInfo.init();
			var request = FeeCollectionSettingDataInfo.fineCollection;
			var eFineCollection = FeeCollectionSettingDataInfo.eFineCollection;
			var Expected_Result = new ServiceResult<bool>()
			{
				Data = false,
				Message = "Something went wrong",
				Status = StatusType.Failure
			};

			//Act
			_mockFineCollectionService.Setup(x => x.AddFineCollection(eFineCollection)).ThrowsAsync(new Exception());
			var Actual_Result = await _manager.AddFineCollection(request);

			//Assert
			Assert.Equivalent(Expected_Result, Actual_Result);
		}

		[Fact]
		public async Task UpdateFineCollection_OnFailure_ReturnException()
		{
			//Arrange
			FeeCollectionSettingDataInfo.init();
			var request = FeeCollectionSettingDataInfo.fineCollectionUpdateRequest;
			var response = FeeCollectionSettingDataInfo.eFineCollection;
			var Expected_Result = new ServiceResult<bool>()
			{
				Data = false,
				Message = "Something went wrong",
				Status = StatusType.Failure
			};

			//Act
			_mapper.Setup(x => x.Map<EFineCollection>(It.IsAny<FineCollectionUpdateRequest>)).Returns(response);
			_mockFineCollectionService.Setup(x => x.UpdateFineCollection(response)).ThrowsAsync(new Exception());
			var Actual_Result = await _manager.UpdateFineCollection(request);

			//Assert
			Assert.Equivalent(Expected_Result, Actual_Result);
		}

	}
}
