﻿using BudgetAppBackend.Application.Contracts;
using BudgetAppBackend.Application.Extensions;
using BudgetAppBackend.Domain.UserAggregate;
using MediatR;

namespace BudgetAppBackend.Application.Features.Authentication.SendVerificationCode
{
    public class SendVerificationCodeCommandHandler : IRequestHandler<SendVerificationCodeCommand, bool>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMediator _mediator;

        public SendVerificationCodeCommandHandler(IAuthRepository authRepository, IMediator mediator)
        {
            _authRepository = authRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _authRepository.GetUserByEmailAsync(request.SendVerificationCodeDto.Email);
            if (user == null)
            {
                return false;
            }

            var verificationCode = User.GenerateVerificationToken();
            user.SetEmailVerificationCode(verificationCode, DateTime.UtcNow.AddHours(1), user.FirstName, user.LastName, user.Email);
            await _authRepository.UpdateUserAsync(user);

            await _mediator.PublishDomainEventsAsync(new[] { user }, cancellationToken);

            return true;
        }
    }
}
