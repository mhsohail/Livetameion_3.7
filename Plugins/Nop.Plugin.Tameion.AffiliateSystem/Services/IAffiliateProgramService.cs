using Nop.Core.Domain.Orders;
using Nop.Plugin.Tameion.AffiliateSystem.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.AffiliateSystem.Services
{
    public interface IAffiliateProgramService
    {
        #region AffiliateProgram
        void InsertAffiliateProgram(AffiliateProgram affiliateProgram);
        void UpdateAffiliateProgram(AffiliateProgram affiliateProgram);
        void DeleteAffiliateProgram(AffiliateProgram affiliateProgram);
        AffiliateProgram GetAffiliateProgramById(int affiliateProgramId);
        IList<AffiliateProgram> GetAllAffiliatePrograms();
        bool VerifyQualificaion(AffiliateProgram AffiliateProgram, Order order);
        #endregion

        #region ProgramCondition
        void InsertProgramCondition(ProgramCondition condition);
        void UpdateProgramCondition(ProgramCondition condition);
        void DeleteProgramCondition(ProgramCondition condition);
        ProgramCondition GetProgramConditionById(int condition);
        IList<ProgramCondition> GetAllProgramConditions();
        #endregion
    }
}
