using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp1.Intefaces
{
    public interface IEmail
    {
        /// <summary>
        /// Validates if user entered url in correct form
        /// </summary>
        /// <param name="url">Entered by user url address</param>
        bool ValidateUrl(string url);

        /// <summary>
        /// Checks if site requested by user exists
        /// </summary>
        /// <param name="url">Entered by user url address</param>
        /// <returns></returns>
        bool ValidateIfSiteExists(string url);

        /// <summary>
        /// Gets source code of site which was requested by user in url
        /// </summary>
        /// <param name="url">Entered by user url address</param>
        Task<string> GetCodeFromUrl(string url);

        /// <summary>
        /// Prepares list of email addresses from requested site
        /// </summary>
        /// <param name="url">Entered by user url address</param>
        List<string> PrepareEmailList(string url);

        /// <summary>
        /// Prints in console list of emails extracted from requested website
        /// </summary>
        /// <param name="url">Entered by user url address</param>
        void PrintList(string url);

        /// <summary>
        /// Prepares list of email addresses from entered string
        /// </summary>
        /// <param name="text">String entered by user</param>
        List<string> PrepareLocalEmailList(string text);

        /// <summary>
        /// Prints in console list of emails extracted from entered string
        /// </summary>
        /// <param name="text">String entered by user</param>
        void PrintLocalList(string text);
    }
}