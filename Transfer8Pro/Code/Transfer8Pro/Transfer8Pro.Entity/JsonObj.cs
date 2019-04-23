using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Transfer8Pro.Entity
{
    public class JsonObj<T>
    {
        public static string ToJson(T obj)
        {
            //Newtonsoft.Json.JsonConvert.DeserializeObject()
            IsoDateTimeConverter convert = new IsoDateTimeConverter();
            convert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json= Newtonsoft.Json.JsonConvert.SerializeObject(obj,convert);
            return json;
        }


        public static T FromJson(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                IsoDateTimeConverter convert = new IsoDateTimeConverter();
                convert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(text, convert);
            }
            else
                return default(T);           
        }

        public static string ToJson(List<T> obj)
        {
            IsoDateTimeConverter convert = new IsoDateTimeConverter();
            convert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj, convert);
            return json;
        }

        public static List<T> FromJson3(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                IsoDateTimeConverter convert = new IsoDateTimeConverter();
                convert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(text, convert);
            }
            else
                return default(List<T>);
        }

        public static object FromJson2(string text)
        {
            IsoDateTimeConverter convert = new IsoDateTimeConverter();
            convert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(convert);
            return Newtonsoft.Json.JsonConvert.DeserializeObject(text, settings);
        }

    }

    public class JsonObj
    {
        public static object FromJson(string text,Type t)
        {
            IsoDateTimeConverter convert = new IsoDateTimeConverter();
            convert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return Newtonsoft.Json.JsonConvert.DeserializeObject(text, t,convert);
        }

        public static string ToJson(object o, Type t)
        {
            IsoDateTimeConverter convert = new IsoDateTimeConverter();
            convert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            settings.Converters.Add(convert);
            return Newtonsoft.Json.JsonConvert.SerializeObject(o, t, settings);
        }
        public static string ToJson(object o)
        {
            IsoDateTimeConverter convert = new IsoDateTimeConverter();
            convert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            settings.Converters.Add(convert);
            return Newtonsoft.Json.JsonConvert.SerializeObject(o, settings);
        }
        public static string ToJsonWithNoneName(object o)
        {
            IsoDateTimeConverter convert = new IsoDateTimeConverter();
            convert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None;
            settings.Converters.Add(convert);
            return Newtonsoft.Json.JsonConvert.SerializeObject(o, settings);
        }
    }
}
