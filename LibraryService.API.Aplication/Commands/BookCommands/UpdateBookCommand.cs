using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Author;
using LibraryService.API.Contracts.IncomingOutgoing.Book;
using LibraryService.API.Contracts.IncomingOutgoing.Genre;
using LibraryService.API.Contracts.IncomingOutgoing.Publisher;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Commands.BookCommands
{
    public class UpdateBookCommand : BookCommandBase<Response>
    {
        public UpdateBookCommand(long id, BookDTO update) : base(id, update) { }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Response>
    {
        private readonly IBookService bookService;

        public UpdateBookCommandHandler(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<Response> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await bookService.GetAsync(request.Id.Value, cancellationToken);

            if (book == null)
            {
                return Response.Error;
            }
            var bookToUpdate = MapDTOToBook(request.Entity, book);

            var updatedBook = await bookService.UpdateAsync(bookToUpdate);

            if (updatedBook == null)
            {
                return Response.Error;
            }

            return Response.Successful;
        }

        public Book MapDTOToBook(BookDTO bookDTO, Book book)
        {
            book.AmountPage = bookDTO.AmountPage;
            book.Title = bookDTO.Title;
            book.Year = bookDTO.Year;
            book.AuthorId = bookDTO.AuthorId;
            book.PublisherId = bookDTO.PublisherId;
            book.GenreId = bookDTO.GenreId;
            return book;
        }
    }
}