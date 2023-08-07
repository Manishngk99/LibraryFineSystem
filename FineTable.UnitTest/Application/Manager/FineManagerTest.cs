using AutoMapper;
using FineTable.Application.DTO.Request;
using FineTable.Application.DTO.Response;
using FineTable.Application.Manager.Implementation;
using FineTable.Domain.Entities;
using FineTable.Domain.Interface;
using FineTable.Infrastructure.Service;
using FineTable.UnitTest.Infrastructure.Data;
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
	public class FineManagerTest
	{
		private readonly FineManager _manager;
		private readonly Mock<IFineService> _fineServiceMock = new Mock<IFineService>();
		private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

		public FineManagerTest()
		{
			_manager = new FineManager(_fineServiceMock.Object, _mapperMock.Object);
		}


		[Fact]
		public async Task GetFineByFineID_ExistingId_ReturnsFineResponse()
		{
			//Arrange
			var fineId = 1;
			var expectedFine = new EFine { Id = fineId };
			var expectedFineResponse = new FineResponse { Id = fineId };
			_fineServiceMock
				.Setup(service => service.GetFineByFineId(fineId))
				.ReturnsAsync(expectedFine);
			_mapperMock
				.Setup(mapper => mapper.Map<FineResponse>(expectedFine))
				.Returns(expectedFineResponse);

			//Act
			var result = await _manager.GetFineById(fineId);

			//Assert
			Assert.NotNull(result);
			Assert.Equal(StatusType.Success, result.Status);	
			Assert.Equal(expectedFineResponse, result.Data);
		}

		[Fact]
		public async Task UpdateFine_OnSuccess_ReturnsTrue()
		{
			//Arrange
			var request = new FineRequest { Id = 1 };
			var expectedFine = new EFine { Id = 1 };

			_fineServiceMock
				.Setup(service=>service.GetFineByFineId(request.Id))
				.ReturnsAsync(expectedFine);
			_mapperMock
				.Setup(mapper => mapper.Map(request,expectedFine))
				.Returns(expectedFine) ;
			_fineServiceMock
				.Setup(service => service.UpdateFineStatus(expectedFine))
				.ReturnsAsync(true);

			//Act
			var result = await _manager.UpdateFineStatus(request);

			//Assert
			Assert.NotNull(result);
			Assert.Equal(StatusType.Success,result.Status);
			Assert.True(result.Data);
		}

		[Fact]
		public async Task GetFineList_ReturnsListOfFines()
		{
			//Arrange
			var expectedFines = new List<EFine>
			{
				new EFine { Id = 1, Amount = 10 },
				new EFine { Id = 2, Amount = 50 }
			};
			var expectedFineResponses = expectedFines.Select(fine => new FineResponse { Id = fine.Id, Amount = fine.Amount }).ToList();

			_fineServiceMock
				.Setup(service => service.GetFine())
				.ReturnsAsync(expectedFines);
			_mapperMock
				.Setup(mapper => mapper.Map<List<FineResponse>>(expectedFines))
				.Returns(expectedFineResponses);

			//Act
			var result = await _manager.GetFine();

			//Assert
			Assert.Equal(StatusType.Success,result.Status);
			Assert.Equal(expectedFineResponses,result.Data);

		}
	}
}
