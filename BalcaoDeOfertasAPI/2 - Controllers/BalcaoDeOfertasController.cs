using Microsoft.AspNetCore.Mvc;
using BalcaoDeOfertasAPI._1___Models;
using BalcaoDeOfertasAPI._3___Services.Interfaces;

namespace BalcaoDeOfertasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BalcaoDeOfertasController : ControllerBase
    {
        private readonly IOfertasService _balcaoDeOfertasService;

        public BalcaoDeOfertasController(IOfertasService balcaoDeOfertasService)
        {
            _balcaoDeOfertasService = balcaoDeOfertasService;
        }

        [HttpGet(Name = "GetBalcaoDeOfertas")]
        [ProducesResponseType(typeof(IList<Oferta>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBalcaoDeOfertas(int page = 1, int pageSize = 10, string? scrollId = null)
        {
            try
            {
                var result = await _balcaoDeOfertasService.GetBalcaoDeOfertasAsync(page, pageSize, scrollId);

                if (!result.Any())
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}