using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerApp
{
    public class DataConvertor
    {
        public static byte[] ToByteData<T>(T data) where T : class
        {
            string data_str = JsonConvert.SerializeObject(data);

            byte[] _data = Encoding.ASCII.GetBytes(data_str);

            return _data;
        }

        public static T FromByteData<T>(byte[] data) where T : class
        {
            string data_str = Encoding.ASCII.GetString(data);

            T _data = (T)JsonConvert.DeserializeObject(data_str, typeof(T));

            return _data;
        }

        public static T FromString<T>(string data) where T : class
        {
            T _data = (T)JsonConvert.DeserializeObject(data, typeof(T));

            return _data;
        }

        public static string ToString<T>(T data) where T : class
        {
            string _data = JsonConvert.SerializeObject(data);

            return _data;
        }
    }
}
