using System;
using System.Collections.Generic;

namespace App.Application.ViewModels.Request
{
    public class PostPedidoRequest
    {

        public int IdCliente { get; set; }
        public DateTime DataPedido { get; set; }
        public int PedidoStatusId { get; set; }
        public int PedidoPagamentoId { get; set; }
        public List<int> Produtos { get; set; }
    }
}
