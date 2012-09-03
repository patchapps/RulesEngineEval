using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IJM.Specification.Example.ValueObjects;
using IJM.Specification;
using IJM.Specification.Example.Specifications;

namespace RulesEngineEval.Controllers
{
    public class AccountResult
    {
        public BankAccount Account { get; set; }
        public String Messages { get; set; }

        public AccountResult()
        {
            Messages = string.Empty;
        }

    }

    public class SpecController : Controller
    {
        //
        // GET: /Specifications/

        public ActionResult Index()
        {
            var accounts = new List<BankAccount>();
            accounts.Add(new BankAccount(1, "", 1, 0));
            accounts.Add(new BankAccount(2, "a name", 10, 0));
            accounts.Add(new BankAccount(3, "another name", 0, -10));
            accounts.Add(new BankAccount(4, "yet another", 0, 0));
            var results = new List<AccountResult>();
            foreach(var account in accounts){
                results.Add(new AccountResult{Account = account});
            }
            return View(results);
        }

        [HttpPost]
        public ActionResult Index(List<AccountResult> res)
        {
            var accounts = new List<BankAccount>();
            accounts.Add(new BankAccount(1, "", 1, 0));
            accounts.Add(new BankAccount(2, "a name", 10, 0));
            accounts.Add(new BankAccount(3, "another name", 0, -10));
            accounts.Add(new BankAccount(4, "yet another", 0, 0));
            var results = DoSpec(accounts);
            return View(results);
        }

        static List<AccountResult> DoSpec(List<BankAccount> accounts)
        {
            List<AccountResult> results = new List<AccountResult>();

            ISpecification<BankAccount> validAccountSpec = new ValidAccountSpecification();
            ISpecification<BankAccount> accountHasFunds = new AccountHasFundsSpecification();

            ISpecification<BankAccount> validAndHasFunds =
                new ValidAccountSpecification().And(new AccountHasFundsSpecification());

            ISpecification<BankAccount> canWithdraw =
                new AccountHasFundsSpecification().Or(new WithinOverdraftSpecification());

            ISpecification<BankAccount> safeAccount =
                new ValidAccountSpecification().And(new AccountHasFundsSpecification().Or(new WithinOverdraftSpecification()));

            foreach (var account in accounts)
            {
                var result = new AccountResult();
                result.Account = account;

                Console.WriteLine(String.Format("Account {0}", account.Id));
                //is valid account?
                if (!validAccountSpec.IsSatisfiedBy(account))
                {
                    result.Messages += "...is NOT valid";
                }
                else
                {
                    result.Messages += "...is valid";
                }

                //does account have funds?
                if (!accountHasFunds.IsSatisfiedBy(account))
                {
                    result.Messages += "...does NOT have funds";
                }
                else
                {
                    result.Messages += "...does have funds";
                }

                //valid and has funds?
                if (!validAndHasFunds.IsSatisfiedBy(account))
                {
                    result.Messages += "...is either NOT valid or does NOT have funds";
                }
                else
                {
                    result.Messages += "...is valid or does have funds";
                }

                //can withdraw?
                if (!canWithdraw.IsSatisfiedBy(account))
                {
                    result.Messages += "...you cannot withdraw";
                }
                else
                {
                    result.Messages += "...you can withdraw";
                }

                //safe account?
                if (!safeAccount.IsSatisfiedBy(account))
                {
                    result.Messages += "...is NOT a safe account";
                }
                else
                {
                    result.Messages += "...is a safe account";
                }
                results.Add(result);
            }
            return results;
        }

    }
}
