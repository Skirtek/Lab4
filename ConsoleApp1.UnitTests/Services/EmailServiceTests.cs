using System.Collections.Generic;
using ConsoleApp1.Services;
using Xunit;

namespace ConsoleApp1.UnitTests.Services
{
    public class EmailServiceTests
    {
        private readonly EmailService _emailService = new EmailService();
        private const string UniversityUrl = "https://dymek.utp.edu.pl/np/";
        private const string MyEmailAddress = "bartosz.mroz@ecom.software";

        [Fact]
        public void ValidateUrl_EmptyString()
        {
            //Act
            var result = _emailService.ValidateUrl(string.Empty);

            //Assert
            Assert.True(!result);
        }

        [Fact]
        public void ValidateUrl_InvalidUrl()
        {
            //Act
            var result = _emailService.ValidateUrl("abc");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateUrl_UrlWithoutProtocol()
        {
            //Act
            var result = _emailService.ValidateUrl("www.google.pl");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateUrl_UrlWithInvalidProtocol()
        {
            //Act
            var result = _emailService.ValidateUrl("hppt://www.google.pl");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateUrl_ValidHttpUrl()
        {
            //Act
            var result = _emailService.ValidateUrl("http://www.google.pl");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateUrl_ValidHttpsUrl()
        {
            //Act
            var result = _emailService.ValidateUrl(UniversityUrl);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateUrl_ValidFtpUrl()
        {
            //Act
            var result = _emailService.ValidateUrl("ftp://mrrandom.cba.pl/");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateUrl_ValidMailtoUrl()
        {
            //Act
            var result = _emailService.ValidateUrl($"mailto://{MyEmailAddress}");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateIfSiteExists_NonExistentPage()
        {
            //Act
            var result = _emailService.ValidateIfSiteExists("http://sadasdasdasdasdasdasdasdasdada.com.pl");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateIfSiteExists_ExistingPage()
        {
            //Act
            var result = _emailService.ValidateIfSiteExists("http://google.pl");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateIfSiteExists_InvalidUrl()
        {
            //Act
            var result = _emailService.ValidateIfSiteExists("abc");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async void GetCodeFromUrl_ReturnsValidString()
        {
            //Act
            var result = await _emailService.GetCodeFromUrl(UniversityUrl);
            
            //Assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public void PrepareEmailList_ReturnsUniqueEmailsList()
        {
            //Act
            var result = _emailService.PrepareEmailList("https://www.nestbank.pl/kontakt.html");

            //Assert
            Assert.True(result.Count == 4);
        }

        [Fact]
        public void PrepareLocalEmailList_EmptyText_ReturnsEmptyList()
        {
            //Act
            var result = _emailService.PrepareLocalEmailList(string.Empty);

            //Assert
            Assert.True(result.Count.Equals(0));
        }

        [Fact]
        public void PrepareLocalEmailList_ReturnsValidEmailList()
        {
            //Act
            var result = _emailService.PrepareLocalEmailList(PrepareEmailList());

            //Assert
            Assert.True(result.Count == 6);
        }

        private string PrepareEmailList()
        {
            var list = new List<string>
            {
                "wkrebs@att.net",
                "dmbkiwi@yahoo.com",
                "kawasaki@icloud.com",
                "geekgrl@outlook.com",
                "plover@yahoo.com",
                MyEmailAddress
            };

            return string.Join(",", list.ToArray());
        }
    }
}