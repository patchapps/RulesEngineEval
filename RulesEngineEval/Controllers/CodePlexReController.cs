using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RulesEngine;
using RulesEngineEval.Models;

namespace RulesEngineEval.Controllers
{
    public class CodePlexReController : Controller
    {
        //
        // GET: /CodePlexRe/

        public ActionResult Simple()
        {
            Person person = new Person();
            person.Name = "Bill";
            person.Phone = "1234214";
            person.DateOfBirth = new DateTime(2000, 10, 2);
            RunSimple(person);
            return View(person);
        }

        [HttpPost]
        public ActionResult Simple(Person person)
        {
            RunSimple(person);
            return View(person);
        }
        
        public static void RunSimple(Person person)
        {
            Engine engine = new Engine();
            engine.For<Person>()
                    .Setup(p => p.DateOfBirth)
                        .MustBeLessThan(DateTime.Now)
                    .Setup(p => p.Name)
                        .MustNotBeNull()
                        .MustMatchRegex("^[a-zA-z]+$")
                    .Setup(p => p.Phone)
                        .MustNotBeNull()
                        .MustMatchRegex("^[0-9]+$");
            
            person.IsValid = engine.Validate(person).ToString();

        }


        public ActionResult CrossField()
        {
            var user = new NewUserRegistration() { UserName = "user1", Password = "123", ConfirmPassword = "123" };
            RunCrossField(user);
            return View(user);
        }

        [HttpPost]
        public ActionResult CrossField(NewUserRegistration user)
        {
            RunCrossField(user);
            return View(user);
        }

        public static void RunCrossField(NewUserRegistration user)
        {
            Engine engine = new Engine();
            engine.For<NewUserRegistration>()
                .Setup(u => user.UserName)
                    .MustNotBeNullOrEmpty()
                .Setup(u => user.Password)
                    .MustNotBeNullOrEmpty()
                .Setup(u => user.ConfirmPassword)
                    .MustEqual(u => user.Password);

            user.IsValid = engine.Validate(user).ToString();

        }

        public ActionResult ObjectGraphValidation()
        {
            var chessClub = new Club("Chess Club"
                 , new Member("athoma13", "athoma13@codeplex.com")
                 , new Member("Arpad Elo", "arpad@hotmail.com")
                 , new Member("Garry Kasparov", "garry@gmail.com")
                 , new Member("Bobby B", "bobby@aol.com")
                 );

            RunObjectGraphValidation(chessClub);
            return View(chessClub);
        }

        [HttpPost]
        public ActionResult ObjectGraphValidation(Club club)
        {
            club = new Club("Chess Club"
                 , new Member("athoma13", "athoma13@codeplex.com")
                 , new Member("Arpad Elo", "arpad@hotmail.com")
                 , new Member("Garry Kasparov", "garry@gmail.com")
                 , new Member("", "bobby@aol.com")
                 );
            RunObjectGraphValidation(club);
            return View(club);
        }

        public static void RunObjectGraphValidation(Club club)
        {
            Engine engine = new Engine();

            engine.For<Member>()
                .Setup(member => member.Name)
                    .MustNotBeNullOrEmpty()
                .Setup(member => member.Email)
                    .MustNotBeNullOrEmpty();

            engine.For<Club>()
                .Setup(c => club.President)
                    .MustNotBeNull()
                    .CallValidate() //Calls engine.Validate for the President.
                .Setup(c => club.Members)
                    .MustNotBeNull()
                    .CallValidateForEachElement(); //Calls engine.Validate() on each element

            club.IsValid = engine.Validate(club).ToString();

        }


        public ActionResult CustomRules()
        {
            var member = new Member { Name = "athoma13", Email = "athoma13@codeplex.com" };
            RunCustomRules(member);
            return View(member);
        }

        [HttpPost]
        public ActionResult CustomRules(Member member)
        {
            RunCustomRules(member);
            return View(member);
        }

        public static void RunCustomRules(Member member)
        {
            Engine engine = new Engine();

            engine.For<Member>()
               .Setup(m => member.Name)
                   .MustNotBeNullOrEmpty()
               .Setup(m => member.Email)
                   .MustNotBeNullOrEmpty()
                   .MustPassRule(new EmailRule());

            
            member.IsValid = engine.Validate(member).ToString();

        }

        public ActionResult ErrorMessages()
        {
            var employee = new Employee() { Email = "bill@hotmail.com", EmployeeNumber = 123, Name = "bill" }; 
            RunErrorMessages(employee);
            return View(employee);
        }

        [HttpPost]
        public ActionResult ErrorMessages(Employee employee)
        {
            RunErrorMessages(employee);
            return View(employee);
        }

        public static void RunErrorMessages(Employee employee)
        {
            var engine = new Engine();
            engine.For<Person>()
                    .Setup(m => m.Name)
                        .MustNotBeNullOrEmpty()
                        .WithMessage("Name must not be empty") //Only when name is null or empty
                        .MustMatchRegex(@"^[a-z]+$")
                        .WithMessage("Name contains invalid characters") //Only when name contains invalid characters

                    .Setup(m => m.Email)
                        .WithMessage("Invalid email address") //For any rule breakers on the Email property.
                        .MustNotBeNullOrEmpty()
                        .MustMatchRegex(@"^[a-z]+@[a-z]+(\.[a-z]+)*$");

            engine.For<Employee>() //Member will inherit rules set for a Person.
                    .Setup(m => m.EmployeeNumber)
                        .WithMessage("Employee number is not valid")
                        .MustBeGreaterThan(0);

            
            var report = new ValidationReport(engine);
            var result = report.Validate(employee).ToString();
            var errors = report.GetErrorMessages(employee);
            employee.IsValid = result;
            string flatErrors = string.Empty;
            foreach (string s in errors)
                flatErrors += s;
            employee.Errors = flatErrors;

            


        }


    }
}
