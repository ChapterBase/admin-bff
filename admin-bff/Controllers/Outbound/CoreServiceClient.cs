using admin_bff.Dtos;
using ChapterBaseAPI.Dtos;
using RestSharp;
using System;
using System.Threading.Tasks;
using ChapterBaseAPI.Dto;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace admin_bff.Controllers.Outbound
{
    public class CoreServiceClient
    {
        private readonly RestClient _restClient;

        public CoreServiceClient(IConfiguration configuration)
        {
            var baseUrl = configuration["CoreService:BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentNullException("CoreService:BaseUrl configuration is missing.");
            }

            _restClient = new RestClient(baseUrl);
        }

        // User methods

        public async Task<ResponseDto<object>> SaveUserAsync(UserDto userDto)
        {
            var request = new RestRequest("/User", Method.Post);
            request.AddJsonBody(userDto);

            return await ExecuteRequestAsync<ResponseDto<object>>(request);
        }

        public async Task<ResponseDto<object>> UpdateUserAsync(UserDto userDto)
        {
            var request = new RestRequest("/User", Method.Put);
            request.AddJsonBody(userDto);

            return await ExecuteRequestAsync<ResponseDto<object>>(request);
        }

        public async Task<ResponseDto<object>> FindAllUsersByRoleAsync(string role)
        {
            var request = new RestRequest("/User/By/Role", Method.Get)
                .AddQueryParameter("role", role);

            return await ExecuteRequestAsync<ResponseDto<object>>(request);
        }

        public async Task<ResponseDto<UserDto>> FindUserByIdAsync(Guid id)
        {
            var request = new RestRequest($"/User/{id}", Method.Get);

            return await ExecuteRequestAsync<ResponseDto<UserDto>>(request);
        }

        // Book methods

        public async Task<ResponseDto<object>> SaveBookAsync(BookDto bookDto)
        {
            var request = new RestRequest("/Book", Method.Post);
            request.AddJsonBody(bookDto);

            return await ExecuteRequestAsync<ResponseDto<object>>(request);
        }

        public async Task<ResponseDto<object>> UpdateBookAsync(BookDto bookDto)
        {
            var request = new RestRequest("/Book", Method.Put);
            request.AddJsonBody(bookDto);

            return await ExecuteRequestAsync<ResponseDto<object>>(request);
        }

        public async Task<ResponseDto<object>> FindAllBooksAsync(RequestDto request)
        {
            var restRequest = new RestRequest("/Book/All", Method.Post);
            restRequest.AddJsonBody(request);

            return await ExecuteRequestAsync<ResponseDto<object>>(restRequest);
        }


        public async Task<ResponseDto<BookDto>> FindBookByIdAsync(Guid id)
        {
            var request = new RestRequest($"/Book/{id}", Method.Get);

            return await ExecuteRequestAsync<ResponseDto<BookDto>>(request);
        }

        public async Task<ResponseDto<object>> DeleteBookAsync(Guid id)
        {
            var request = new RestRequest($"/Book/{id}", Method.Delete);

            return await ExecuteRequestAsync<ResponseDto<object>>(request);
        }

        private async Task<T> ExecuteRequestAsync<T>(RestRequest request) where T : class
        {
            var response = await _restClient.ExecuteAsync<T>(request);
            if (!response.IsSuccessful || response.Data == null)
            {
                throw new Exception($"Request failed. StatusCode: {response.StatusCode}, Message: {response.ErrorMessage}");
            }

            return response.Data;
        }

        // Banner methods

        public async Task<ResponseDto<object>> SaveBannerAsync(BannerDto bannerDto)
        {
            var request = new RestRequest("/Banner", Method.Post);
            request.AddJsonBody(bannerDto);

            return await ExecuteRequestAsync<ResponseDto<object>>(request);
        }

        public async Task<ResponseDto<object>> UpdateBannerAsync(BannerDto bannerDto)
        {
            var request = new RestRequest("/Banner", Method.Put);
            request.AddJsonBody(bannerDto);

            return await ExecuteRequestAsync<ResponseDto<object>>(request);
        }

        public async Task<ResponseDto<object>> FindAllBannersByStatusAsync(string status)
        {
            var request = new RestRequest("/Banner/By/Status", Method.Get)
                .AddQueryParameter("status", status);

            return await ExecuteRequestAsync<ResponseDto<object>>(request);
        }
    }
}
