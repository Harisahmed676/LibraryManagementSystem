using LibraryManagement.Core.DTOs.BorrowRecord;
using LibraryManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowService _borrowService;

        public BorrowController(IBorrowService borrowService)
        {
            _borrowService = borrowService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var borrows = await _borrowService.GetAllBorrowsAsync();
            return Ok(borrows);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActive()
        {
            var borrows = await _borrowService.GetActiveBorrowsAsync();
            return Ok(borrows);
        }

        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdue()
        {
            var borrows = await _borrowService.GetOverdueBorrowsAsync();
            return Ok(borrows);
        }

        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetByMember(int memberId)
        {
            var borrows = await _borrowService.GetBorrowsByMemberAsync(memberId);
            return Ok(borrows);
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook([FromBody] CreateBorrowRecordDto dto)
        {
            try
            {
                var result = await _borrowService.BorrowBookAsync(dto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDto dto)
        {
            try
            {
                await _borrowService.ReturnBookAsync(dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}