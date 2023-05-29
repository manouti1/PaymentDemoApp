namespace PaymentAPI.Application.Exceptions
{
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger;

        public ExceptionHandlingBehavior(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Null argument error occurred while processing {Request}", typeof(TRequest).Name);
                throw;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid argument error occurred while processing {Request}", typeof(TRequest).Name);
                throw;
            }
            catch (Exception ex)
            {
                // This will catch any exception that we're not explicitly handling
                _logger.LogError(ex, "An unexpected error occurred while processing {Request}", typeof(TRequest).Name);
                throw;
            }
        }
    }
}
