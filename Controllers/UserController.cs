using Base_API.DTO;
using Base_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Base_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(UserService userService) : ControllerBase
    {
        private protected readonly UserService _userService = userService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(uint id)
        {
            var response = await _userService.GetByIdAsync(id);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(uint id, [FromBody] UpdateUserDto request)
        {
            var response = await _userService.UpdateAsync(id, request);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(uint id)
        {
            var response = await _userService.DeleteAsync(id);

            return Ok(response);
        }
    }
}
