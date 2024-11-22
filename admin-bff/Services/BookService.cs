using admin_bff.Controllers.Outbound;
using admin_bff.Dtos;
using ChapterBaseAPI.Dtos;
using System.Threading.Tasks;

namespace admin_bff.Services
{
    public class BookService
    {
        private readonly CoreServiceClient _coreServiceClient;

        public BookService(CoreServiceClient coreServiceClient)
        {
            _coreServiceClient = coreServiceClient;
        }

        public async Task<ResponseDto<object>> SaveBook(BookDto bookDto)
        {
            try
            {
                var response = await _coreServiceClient.SaveBookAsync(bookDto);
                return response;
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while saving the book: " + ex.Message,
                    Data = null
                };
            }
        }
    }
}
