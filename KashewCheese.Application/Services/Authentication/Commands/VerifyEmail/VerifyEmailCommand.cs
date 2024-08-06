using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Authentication.Commands.VerifyEmail
{
    public record VerifyEmailCommand(string email,string code): IRequest<string>;
}
