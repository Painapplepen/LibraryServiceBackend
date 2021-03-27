﻿using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Admin;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Commands.AdminCommands
{
    public class DeleteAdminCommand : IRequest
    {
        public long Id { get; }

        public DeleteAdminCommand(long id)
        {
            Id = id;
        }
    }

    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand>
    {
        private readonly IAdminService adminService;

        public DeleteAdminCommandHandler(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<Unit> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            await adminService.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}