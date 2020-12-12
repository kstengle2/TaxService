using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace TaxService
{
    public static class Utilities
    {
        
        public static bool IsUSZipCode(string zipCode)
        {
            var _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
            var validZipCode = true;
            if (!Regex.Match(zipCode, _usZipRegEx).Success)
            {
                validZipCode = false;
            }
            return validZipCode;
        }
        public static string CallRestAPI(string url, string bearerToken)
        {
            try
            {
                HttpWebRequest myHttpWebRequest = null;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                myHttpWebRequest.Timeout = 15000;
                myHttpWebRequest.Method = "GET";
                myHttpWebRequest.ContentType = "application/json";
                myHttpWebRequest.Headers.Add("Authorization", "Bearer " + bearerToken);

                WebResponse myWebResponse = myHttpWebRequest.GetResponse();
                string result;
                using (var reader = new StreamReader(myWebResponse.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
