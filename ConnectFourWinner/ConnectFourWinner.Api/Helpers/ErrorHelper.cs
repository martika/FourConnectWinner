using ConnectFourWinner.Domain.Entities;
using System;

namespace ConnectFourWinner.Api.Helpers
{
    public static class ErrorHelper
    {
        public static ErrorResponse GetErrorResponse(Exception ex)
        {
            return new ErrorResponse
            {
                Message = ex.InnerException != null ? ex.InnerException.Message : string.Empty,
                UserMessage = ex.Message
            };
        }
    }
}
