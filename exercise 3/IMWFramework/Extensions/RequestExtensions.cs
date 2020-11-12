using exercice_2.IMWFramework.Managers;
using exercice_2.IMWFramework.Middleware.CustomExceptionMiddleware;
using exercice_2.IMWFramework.ResponseModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace exercice_2.IMWFramework.Extensions
{
    public static class RequestExtensions
    {
        //Check if some fields are missing / empty
        public static List<string> ValidateRequestData(this HttpRequest request, List<string> keysToValidate, bool returnIfError = false)
        {
            var invalidFields = new List<string>();

            foreach (var keyName in keysToValidate)
            {
                if (!request.Form.ContainsKey(keyName) || string.IsNullOrEmpty(request.Form[keyName]))
                {
                    invalidFields.Add(keyName);
                }
            }

            if (invalidFields.Count > 0)
            {
                if (returnIfError)
                    return invalidFields;
                else
                    throw new ApiException(ApiError.MissingOrInvalidData, $"Invalid or missing argument(s): { string.Join(", ", invalidFields) }");
            }
            else
            {
                return null;
            }
        }

        //check if the data entered corresponds to the right field format
        public static List<string> ValidateRequestData(this HttpRequest request, List<(string key, string regexFormat)> itemsToValidate, bool returnIfError = false)
        {
            var invalidFields = new List<string>();

            foreach (var item in itemsToValidate)
            {
                if(!request.Form.ContainsKey(item.key) || string.IsNullOrEmpty(request.Form[item.key]) || !Regex.IsMatch(request.Form[item.key], item.regexFormat))
                {
                    invalidFields.Add(item.key);
                }
            }

            if (invalidFields.Count > 0)
            {
                if (returnIfError)
                    return invalidFields;
                else
                    throw new ApiException(ApiError.MissingOrInvalidData, $"Invalid or missing argument(s): { string.Join(", ", invalidFields) }");
            }
            else
            {
                return null;
            }
        }

      

        public static ResponseObject CreateEmptyResponse(this HttpRequest request)
        {
            return ResponseObject.ResponseEmpty();
        }

        public static ResponseObject CreateWithError(this HttpRequest request, ApiError error)
        {
            //Set the status code for the request
            request.HttpContext.SetStatusCode((int)error);

            //Return the error by getting the default description of the error
            return ResponseObject.ResponseWithError((int)error, error.GetDescription());
        }

        public static ResponseObject CreateWithError(this HttpRequest request, int code, string message)
        {
            return ResponseObject.ResponseWithError(code, message);
        }
    }
}
