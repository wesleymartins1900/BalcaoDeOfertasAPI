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
        [ProducesResponseType(typeof(OfertaOutputDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CriarOferta([FromBody] NovaOfertaInputDTO inputDto)
        {
            try
            {
                var result = await _ofertasService.CriarOferta(inputDto);

                if (result.CodigoErro is (short)CodigoDeErros.Codigo.SaldoInsuficiente)
                    return Forbid(result.MensagemDeRetorno);

                if (result.CodigoErro is (short)CodigoDeErros.Codigo.LimiteDeOfertas)
                    return BadRequest(result.MensagemDeRetorno);

                return Created($"Oferta/{result.Id}", result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ExcluirOferta")]
        [ProducesResponseType(typeof(OfertaOutputDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirOferta([FromBody] ExcluirOfertaInputDTO inputDto)
        {
            try
            {
                var result = await _ofertasService.ExcluirOferta(inputDto);

                if (result.CodigoErro is (short)CodigoDeErros.Codigo.NaoLocalizado)
                    return NotFound(result.MensagemDeRetorno);

                if (result.CodigoErro is (short)CodigoDeErros.Codigo.UsuarioIncorreto)
                    return BadRequest(result.MensagemDeRetorno);

                if (result.CodigoErro is (short)CodigoDeErros.Codigo.Excluido)
                    return Ok(result.MensagemDeRetorno);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}