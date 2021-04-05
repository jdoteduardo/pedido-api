using CpmPedidos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CpmPedidos.Repository.Maps
{
    public class EnderecoMap : BaseDomainMap<Endereco>
    {
        public EnderecoMap() : base("tb_endereco") { }

        public override void Configure(EntityTypeBuilder<Endereco> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Tipo).HasColumnName("tipo").IsRequired();
            builder.Property(x => x.Logradouro).HasColumnName("logradouro").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Bairro).HasColumnName("bairro").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Numero).HasColumnName("numero").HasMaxLength(10).IsRequired();
            builder.Property(x => x.Complemento).HasColumnName("complemento").HasMaxLength(50);
            builder.Property(x => x.Cep).HasColumnName("cep").HasMaxLength(8);
            builder.Property(x => x.IdCidade).HasColumnName("id_cidade").IsRequired();
            builder.Property(x => x.IdCidade).HasColumnName("id_cidade").IsRequired();


            builder.HasOne(x => x.Cidade).WithMany().HasForeignKey(x => x.IdCidade);
            builder.HasOne(x => x.Cliente).WithOne(x => x.Endereco).HasForeignKey<Cliente>(x => x.IdEndereco);
        }
    }
}
