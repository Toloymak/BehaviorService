using System;
using System.Net;
using Dao.Entities;
using Moq;
using NUnit.Framework;
using Proxies;
using Proxies.Configs;
using Proxies.Models;
using RestSharp;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UnitTests.Tests.Proxy
{
    public abstract class ProxyTestsBase : TestBase
    {
        private const string Fio = "Иванов Иван Иванович";
        private const int Age = 20;
        private const string Url = "http://test/";

        private readonly VerdictType verdictType = VerdictType.Good;
        private readonly string appName; 

        private IBehaviorProxy santaAppProxy;
        private VerdictDto verdictDto;
        private HttpStatusCode statusCode;
        private Exception exception;
        private string errorMessage;

        private Mock<IRestClient> mockRestClient;

        protected ProxyTestsBase(string appName)
        {
            verdictDto = new VerdictDto()
            {
                Fio = Fio,
                Age = Age,
                Verdict = verdictType
            };
            
            this.appName = appName;
            errorMessage = null;
        }
        
        [SetUp]
        public void Setup()
        {
            statusCode = HttpStatusCode.OK;
            exception = null;
            errorMessage = null;
            
            mockRestClient = new Mock<IRestClient>();
        }

        [TestCase(Fio, Age)]
        [TestCase("Петров Петр", 50)]
        [TestCase("john Wight", Age)]
        [TestCase(Fio, 0)]
        public void GetVerdict_Success(string requestName, int requestAge)
        {
            CreateSantaProxy();

            var result = santaAppProxy.GetVerdict(requestName, requestAge);
            
            Assert.AreEqual(Age, result.VerdictDto.Age);
            Assert.AreEqual(Fio, result.VerdictDto.Fio);
            Assert.AreEqual(verdictType, result.VerdictDto.Verdict);
        }
        
        [TestCase(HttpStatusCode.Forbidden)]
        [TestCase(HttpStatusCode.NotFound)]
        [TestCase(HttpStatusCode.InternalServerError)]
        public void GetVerdict_NotOkStatusCode_Error(HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;
            this.errorMessage = "Ошибка";
            
            CreateSantaProxy();
            
            var result = santaAppProxy.GetVerdict(Fio, Age);
            
            Assert.AreEqual(result.StatusCode, statusCode);
            Assert.AreEqual(result.ErrorMessage, errorMessage);
        }
        
        [Test]
        public void GetVerdict_RequestError_Exception()
        {
            this.exception = new Exception();
            CreateSantaProxy();

            Assert.Throws<Exception>(() => santaAppProxy.GetVerdict(Fio, Age));
        }

        private void CreateSantaProxy()
        {
            mockRestClient
                .Setup(c => c.Execute(It.IsAny<IRestRequest>()))
                .Returns(new RestResponse()
                {
                    Content = errorMessage == null 
                        ? JsonSerializer.Serialize(verdictDto)
                        : errorMessage,
                    StatusCode = statusCode,
                    ErrorException = exception
                });
            
            var settingCollection = new ProxySettingCollection()
            {
                Proxies = new ProxySettings[]
                {
                    new ProxySettings()
                    {
                        Name = appName,
                        Url = Url
                    }
                }
            };

            santaAppProxy = CreateProxy(mockRestClient.Object, settingCollection);
        }

        protected abstract IBehaviorProxy CreateProxy(IRestClient restClient, ProxySettingCollection settingCollection);
    }
}
