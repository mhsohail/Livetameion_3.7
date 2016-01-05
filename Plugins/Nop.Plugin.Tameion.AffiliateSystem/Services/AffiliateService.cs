using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Tameion.AffiliateSystem.DomainModels;
using Nop.Core.Data;
using Nop.Services.Events;

namespace Nop.Plugin.Tameion.AffiliateSystem.Services
{
    public class AffiliateService : IAffiliateService
    {
        private readonly IRepository<Affiliate> _affiliateRepository;
        private readonly IEventPublisher _eventPublisher;

        public AffiliateService(IRepository<Affiliate> affiliateRepository,
            IEventPublisher eventPublisher)
        {
            _affiliateRepository = affiliateRepository;
            _eventPublisher = eventPublisher;
        }

        public void DeleteAffiliate(Affiliate affiliate)
        {
            if (affiliate == null)
                throw new ArgumentNullException("affiliate");

            _affiliateRepository.Delete(affiliate);

            _eventPublisher.EntityDeleted(affiliate);
        }

        public Affiliate GetAffiliateById(int affiliateId)
        {
            return _affiliateRepository.GetById(affiliateId);
        }

        public IList<Affiliate> GetAllAffiliates()
        {
            return _affiliateRepository.Table.ToList();
        }

        public void InsertAffiliate(Affiliate affiliate)
        {
            if (affiliate == null)
                throw new ArgumentNullException("affiliate");

            _affiliateRepository.Insert(affiliate);

            _eventPublisher.EntityInserted(affiliate);
        }

        public void UpdateAffiliate(Affiliate affiliate)
        {
            if (affiliate == null)
                throw new ArgumentNullException("affiliate");

            _affiliateRepository.Update(affiliate);

            _eventPublisher.EntityUpdated(affiliate);
        }
    }
}
