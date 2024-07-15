using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Application.DTO;
using KashewCheese.Domain.Entities;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Infrastructure.Persistence
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(CategoryDto category)
        {
            Category ct = new Category()
            {
                Name = category.Name,
                DescriptionEN=category.DescriptionEN,
                DescriptionVN=category.DescriptionVN,
                Slug=category.Slug,
                IsDelete=category.IsDelete,
                IsDraft=category.IsDraft,
                IsPublished=category.IsPublished,
            };

            await _context.Category.AddAsync(ct);
            await _context.SaveChangesAsync();

        }
    }
}
