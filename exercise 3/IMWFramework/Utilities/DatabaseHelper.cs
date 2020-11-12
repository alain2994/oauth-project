using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercice_2.IMWFramework.Utilities
{
    public static class DatabaseHelper
    {
        public static List<Dictionary<string, object>> GetRowsFromDBQuery(DbConnection dbConnection, Dictionary<string, string> dbFields, string query)
        {
            var selectedFields = new StringBuilder();
            var i = 0;
            foreach (KeyValuePair<string, string> entry in dbFields)
            {
                selectedFields.Append(entry.Key);
                if (entry.Value != null)
                    selectedFields.Append($" AS {entry.Value}");
                if (i < (dbFields.Count - 1))
                    selectedFields.Append(", ");
                i++;
            }
            var formattedString = String.Format(query, selectedFields.ToString());
            var dbItems = new List<Dictionary<string, object>>() { };

            dbConnection.Open();

            var cmd = dbConnection.CreateCommand();
            cmd.CommandText = formattedString;
            cmd.CommandType = CommandType.Text;

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var currentItemDict = new Dictionary<string, object>() { };
                //const int fieldCount = reader.FieldCount;
                var currentCourseOffering = new object[100];
                reader.GetValues(currentCourseOffering);
                i = 0;
                foreach (KeyValuePair<string, string> entry in dbFields)
                {
                    var key = "";
                    if (entry.Value == null)
                    {
                        key = (entry.Key.Contains(".") ? entry.Key.Split('.')[1] : entry.Key);
                    }
                    else
                    {
                        key = entry.Value;
                    }

                    currentItemDict[key] = currentCourseOffering[i];

                    i++;
                }
                dbItems.Add(currentItemDict);
            }
            reader.Close();
            return dbItems;
        }

    }
}
