using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Author;
using LibraryService.API.Contracts.IncomingOutgoing.Book;
using LibraryService.API.Contracts.IncomingOutgoing.BookFund;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Commands.AuthorCommands
{
    public class AddBookFundCommand : BookFundCommandBase<long>
    {
        public AddBookFundCommand(BookFundDTO bookFund) : base(bookFund) { }
    }

    public class AddBookCommandHandler : IRequestHandler<AddBookFundCommand, long>
    {
        private readonly IBookFundService bookFundService;

        public AddBookCommandHandler(IBookFundService bookFundService)
        {
            this.bookFundService = bookFundService;
        }

        public async Task<long> Handle(AddBookFundCommand request, CancellationToken cancellationToken)
        {
            var author = MapToBookFund(request.Entity);
            var insertedAuthor = await bookFundService.InsertAsync(author);
            return insertedAuthor.Id;
        }
        // Check it mb the best way use just domain models than DTO
        private BookFund MapToBookFund(BookFundDTO bookFund)
        {
            return new BookFund
            {
                Amount = bookFund.Amount,
                Book =
                {
                    AmountPage = bookFund.Book.AmountPage,
                    Title = bookFund.Book.Title,
                    Year = bookFund.Book.Year,
                    Author =
                    {
                        Name = bookFund.Book.Author.Name,
                        Surname = bookFund.Book.Author.Surname,
                        Patronymic = bookFund.Book.Author.Patronymic
                    },
                    Genre =
                    {
                        Name = bookFund.Book.Genre.Name
                    },
                    Publisher =
                    {
                        Name = bookFund.Book.Publisher.Name
                    }
                },
                Library =
                {
                    Address = bookFund.Library.Address,
                    Name = bookFund.Library.Name,
                    Telephone = bookFund.Library.Telephone
                }
            };
        }
    }
}