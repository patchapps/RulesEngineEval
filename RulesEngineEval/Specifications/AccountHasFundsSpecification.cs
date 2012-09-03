using IJM.Specification.Example.ValueObjects;

namespace IJM.Specification.Example.Specifications
{
    public class AccountHasFundsSpecification: ISpecification<BankAccount>
    {
        #region Constructor(s)
        public AccountHasFundsSpecification()
        {
        }
        #endregion

        #region Public Method(s)
        public bool IsSatisfiedBy(BankAccount entity)
        {
            return (entity.CurrentBalance > 0);
        }
        #endregion        
    }
}
