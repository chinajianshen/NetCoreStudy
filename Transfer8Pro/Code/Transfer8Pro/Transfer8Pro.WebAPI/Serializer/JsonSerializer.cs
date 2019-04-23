using Nancy;
using Nancy.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Transfer8Pro.Entity;

namespace Transfer8Pro.WebAPI.Serializer
{
    public class JsonSerializer : ISerializer
    {
        public bool CanSerialize(string contentType)
        {
            return IsJsonType(contentType);
        }

        public IEnumerable<string> Extensions
        {
            get
            {
                yield return "json";
            }
        }

        public void Serialize<TModel>(string contentType, TModel model, Stream outputStream)
        {
            using (StreamWriter streamWriter = new StreamWriter(new UnclosableStreamWrapper(outputStream)))
            {
                try
                {
                    var str = JsonObj.ToJsonWithNoneName(model);

                    streamWriter.Write(str);
                }
                catch (Exception e)
                {
                    streamWriter.Write(e.Message);
                }
            }
        }

        private static bool IsJsonType(string contentType)
        {
            if (string.IsNullOrEmpty(contentType))
                return false;
            string str = contentType.Split(';')[0];
            if (str.Equals("application/json", StringComparison.OrdinalIgnoreCase) || str.StartsWith("application/json-", StringComparison.OrdinalIgnoreCase) || str.Equals("text/json", StringComparison.OrdinalIgnoreCase))
                return true;
            if (str.StartsWith("application/vnd", StringComparison.OrdinalIgnoreCase))
                return str.EndsWith("+json", StringComparison.OrdinalIgnoreCase);
            return false;
        }
    }
}