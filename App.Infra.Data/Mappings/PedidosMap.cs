using App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infra.Data.Mappings
{
    public class PedidosMap : IEntityTypeConfiguration<PedidoBD>
    {
        public PedidosMap()
        {

        }
        public void Configure(EntityTypeBuilder<PedidoBD> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ClienteId)
                .IsRequired();

            builder.Property(p => p.DataPedido)
                .IsRequired()
                .HasColumnType("dateTime");

            builder.Property(p => p.PedidoStatusId)
                .IsRequired();

            builder.Property(p => p.PedidoPagamentoId)
                .IsRequired();

            builder.ToTable("Pedidos");
        }
    }
}
