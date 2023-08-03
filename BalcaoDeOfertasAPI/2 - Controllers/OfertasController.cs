using Microsoft.AspNetCore.Mvc;
using DtosBalcaoDeOfertas.InputDTO;
using BalcaoDeOfertasAPI._0___Config.Utils;
using BalcaoDeOfertasAPI._3___Services.Interfaces;

namespace BalcaoDeOfertasAPI._2___Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfertasController : ControllerBase
    {
        private readonly IOfertasService _ofertasService;

        public OfertasController(IOfertasService ofertasService)
        {
            _ofertasService = ofertasService;
        }

        [HttpPost]
        [Route("CriarOferta")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarOferta([FromBody] OfertaInputDTO inputDto)
        {
            try
            {
                var result = await _ofertasService.CriarOferta(inputDto);

                if (result.CodigoErro is (short)CodigoDeErros.Codigo.Duplicado)
                    return Conflict(result.Erro);

                if (result.CodigoErro is (short)CodigoDeErros.Codigo.LimiteDeOfertas)
                    return BadRequest(result.Erro);

                return Created($"Oferta/{result.Id}", result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ExcluirOferta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirOferta([FromBody] OfertaInputDTO inputDto)
        {
            try
            {
                var result = await _ofertasService.ExcluirOferta(inputDto);

                if (result.CodigoErro is (short)CodigoDeErros.Codigo.NaoLocalizado)
                    return NotFound(result.Erro);

                if (result.CodigoErro is (short)CodigoDeErros.Codigo.UsuarioIncorreto)
                    return BadRequest(result.Erro);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}