using KashewCheese.Contracts.Product;

namespace KashewCheese.Application.DTO
{
    public class CreateProductDto
    {
        public string Name {  get; set; }
        public string Slug {  get; set; }
        public string Description { get; set; }
        public string ProductThumb { get; set; }
        public int CategoryId { get; set; }
        public ICollection<CreateProductVariantRequest> Options {  get; set; }

    }
}
