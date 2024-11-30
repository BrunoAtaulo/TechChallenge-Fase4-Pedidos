using App.Application.Interfaces;
using App.Application.ViewModels.Request;
using App.Application.ViewModels.Response;
using App.Domain.Interfaces;
using App.Domain.Models;
using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PedidosService : IPedidosService
    {
        private readonly IPedidosRepository _repository;

        public PedidosService(IPedidosRepository repository)
        {
            _repository = repository;
        }


        public async Task<IList<Pedido>> GetPedidos(FiltroPedidos filtro)
        {
            var lstPedidos = await _repository.GetPedidosAsync(filtro.IdPedido, (EnumPedidoStatus?)filtro.PedidoStatus, (EnumPedidoPagamento?)filtro.PedidoPagamento);
            if (lstPedidos == null)
                return null;
            return lstPedidos.Select(s => new Pedido(s)).ToList();
        }


        public async Task<bool> UpdatePedido(FiltroPedidoById filtro)
        {
            var pedido = await _repository.GetPedidosByIdAsync(filtro.idPedido);
            if (pedido == null)
                return false;
            pedido.PedidoPagamentoId = (int)EnumPedidoPagamento.Pago;
            return await _repository.UpdatePedidoAsync(pedido);
        }

        public async Task<Pedido> GetPedidoById(FiltroPedidoById filtro)
        {
            var pedido = await _repository.GetPedidosByIdAsync(filtro.idPedido);
            if (pedido == null)
                return null;

            return new Pedido(pedido);
        }

        public async Task<FiltroPedidoById> PostPedido(PostPedidoRequest input)
        {
            var item = new PedidoBD(input.IdCliente, input.DataPedido, input.PedidoStatusId, input.PedidoPagamentoId);

            await _repository.PostPedido(item);

            return new FiltroPedidoById
            {
                idPedido = item.Id,
            };
        }
    }
}