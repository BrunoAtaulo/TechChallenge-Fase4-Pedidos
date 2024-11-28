using App.Application.ViewModels.Enuns;
using System;

namespace App.Application.ViewModels.Response
{
    public class Pedido
    {

        public Pedido(Domain.Models.PedidoBD _pedido)
        {

            IdCliente = _pedido.ClienteId;
            IdPedido = _pedido.Id;
            PedidoStatus = (EnumPedidoStatus?)_pedido.PedidoStatusId;
            PedidoPagamento = (EnumPedidoPagamento?)_pedido.PedidoPagamentoId;
            DataPedido = _pedido.DataPedido;
        }

        public int IdCliente { get; set; }
        public int IdPedido { get; set; }

        public EnumPedidoStatus? PedidoStatus { get; set; }
        public EnumPedidoPagamento? PedidoPagamento { get; set; }
        public DateTime DataPedido { get; set; }

    }
}
