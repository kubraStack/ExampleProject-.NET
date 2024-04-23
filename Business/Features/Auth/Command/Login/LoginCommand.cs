using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Utilities.Hashing;
using Core.Utilities.JWT;
using DataAccess.Abstracts;
using Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Business.Features.Auth.Command.Login
{
    public class LoginCommand : IRequest<AccessToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }


        public class LoginCommandHandler : IRequestHandler<LoginCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;

            public LoginCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
                _userOperationClaimRepository = userOperationClaimRepository;
            }

            public async Task<AccessToken> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(i => i.Email == request.Email);
                if (user == null)
                {
                    throw new BusinessExeption("Giriş Başarısız");
                }

                bool isPasswordMatch = HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);

                if (isPasswordMatch)
                {
                    throw new BusinessExeption("Giriş başarısız");
                }

                //Kullanıcı rollerini sorgula
                List<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(i => i.UserId == user.Id, include: i => i.Include(i => i.OperatiomClaim));
                return _tokenHelper.CreateToken(user, userOperationClaims.Select(i=>(Core.Entities.OperationClaim)i.OperatiomClaim).ToList();
            }
        }
    }
}

//byte array'in içinde olan SequenceEqual fonksiyonu byte array'leri karşılaştırır.