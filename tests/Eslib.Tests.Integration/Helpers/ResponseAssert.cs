using System.Buffers;
using System.Net;
using Eslib.Models.Internals;
using NUnit.Framework;

namespace Eslib.Tests.Integration.Helpers
{
    public static class ResponseAssert
    {
        public static void IsSuccessful(IResponse response)
        {
            if (response.StatusCode is not (
                HttpStatusCode.OK or
                HttpStatusCode.NotModified or
                HttpStatusCode.NoContent))
            {
                Assert.Fail("Expected a successful HttpStatusCode but did not get one");
            }

            if (!response.Success || response.Error is not null)
                Assert.Fail("Expected a successful response but did not get one");
        }
    }
}