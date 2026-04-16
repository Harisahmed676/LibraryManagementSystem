using LibraryManagement.Core.DTOs.Library;
using LibraryManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var libraries = await _libraryService.GetAllLibrariesAsync();
            return Ok(libraries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var library = await _libraryService.GetLibraryByIdAsync(id);
            if (library == null) return NotFound($"Library with id {id} not found");
            return Ok(library);
        }

        [HttpGet("{id}/books")]
        public async Task<IActionResult> GetWithBooks(int id)
        {
            var library = await _libraryService.GetLibraryWithBooksAsync(id);
            if (library == null) return NotFound($"Library with id {id} not found");
            return Ok(library);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLibraryDto dto)
        {
            var created = await _libraryService.CreateLibraryAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLibraryDto dto)
        {
            try
            {
                await _libraryService.UpdateLibraryAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _libraryService.DeleteLibraryAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}