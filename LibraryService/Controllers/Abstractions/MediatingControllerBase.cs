using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.API.Host.Controllers.Abstractions
{
    public abstract class MediatingControllerBase : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MediatingControllerBase" /> class.
        /// </summary>
        protected MediatingControllerBase(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        ///     Creates an <see cref="HttpResponseMessage" /> that contains an HTTP 500 (internal server error) error code.
        /// </summary>
        protected IActionResult InternalServerError()
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        /// <summary>
        ///     Executes the provided query, interprets its result and returns an appropriate <see cref="IActionResult" />.
        /// </summary>
        /// <param name="query">The query to execute.</param>
        /// <param name="notFoundMessage">An optional message to included when the resource was not found.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <typeparam name="TResult">The type of result produced by the query.</typeparam>
        protected async Task<IActionResult> ExecuteQueryAsync<TResult>(IRequest<TResult> query, string notFoundMessage = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(Resources.Resources.InvalidDataProvided);
            }

            TResult response = await mediator.Send(query, cancellationToken);

            if (response == null)
            {
                var actualNotFoundMessage = string.IsNullOrWhiteSpace(notFoundMessage)
                                                ? string.Format(Resources.Resources.ResourceNotFound)
                                                : notFoundMessage;

                return NotFound(actualNotFoundMessage);
            }

            return Ok(response);
        }

        /// <summary>
        ///     Executes the provided command, interprets its result and returns an appropriate <see cref="IActionResult" />.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="notFoundMessage">An optional message to included when the resource was not found.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <typeparam name="TResult">The type of result produced by the command.</typeparam>
        protected async Task<IActionResult> ExecuteCommandAsync<TResult>(IRequest<TResult> command, string notFoundMessage = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(Resources.Resources.InvalidDataProvided);
            }

            TResult response = await mediator.Send(command, cancellationToken);
            if (response == null)
            {
                throw new Exception(Resources.Resources.ErrorProcessingRequest);
            }

            return Ok(response);
        }

        /// <summary>
        ///     Executes the provided command and returns its result without interpretation.
        /// </summary>
        /// <typeparam name="TResult">The type of result produced by the command.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        protected Task<TResult> ExecuteCommandRawAsync<TResult>(IRequest<TResult> command, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return mediator.Send(command, cancellationToken);
        }

        /// <summary>
        ///     Executes the provided query and returns its result without interpretation.
        /// </summary>
        /// <typeparam name="TResult">The type of result produced by the query.</typeparam>
        /// <param name="query">The query to execute.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        protected Task<TResult> ExecuteQueryRawAsync<TResult>(IRequest<TResult> query, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            return mediator.Send(query, cancellationToken);
        }
    }
}