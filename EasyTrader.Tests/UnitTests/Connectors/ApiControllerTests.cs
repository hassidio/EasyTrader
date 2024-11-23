using EasyTrader.Core;
using EasyTrader.Core.Common;
using EasyTrader.Core.Common.Tasks;
using EasyTrader.Core.Connectors;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Models.Exceptions;
using EasyTrader.Tests.Models;
using EasyTrader.Tests.Models.Helpers;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace EasyTrader.Tests.UnitTests.Connectors
{
    [TestClass]
    public class ApiControllerTests : UnitTestBase
    {
        public ErrorEventArgs ErrorResultEventArgs { get; set; }
        public string TimeStamp { get { return $"{DateTime.Now.ToString("mm:ss.fff")} Thread[{Thread.CurrentThread.ManagedThreadId}]"; } }

        private void ApiControllerTest_Error(object sender, ErrorEventArgs e)
        {
            Debug.WriteLine(e.GetException().Message);
            ErrorResultEventArgs = e;
        }

        #region ApiController

        [TestMethod]
        public void ApiController_Test()
        {
            Log.Debug("Start ApiController_Test");

            // Setup
            var apiController = new ApiController<MockApi>(new Throttle());

            // Test
            Assert.IsTrue(apiController.Api.GetType() == typeof(MockApi));
            Assert.IsTrue(apiController.Throttle is not null);
        }

        [TestMethod]
        public void Send_ApiRequestor_Test()
        {
            Log.Debug("Start Send_ApiRequestor_Test");

            // Setup
            var apiHttpRequest = NewMockApiHttpRequest(new MockEntity());

            var apiController = new ApiController<MockApi>(new Throttle());
            apiController.Api.ApiHttpResponse.Response = JsonConvert.SerializeObject(new MockObject());

            // Test
            Task<ApiHttpResponse> task1 = apiController.Api.SendAsync(apiHttpRequest);
            Task<ApiHttpResponse> task2 = apiController.Api.OnSendAsync(apiHttpRequest);

            Assert.IsTrue(task1.Result is not null);
            Assert.IsTrue(task2.Result is not null);

            var mockObj1 = JsonConvert.DeserializeObject<MockObject>(task1.Result.Response.ToString());
            var mockObj2 = JsonConvert.DeserializeObject<MockObject>(task2.Result.Response.ToString());

            Assert.IsTrue(mockObj1.GetType() == typeof(MockObject));
            Assert.IsTrue(mockObj2.GetType() == typeof(MockObject));
        }

        [TestMethod]
        public void Send_MockEntityRequest_Test()
        {
            Log.Debug("Start Send_MockEntityRequest_Test");

            // Setup
            var apiHttpRequest = NewMockApiHttpRequest(new MockEntity());
            var apiController = new ApiController<MockApi>(new Throttle());
            apiController.Api.ApiHttpResponse.Response = JsonConvert.SerializeObject(new MockObject());

            // Test
            MockObject result = apiController.Send<MockObject>(apiHttpRequest);

            Assert.IsTrue(result is not null);
            Assert.IsTrue(apiController.ApiHttpResponse.Response is not null);

            Assert.IsTrue(result.GetType() == typeof(MockObject));
            Assert.IsTrue(
                JsonConvert.DeserializeObject<MockObject>(apiController.ApiHttpResponse.Response.ToString()).GetType()
                == typeof(MockObject));

        }

        [TestMethod]
        public void Send_RestApi_Success_Test()
        {
            Log.Debug("Start Send_RestApi_Test");

            // Setup
            var apiController = new ApiController<RestApi>(new Throttle());
            apiController.Error += ApiControllerTest_Error;

            var mockEntity = new MockEntity();
            mockEntity.IsMockApiRequest = false;

            // Test
            string response = apiController.Send<string>(
                new ApiHttpRequest(
                    mockEntity
                    , @"http://google.com")); // return HTML string

            Assert.IsTrue(response != null);
            Assert.IsTrue(response == apiController.ApiHttpResponse.Response.ToString());

        }

        [TestMethod]
        public void Send_RestApi_Fail_Test()
        {
            Log.Debug("Start Send_RestApi_Test");

            // Setup
            var apiController = new ApiController<RestApi>(new Throttle());
            var mockEntity = new MockEntity();
            //mockEntity.IsMockApiRequest = false;
            var code = 0;
            apiController.Error += ApiControllerTest_Error;
            ErrorResultEventArgs = null;
            var clientException = CommonException.UnsuccessfulResponse(0);


            // Test
            string response = apiController.Send<string>(
                new ApiHttpRequest(
                    mockEntity, @"http://google.com")); // return HTML string

            Exception exception = ErrorResultEventArgs.GetException();

            Assert.IsTrue(response == null);
            Assert.IsTrue(exception.GetType() == typeof(AggregateException));
            Assert.IsTrue(exception.InnerException.GetType() == typeof(ClientException));
            Assert.IsTrue(exception.InnerException.Message == clientException.Message);

            Assert.IsTrue(code == 0);
        }

        [TestMethod]
        public void Api_Throttle_CallsPerSecond_Sync_Test()
        {
            Log.Debug("Start Api_Throttle_CallsPerSecond_Sync_Test");

            // Setup
            int callsPerSecondPolicy = 2;
            int? weightCooldownSecondsPolicy = null;
            int? maximumTotalWeight = null;
            int callEveryMilliseconds = 1000 / callsPerSecondPolicy;
            DateTime lastCall;

            var mockEntityRequestor = new MockEntity { /*EntityName = "Mock Requestor"*/ };
            var mockEntityResponse = new MockClientObject { /*EntityName = "Mock Client Response"*/ };

            var throttle = new Throttle(NewtApiConfiguration(
                callsPerSecondPolicy, weightCooldownSecondsPolicy, maximumTotalWeight));

            var apiHttpRequest = NewMockApiHttpRequest(mockEntityRequestor);
            var apiController = new ApiController<MockApi>(throttle);
            apiController.Api.ApiHttpResponse.Response = JsonConvert.SerializeObject(mockEntityResponse);

            Assert.IsTrue(throttle.CallEveryMilliseconds == callEveryMilliseconds);

            // Test round 1
            lastCall = throttle.LastCall;
            var mockEntity1 = apiController.Send<MockEntity>(apiHttpRequest);
            Assert.IsTrue(lastCall < throttle.LastCall);
            Assert.IsTrue(lastCall.AddMilliseconds(callEveryMilliseconds) < throttle.LastCall); // sleep for callEveryMilliseconds

            // Test round 2
            lastCall = throttle.LastCall;
            var mockEntity2 = apiController.Send<MockEntity>(apiHttpRequest);
            Assert.IsTrue(lastCall.AddMilliseconds(callEveryMilliseconds) < throttle.LastCall); // sleep for callEveryMilliseconds
        }

        [TestMethod]
        public void Api_Throttle_CallsPerSecond_Async_Test()
        {
            Log.Debug("Start Api_Throttle_CallsPerSecond_Async_Test");

            // Setup
            int callsPerSecondPolicy = 2;
            int? weightCooldownSecondsPolicy = null;
            int? maximumTotalWeight = null;
            int callEveryMilliseconds = 1000 / callsPerSecondPolicy;
            var numberOfTasks = 5;
            DateTime lastCallStart;

            var mockEntityRequestor = new MockEntity { /*EntityName = "Mock Requestor"*/ };
            var mockEntityResponse = new MockClientObject { /*EntityName = "Mock Client Response"*/ };

            var throttle = new Throttle(NewtApiConfiguration(
                callsPerSecondPolicy, weightCooldownSecondsPolicy, maximumTotalWeight));

            var apiHttpRequest = NewMockApiHttpRequest(mockEntityRequestor);
            var apiController = new ApiController<MockApi>(throttle);
            apiController.Api.ApiHttpResponse.Response = JsonConvert.SerializeObject(mockEntityResponse);
            var mockApi = (MockApi)apiController.Api;
            CommonTaskScheduler taskScheduler = new CommonTaskScheduler();
            Task[] tasks = new Task[numberOfTasks];

            Debug.WriteLine($"{TimeStamp}: Main call every milliseconds: {callEveryMilliseconds}");
            Assert.IsTrue(throttle.CallEveryMilliseconds == callEveryMilliseconds);

            // Test 
            lastCallStart = throttle.LastCall;

            Debug.WriteLine($"{TimeStamp}: Main Last call: {lastCallStart.ToString("mm:ss.fff")}");
            Debug.WriteLine($"");

            for (int i = 0; i < numberOfTasks; i++)
            {
                Debug.WriteLine($"{TimeStamp}: Main Task [{i}] queue and start");
                tasks[i] = new Task(() => apiController.Send<MockEntity>(apiHttpRequest));

                taskScheduler.QueueTask(tasks[i]);
            }

            Debug.WriteLine($"{TimeStamp}: Main Scheduler all tasks sent.      Throttle last call:  {throttle.LastCall.ToString("mm:ss.fff")}");
            Assert.IsTrue(lastCallStart == throttle.LastCall);

            taskScheduler.WaitAll();

            Debug.WriteLine($"{TimeStamp}: Main Scheduler tasks completed!      Throttle last call:  {throttle.LastCall.ToString("mm:ss.fff")}");
            Assert.IsTrue(mockApi.LastSent >= throttle.LastCall);
            Assert.IsTrue(mockApi.SendCount == numberOfTasks);

            Debug.WriteLine($"");

            for (int i = 0; i < numberOfTasks; i++)
            {
                Debug.WriteLine($"{TimeStamp}: Main Task [{i}] complete!");
                Assert.IsTrue(tasks[i].IsCompletedSuccessfully);
            }

            Assert.IsTrue(lastCallStart.AddMilliseconds(numberOfTasks * callEveryMilliseconds) < throttle.LastCall);
        }

        [TestMethod]
        public void Api_Throttle_Weight_MaximumMessageWeightSoFar_Test()
        {
            Log.Debug("Start Api_Throttle_Weight_MaximumMessageWeightSoFar_Test");

            // Setup
            int callsPerSecondPolicy = 20;
            int? weightCooldownSecondsPolicy = 3;
            int? maximumTotalWeight = 100;
            int callEveryMilliseconds = 1000 / callsPerSecondPolicy;
            int initialWeight = 0;
            int numberOfTasks = 5;
            string responseHeaderWeightKeyName = "weight";
            int responseMessageHeaderWeight = 16;

            var mockEntityRequestor = new MockEntity { /*EntityName = "Mock Requestor"*/ };
            var mockEntityResponse = new MockClientObject { /*EntityName = "Mock Client Response"*/ };
            var throttle = new Throttle(NewtApiConfiguration(
                callsPerSecondPolicy, weightCooldownSecondsPolicy, maximumTotalWeight));
            var apiController = new ApiController<MockApi>(throttle);
            apiController.Api.ApiHttpResponse.Response = JsonConvert.SerializeObject(mockEntityResponse);
            apiController.Api.ApiHttpResponse.HttpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            apiController.Api.ApiHttpResponse.HttpResponse.Headers.Add(responseHeaderWeightKeyName, [$"{responseMessageHeaderWeight}"]);
            var apiHttpRequest = NewMockApiHttpRequest(mockEntityRequestor);
            apiHttpRequest.ResponseHeaderWeightKeyName = responseHeaderWeightKeyName;
            var mockApi = (MockApi)apiController.Api;
            CommonTaskScheduler taskScheduler = new CommonTaskScheduler();
            Task[] tasks = new Task[numberOfTasks];

            // Test setup
            Assert.IsTrue(apiController.Throttle.CallEveryMilliseconds == callEveryMilliseconds);
            Assert.IsTrue(apiController.Throttle.CurrentWeightUsed == 0);
            Assert.IsTrue(apiController.Throttle.MaximumMessageWeightSoFar == 0);
            Assert.IsTrue(apiController.Throttle.MaximumTotalWeight == maximumTotalWeight);
            Assert.IsTrue(apiController.Throttle.WeightCooldownSecondsPolicy == weightCooldownSecondsPolicy);
            Assert.IsTrue(
                apiController.Api.ApiHttpResponse.Headers[responseHeaderWeightKeyName].First().ToString()
                == responseMessageHeaderWeight.ToString());
            Assert.IsTrue(mockApi.ApiHttpResponse.ResponseCurrentWeightUsed == 0);

            // Test AddWeight initial
            throttle.AddWeight(initialWeight);
            Assert.IsTrue(apiController.Throttle.MaximumMessageWeightSoFar == initialWeight);
            Assert.IsTrue(apiController.Throttle.CurrentWeightUsed == initialWeight);

            Debug.WriteLine($"{TimeStamp}: Main start");
            Debug.WriteLine($"{TimeStamp}: Main maximum total weight: {apiController.Throttle.MaximumTotalWeight}");
            Debug.WriteLine($"{TimeStamp}: Main weight cooldown seconds policy: {apiController.Throttle.WeightCooldownSecondsPolicy}");
            Debug.WriteLine($"{TimeStamp}: Main maximum message weight so far: {apiController.Throttle.MaximumMessageWeightSoFar}");
            Debug.WriteLine($"{TimeStamp}: Main current weight used: {apiController.Throttle.CurrentWeightUsed}");
            Debug.WriteLine($"{TimeStamp}: Main api response weight: {mockApi.ApiHttpResponse.ResponseCurrentWeightUsed}");
            Debug.WriteLine($"");

            for (int i = 0; i < numberOfTasks; i++)
            {
                Debug.WriteLine($"{TimeStamp}: Main Task [{i}] queue and start");
                tasks[i] = new Task(() => apiController.Send<MockEntity>(apiHttpRequest));

                taskScheduler.QueueTask(tasks[i]);
            }

            Debug.WriteLine($"{TimeStamp}: Main Scheduler all tasks sent.");
            Debug.WriteLine($"");
            Debug.WriteLine($"{TimeStamp}: Main maximum message weight so far: {apiController.Throttle.MaximumMessageWeightSoFar}");
            Debug.WriteLine($"{TimeStamp}: Main current weight used: {apiController.Throttle.CurrentWeightUsed}");
            Debug.WriteLine($"{TimeStamp}: Main api response weight: {mockApi.ApiHttpResponse.ResponseCurrentWeightUsed}");

            Assert.IsTrue(apiController.Throttle.MaximumMessageWeightSoFar >= initialWeight);
            Assert.IsTrue(apiController.Throttle.CurrentWeightUsed >= initialWeight);
            Assert.IsTrue(mockApi.ApiHttpResponse.ResponseCurrentWeightUsed >= 0);

            taskScheduler.WaitAll();

            Debug.WriteLine($"{TimeStamp}: Main Scheduler tasks completed!");
            Debug.WriteLine($"{TimeStamp}: Main maximum message weight so far: {throttle.MaximumMessageWeightSoFar}");
            Debug.WriteLine($"{TimeStamp}: Main current weight used: {throttle.CurrentWeightUsed}");
            Debug.WriteLine($"{TimeStamp}: Main api response weight: {mockApi.ApiHttpResponse.ResponseCurrentWeightUsed}");

            Assert.IsTrue(apiController.Throttle.MaximumMessageWeightSoFar == responseMessageHeaderWeight);
            Assert.IsTrue(apiController.Throttle.CurrentWeightUsed ==
                initialWeight + (numberOfTasks * responseMessageHeaderWeight));
            Assert.IsTrue(mockApi.ApiHttpResponse.ResponseCurrentWeightUsed == apiController.Throttle.CurrentWeightUsed);

            Debug.WriteLine($"");

            for (int i = 0; i < numberOfTasks; i++)
            {
                Debug.WriteLine($"{TimeStamp}: Main Task [{i}] complete!");
                Assert.IsTrue(tasks[i].IsCompletedSuccessfully);
            }
        }

        [TestMethod]
        public void Api_Throttle_Weight_MaximumTotal_Test()
        {
            Log.Debug("Start Api_Throttle_Weight_MaximumTotal_Test");

            // Setup
            int callsPerSecondPolicy = 100;
            int? weightCooldownSecondsPolicy = 3;
            int? maximumTotalWeight = 100;

            int callEveryMilliseconds = 1000 / callsPerSecondPolicy;
            int initialWeight = 0;
            int numberOfTasks = 5;
            string responseHeaderWeightKeyName = "weight";
            int responseMessageHeaderWeight = ((int)maximumTotalWeight - initialWeight) / numberOfTasks;

            var mockEntityRequestor = new MockEntity { /*EntityName = "Mock Requestor"*/ };
            var mockEntityResponse = new MockClientObject { /*EntityName = "Mock Client Response"*/ };
            var throttle = new Throttle(NewtApiConfiguration(
                callsPerSecondPolicy, weightCooldownSecondsPolicy, maximumTotalWeight));
            var apiController = new ApiController<MockApi>(throttle);
            apiController.Api.ApiHttpResponse.Response = JsonConvert.SerializeObject(mockEntityResponse);
            apiController.Api.ApiHttpResponse.HttpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            apiController.Api.ApiHttpResponse.HttpResponse.Headers.Add(responseHeaderWeightKeyName, [$"{responseMessageHeaderWeight}"]);
            var apiHttpRequest = NewMockApiHttpRequest(mockEntityRequestor);
            apiHttpRequest.ResponseHeaderWeightKeyName = responseHeaderWeightKeyName;
            var mockApi = (MockApi)apiController.Api;
            mockApi.MaximumTotalWeight = (int)maximumTotalWeight;
            CommonTaskScheduler taskScheduler = new CommonTaskScheduler();
            Task[] tasks = new Task[numberOfTasks];

            // Test setup
            Assert.IsTrue(apiController.Throttle.CallEveryMilliseconds == callEveryMilliseconds);
            Assert.IsTrue(apiController.Throttle.CurrentWeightUsed == 0);
            Assert.IsTrue(apiController.Throttle.MaximumMessageWeightSoFar == 0);
            Assert.IsTrue(apiController.Throttle.MaximumTotalWeight == maximumTotalWeight);
            Assert.IsTrue(apiController.Throttle.WeightCooldownSecondsPolicy == weightCooldownSecondsPolicy);
            Assert.IsTrue(
                apiController.Api.ApiHttpResponse.Headers[responseHeaderWeightKeyName].First().ToString()
                == responseMessageHeaderWeight.ToString());
            Assert.IsTrue(mockApi.ApiHttpResponse.ResponseCurrentWeightUsed == 0);


            // Test AddWeight
            throttle.AddWeight(initialWeight);
            Assert.IsTrue(apiController.Throttle.MaximumMessageWeightSoFar == initialWeight);
            Assert.IsTrue(apiController.Throttle.CurrentWeightUsed == initialWeight);

            Debug.WriteLine($"{TimeStamp}: Main start");
            Debug.WriteLine($"{TimeStamp}: Main message weight: {responseMessageHeaderWeight}");
            Debug.WriteLine($"{TimeStamp}: Main maximum total weight: {apiController.Throttle.MaximumTotalWeight}");
            Debug.WriteLine($"{TimeStamp}: Main weight cooldown seconds policy: {apiController.Throttle.WeightCooldownSecondsPolicy}");
            Debug.WriteLine($"{TimeStamp}: Main maximum message weight so far: {apiController.Throttle.MaximumMessageWeightSoFar}");
            Debug.WriteLine($"{TimeStamp}: Main current weight used: {apiController.Throttle.CurrentWeightUsed}");
            Debug.WriteLine($"{TimeStamp}: Main api response weight: {mockApi.ApiHttpResponse.ResponseCurrentWeightUsed}");
            
            Debug.WriteLine($"");

            // Send tasks
            for (int i = 0; i < numberOfTasks; i++)
            {
                Debug.WriteLine($"{TimeStamp}: Main Task [{i}] queue and start");
                tasks[i] = new Task(() => apiController.Send<MockEntity>(apiHttpRequest));

                taskScheduler.QueueTask(tasks[i]);
            }

            Debug.WriteLine($"{TimeStamp}: Main Scheduler all tasks sent.");
            Debug.WriteLine($"{TimeStamp}: Main current weight used: {apiController.Throttle.CurrentWeightUsed}");
            Debug.WriteLine($"{TimeStamp}: Main api response weight: {mockApi.ApiHttpResponse.ResponseCurrentWeightUsed}");

            Assert.IsTrue(apiController.Throttle.CurrentWeightUsed >= initialWeight);
            Assert.IsTrue(mockApi.ApiHttpResponse.ResponseCurrentWeightUsed >= 0);

            taskScheduler.WaitAll();

            Debug.WriteLine($"{TimeStamp}: Main Scheduler tasks completed!");

            for (int i = 0; i < numberOfTasks; i++)
            {
                Debug.WriteLine($"{TimeStamp}: Main Task [{i}] complete!");
                Assert.IsTrue(tasks[i].IsCompletedSuccessfully);
            }

            Debug.WriteLine($"");

            Debug.WriteLine($"{TimeStamp}: Main api response weight: {mockApi.ApiHttpResponse.ResponseCurrentWeightUsed}");
            Debug.WriteLine($"{TimeStamp}: Main current weight used (Before add): {apiController.Throttle.CurrentWeightUsed}");
            Debug.WriteLine($"{TimeStamp}: Main maximum total weight not reached: {apiController.Throttle.MaximumTotalWeight}");

            Assert.IsTrue(mockApi.ApiHttpResponse.ResponseCurrentWeightUsed == responseMessageHeaderWeight * numberOfTasks);
            Assert.IsTrue(apiController.Throttle.CurrentWeightUsed ==
                initialWeight + (numberOfTasks * responseMessageHeaderWeight));

            Assert.IsTrue(apiController.Throttle.MaximumTotalWeight >= apiController.Throttle.CurrentWeightUsed);

            Assert.IsTrue(apiController.Throttle.MaximumTotalWeight
                < apiController.Throttle.CurrentWeightUsed + responseMessageHeaderWeight);

            // Setup overflow weight use
            apiController.Throttle.AddWeight(
                (int)apiController.Throttle.MaximumTotalWeight - apiController.Throttle.CurrentWeightUsed);

            Assert.IsTrue(apiController.Throttle.MaximumTotalWeight == apiController.Throttle.CurrentWeightUsed);
            Debug.WriteLine($"{TimeStamp}: Main current weight used (after add): {apiController.Throttle.CurrentWeightUsed}");

            var lastCallafterCooldown = apiController.Throttle.LastCall.AddSeconds((int)weightCooldownSecondsPolicy);

            Debug.WriteLine($"{TimeStamp}: Main Last call: {apiController.Throttle.LastCall.ToString("mm:ss.fff")}");
            Debug.WriteLine($"{TimeStamp}: Main Result call after cool down expected: {lastCallafterCooldown.ToString("mm:ss.fff")}");
            Debug.WriteLine($"");

            // Add overflow task
            var additionalTask = new Task(() => apiController.Send<MockEntity>(apiHttpRequest));
            taskScheduler.QueueTask(additionalTask);
            taskScheduler.WaitAll();
            Debug.WriteLine($"{TimeStamp}: Main Scheduler additional task completed!");


            Debug.WriteLine($"{TimeStamp}: Main current weight used: {apiController.Throttle.CurrentWeightUsed}");
            Debug.WriteLine($"{TimeStamp}: Main api response weight: {mockApi.ApiHttpResponse.ResponseCurrentWeightUsed}");
            Debug.WriteLine($"{TimeStamp}: Main Last call: {apiController.Throttle.LastCall.ToString("mm:ss.fff")}");

            
            Assert.IsTrue(apiController.Throttle.CurrentWeightUsed == mockApi.ApiHttpResponse.ResponseCurrentWeightUsed);
            Assert.IsTrue(apiController.Throttle.CurrentWeightUsed == responseMessageHeaderWeight);
            Assert.IsTrue(apiController.Throttle.LastCall >= lastCallafterCooldown);
        }

        #endregion


        #region ApiController Exceptions
        [TestMethod]
        public void ApiException_Test()
        {
            var code = 400;
            // Setup
            var apiHttpRequest = NewMockApiHttpRequest(new MockEntity());
            var apiController = new ApiController<MockApi>(new Throttle());
            apiController.Api.ApiHttpResponse.Response = JsonConvert.SerializeObject(new MockObject());
            ((MockApi)apiController.Api).ExceptionCode = code;
            apiController.Error += ApiControllerTest_Error;

            // Test
            MockObject result = apiController.Send<MockObject>(apiHttpRequest);

            Exception exception = ErrorResultEventArgs.GetException();
            Assert.IsTrue(exception is not null);

            var innerEx = exception.InnerException;
            Assert.IsTrue(innerEx is not null);
            Assert.IsTrue(innerEx.GetType() == typeof(ClientException));
            Assert.IsTrue(((ClientException)innerEx).Code == code);

        }
        #endregion

    }
}