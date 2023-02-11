using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Northgard.Core.Encryption
{
    /// <summary> 
    /// Class provides static method to serialize and encrypt data to XML 
    ///  
    /// Works on Android, iOS, Standalone. 
    ///  
    /// Does not encrypt data on WP8 
    /// </summary> 
    public static class EncryptedXmlSerializer
    {
        private static readonly string PrivateKey = SystemInfo.deviceUniqueIdentifier.Replace("-", string.Empty);

        #region API 

        /// <summary> 
        /// Reads and decrypts file at specified path 
        /// </summary> 
        /// <param name="path">Patht to file</param> 
        /// <typeparam name="T">Type of the serialized object</typeparam> 
        /// <returns>Decrypted deserialized object or null if file does not exist</returns> 
        public static T Load<T>(string path) where T : class
        {
            T result;

            if (!File.Exists(path))
            {
                return null;
            }

            var test = new List<int>();
            test.RemoveAll(i => i == 0);
            string data;
            using (var reader = new StreamReader(path))
            {
                var text = reader.ReadToEnd();
                if (string.IsNullOrEmpty(text))
                    return null;
                try
                {
                    data = DecryptData(text);
                }
                catch
                {
                    return null;
                }
            }

            var stream = new MemoryStream();
            using (var sw = new StreamWriter(stream) { AutoFlush = true })
            {
                sw.WriteLine(data);
                stream.Position = 0;
                result = new XmlSerializer(typeof(T)).Deserialize(stream) as T;
            }

            return result;
        }

        /// <summary> 
        /// Encrypts and serializes file at the specified path. 
        /// </summary> 
        /// <param name="path">Path to save the file</param> 
        /// <param name="value">Object to serialize</param> 
        /// <typeparam name="T">Type of object to serialize</typeparam> 
        public static void Save<T>(string path, object value) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, value);
                stream.Flush();
                stream.Position = 0;
                string sr = new StreamReader(stream).ReadToEnd();
                var fileStream = new FileStream(path, FileMode.Create);
                var streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(EncryptData(sr));
                streamWriter.Close();
                fileStream.Close();
            }
        }

        #endregion

        public static string SerializeData<T>(T data, bool encrypt = true) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            using var stream = new MemoryStream();
            serializer.Serialize(stream, data);
            stream.Flush();
            stream.Position = 0;
            string sr = new StreamReader(stream).ReadToEnd();
            if (encrypt)
            {
                return EncryptData(sr);   
            }
            return sr;
        }
        public static T DeserializeData<T>(string text, bool encrypt = true) where T : class
        {
            string data;
            if (string.IsNullOrEmpty(text))
                return null;
            if (encrypt)
            {
                try
                {
                    data = DecryptData(text);
                }
                catch
                {
                    return null;
                }   
            }
            else
            {
                data = text;
            }

            var stream = new MemoryStream();
            using var sw = new StreamWriter(stream) { AutoFlush = true };
            sw.WriteLine(data);
            stream.Position = 0;
            T result = new XmlSerializer(typeof(T)).Deserialize(stream) as T;
            return result;
        }

        #region encrypt_decrypt 
        private static string EncryptData(string toEncrypt)
        {
#if UNITY_WP8
             return toEncrypt; 
#else
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged rDel = CreateRijndaelManaged();
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
#endif
        }

        private static string DecryptData(string toDecrypt)
        {
#if UNITY_WP8
             return toDecrypt; 
#else
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            RijndaelManaged rDel = CreateRijndaelManaged();
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
#endif
        }

#if !UNITY_WP8
        private static RijndaelManaged CreateRijndaelManaged()
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(PrivateKey);
            var result = new RijndaelManaged();

            var newKeysArray = new byte[16];
            Array.Copy(keyArray, 0, newKeysArray, 0, 16);

            result.Key = newKeysArray;
            result.Mode = CipherMode.ECB;
            result.Padding = PaddingMode.PKCS7;
            return result;
        }
#endif
        #endregion
    }
}
