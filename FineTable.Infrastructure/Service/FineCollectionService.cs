﻿using FineTable.Domain.Entities;
using FineTable.Domain.Enum;
using FineTable.Domain.Interface;
using FineTable.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace FineTable.Infrastructure.Service
{
    public class FineCollectionService : IFineCollectionService
    {
        private readonly IServiceFactory _serviceFactory;

        public FineCollectionService(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task<bool> AddFineCollection(EFineCollection eFineCollection)
        {
            try
            {
                var service = _serviceFactory.GetInstance<EFineCollection>();
                await service.AddAsync(eFineCollection);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteFineCollection(int id)
        {
            try
            {
                var service = _serviceFactory.GetInstance<EFineCollection>();
                var findFine = await service.FindAsync(id);
                if (findFine != null)
                {
                    findFine.FineStatus = FineStatus.InActive;
                    findFine.CreatedDate = DateTime.Now.ToUniversalTime();
                    findFine.ReturnDate = DateTime.Now.ToUniversalTime();
                    await service.UpdateAsync(findFine);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<EFineCollection> GetFineCollectionById(int id)
        {
            try
            {
                var service = _serviceFactory.GetInstance<EFineCollection>();
                var fineFine = service.ListAsync().Result.FirstOrDefault(x=>x.Id==id);
                if (fineFine == null)
                {
                    return null;
                }
                return fineFine;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<EFineCollection>> GetFineCollections()
        {
            try
            {
                var service = _serviceFactory.GetInstance<EFineCollection>();
                var result = await service.ListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateFineCollection(EFineCollection eFineCollection)
        {
            try
            {
                var service = _serviceFactory.GetInstance<EFineCollection>();
                var result = await service.FindAsync(eFineCollection.Id);
                if (result == null)
                {
                    return false;
                }
                //result.CreatedDate = DateTime.Now.ToUniversalTime();

                var parse = new EFineCollection()
                {
                    Id = eFineCollection.Id,
                    ReturnDate = DateTime.Now.ToUniversalTime(),
                    CreatedDate = DateTime.Now.ToUniversalTime(),
                    Amount = eFineCollection.Amount,
                    Days = eFineCollection.Days,
                    MemberID = eFineCollection.MemberID,
                    FineStatus = eFineCollection.FineStatus,
                    MemberType = eFineCollection.MemberType,
                };

                await service.UpdateAsync(parse);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
