using Polly;
using Polly.Retry;
using Polly.CircuitBreaker;
using Polly.Fallback;

namespace ApiResilienceWithPolly.Policies
{
    public class RequestPolicies
    {

        public AsyncRetryPolicy<HttpResponseMessage> HttpRetryPolicy { get; }
        public AsyncRetryPolicy<HttpResponseMessage> HttpWaitAndRetryPolicy { get; }
        public AsyncCircuitBreakerPolicy<HttpResponseMessage> HttpCircuitBreakerPolicy { get; }
        public AsyncCircuitBreakerPolicy<HttpResponseMessage> HttpCircuitBreakerWithCheckStatePolicy { get; }
        
        public RequestPolicies()
        {
            int retryCount = 5;

            HttpRetryPolicy = Policy.HandleResult<HttpResponseMessage>(result => !result.IsSuccessStatusCode).RetryAsync(retryCount);

            HttpWaitAndRetryPolicy = Policy.HandleResult<HttpResponseMessage>(result => !result.IsSuccessStatusCode)
                                     .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(2));

            HttpCircuitBreakerPolicy = Policy.HandleResult<HttpResponseMessage>(result => !result.IsSuccessStatusCode)
                                     .CircuitBreakerAsync(1, TimeSpan.FromSeconds(20));

            HttpCircuitBreakerWithCheckStatePolicy = Policy.HandleResult<HttpResponseMessage>(result => !result.IsSuccessStatusCode)
                                     .CircuitBreakerAsync(1, TimeSpan.FromSeconds(20), CircuitToStateOpen, CircuitToStateClosed);
        }

        private void CircuitToStateOpen(DelegateResult<HttpResponseMessage> result, TimeSpan ts)
        {
            Console.WriteLine("Argh, Circuit en panne !");
        }

        private void CircuitToStateClosed()
        {
            Console.WriteLine("Ouf, Circuit revenu à la normale !");
        }
    }
}

