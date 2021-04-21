using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Author;
using LibraryService.API.Contracts.IncomingOutgoing.BookFund;
using LibraryService.API.Contracts.IncomingOutgoing.Genre;
using LibraryService.API.Contracts.IncomingOutgoing.Publisher;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Commands.BookFundCommands
{
    public class UpdateBookFundCommand : BookFundCommandBase<Response>
    {
        public UpdateBookFundCommand(long id, BookFundDTO update) : base(id, update) { }
    }

    public class UpdateBookFundCommandHandler : IRequestHandler<UpdateBookFundCommand, Response>
    {
        private readonly IBookFundService bookFundService;

        public UpdateBookFundCommandHandler(IBookFundService bookFundService)
        {
            this.bookFundService = bookFundService;
        }

        public async Task<Response> Handle(UpdateBookFundCommand request, CancellationToken cancellationToken)
        {
            var bookFund = await bookFundService.GetAsync(request.Id.Value, cancellationToken);

            if (bookFund == null)
            {
                return Response.Error;
            }

            var bookFundToUpdate = MapDTOToBookFund(request.Entity, bookFund);

            var updatedBookFund = await bookFundService.UpdateAsync(bookFundToUpdate);

            if (updatedBookFund == null)
            {
                return Response.Error;
            }

            return Response.Successful;
        }

        private BookFund MapDTOToBookFund(BookFundDTO bookFundDTO, BookFund bookFund)
        {
            bookFund.Amount = bookFundDTO.Amount;
            bookFund.BookId = bookFundDTO.BookId;
            bookFund.LibraryId = bookFundDTO.LibraryId;
            
            return bookFund;
        }
    }
}