using admin_bff.Controllers.Outbound;
using admin_bff.Dtos;
using ChapterBaseAPI.Dtos;

namespace admin_bff.Services
{
    public class UserService
    {
        private readonly CoreServiceClient _coreServiceClient;
        private readonly JwtUtilService _jwtUtilService;

        public UserService(CoreServiceClient coreServiceClient, JwtUtilService jwtUtilService)
        {
            _coreServiceClient = coreServiceClient;
            _jwtUtilService = jwtUtilService;
        }

        // Save a user after decoding ID token
        public async Task<ResponseDto<object>> SaveUserAsync(string idToken)
        {
            try
            {
                var userDto = _jwtUtilService.DecodeToken(idToken);

                if (userDto == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Failed to decode ID token."
                    };
                }

                userDto.Role = "ADMIN";
                return await _coreServiceClient.SaveUserAsync(userDto);
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = $"An error occurred while saving the user: {ex.Message}"
                };
            }
        }

        // Update user details
        public async Task<ResponseDto<object>> UpdateUserAsync(UserDto userDto)
        {
            try
            {
                return await _coreServiceClient.UpdateUserAsync(userDto);
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = $"An error occurred while updating the user: {ex.Message}"
                };
            }
        }

        // Retrieve all users by role
        public async Task<ResponseDto<object>> FindAllUsersByRoleAsync(string role)
        {
            try
            {
                return await _coreServiceClient.FindAllUsersByRoleAsync(role);
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = $"An error occurred while retrieving users: {ex.Message}"
                };
            }
        }

        // Retrieve a user by ID
        public async Task<ResponseDto<UserDto>> FindUserByIdAsync(Guid id)
        {
            try
            {
                return await _coreServiceClient.FindUserByIdAsync(id);
            }
            catch (Exception ex)
            {
                return new ResponseDto<UserDto>
                {
                    Success = false,
                    Message = $"An error occurred while retrieving the user: {ex.Message}"
                };
            }
        }

    }
}
