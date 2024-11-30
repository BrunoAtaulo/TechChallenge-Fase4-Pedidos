using App.Domain.Models;
using App.Infra.Data.Context;
using Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
namespace App.Domain.Interfaces
{
    public class PedidoRepository : IPedidosRepository
    {
        private readonly MySQLContext _dbContext;

        public PedidoRepository(MySQLContext context)
        {
            _dbContext = context;
        }
        public async Task<IList<PedidoBD>> GetPedidosAsync(int? idPedido, EnumPedidoStatus? pedidoStatus, EnumPedidoPagamento? pedidoPagamento)
        {
            var query = _dbContext.Pedidos.AsQueryable();

            if (idPedido.HasValue)
                query = query.Where(p => p.Id == idPedido);

            if (pedidoStatus.HasValue)
                query = query.Where(p => p.PedidoStatusId == (int)pedidoStatus.Value);

            if (pedidoPagamento.HasValue)
                query = query.Where(p => p.PedidoPagamentoId == (int)pedidoPagamento.Value);

            return await query.ToListAsync();
        }

        public async Task<PedidoBD> GetPedidosByIdAsync(int idPedido)
        {
            var query = _dbContext.Pedidos.AsQueryable();

            query = query.Where(p => p.Id == idPedido);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<bool> UpdatePedidoAsync(PedidoBD pedido)
        {
            _dbContext.Pedidos.Update(pedido);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<PedidoBD> GetPedidosById(int idPedido)
        {
            return await _dbContext.Pedidos.FirstOrDefaultAsync(p => p.Id == idPedido);
        }

        public async Task PostPedido(PedidoBD Pedido)
        {
            await _dbContext.Pedidos.AddAsync(Pedido);
            await _dbContext.SaveChangesAsync();
        }


    }

}