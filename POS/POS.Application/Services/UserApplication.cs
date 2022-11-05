using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using POS.Application.Commons.Base;
using POS.Application.Dtos.User.Request;
using POS.Application.Dtos.User.Response;
using POS.Application.Interfaces;
using POS.Application.Validators.User;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Static;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace POS.Application.Services
{
    public class UserApplication : IUserApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserValidator _validationRules;
        private readonly IConfiguration _configuration;


        public UserApplication(IUnitOfWork unitOfWork, IMapper mapper, UserValidator validationRules, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
            _configuration = configuration;

        }

        private string GenerateToken(User account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, account.Email!),
                new Claim(JwtRegisteredClaimNames.FamilyName, account.UserName!),
                new Claim(JwtRegisteredClaimNames.GivenName, account.Email!),
                new Claim(JwtRegisteredClaimNames.UniqueName, account.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, Guid.NewGuid().ToString(), ClaimValueTypes.Integer64)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:Expires"])),
                notBefore: DateTime.UtcNow,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<BaseResponse<string>> GenerateToken(TokenRequestDto requestDto)
        {
            var response = new BaseResponse<string>();
            var account = await _unitOfWork.User.AccountByUserName(requestDto.UserName!);

            if (account is not null)
            {
                if (BC.Verify(requestDto.Password, account.Password))
                {

                    response.IsSuccess = true;
                    response.Data =GenerateToken(account);
                    response.Message = ReplyMessage.MESSAGE_TOKEN;
                    return response;
                }
                else
                {

                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_TOKEN_ERROR;
                }

            }
            else
            {

                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_TOKEN_ERROR;
            }
            return response;
        }

        public async Task<BaseResponse<BaseEntityResponse<UserResponseDto>>> ListUser(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<UserResponseDto>>();
            var user = await _unitOfWork.User.ListUser(filters);

            if (user is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<UserResponseDto>>(user);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

            }
            return response;
        }

        public async Task<BaseResponse<bool>> RegisterUser(UserRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var validationResult = await _validationRules.ValidateAsync(requestDto);
            var account = _mapper.Map<User>(requestDto);
            account.Password = BC.HashPassword(requestDto.Password);



            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
                return response;
            }

            if (requestDto.Image is not null)
            {
                account.Image = await _unitOfWork.Storage.SaveFile(AzureContainers.USERS, requestDto.Image);
                // user.Image = await almacenadorArchivos.GuardarArchivo(contenedor, userRequestDto.Image);

            }

            response.Data = await _unitOfWork.User.RegisterAsync(account);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public async Task<BaseResponse<UserResponseDto>> UserById(int userId)
        {
            var response = new BaseResponse<UserResponseDto>();
            var user = await _unitOfWork.User.GetByIdAsync(userId);

            if (user is not null)
            {
                response.Data = _mapper.Map<UserResponseDto>(user);
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

            }
            return response;
        }


        public async Task<BaseResponse<bool>> EditUser(int userId, UserRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var userEdit = await UserById(userId);

            if (userEdit.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            var user = _mapper.Map<User>(requestDto);
            user.Id = userId;
            user.Password = BC.HashPassword(requestDto.Password);
            response.Data = await _unitOfWork.User.EditAsync(user);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RemoveUser(int userId)
        {
            var response = new BaseResponse<bool>();
            var user = await UserById(userId);

            if (user.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            response.Data = await _unitOfWork.User.RemoveAsync(userId);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_DELETE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;

        }


        public async Task<BaseResponse<bool>> RemoveUser2(int userId)
        {
            var response = new BaseResponse<bool>();
            var userRemove = await UserById(userId);

            if (userRemove.Data == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            response.Data = await _unitOfWork.User.RemoveUser2(userId);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_DELETE;
            }
            else
            {
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

    }
}
