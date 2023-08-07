using FineTable.API.Controllers;
using FineTable.Application.Manager.Implementation;
using FineTable.Application.Manager.Interface;
using FineTable.Domain.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineTable.UnitTest.Application.Manager
{
	public class FineCollectionManagerTest
	{
		private readonly FineCollectionManager _manager;
		private readonly Mock<IFineCollectionService> _mockFineCollectionService = new Mock<IFineCollectionService>();
		private readonly Mock<IFineService> _mockFineService= new Mock<IFineService>();

		public FineCollectionManagerTest()
		{
			_manager = new FineCollectionManager(_mockFineCollectionService.Object, _mockFineService.Object);
		}


	}
}
