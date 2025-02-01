﻿using BudgetAppBackend.Application.Contracts;
using MediatR;

namespace BudgetAppBackend.Application.Features.Authentication.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
    {
        private readonly IAuthRepository _authRepository;

        public ResetPasswordCommandHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _authRepository.GetUserByEmailAsync(request.resetPassword.Email);
            if (user == null)
            {
                throw new ArgumentException("Invalid email address.");
            }

            user.ChangePassword(request.resetPassword.NewPassword);

            await _authRepository.UpdateUserAsync(user);

            return Unit.Value;
        }
    }
}
