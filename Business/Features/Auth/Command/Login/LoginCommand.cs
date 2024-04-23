using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Utilities.Hashing;
using Core.Utilities.JWT;
using DataAccess.Abstracts;
using Entities;
using MediatR;


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

            public LoginCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
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

               return _tokenHelper.CreateToken(user);
            }
        }
    }
}

//byte array'in içinde olan SequenceEqual fonksiyonu byte array'leri karşılaştırır.