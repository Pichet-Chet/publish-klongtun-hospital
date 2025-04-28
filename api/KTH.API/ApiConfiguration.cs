using KTH.MODELS;
using KTH.MODELS.Constants;
using Ubiety.Dns.Core;

namespace KTH.API
{
    public class ApiConfiguration
    {
        public ApiConfiguration()
        {
        }

        internal static object? GetResponseController(ResponseModel response)
        {
            if (response == null)
            {
                return new
                {
                    Status = ConstantsResponse.StatusError,
                    response.Code,
                    Message = "Response is null",
                };
            }

            if (response.Code == ConstantsResponse.HttpCode200)
            {
                if (response.Message == ConstantsResponse.HttpCode204Message)
                {
                    return new
                    {
                        response.Status,
                        response.Code,
                        response.Message,
                        response.Output,
                    };
                }

                if (response.Rows == null)
                {
                    return new
                    {
                        response.Status,
                        response.Code,
                        response.Message,
                        response.Output,
                    };
                }

                return new
                {

                    response.Status,
                    response.Code,
                    response.Message,
                    response.PageNumber,
                    response.PageSize,
                    response.Rows,
                    response.Output,

                };
            }

            

            // This handles both StatusError and any other status
            return new
            {
                response.Status,
                response.Code,
                response.Message
            };
        }
    }
}

