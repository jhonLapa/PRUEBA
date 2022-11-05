﻿using POS.Application.Commons.Base;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.Category.Response;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;

namespace POS.Application.Interfaces
{
    public interface ICategoryApplication
    {
        Task<BaseResponse<BaseEntityResponse<CategoryResponseDto>>> ListCategory(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<CategorySelectResponseDto>>> ListSelectCategory();
        Task<BaseResponse<CategoryResponseDto>> CategoryById(int categoryId);
        Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDto requestDto);
        Task<BaseResponse<bool>> EditCategory(int categoryId, CategoryRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveCategory(int categoryId);
        Task<BaseResponse<bool>> RemoveCategory2(int categoryId);


    }
}
