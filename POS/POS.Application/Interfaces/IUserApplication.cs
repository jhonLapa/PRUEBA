using POS.Application.Commons.Base;
using POS.Application.Dtos.User.Request;
using POS.Application.Dtos.User.Response;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;

namespace POS.Application.Interfaces
{
    public interface IUserApplication
    {
        Task<BaseResponse<string>> GenerateToken(TokenRequestDto requestDto);
        Task<BaseResponse<BaseEntityResponse<UserResponseDto>>> ListUser(BaseFiltersRequest filters);
        Task<BaseResponse<bool>> RegisterUser(UserRequestDto requestDto);
        Task<BaseResponse<UserResponseDto>> UserById(int userId);
        Task<BaseResponse<bool>> EditUser(int userId, UserRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveUser(int userId);
        Task<BaseResponse<bool>> RemoveUser2(int userId);


    }
}
