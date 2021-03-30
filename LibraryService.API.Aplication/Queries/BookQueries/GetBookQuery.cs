﻿using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.IncomingOutgoing.Author;
using LibraryService.API.Contracts.IncomingOutgoing.Book;
using LibraryService.API.Contracts.IncomingOutgoing.Genre;
using LibraryService.API.Contracts.IncomingOutgoing.Publisher;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Queries.BookQueries
{
    public class GetBookQuery : IRequest<BookDTO>
    {
        public long Id { get; }

        public GetBookQuery(long id)
        {
            Id = id;
        }
    }

    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookDTO>
    {
        private readonly IBookService bookService;

        public GetBookQueryHandler(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<BookDTO> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await bookService.GetAsync(request.Id);
            return MapToBookDTO(book);
        }

        public BookDTO MapToBookDTO(Book book)
        {
            return new BookDTO()
            {
                AmountPage = book.AmountPage,
                Title = book.Title,
                Year = book.Year,
                Author = new AuthorDTO()
                {
                    Name = book.Author.Name,
                    Surname = book.Author.Surname,
                    Patronymic = book.Author.Patronymic
                },
                Genre = new GenreDTO()
                {
                    Name = book.Genre.Name
                },
                Publisher = new PublisherDTO()
                {
                    Name = book.Publisher.Name
                }
            };
        }
    }
}