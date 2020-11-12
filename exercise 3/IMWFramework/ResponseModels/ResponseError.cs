
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace exercice_2.IMWFramework.ResponseModels
{

    public class ResponseError
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = null;

        [JsonPropertyName("code")]
        public int? Code { get; set; } = null;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}