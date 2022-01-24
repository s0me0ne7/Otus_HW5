using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Otus_HW5
{
    public static class Serializer
    {
        /// <summary> Serialize from object to CSV </summary>
        /// <param name="obj">any object</param>
        /// <returns>CSV</returns>
        public static string SerializeFromObjectToCSV(object obj)
        {
            StringBuilder resString = new StringBuilder();
            resString.Append("{");

            Type tp = obj.GetType();

            PropertyInfo[] properties = tp.GetProperties();
            bool firsttry = true;
            bool needSeparator = false;
            foreach (PropertyInfo property in properties)
            { if (!firsttry)
                    resString.Append(",");
                else
                    firsttry = false;
                resString.Append($"\"{property.Name}\":\"{property.GetValue(obj).ToString()}\"");
            }
            resString.Append("}");
            return resString.ToString();
        }
        /// <summary> Deserialize from CSV to object</summary>
        /// <param name="csv">string in CSV format</param>
        /// <returns>object</returns>
        public static object DeserializeFromCSVToObject<T>(string csv) where T : class, new()
        {
            Type objType = typeof(T);
            object obj = new T();
            var rrr = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty).ToList();

            var props = csv.Replace("{", "").Replace("}", "").Split(",");

            for (int i = 0; i < props.Length; i++)
            {
                var values = props[i].Split(':');
                string propName = values[0].Replace("\"","");
                string propValue = values[1].Replace("\"", "");

                var value = Convert.ChangeType(propValue, rrr[i].PropertyType);
                rrr[i].SetValue(obj: obj, value: value);
            }

            return obj;
        }
    }
}
