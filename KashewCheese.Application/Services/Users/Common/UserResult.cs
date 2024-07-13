using KashewCheese.Application.DTO;
using KashewCheese.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Authentication.Common
{
    public record UserResult(List<User> Users);
}
