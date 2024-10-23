using ConsoleApplication.Application.Services;
using ConsoleApplication.Domain.Interfaces.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace ConsoleApplication.Application.Tests.Services.LocationService
{
    [TestClass]

    public class LocationInfoService_GetAsStringAsync
    {
        private LocationInfoService sut;
        private readonly Mock<ILocationInfoRepository> repository;

        public LocationInfoService_GetAsStringAsync()
        {
            var configuration = new CustomConfiguration
            {
                LocationInfoUrl = "https://myApi.com",
                WeatherInfoUrl = "https://myApi.com",
                Latitude = "1",
                Longitude = "2"
            };
            repository = new Mock<ILocationInfoRepository>();

            sut = new LocationInfoService(
                configuration,
                repository.Object
                );
        }

        [TestMethod]
        public async Task Configuration_IsNull_Return_Null()
        {
            sut = new LocationInfoService(null, repository.Object);

            var result = await sut.GetAsStringAsync();
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task LocationInfoUrl_IsNull_Return_Null()
        {
            sut = new LocationInfoService(new CustomConfiguration(), repository.Object);

            var result = await sut.GetAsStringAsync();
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task LocationInfoUrl_IsNotWellFormatted_Return_Null()
        {
            sut = new LocationInfoService(new CustomConfiguration { LocationInfoUrl = "NotWellFormatted"}, repository.Object);

            var result = await sut.GetAsStringAsync();
            Assert.IsNull(result);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        public async Task Body_IsNull_Return_Null(string body)
        {
            repository.Setup(_ => 
            _.GetAsync(
                It.IsAny<string>())
            ).ReturnsAsync(body);

            var result = await sut.GetAsStringAsync();
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Body_IsNot_Valid_Json_Return_Null()
        {
            repository.Setup(_ =>
            _.GetAsync(
                It.IsAny<string>())
            ).ReturnsAsync("notValidJson");

            var result = await sut.GetAsStringAsync();
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Key_NotFound_In_Json_String_Return_Null()
        {
            var jsonMock = @"{
                ""name"": ""John"",
                ""age"": 30,
                ""isStudent"": false,
                ""courses"": [""Math"", ""Science""]
            }";

            repository.Setup(_ =>
            _.GetAsync(
                It.IsAny<string>())
            ).ReturnsAsync(jsonMock);

            var result = await sut.GetAsStringAsync();
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Ok()
        {

            var jsonMock = @"
            {
                ""address"": 
                    {
                        ""Town"": ""Town"",
                        ""County"": ""County"",
                        ""State"": ""State"",
                        ""Country"": ""Country"",
                        ""PostCode"": ""PostCode""
                    }
            }";

            repository.Setup(_ =>
                _.GetAsync(
                    It.IsAny<string>())
                ).ReturnsAsync(jsonMock);

            var result = await sut.GetAsStringAsync();
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "City: Town Province: County State: Country CAP: PostCode");
        }
    }
}