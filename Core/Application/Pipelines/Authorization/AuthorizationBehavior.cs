using Core.Application.Pipelines.Logging;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Authorization
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ISecuredRequest
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new BusinessExeption("Giriş yapmadınız");
            }
            if (request.RequiredRoles.Any())
            {
                ICollection<string>? userRoles = _httpContextAccessor.HttpContext.User.Claims
                       .Where(i => i.Type == ClaimTypes.Role)
                       .Select(i => i.Value)
                       .ToList(); //Roller birden  fazla liste olarak döneceği için where ile ulaştık.

                bool hasNoMatchRole = userRoles.FirstOrDefault(i => i == "Admin" || request.RequiredRoles.Contains(i)).IsNullOrEmpty();
                if (hasNoMatchRole)
                {
                    throw new BusinessExeption("Bunu yapmaya yetkiniz yok.");
                }
            }
           

            TResponse response = await next();
            return response;
        }
    }
}


//Kullanıcı - Rol verisini tanımla
//Jwt'ye bu bilgiyi ekle
//JWT doğrulanırken komut rol gerektiriyor ise, rol kontrolü de devreye alınmalı.