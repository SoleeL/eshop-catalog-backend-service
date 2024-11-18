using Catalog.Application.DTOs.Bases;

namespace Catalog.API.Filters;

public class RequestIdFilter: IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        BaseResponseDto<string> baseResponseDto = new BaseResponseDto<string>();
        baseResponseDto.Succcess = false;

        // README: El metodo que controla el proceso de respuesta a la request tiene a "X-RequestId" como parametro,
        // siempre se espera que este sea una dato obligatorio y el servicio respondera con una excepcion 500, la cual
        // sera manejada por el manejador global.
        //
        // README: Esto es una desventaja de las minimal api de .net.
        //
        // 400 Bad Request
        // {
        //     "success": false,
        //     "data": {
        //         "title": "Required parameter \"string requestId\" was not provided from header.",
        //         "status": 500
        //     },
        //     "Message": "Server error"
        // }
        
        // README: Para evitar que el servicio retorne data con un error 500 se debe establecer que el parametro
        // "requestId" sea opcional, para gestionar la respuesta de que "X-RequestId" no se paso en la cabecera con un
        // mensaje mas simple y entendible.
        // [FromHeader(Name = "X-RequestId")] string? requestId
        
        bool requestContainRequestId = context.HttpContext.Request.Headers.ContainsKey("X-RequestId");
        
        if (!requestContainRequestId)
        {
            baseResponseDto.Message = "The X-RequestId is not provided.";
            return TypedResults.BadRequest(baseResponseDto);
        }
        
        // README: Se infiere que "X-RequestId" viene por defecto
        string? requestId = context.HttpContext.Request.Headers["X-RequestId"];
        
        if (string.IsNullOrEmpty(requestId))
        {
            baseResponseDto.Message = "The X-RequestId is empty.";
            return TypedResults.BadRequest(baseResponseDto);
        }
        
        bool isResquestIdParsed = Guid.TryParse(requestId, out Guid requestGuid);

        if (!isResquestIdParsed)
        {
            baseResponseDto.Message = "The X-RequestId is not a valid Guid.";
            return TypedResults.BadRequest(baseResponseDto);
        }
        
        if (requestGuid == Guid.Empty)
        {
            baseResponseDto.Message = "The X-RequestId is a empty Guid.";
            return TypedResults.BadRequest(baseResponseDto);
        }

        var results = await next(context);
        return results;
    }
}