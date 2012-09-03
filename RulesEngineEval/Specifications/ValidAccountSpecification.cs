using System;
using IJM.Specification.Example.ValueObjects;

namespace IJM.Specification.Example.Specifications
{ 
    public class ValidAccountSpecification: ISpecification<BankAccount>
    {
        #region Constructor(s)
        public ValidAccountSpecification()
        {
        }
        #endregion

        #region Public Method(s)
        public bool IsSatisfiedBy(BankAccount entity)
        {
            return (!String.IsNullOrWhiteSpace(entity.AccountName));
        }
        #endregion        
    }
}
