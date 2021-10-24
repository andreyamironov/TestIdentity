using AMir.Interface.Data;
using IdentityServerAspNetIdentity.Commands.Users;
using IdentityServerAspNetIdentity.Models;
using IdentityServerAspNetIdentity.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Handlers.Users
{
    public class CreateUserPostHandler : IRequestHandler<CreateUserPostCommand, UserCreateResult>
    {
        IWriterRepositoryAsync<UserCreateResult> _writer;

        public CreateUserPostHandler(IWriterRepositoryAsync<UserCreateResult> writer)
        {
            _writer = writer;
        }

        public Task<UserCreateResult> Handle(CreateUserPostCommand request, CancellationToken cancellationToken)
        {
            UserCreateViewModel userCreateViewModel = request.UserCreateViewModel;

            UserCreateResult user = new UserCreateResult()
            {
                UserName = userCreateViewModel.EMail,
                Email = userCreateViewModel.EMail,
                EmailConfirmed = true,
                Password= userCreateViewModel.Password           
            };

            var result = _writer.Create(user);
            return result;
        }
    }
}
