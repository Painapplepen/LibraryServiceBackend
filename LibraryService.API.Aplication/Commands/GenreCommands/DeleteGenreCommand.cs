﻿using System.Threading;
using System.Threading.Tasks;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Commands.GenreCommands
{
    public class DeletePublisherCommand : IRequest
    {
        public long Id { get; }

        public DeletePublisherCommand(long id)
        {
            Id = id;
        }
    }

    public class DeleteGenreCommandHandler : IRequestHandler<DeletePublisherCommand>
    {
        private readonly IGenreService genreService;

        public DeleteGenreCommandHandler(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        public async Task<Unit> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
        {
            await genreService.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
