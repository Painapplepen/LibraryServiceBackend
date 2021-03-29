using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Book;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Commands.BookCommands
{
    public class UpdateBookCommand : BookCommandBase<BookDTO>
    {
        public UpdateBookCommand(long id, BookDTO update) : base(id, update) { }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDTO>
    {
        private readonly IBookService bookService;

        public UpdateBookCommandHandler(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<BookDTO> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await bookService.GetAsync(request.Id);

            var bookToUpdate = MapDTOToBook(request.Entity, book);

            var updatedBook = await bookService.UpdateAsync(bookToUpdate);

            return MapToBookDTO(updatedBook);
        }

        public Book MapDTOToBook(BookDTO bookDTO, Book book)
        {
            book.AmountPage = bookDTO.AmountPage;
            book.Title = bookDTO.Title;
            book.Year = bookDTO.Year;
            book.Author.Name = bookDTO.Author.Name;
            book.Author.Surname = bookDTO.Author.Surname;
            book.Author.Patronymic = bookDTO.Author.Patronymic;
            book.Publisher.Name = bookDTO.Publisher.Name;
            book.Genre.Name = bookDTO.Genre.Name;
            return book;
        }
        //Check it
        public BookDTO MapToBookDTO(Book book)
        {
            return new BookDTO()
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