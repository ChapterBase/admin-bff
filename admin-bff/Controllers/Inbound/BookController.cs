using admin_bff.Dtos;
using admin_bff.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace admin_bff.Controllers.Inbound
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BookController(BookService bookService) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] BookDto bookDto)
        {
            var response = await bookService.SaveBook(bookDto);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BookDto bookDto)
        {
            var response = await bookService.UpdateBook(bookDto);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("All")]
        public async Task<IActionResult> FindAll([FromBody] RequestDto request)
        {
            var response = await bookService.FindAllBooks(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById([FromRoute] Guid id)
        {
            var response = await bookService.FindBookById(id);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await bookService.DeleteBook(id);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
