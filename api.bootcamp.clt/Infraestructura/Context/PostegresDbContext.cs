using Api.BootCamp.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Api.BootCamp.Infraestructura.Context
{
    public class PostegresDbContext : DbContext
    {
        public PostegresDbContext(DbContextOptions<PostegresDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categorias> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Categorias>(entity =>
                {
                    entity.ToTable("categorias");

                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Id).HasColumnName("id");
                    entity .Property(e => e.Nombre).HasColumnName("nombre");
                    entity.Property(e => e.Descripcion).HasColumnName("descripcion");
                    entity.Property(e => e.Estado).HasColumnName("estado");
                });

            modelBuilder
                .Entity<Producto>(entity =>
                {
                    entity.ToTable("productos");

                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Id) .HasColumnName("id");
                    entity.Property(e => e.Codigo).HasColumnName("codigo");
                    entity.Property(e => e.Nombre).HasColumnName("nombre");
                    entity.Property(e => e.Descripcion).HasColumnName("descripcion");
                    entity.Property(e => e.Precio).HasColumnName("precio");
                    entity.Property(e => e.Activo).HasColumnName("activo");
                    entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
                    entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
                    entity.Property(e => e.FechaActualizacion).HasColumnName("fecha_actualizacion");
                    entity .Property(e => e.CantidadStock).HasColumnName("cantidad_stock");
                });
        }
    }
}
