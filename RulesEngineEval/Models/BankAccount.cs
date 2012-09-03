namespace IJM.Specification.Example.ValueObjects
{
    public class BankAccount
    {
        #region Properties
        public int Id
        {
            get;
            private set;
        }
        
        public string AccountName
        {
            get;
            private set;
        }

        public int CurrentBalance
        {
            get;
            private set;
        }

        public int OverdraftAmount
        {
            get;
            private set;
        }
        #endregion

        #region Constructor(s)
        public BankAccount()
        {

        }
        
        /// <summary>
        /// Initialise a new instance of the BankAccount class
        /// </summary>
        public BankAccount(int id, string accountName, int currentBalance, int overdraftAmount)
        {
            this.Id = id;
            this.AccountName = accountName;
            this.CurrentBalance = currentBalance;
            this.OverdraftAmount = overdraftAmount;
        }
        #endregion
    }
}
