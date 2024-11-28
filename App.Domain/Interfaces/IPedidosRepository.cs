using App.Domain.Models;
using Domain.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Domain.Interfaces
{
    public interface IPedidosRepository
    {

        Task<IList<PedidoBD>> GetPedidosAsync(int? idPedido, EnumPedidoStatus? pedidoStatus, EnumPedidoPagamento? pedidoPagamento);
        Task<bool> UpdatePedidoAsync(PedidoBD pedido);
        Task<PedidoBD> GetPedidosByIdAsync(int idPedido);


    }
}
