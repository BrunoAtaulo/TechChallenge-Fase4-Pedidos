using App.Application.ViewModels.Request;
using App.Application.ViewModels.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IPedidosService
    {
        Task<bool> UpdatePedido(FiltroPedidoById filtro);
        Task<IList<Pedido>> GetPedidos(FiltroPedidos filtro);
        Task<Pedido> GetPedidoById(FiltroPedidoById filtro);

        Task<FiltroPedidoById> PostPedido(PostPedidoRequest input);

    }
}
