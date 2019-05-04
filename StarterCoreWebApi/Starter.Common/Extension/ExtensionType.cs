using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Starter.Common.Extension
{
    public static class ExtensionType
    {
        #region Json相关

        /// <summary>
        /// 将对象序列化成Jsonstring
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string ToJsonSerialize(this object item)
        {
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            return JsonConvert.SerializeObject(item, Newtonsoft.Json.Formatting.Indented, timeConverter);
        }

        /// <summary>
        /// 将对象序列化成Jsonstring
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string ToJsonSerializeNONull(this object item)
        {
            var jSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            return JsonConvert.SerializeObject(item, Newtonsoft.Json.Formatting.Indented, jSetting);
        }
        /// <summary>
        /// 将Jsonstring反序列化成目标对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <param name="isIgnoreNull">是否忽略为NULL的值</param>
        /// <returns></returns>
        public static T DeserializeFromJson<T>(this string jsonString, bool isIgnoreNull = false)
        {
            //是否忽略为NULL的值
            if (isIgnoreNull)
            {
                var jsonSetting = new JsonSerializerSettings();
                jsonSetting.NullValueHandling = NullValueHandling.Ignore;
                return JsonConvert.DeserializeObject<T>(jsonString, jsonSetting);
            }
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        #endregion

        #region XML相关

        /// <summary>
        /// XML序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static XDocument XmlSerialize<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            var document = new XDocument();
            using (var writer = document.CreateWriter())
            {
                serializer.Serialize(writer, obj);
            }

            return document;
        }

        /// <summary>
        /// XML反序列化
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="document">Xml文档</param>
        /// <returns></returns>
        public static T Deserialize<T>(this XDocument document)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            try
            {
                using (var reader = document.CreateReader())
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                //Logging.Logger.Error(ex.Message, ex);
                return default(T);
            }
        }

        /// <summary>
        /// XML反序列化
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="xml">Xml字符串</param>
        /// <returns></returns>
        public static T XmlDeserializeFromString<T>(this string xml)
        {
            try
            {
                XDocument document = XDocument.Parse(xml);
                return document.Deserialize<T>();
            }
            catch (Exception)
            {
                //Logging.Logger.Error(ex.Message, ex);
                return default(T);
            }
        }

        /// <summary>
        /// XML反序列化
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="xmlPath">xml文件路径(物理路径)</param>
        /// <returns></returns>
        public static T XmlDeserializeFromFile<T>(string xmlPath)
        {
            var type = typeof(T);

            using (var stream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(type);
                    return (T)serializer.Deserialize(stream);
                }
                catch (Exception)
                {
                    //Logging.Logger.Error(ex.Message, ex);
                    return default(T);
                }
            }
        }

        #endregion
    }
}
