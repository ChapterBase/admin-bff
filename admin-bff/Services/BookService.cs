using admin_bff.Controllers.Outbound;
using admin_bff.Dtos;

namespace admin_bff.Services
{
    public class BookService
    {
        private readonly CoreServiceClient _coreServiceClient;

        public BookService(CoreServiceClient coreServiceClient)
        {
            _coreServiceClient = coreServiceClient;
        }

        public async Task SaveBook(BookDto bookDto)
        {
           await _coreServiceClient.SaveBookAsync(bookDto);   
        }


    }
}
