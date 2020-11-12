using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace exercice_2.IMWFramework.Managers
{
    public enum ApiError
    {    

        [Description("Invalid Phone Number")]
        InvalidPhoneNumber = 454,

        [Description("Username already exists. Please choose a different username")]
        UsernameConflict = 455,

        [Description("Email already exists. Please choose a different email")]
        EmailConflict = 456,

        [Description("No record found")]
        InvalidRecord = 460,

        [Description("Missing or invalid data")]
        MissingOrInvalidData = 461,

        [Description("Incorrect Password or Username")]
        IncorrectPasswordOrUsername = 457,

        InternalServerError = 500

            

    }

    public static class EnumerationExtension
    {
        public static string GetDescription(this Enum enumValue)
        {
            return enumValue.GetType()
                       .GetMember(enumValue.ToString())
                       .First()
                       .GetCustomAttribute<DescriptionAttribute>()?
                       .Description ?? string.Empty;
        }
    }

    public class ErrorManager
    {

    }
}
