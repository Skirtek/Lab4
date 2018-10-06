using ConsoleApp1.Enums;
using ConsoleApp1.Services;
using Xunit;

namespace ConsoleApp1.UnitTests.Services
{
    public class ZipCodeServiceTests
    {
        private readonly ZipCodeService _zipCodeService = new ZipCodeService();
        private const string LocalZipCode = "85-796";
        private const string NonexistenceZipCode = "41-231";

        [Fact]
        public void ValidateZipCodeInput_WrongZipCode()
        {
            //Act
            var result = _zipCodeService.ValidateZipCodeInput("abc");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateZipCodeInput_WrongZipCodeFormat()
        {
            //Act
            var result = _zipCodeService.ValidateZipCodeInput("85 111");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateZipCodeInput_CorrectZipCode()
        {
            //Act
            var result = _zipCodeService.ValidateZipCodeInput("85-132");

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void GetCityForCode_Offline_OneChar()
        {
            //Act
            var result = _zipCodeService.GetCityForCode_Offline("a");

            //Assert
            Assert.True(result.Equals(ListOfCities.Undefined));
        }

        [Fact]
        public void GetCityForCode_Offline_UndefinedCityZipCode()
        {
            //Act
            var result = _zipCodeService.GetCityForCode_Offline(NonexistenceZipCode);

            //Assert
            Assert.True(result.Equals(ListOfCities.Undefined));
        }

        [Fact]
        public void GetCityForCode_Offline_CorrectCityZipCode()
        {
            //Act
            var result = _zipCodeService.GetCityForCode_Offline(LocalZipCode);

            //Assert
            Assert.True(result.Equals(ListOfCities.Bydgoszcz));
        }

        [Fact]
        public void GetCityForCode_JSONHandler_NonexistenceZipCode()
        {
            //Act
            var result = _zipCodeService.GetCityForCode_JSONHandler(NonexistenceZipCode);

            //Assert
            Assert.Same(result,ListOfCities.Undefined.ToString());
        }

        [Fact]
        public void GetCityForCode_JSONHandler_CorrectCityZipCode()
        {
            //Act
            var result = _zipCodeService.GetCityForCode_JSONHandler(LocalZipCode);

            //Assert
            Assert.Contains(result, "Bydgoszcz");
        }
    }
}