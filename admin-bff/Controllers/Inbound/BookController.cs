using admin_bff.Dtos;
using admin_bff.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            var response = await _bookService.SaveBook(bookDto);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
