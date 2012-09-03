using IJM.Specification.Example.ValueObjects;

namespace IJM.Specification.Example.Specifications
{
    public class WithinOverdraftSpecification: ISpecification<BankAccount>
    {
        #region Constructor(s)
        public WithinOverdraftSpecification()
        {
        }
        #endregion

        #region Public Method(s)
        public bool IsSatisfiedBy(BankAccount entity)
        {
            return ((entity.CurrentBalance > entity.OverdraftAmount));
        }
        #endregion        
    }
}
