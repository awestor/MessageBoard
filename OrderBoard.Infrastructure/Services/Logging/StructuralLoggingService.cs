using OrderBoard.AppServices.Other.Services;
using Serilog.Context;

namespace OrderBoard.Infrastructure.Services.Logging
{
    public class StructuralLoggingService : IStructuralLoggingService
    {
        public IDisposable PushProperty(string name, object value)
        {
            return LogContext.PushProperty(name, value, false);
        }
    }
}
