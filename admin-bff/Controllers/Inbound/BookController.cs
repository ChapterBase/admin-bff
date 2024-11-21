using admin_bff.Dtos;
using admin_bff.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace admin_bff.Controllers.Inbound
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookDto bookDto)
        {
            await _bookService.SaveBook(bookDto);
            return Ok();
        }

    }
}
