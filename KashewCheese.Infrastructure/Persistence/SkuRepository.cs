using AutoMapper;
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
    public class SkuRepository : ISkuRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public SkuRepository(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateBulkSku(List<CreateSkuDto> skuList)
        {

            List<Sku> data=new();
            foreach (var sku in skuList)
            {
                Sku s = new()
                {
                    ProductId = sku.ProductId,
                    ProductPrice = sku.ProductPrice,
                    ProductThumb = sku.ProductThumb,
                    SkuIdx = sku.SkuIdx,
                    Slug = sku.Slug,
                    Stock = sku.Stock,
                    Name = sku.Name,
                };
                data.Add(s);
            }
            await _context.Sku.AddRangeAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task CreateSku(CreateSkuDto skuDto)
        {
            Sku sku = new()
            {
                ProductId=skuDto.ProductId,
                ProductPrice=skuDto.ProductPrice,
                ProductThumb=skuDto.ProductThumb,
                SkuIdx=skuDto.SkuIdx,
                Slug=skuDto.Slug,
                Stock=skuDto.Stock,
                Name=skuDto.Name,
            };
            await _context.Sku.AddAsync(sku);
            await _context.SaveChangesAsync();
        }

    }
}
