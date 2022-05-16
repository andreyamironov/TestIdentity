using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestIdentity.Identity.Commands.Users;
using TestIdentity.Identity.Models;

namespace TestIdentity.Identity.Handlers.Users
{
    public class CreateUserPostHandler : IRequestHandler<CreateUserPostCommand, ApplicationUser>
    {
        UserManager<ApplicationUser> _userManager;
        IMapper _mapper;

        public CreateUserPostHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ApplicationUser> Handle(CreateUserPostCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser()
            {
                UserName = request.CreateClientViewModel.EMail,
                Email = request.CreateClientViewModel.EMail,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.CreateClientViewModel.Password);

            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var error in result.Errors)
                {
                    sb.AppendLine(error.Description);
                }

                throw new AMir.Exception.CommonOperationException(sb.ToString());
            }

            return user;
        }
    }
}
