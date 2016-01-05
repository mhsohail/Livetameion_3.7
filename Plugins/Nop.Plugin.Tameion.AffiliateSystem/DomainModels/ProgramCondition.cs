using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.AffiliateSystem.DomainModels
{
    public class ProgramCondition : BaseEntity
    {
        #region Properties
        public string Name { get; set; }
        public LogicalOperation Operation
        {
            get { return (LogicalOperation)OperationId; }
            set
            {
                OperationId = (int)value;
                if (value == LogicalOperation.GreaterThan)
                    OperationSymbol = ">";
                if (value == LogicalOperation.GreaterThanOrEquals)
                    OperationSymbol = ">=";
                if (value == LogicalOperation.Is)
                    OperationSymbol = "==";
                if (value == LogicalOperation.IsNot)
                    OperationSymbol = "!=";
                if (value == LogicalOperation.IsNotOneOf)
                    OperationSymbol = "!=*";
                if (value == LogicalOperation.IsOneOf)
                    OperationSymbol = "==*";
                if (value == LogicalOperation.LessThan)
                    OperationSymbol = "<";
                if (value == LogicalOperation.LessThanOrEquals)
                    OperationSymbol = "<=";
            }
        }
        public int OperationId { get; set; }
        public string OperationSymbol { get; set; }
        public string Value { get; set; }
        public int AffiliateProgramId { get; set; }
        public string ConditionAttributeName { get; set; }
        #endregion

        #region Navigation Properties
        public virtual AffiliateProgram AffiliateProgram { get; set; }
        #endregion
    }
}
