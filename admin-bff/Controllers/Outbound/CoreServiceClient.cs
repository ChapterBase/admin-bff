using admin_bff.Dtos;
using ChapterBaseAPI.Dtos;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace admin_bff.Controllers.Outbound
{
    public class CoreServiceClient
    {
        private readonly RestClient _restClient;

        public CoreServiceClient(IConfiguration configuration)
        {
            // Get base URL from appsettings.json
            var baseUrl = configuration["CoreService:BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentNullException("CoreService:BaseUrl configuration is missing.");
            }

            _restClient = new RestClient(baseUrl);
        }

        public async Task<ResponseDto<object>> SaveUserAsync(UserDto userDto)
        {
            var request = new RestRequest("/User", Method.Post);
            request.AddJsonBody(userDto);

            var response = await _restClient.ExecuteAsync<ResponseDto<object>>(request);
            if (!response.IsSuccessful || response.Data == null)
            {
                throw new Exception($"Failed to save user. StatusCode: {response.StatusCode}, Message: {response.ErrorMessage}");
            }

            return response.Data;
        }

        public async Task<ResponseDto<object>> SaveBookAsync(BookDto bookDto)
        {
            var request = new RestRequest("/Book", Method.Post);
            request.AddJsonBody(bookDto);

            var response = await _restClient.ExecuteAsync<ResponseDto<object>>(request);
            if (!response.IsSuccessful || response.Data == null)
            {
                throw new Exception($"Failed to save book. StatusCode: {response.StatusCode}, Message: {response.ErrorMessage}");
            }

            return response.Data;
        }
    }
}
