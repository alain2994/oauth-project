using exercice_2.IMWFramework.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace exercice_2.IMWFramework.ResponseModels
{

    public class ResponseObject
    {
        [JsonPropertyName("error")]
        public ResponseError Error { get; set; } = new ResponseError();

        [JsonPropertyName("data")]
        public Object Data { get; set; }

        public ResponseObject()
        {
            Error = new ResponseError();
            Data = new ResponseEmpty();
        }

        public static ResponseObject ResponseWithData(ResponseModel dataSent)
        {
            ResponseObject response = new ResponseObject() { Data = dataSent };
            return response;
        }

        public static ResponseObject ResponseWithError(int code, String message)
        {
            ResponseError responseError = new ResponseError()
            {
                Code = code,
                Message = message
            };

            ResponseObject response = new ResponseObject()
            {
                Error = responseError
            };

            return response;
        }

        public static ResponseObject ResponseEmpty()
        {
            return new ResponseObject();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

 
    }
}
