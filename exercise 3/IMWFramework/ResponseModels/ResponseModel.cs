using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exercice_2.IMWFramework.ResponseModels
{
    public interface ResponseModel {}

    public class ResponseEmpty: ResponseModel {}

    public class ResponseDictionary<T> : Dictionary<string, T>, ResponseModel where T: ResponseModel { }

    public class ResponseList<T> : List<T>, ResponseModel where T: ResponseModel { }
}

