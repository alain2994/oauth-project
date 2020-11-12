using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using exercice_2.IMWFramework.Extensions;
using exercice_2.IMWFramework.Managers;
using exercice_2.IMWFramework.ResponseModels;
using exercice_2.IMWFramework.Utilities;
using exercice_2.IMWFramework.Utilities.Keys;
using exercise_3.IMWFramework.Utilities;
using exercise_3.Repository;
using exercise_3.Repository.Models;
using exercise_3.ResponseModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exercise_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        [HttpGet]
        [Route("accounts")]
        public ResponseObject GetAccounts ()
        {
            var db = new AccountsDbContext();
            var dbAccounts = db.Accounts;
            var responseAccounts = new ResponseList<ResponseAccount>();

            foreach( var account in dbAccounts)
            {
                var responseAccount = new ResponseAccount();

               responseAccount= DbToResponseMapper.MapResponseFromDbObject<Account, ResponseAccount>(account, responseAccount);
               responseAccounts.Add(responseAccount);
            }

            return ResponseObject.ResponseWithData(responseAccounts);
        }

        [HttpPost]
        [Route("register")]

        public ResponseObject Register ()
        {
            string firstName = Request.Form[CommonKeys.FIRST_NAME];
            string lastName = Request.Form[CommonKeys.LAST_NAME];
            string address = Request.Form[CommonKeys.ADDRESS];
            string email = Request.Form[CommonKeys.EMAIL];
            string userName = Request.Form[CommonKeys.USERNAME];
            string password = Request.Form[CommonKeys.PASSWORD];

            var fields = new List<(string, string)>
            {
                (CommonKeys.FIRST_NAME, RegexFormats.ALPHABETIC_STRING),
                (CommonKeys.LAST_NAME, RegexFormats.ALPHABETIC_STRING),
                (CommonKeys.ADDRESS, RegexFormats.ALPHANUMERIC_STRING),
                (CommonKeys.EMAIL, RegexFormats.EMAIL),
                (CommonKeys.USERNAME, RegexFormats.ALPHANUMERIC_STRING),
            };

            Request.ValidateRequestData(fields);

            List<string> field = new List<string>();
            field.Add(CommonKeys.PASSWORD);

            Request.ValidateRequestData(field);
            var db = new AccountsDbContext();

            bool doesUserNameExists = db.Accounts.Any(i => i.Username == userName);
            bool doesEmailExists = db.Accounts.Any(i => i.Email == email);

            if (doesUserNameExists == true)
            {
                return Request.CreateWithError(ApiError.UsernameConflict);
            }

            if (doesEmailExists == true)
            {
                return Request.CreateWithError(ApiError.EmailConflict);
            }
            string hashedPassword = Hasher.HashWithSHA1(password, userName);
           
            var account = new Account()
            {
                FirstName = firstName,
                LastName = lastName,
                Username = userName,
                Address = address,
                Email = email,
                Password = hashedPassword
            };

           
            db.Add(account);
            db.SaveChanges();

            return Request.CreateEmptyResponse();
        }

        [HttpPost]
        [Route("login")]

        public ResponseObject LogIn ()
        {
            string username = Request.Form[CommonKeys.USERNAME];
            string password = Request.Form[CommonKeys.PASSWORD];

            var fields = new List<string>() { CommonKeys.USERNAME, CommonKeys.PASSWORD };
            Request.ValidateRequestData(fields);

            var db = new AccountsDbContext();

            string hashedPassword = Hasher.HashWithSHA1(password, username);

            var isPasswordAndUsernameCorrect = db.Accounts.Any(j => j.Username == username && j.Password == hashedPassword);

            if(isPasswordAndUsernameCorrect == false)
            {
                return Request.CreateWithError(ApiError.IncorrectPasswordOrUsername);
            }

            return ResponseObject.ResponseEmpty();
        }
    }
}
