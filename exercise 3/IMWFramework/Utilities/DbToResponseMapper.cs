using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace exercice_2.IMWFramework.Utilities
{
    public class DbToResponseMapper
    {
        //Template Function that can be used in any project (Generic)
        public static ResponseObject MapResponseFromDbObject<DBObject, ResponseObject>(DBObject modelInstance, ResponseObject responseObject, List<String> fieldsToIgnore = null)
        {
                var responseObjType = responseObject.GetType();

            foreach (var property in responseObjType.GetProperties())
            {
                if (fieldsToIgnore != null)
                {
                    if (!fieldsToIgnore.Contains(property.Name))
                    {
                        var value = modelInstance.GetType().GetProperty(property.Name).GetValue(modelInstance);
                        responseObject.GetType().GetProperty(property.Name).SetValue(responseObject, value);
                    }
                }
                else
                {
                    var value = modelInstance.GetType().GetProperty(property.Name).GetValue(modelInstance);
                    responseObject.GetType().GetProperty(property.Name).SetValue(responseObject, value);
                }
            }

            return responseObject;
        }

        
    }
}
