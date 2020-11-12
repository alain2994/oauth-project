using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exercice_2.IMWFramework.Utilities
{
    public static class RegexFormats
    {
        public static string PHONE_NUMBER = @"^((961[\s+-]*(3|7(0|1)))|(03|7(0|1)))[\s+-]?\d{3}[\s+-]?\d{3}$";
        public static string ALPHABETIC_STRING = @"\A[^\d_]+\z";
        public static string ALPHANUMERIC_STRING = @"^[^*|\!\:<>[\]{}`\\();@&$]+$";
        public static string DATE_OF_BIRTH = @"^(0[1-9]|[12][0-9]|3[01])[- \/.](0[1-9]|1[012])[- \/.](19|20)\d\d$";
        public static string TIME = @"^(0|1|2)[0-9]:[0-5][0-9]$";
        public static string NUMBER = @"^[0-9]*$";
        public static string EMAIL = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
    }
}


