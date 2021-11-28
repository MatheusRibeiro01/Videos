using Filmes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Filme.Data.Map
{

    public sealed class VideoMap : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired();
            
            builder.Property(x => x.Titulo)
                .HasColumnName("Titulo")
                .IsRequired();
            
            builder.Property(x => x.Descricao)
                .HasColumnName("Descricao")
                .IsRequired();
            
            builder.Property(x => x.Url)
                .HasColumnName("Url")
                .IsRequired();
            
            
            

        }
    }
}