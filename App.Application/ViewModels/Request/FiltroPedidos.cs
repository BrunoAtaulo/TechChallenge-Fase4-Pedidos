using App.Application.ViewModels.Enuns;

namespace App.Application.ViewModels.Request
{
    public class FiltroPedidos
    {
        public int? IdPedido { get; set; }
        public EnumPedidoStatus? PedidoStatus { get; set; }
        public EnumPedidoPagamento? PedidoPagamento { get; set; }
    }
}
