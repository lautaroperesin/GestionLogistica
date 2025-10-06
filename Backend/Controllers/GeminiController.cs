using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GeminiController : ControllerBase
    {
        private readonly IGeminiService _geminiService;

        public GeminiController(IGeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        [HttpGet("prompt/{textPrompt}")]
        public async Task<IActionResult> GetPromptResponse(string textPrompt)
        {
            try
            {
                var respuesta = await _geminiService.GetPromptResponse(textPrompt);
                return Ok(new { Respuesta = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}
