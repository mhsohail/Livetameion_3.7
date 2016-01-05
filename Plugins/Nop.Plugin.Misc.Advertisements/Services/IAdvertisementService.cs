using System.Collections.Generic;
using Nop.Core;
using Nop.Plugin.Misc.Advertisements.Models;
using Nop.Plugin.Misc.Advertisements.DomainModels;
using Nop.Core.Domain.Catalog;

namespace Nop.Plugin.Misc.Advertisements.Services
{
    public partial interface IAdvertisementService
    {
        Advertisement GetAdvertisementById(int AdvertisementId);
        void UpdateAdvertisement(Advertisement advertisement);
        void InsertAdvertisement(Advertisement advertisement);
        void DeleteAdvertisement(Advertisement advertisement);
        IPagedList<Advertisement> SearchAdvertisements(
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            int storeId = 0,
            int vendorId = 0,
            string keywords = null,
            ProductSortingEnum orderBy = ProductSortingEnum.Position);
        IList<Advertisement> GetAdvertisementsByIds(int[] advertisementIds);
    }
}
