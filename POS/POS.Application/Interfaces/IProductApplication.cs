using POS.Application.Commons.Base;
using POS.Application.Dtos.Product.Request;
using POS.Application.Dtos.Product.Response;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;


namespace POS.Application.Interfaces
{
    public interface IProductApplication
    {
        Task<BaseResponse<BaseEntityResponse<ProductResponseDto>>> ListProduct(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<ProductSelectResponseDto>>> ListSelectProduct();
        Task<BaseResponse<ProductResponseDto>> ProductById(int productId);
        Task<BaseResponse<bool>> RegisterProduct(ProductRequestDto requestDto);
        Task<BaseResponse<bool>> EditProduct(int productId, ProductRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveProduct(int productId);
        Task<BaseResponse<bool>> RemoveProduct2(int productId);

    }
}
