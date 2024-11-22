using admin_bff.Dtos;
using admin_bff.Services;
using ChapterBaseAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> Save([FromBody] BookDto bookDto)
        {
            var response = await _bookService.SaveBook(bookDto);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BookDto bookDto)
        {
            var response = await _bookService.UpdateBook(bookDto);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> FindAll([FromQuery] int page, [FromQuery] int size)
        {
            var response = await _bookService.FindAllBooks(page, size);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById([FromRoute] Guid id)
        {
            var response = await _bookService.FindBookById(id);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await _bookService.DeleteBook(id);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
