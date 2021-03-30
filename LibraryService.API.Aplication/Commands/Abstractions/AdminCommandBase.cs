using LibraryService.API.Contracts.IncomingOutgoing.Admin;
using MediatR;

namespace LibraryService.API.Application.Commands.Abstractions
{
    public class AdminCommandBase<TResponse> : IRequest<TResponse>
    {
        public AdminDTO Entity { get; set; }
        public long Id { get; set; }

        protected AdminCommandBase(long id, AdminDTO entity)
        {
            Id = id;
            Entity = entity;
        }

        protected AdminCommandBase(AdminDTO entity)
        {
            Entity = entity;
        }
    }
}