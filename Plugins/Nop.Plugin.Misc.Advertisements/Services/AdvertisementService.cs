using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Misc.Advertisements.DomainModels;
using Nop.Plugin.Misc.Advertisements.Models;
using Nop.Core.Data;
using Nop.Services.Events;
using Nop.Core.Domain.Catalog;

namespace Nop.Plugin.Misc.Advertisements.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IRepository<Advertisement> _advertisementRepository;
        private readonly IEventPublisher _eventPublisher;

        public AdvertisementService(IRepository<Advertisement> advertisementRepository,
            IEventPublisher eventPublisher)
        {
            _advertisementRepository = advertisementRepository;
            _eventPublisher = eventPublisher;
        }

        public void DeleteAdvertisement(Advertisement advertisement)
        {
            if (advertisement == null)
                throw new ArgumentNullException("advertisement");

            var ad = _advertisementRepository.GetById(advertisement.Id);
            ad.Deleted = true;
            UpdateAdvertisement(ad);

            _eventPublisher.EntityDeleted(advertisement);
        }
        
        public Advertisement GetAdvertisementById(int advertisementId)
        {
            return _advertisementRepository.GetById(advertisementId);
        }

        public void InsertAdvertisement(Advertisement advertisement)
        {
            if (advertisement == null)
                throw new ArgumentNullException("advertisement");

            _advertisementRepository.Insert(advertisement);

            _eventPublisher.EntityInserted(advertisement);
        }

        public IPagedList<Advertisement> SearchAdvertisements(
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            int storeId = 0,
            int vendorId = 0,
            string keywords = null,
            ProductSortingEnum orderBy = ProductSortingEnum.Position)
        {
            #region Search products

            //products
            var query = _advertisementRepository.Table;
            query = query.Where(p => !p.Deleted);

            if (storeId > 0)
            {
                query = query.Where(ad => ad.StoreId == storeId);
            }

            if (vendorId > 0)
            {
                query = query.Where(ad => ad.VendorId == vendorId);
            }

            if (orderBy == ProductSortingEnum.Position)
            {
                //otherwise sort by name
                query = query.OrderBy(p => p.Name);
            }
            else if (orderBy == ProductSortingEnum.NameAsc)
            {
                //Name: A to Z
                query = query.OrderBy(p => p.Name);
            }
            else if (orderBy == ProductSortingEnum.NameDesc)
            {
                //Name: Z to A
                query = query.OrderByDescending(p => p.Name);
            }
            else if (orderBy == ProductSortingEnum.CreatedOn)
            {
                //creation date
                query = query.OrderByDescending(p => p.CreatedOnUtc);
            }
            else
            {
                //actually this code is not reachable
                query = query.OrderBy(p => p.Name);
            }
            #endregion

            var products = new PagedList<Advertisement>(query, pageIndex, pageSize);

            return products;
        }

        public void UpdateAdvertisement(Advertisement advertisement)
        {
            if (advertisement == null)
                throw new ArgumentNullException("advertisement");

            _advertisementRepository.Update(advertisement);

            _eventPublisher.EntityUpdated(advertisement);
        }

        public virtual IList<Advertisement> GetAdvertisementsByIds(int[] advertisementIds)
        {
            if (advertisementIds == null || advertisementIds.Length == 0)
                return new List<Advertisement>();

            var query = from p in _advertisementRepository.Table
                        where advertisementIds.Contains(p.Id)
                        select p;
            var advertisements = query.ToList();
            //sort by passed identifiers
            var sortedAdvertisements = new List<Advertisement>();
            foreach (int id in advertisementIds)
            {
                var advertisement = advertisements.Find(x => x.Id == id);
                if (advertisement != null)
                    sortedAdvertisements.Add(advertisement);
            }
            return sortedAdvertisements;
        }
    }
}
