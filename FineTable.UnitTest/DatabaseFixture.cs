using FineTable.Infrastructure.Repository;
using FineTable.UnitTest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Infrastructure.Mapper;

namespace FineTable.UnitTest
{
	public class DatabaseFixture : IDisposable
	{
		public DatabaseContext mockDbContext;

		public DatabaseFixture()
		{
			MapperHelper._isUnitTest = true;

			// ConfigurationStoreOptions storeOptions = new ConfigurationStoreOptions();
			// ^ We don't need this line since it's not used.

			var serviceCollection = new ServiceCollection();
			// Add any other services or dependencies you need for your tests to this service collection.

			var builder = new DbContextOptionsBuilder<DatabaseContext>();
			builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
			builder.UseApplicationServiceProvider(serviceCollection.BuildServiceProvider());

			var databaseContext = new DatabaseContext(builder.Options);
			databaseContext.Database.EnsureCreated();

			#region Feed Fine Data
			FeeSettingDataInfo.Init();
			var feeSettings = FeeSettingDataInfo.FeeSetting;
			databaseContext.Database.EnsureCreated();
            #endregion
			FeeCollectionSettingDataInfo.init();
			var feeCollectionSetting = FeeCollectionSettingDataInfo.eFineCollectionList;
			databaseContext.FineCollection.AddRange(feeCollectionSetting);


            databaseContext.SaveChanges();

			mockDbContext = databaseContext;
		}

		public void Dispose()
		{
			MapperHelper._isUnitTest = false;
			mockDbContext.Database.EnsureDeleted();
			// clean up test data from the database
		}

	}
}
