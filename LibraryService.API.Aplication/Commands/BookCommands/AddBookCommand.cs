using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Book;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Commands.BookCommands
{
    public class AddBookCommand : BookCommandBase<long>
    {
        public AddBookCommand(BookDTO book) : base(book) { }
    }

    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, long>
    {
        private readonly IBookService bookService;

        public AddBookCommandHandler(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<long> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = MapToBook(request.Entity);
            var insertedBook = await bookService.InsertAsync(book);
            return insertedBook.Id;
        }

        //Check it
        private Book MapToBook(BookDTO book)
        {
            return new Book
            {
                AmountPage = book.AmountPage,
                Title = book.Title,
                Year = book.Year,
                Author =
                {
                    Name = book.Author.Name,
                    Surname = book.Author.Surname,
                    Patronymic = book.Author.Patronymic
                },
                Genre =
                {
                    Name = book.Genre.Name
                },
                Publisher = 
                {
                    Name = book.Publisher.Name
                }
            };
        }
    }
}