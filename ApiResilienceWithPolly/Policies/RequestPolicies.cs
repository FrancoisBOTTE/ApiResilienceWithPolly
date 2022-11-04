using Polly;
using Polly.Retry;

namespace ApiResilienceWithPolly.Policies
{
    public class RequestPolicies
    {
        
        public AsyncRetryPolicy<HttpResponseMessage> HttpRetryPolicy { get; }
        
        public RequestPolicies()
        {
            int retryCount = 5;
            
            HttpRetryPolicy = Policy.HandleResult<HttpResponseMessage>(result=> !result.IsSuccessStatusCode).RetryAsync(retryCount);
        }
    }
}
