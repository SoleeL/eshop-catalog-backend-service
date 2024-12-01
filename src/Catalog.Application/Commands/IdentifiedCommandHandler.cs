using Catalog.Application.Commands.Brands;
using Catalog.Application.Commands.Categories;
using Catalog.Application.Commands.Types;
using Catalog.Application.Extensions;
using Catalog.Application.Idempotency;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Commands;

/// <summary>
/// Provides a base implementation for handling duplicate request and ensuring idempotent updates, in the cases where
/// a requestid sent by client is used to detect duplicate requests.
/// </summary>
/// <typeparam name="T">Type of the command handler that performs the operation if request is not duplicated</typeparam>
/// <typeparam name="TR">Return value of the inner command handler</typeparam>
public abstract class IdentifiedCommandHandler<T, TR> : IRequestHandler<IdentifiedCommand<T, TR>, TR>
    where T : IRequest<TR>
{
    private readonly IMediator _mediator;
    private readonly IRequestManager _requestManager;
    private readonly ILogger<IdentifiedCommandHandler<T, TR>> _logger;

    public IdentifiedCommandHandler(
        IMediator mediator,
        IRequestManager requestManager,
        ILogger<IdentifiedCommandHandler<T, TR>> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _mediator = mediator;
        _requestManager = requestManager;
        _logger = logger;
    }

    /// <summary>
    /// Creates the result value to return if a previous request was found
    /// </summary>
    /// <returns></returns>
    protected abstract TR CreateResultForDuplicateRequest();

    /// <summary>
    /// This method handles the command. It just ensures that no other request exists with the same ID, and if this is the case
    /// just enqueues the original inner command.
    /// </summary>
    /// <param name="message">IdentifiedCommand which contains both original command & request ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Return value of inner command or default value if request same ID was found</returns>
    public async Task<TR> Handle(IdentifiedCommand<T, TR> message, CancellationToken cancellationToken)
    {
        // var alreadyExists = await _requestManager.ExistAsync(message.Id);
        // if (alreadyExists) return CreateResultForDuplicateRequest();
        //
        // // TODO: Uso de OrderingDomainException
        // await _requestManager.CreateRequestForCommandAsync<T>(message.Id);
        //
        // try
        // {
        //     var command = message.Command;
        //     var commandName = command.GetGenericTypeName();
        //     string nameProperty;
        //
        //     switch (command)
        //     {
        //         case CreateBrandCommand addBrandCommand:
        //             nameProperty = nameof(addBrandCommand.Name);
        //             break;
        //
        //         case AddCategoryCommand addCategoryCommand:
        //             nameProperty = nameof(addCategoryCommand.Name);
        //             break;
        //
        //         case AddTypeCommand addTypeCommand:
        //             nameProperty = nameof(addTypeCommand.Name);
        //             break;
        //
        //         default:
        //             nameProperty = "n/a";
        //             break;
        //     }
        //
        //     _logger.LogInformation(
        //         "Sending command: {CommandName} - {nameProperty} ({@Command})",
        //         commandName,
        //         nameProperty,
        //         command);
        //
        //     // Send the embedded business command to mediator so it runs its related CommandHandler 
        //     var result = await _mediator.Send(command, cancellationToken);
        //
        //     _logger.LogInformation(
        //         "Command result: {@Result} - {CommandName} - {nameProperty} ({@Command})",
        //         result,
        //         commandName,
        //         nameProperty,
        //         command);
        //
        //     return result;
        // }
        // catch
        // {
        //     return default;
        // }
        
        return default(TR);
    }
}
