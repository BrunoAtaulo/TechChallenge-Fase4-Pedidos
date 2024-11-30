using System;
using System.Collections.Generic;

namespace App.Domain.Models
{
    public class PedidoBD
    {

        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int PedidoStatusId { get; set; }
        public int PedidoPagamentoId { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public List<Produto> Produtos { get; set; }
        public virtual Cliente Cliente { get; set; }
        public string StatusPedido { get; set; }

        public PedidoBD(int clienteId, DateTime dataPedido, int pedidoStatusId, int pedidoPagamentoId)
        {

            ClienteId = clienteId;
            DataPedido = dataPedido;
            PedidoStatusId = pedidoStatusId;
            PedidoPagamentoId = pedidoPagamentoId;
            Produtos = new List<Produto>();
            // ValidateEntity();
        }



        /*  public void ValidateEntity()
          {
              AssertionConcern.AssertArgumentNotEmpty(StatusPedido, "O status do pedido é obrigatório!");

          }*/
    }
}
