using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConsoleApp1.Intefaces;
using ConsoleApp1.Resources;

namespace ConsoleApp1.Services
{
    public class EmailService : IEmail
    {
        private readonly HttpClient _client = new HttpClient();

        public bool ValidateUrl(string url) => Uri.TryCreate(url, UriKind.Absolute, out Uri result) &&
                   (result.Scheme == Uri.UriSchemeHttp ||
                    result.Scheme == Uri.UriSchemeFtp ||
                    result.Scheme == Uri.UriSchemeHttps);

        public bool ValidateIfSiteExists(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 5000;
                request.Credentials = CredentialCache.DefaultCredentials;

                var response = (HttpWebResponse)request.GetResponse();

                return response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetCodeFromUrl(string url) => await _client.GetStringAsync(url);

        public List<string> PrepareEmailList(string url)
        {
            var list = new List<string>();
            //pattern: [] dowolny znak z zakresu , + = jedno lub więcej wystąpień , {2,n} długość co najmniej 2 , ? = opcjonalne
            var regex = new Regex(@"[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-z0-9]+\.[a-z]{2,}(\.[a-z]{2,})?");

            try
            {
                foreach (Match match in regex.Matches(GetCodeFromUrl(url).Result))
                {
                    list.Add(match.Value);
                }
                return list.Distinct().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return list;
            }
        }

        public void PrintList(string url)
        {
            var list = PrepareEmailList(url);

            if (list.Count != 0)
            {
                foreach (string email in list)
                {
                    Console.WriteLine(email);
                }
            }
            else
            {
                Console.WriteLine(AppResources.PrintList_DidntFoundAnyEmails);
            }
        }

        public List<string> PrepareLocalEmailList(string text)
        {
            var list = new List<string>();
            //pattern: [] dowolny znak z zakresu , + = jedno lub więcej wystąpień , {2,n} długość co najmniej 2 , ? = opcjonalne
            var regex = new Regex(@"[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-z0-9]+\.[a-z]{2,}(\.[a-z]{2,})?");

            try
            {
                foreach (Match match in regex.Matches(text))
                {
                    list.Add(match.Value);
                }
                return list.Distinct().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return list;
            }
        }

        public void PrintLocalList(string text)
        {
            var list = PrepareLocalEmailList(text).Distinct().ToList();

            if (list.Count != 0)
            {
                foreach (string email in list)
                {
                    Console.WriteLine(email);
                }
            }
            else
            {
                Console.WriteLine(AppResources.PrintList_DidntFoundAnyEmails);
            }
        }
    }
}