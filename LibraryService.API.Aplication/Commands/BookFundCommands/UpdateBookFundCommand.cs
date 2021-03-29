using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Author;
using LibraryService.API.Contracts.IncomingOutgoing.BookFund;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Commands.BookFundCommands
{
    public class UpdateBookFundCommand : BookFundCommandBase<BookFundDTO>
    {
        public UpdateBookFundCommand(long id, BookFundDTO update) : base(id, update) { }
    }

    public class UpdateBookFundCommandHandler : IRequestHandler<UpdateBookFundCommand, BookFundDTO>
    {
        private readonly IBookFundService bookFundService;

        public UpdateBookFundCommandHandler(IBookFundService bookFundService)
        {
            this.bookFundService = bookFundService;
        }

        public async Task<BookFundDTO> Handle(UpdateBookFundCommand request, CancellationToken cancellationToken)
        {
            var bookFund = await bookFundService.GetAsync(request.Id);

            var bookFundToUpdate = MapDTOToBookFund(request.Entity, bookFund);

            var updatedBookFund = await bookFundService.UpdateAsync(bookFundToUpdate);

            return MapToBookFundDTO(updatedBookFund);
        }
        // Do them private next time
        public BookFund MapDTOToBookFund(BookFundDTO bookFundDTO, BookFund bookFund)
        {
            bookFund.Amount = bookFundDTO.Amount;
            bookFund.Book.AmountPage = bookFundDTO.Book.AmountPage;
            bookFund.Book.Title = bookFundDTO.Book.Title;
            bookFund.Book.Year = bookFundDTO.Book.Year;
            bookFund.Book.Author.Name = bookFundDTO.Book.Author.Name;
            bookFund.Book.Author.Surname = bookFundDTO.Book.Author.Surname;
            bookFund.Book.Author.Patronymic = bookFundDTO.Book.Author.Patronymic;
            bookFund.Book.Genre.Name = bookFundDTO.Book.Genre.Name;
            bookFund.Book.Publisher.Name = bookFundDTO.Book.Publisher.Name;
            bookFund.Library.Address = bookFundDTO.Library.Address;
            bookFund.Library.Name = bookFundDTO.Library.Name;
            bookFund.Library.Telephone = bookFundDTO.Library.Telephone;
            
            return bookFund;
        }

        public BookFundDTO MapToBookFundDTO(BookFund bookFund)
        {
            return new BookFundDTO()
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