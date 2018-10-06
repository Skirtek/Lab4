using System;
using ConsoleApp1.Enums;
using ConsoleApp1.Resources;
using ConsoleApp1.Services;

namespace ConsoleApp1
{
    public class Program
    {
        private static readonly ZipCodeService ZipCodeService = new ZipCodeService();
        private static readonly ConnectionService ConnectionService = new ConnectionService();
        private static readonly EmailService EmailService = new EmailService();

        private static void Main(string[] args)
        {
            Console.WriteLine(AppResources.Main_ChooseFunction);
            Console.WriteLine(AppResources.Main_ValidateZipCode);
            Console.WriteLine(AppResources.Main_GetEmailAddresses);
            Console.WriteLine(AppResources.Main_Quit);
            var option = Console.ReadLine();
            do
            {
                switch (option)
                {
                    case "1":
                        ValidateZipCode();
                        break;
                    case "2":
                        GetEmails();
                        break;
                    case "3":
                        Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine(AppResources.Common_OptionIsNotAvailable);
                        option = Console.ReadLine();
                        break;
                }
            } while (option != null && (!option.Equals("1") || !option.Equals("2") || !option.Equals("3")));
        }

        private static void ValidateZipCode()
        {
            Console.WriteLine(AppResources.ValidateZipCode_EnterZipCode);
            string code = Console.ReadLine();
            Console.WriteLine(ZipCodeService.ValidateZipCodeInput(code)
                ? ConnectionService.HasInternetConnection() ?
                    $"{AppResources.ValidateZipCode_CodeValidation} {ValidationResult.poprawny}. {AppResources.ValidateZipCode_BelongsTo} {ZipCodeService.GetCityForCode_JSONHandler(code)}"
                    : $"{AppResources.ValidateZipCode_CodeValidation} {ValidationResult.poprawny}. {AppResources.ValidateZipCode_BelongsTo} {ZipCodeService.GetCityForCode_Offline(code)}"
                : $"{AppResources.ValidateZipCode_CodeValidation} {ValidationResult.niepoprawny}.");
            ExitApp();
        }

        private static void GetEmails()
        {
            Console.WriteLine(AppResources.GetEmails_GetType);
            Console.WriteLine(AppResources.GetEmails_Text);
            Console.WriteLine(AppResources.GetEmails_Website);
            var option = Console.ReadLine();
            do
            {
                switch (option)
                {
                    case "1":
                        GetEmailsFromLocalString();
                        break;
                    case "2":
                        GetEmailsFromWebsite();
                        break;
                    default:
                        Console.WriteLine(AppResources.Common_OptionIsNotAvailable);
                        option = Console.ReadLine();
                        break;
                }
            } while (option != null && (!option.Equals("1") || !option.Equals("2")));
        }

        private static void ExitApp()
        {
            Console.WriteLine(AppResources.Common_ExitApp);
            Console.ReadLine();
            Environment.Exit(1);
        }
        private static void GetEmailsFromLocalString()
        {
            Console.WriteLine(AppResources.GetEmailsFromLocalString_GetString);
            string text = Console.ReadLine();
            Console.WriteLine();
            EmailService.PrintLocalList(text);
            ExitApp();
        }

        private static void GetEmailsFromWebsite()
        {
            if (ConnectionService.HasInternetConnection())
            {
                Console.WriteLine(AppResources.GetEmailsFromWebsite_EnterUrl);
                string url = Console.ReadLine();
                Console.WriteLine(EmailService.ValidateUrl(url) ?
                    EmailService.ValidateIfSiteExists(url) ?
                        "" : string.Format(AppResources.GetEmailsFromWebsite_SiteIsUnavailable,url)
                    : string.Format(AppResources.GetEmailsFromWebsite_UrlIsInvalid, url));
                if (EmailService.ValidateIfSiteExists(url))
                {
                    EmailService.PrintList(url);
                }
                ExitApp();
            }
            else
            {
                Console.WriteLine(AppResources.Common_NoInternetConnection);
                ExitApp();
            }
        }
    }
}