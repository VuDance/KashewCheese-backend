using KashewCheese.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Common.Interfaces.Persistence
{
    public interface ICategoryRepository
    {
        Task Create(CategoryDto category);
    }
}
