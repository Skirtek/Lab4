using System;
using System.Net;
using ConsoleApp1.Intefaces;

namespace ConsoleApp1.Services
{
    public class ConnectionService: IConnection
    {
        public bool HasInternetConnection()
        {
            var address = new Uri(AppSettings.CheckInternetConnectionString);
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(address);
                request.Timeout = 5000;
                request.Credentials = CredentialCache.DefaultCredentials;
                var response = (HttpWebResponse)request.GetResponse();

                return response.StatusCode == HttpStatusCode.NoContent;
            }
            catch
            {
                return false;
            }
        }
    }
}