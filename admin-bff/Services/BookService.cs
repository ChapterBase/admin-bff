using admin_bff.Controllers.Outbound;
using admin_bff.Dtos;
using ChapterBaseAPI.Dtos;
using System;
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
                return await _coreServiceClient.SaveBookAsync(bookDto);
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while saving the book: " + ex.Message
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateBook(BookDto bookDto)
        {
            try
            {
                return await _coreServiceClient.UpdateBookAsync(bookDto);
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while updating the book: " + ex.Message
                };
            }
        }

        public async Task<ResponseDto<object>> FindAllBooks(int page, int size)
        {
            try
            {
                return await _coreServiceClient.FindAllBooksAsync(page, size);
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving books: " + ex.Message
                };
            }
        }

        public async Task<ResponseDto<BookDto>> FindBookById(Guid id)
        {
            try
            {
                return await _coreServiceClient.FindBookByIdAsync(id);
            }
            catch (Exception ex)
            {
                return new ResponseDto<BookDto>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the book: " + ex.Message
                };
            }
        }

        public async Task<ResponseDto<object>> DeleteBook(Guid id)
        {
            try
            {
                return await _coreServiceClient.DeleteBookAsync(id);
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "An error occurred while deleting the book: " + ex.Message
                };
            }
        }
    }
}
