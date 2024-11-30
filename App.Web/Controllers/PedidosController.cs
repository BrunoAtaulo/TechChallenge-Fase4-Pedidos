using App.Application.Interfaces;
using App.Application.ViewModels.Request;
using App.Application.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace Api.Controllers
{
    [ApiController]
    [Route("pedidos/")]
    public class PedidosController : ControllerBase
    {

        private readonly IPedidosService _pedidosService;

        public PedidosController(IPedidosService pedidosService)
        {
            _pedidosService = pedidosService;
        }


        #region [GET/pedidos]
        [SwaggerResponse(200, "Consulta executada com sucesso!", typeof(List<Pedido>))]
        [SwaggerResponse(204, "Requisição concluída, porém não há dados de retorno!")]
        [SwaggerResponse(400, "Condição prévia dada em um ou mais dos campos avaliado como falsa.")]
        [HttpGet("")]
        [SwaggerOperation(
            Summary = "Busca todos os pedidos realizados.",
             Description = @"Endpoint para buscar todos os pedidos que foram realizados. A busca pode ser feita pelos filtros abaixo:</br></br>
                            <b>Parâmetros de entrada:</b></br></br>                           
                             &bull; <b>idPedido</b>:  Id do pedido. &rArr; <font color='green'><b>Opcional</b></font><br>
                        &bull; <b>pedidoStatus</b>: Status atual do pedido. &rArr; <font color='green'><b>Opcional</b></font><br>
                             <strong> 1 = </strong> Recebido<br/>
                             <strong> 2 = </strong> Em preparação<br/>
                             <strong> 3 = </strong>  Pronto<br/>
                             <strong> 4 = </strong> Finalizado                 
                        &bull; <b>pedidoPagamento</b>: Status atual do pagamento. &rArr; <font color='green'><b>Opcional</b></font><br>
                             <strong> 1 = </strong> Pendente<br/>
                             <strong> 2 = </strong> Pago<br/>
                             <strong> 3 = </strong> Cancelado   
                        
            ",
            Tags = new[] { "Pedidos" }
        )]
        [Consumes("application/json")]
        public async Task<IActionResult> GetPedidos([FromQuery] FiltroPedidos filtro)
        {

            var rtn = await _pedidosService.GetPedidos(filtro);
            if (rtn == null)
                return NoContent();
            return Ok(rtn);
        }
        #endregion

        #region PATCH/checkout/{idPedido}
        [SwaggerResponse(200, "A solicitação foi atendida e resultou na criação de um ou mais novos recursos.")]
        [SwaggerResponse(400, "A solicitação não pode ser entendida pelo servidor devido a sintaxe malformada!")]
        [SwaggerResponse(401, "Requisição requer autenticação do usuário!")]
        [SwaggerResponse(403, "Privilégios insuficientes!")]
        [SwaggerResponse(404, "O recurso solicitado não existe!")]
        [SwaggerResponse(412, "Condição prévia dada em um ou mais dos campos avaliado como falsa!")]
        [SwaggerResponse(500, "Servidor encontrou uma condição inesperada!")]
        [HttpPatch("{idPedido}/checkout")]
        [SwaggerOperation(
          Summary = "Endpoint para Finalizar o Pagamento do Pedido",
          Description = @"Endpoint atualizar o pedido.</br></br>
                            <b>Parâmetros de entrada:</b></br></br>
                             &bull; <b>idPedido</b>:  Id do pedido que irá ser atualizado. &rArr; <font color='red'><b>Obrigatório</b></font><br>

",
          Tags = new[] { "Pedidos" }
          )]
        [Consumes("application/json")]
        public async Task<IActionResult> PatchPedido([FromRoute] FiltroPedidoById filtro)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _pedidosService.UpdatePedido(filtro);

            if (!success)
                return NotFound("Pedido não encontrado.");

            return Ok("Pedido Pago com sucesso.");
        }
        #endregion

        #region [GET/pedidos/idPedido]
        [SwaggerResponse(200, "Consulta executada com sucesso!", typeof(Pedido))]
        [SwaggerResponse(204, "Requisição concluída, porém não há dados de retorno!")]
        [SwaggerResponse(400, "Condição prévia dada em um ou mais dos campos avaliado como falsa.")]
        [HttpGet("{idPedido}")]
        [SwaggerOperation(
            Summary = "Busca um pedido.",
             Description = @"Endpoint para buscar um pedido que foi realizado. A busca pode ser feita pelos filtros abaixo:</br></br>
                               <b>Parâmetros de entrada:</b></br></br>                           
                                &bull; <b>idPedido</b>:  Id do pedido. &rArr; <font color='red'><b>Obrigatório</b></font><br>


               ",
            Tags = new[] { "Pedidos" }
        )]
        [Consumes("application/json")]
        public async Task<IActionResult> GetPedidos([FromRoute] FiltroPedidoById filtro)
        {

            var rtn = await _pedidosService.GetPedidoById(filtro);
            if (rtn == null)
                return NoContent();
            return Ok(rtn);
        }
        #endregion

        #region POST/pedidos
        [SwaggerResponse(201, "A solicitação foi atendida e resultou na criação de um novo pedido.", typeof(int))]
        [SwaggerResponse(400, "A solicitação não pode ser entendida pelo servidor devido a sintaxe malformada!")]
        [SwaggerResponse(401, "Requisição requer autenticação do usuário!")]
        [SwaggerResponse(403, "Privilégios insuficientes!")]
        [SwaggerResponse(404, "O recurso solicitado não existe!")]
        [SwaggerResponse(400, "Condição prévia dada em um ou mais dos campos avaliado como falsa!")]
        [SwaggerResponse(500, "Servidor encontrou uma condição inesperada!")]
        [HttpPost("")]
        [SwaggerOperation(
            Summary = "Cria um novo pedido com lista de produtos.",
            Description = @"Endpoint para criar um novo pedido com base no idCliente e na lista de produtos fornecida no corpo da requisição. Retorna o id do pedido criado.",
            Tags = new[] { "Pedidos" }
        )]
        [Consumes("application/json")]
        public async Task<IActionResult> PostPedido([FromBody] PostPedidoRequest input)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var idPedidoCriado = await _pedidosService.PostPedido(input);

            return new ObjectResult(idPedidoCriado) { StatusCode = StatusCodes.Status201Created };
        }
        #endregion



    }
}