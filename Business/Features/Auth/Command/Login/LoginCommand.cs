using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Utilities.Hashing;
using DataAccess.Abstracts;
using Entities;
using MediatR;
using System.Security.Cryptography;
using System.Text;


namespace Business.Features.Auth.Command.Login
{
    public class LoginCommand : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }


        public class LoginCommandHandler : IRequestHandler<LoginCommand>
        {
            private readonly IUserRepository _userRepository;

            public LoginCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(i => i.Email == request.Email);
                if (user == null)
                {
                    throw new BusinessExeption("Giriş Başarısız");
                }

                bool isPasswordMatch = HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);

                if (!isPasswordMatch)
                {
                    throw new BusinessExeption("Giriş başarısız");
                }
            }
        }
    }
}

//byte array'in içinde olan SequenceEqual fonksiyonu byte array'leri karşılaştırır.