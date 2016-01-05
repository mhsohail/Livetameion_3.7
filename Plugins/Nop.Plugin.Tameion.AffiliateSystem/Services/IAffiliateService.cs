using Nop.Plugin.Tameion.AffiliateSystem.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.AffiliateSystem.Services
{
    public interface IAffiliateService
    {
        void InsertAffiliate(Affiliate affiliate);
        void UpdateAffiliate(Affiliate affiliate);
        void DeleteAffiliate(Affiliate affiliate);
        Affiliate GetAffiliateById(int affiliateId);
        IList<Affiliate> GetAllAffiliates();
    }
}
