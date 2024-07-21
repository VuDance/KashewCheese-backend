using KashewCheese.Application.Services.Categories.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Application.Services.Categories.Commands.Create
{
    public record CreateCommand
    ( 
    string Name,
    string DescriptionVN,
    string DescriptionEN,
    string Slug,
    bool IsDelete,
    bool IsDraft,
    bool IsPublished
    ):IRequest<CreateCategoryResult>;
}
