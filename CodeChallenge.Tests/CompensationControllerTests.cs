using System;
using System.Net;
using System.Net.Http;
using System.Text;
using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Frameworks;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            var compensation = new Compensation()
            {
                Employee = new Employee
                {
                    EmployeeId = Guid.NewGuid().ToString(),
                    Department = "Complaints",
                    FirstName = "Debbie",
                    LastName = "Downer",
                    Position = "Receiver",
                },
                Salary = 500000
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            var postRequestTask = _httpClient.PostAsync("api/compensation", new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.Employee);
            Assert.AreEqual(compensation.Employee.EmployeeId, newCompensation.Employee.EmployeeId);
            Assert.AreEqual(compensation.Employee.FirstName, newCompensation.Employee.FirstName);
            Assert.AreEqual(compensation.Employee.LastName, newCompensation.Employee.LastName);
            Assert.AreEqual(compensation.Employee.Department, newCompensation.Employee.Department);
            Assert.AreEqual(compensation.Employee.Position, newCompensation.Employee.Position);
            Assert.IsTrue(newCompensation.EffectiveDate > DateTime.MinValue);
        }
        
        [TestMethod]
        public void CreateCompensation_Returns_BadRequest()
        {
            var compensation = new Compensation()
            {
                Employee = null,
                Salary = 500000
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            var postRequestTask = _httpClient.PostAsync("api/compensation", new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [TestMethod]
        public void GetCompensationById_Returns_NotFound()
        {
            var id = "77788844-edd3-4847-99fe-c4518e82c86f";
            var postRequestTask = _httpClient.GetAsync($"api/compensation/employee/{id}");
            var response = postRequestTask.Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        [TestMethod]
        public void GetCompensationById_Returns_Ok()
        {
            var id = "470d53af-5520-414e-ab51-c546fc680ca9";
            var postRequestTask = _httpClient.GetAsync($"api/compensation/employee/{id}");
            var response = postRequestTask.Result;

            var expectedFirstName = "Joshua";
            var expectedLastName = "Kyte";

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            
            var compensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(compensation);
            Assert.IsNotNull(compensation.Employee);
            Assert.AreEqual(compensation.Employee.FirstName, expectedFirstName);
            Assert.AreEqual(compensation.Employee.LastName, expectedLastName);
            Assert.AreEqual(compensation.Employee.EmployeeId, id);
        }
    }
}