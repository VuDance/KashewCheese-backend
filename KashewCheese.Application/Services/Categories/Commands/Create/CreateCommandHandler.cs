using AutoMapper;
using KashewCheese.Application.Common.Interfaces.Persistence;
using KashewCheese.Application.DTO;
using KashewCheese.Application.Services.Categories.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Categories.Commands.Create
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand,CreateCategoryResult>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCommandHandler(ICategoryRepository categoryRepository,IMapper mapper) {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CreateCategoryResult> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            CategoryDto categoryDto = _mapper.Map<CategoryDto>(request);
            await _categoryRepository.Create(categoryDto);
            return new CreateCategoryResult("Category created successfully");
        }
    }
}
