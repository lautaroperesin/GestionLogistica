using GestionLogisticaBackend.Models;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LogisticaBackend.Data
{
    public class LogisticaContext : DbContext
    {
        public LogisticaContext(DbContextOptions<LogisticaContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Conductor> Conductores { get; set; }
        public DbSet<Envio> Envios { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<EstadoEnvio> EstadosEnvio { get; set; }
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<TipoCarga> TiposCarga { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }
        public DbSet<MovimientoCaja> MovimientosCaja { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relaciones específicas para evitar ciclos
            modelBuilder.Entity<Envio>()
                .HasOne(e => e.Origen)
                .WithMany(u => u.EnviosOrigen)
                .HasForeignKey(e => e.IdOrigen)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Envio>()
                .HasOne(e => e.Destino)
                .WithMany(u => u.EnviosDestino)
                .HasForeignKey(e => e.IdDestino)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar índices únicos
            modelBuilder.Entity<Conductor>()
                .HasIndex(c => c.Dni)
                .IsUnique();

            modelBuilder.Entity<Vehiculo>()
                .HasIndex(v => v.Patente)
                .IsUnique();

            modelBuilder.Entity<Pais>()
                .HasIndex(p => p.Nombre)
                .IsUnique();

            modelBuilder.Entity<TipoCarga>()
                .HasIndex(tc => tc.Nombre)
                .IsUnique();

            modelBuilder.Entity<Factura>()
                .HasIndex(f => f.NumeroFactura)
                .IsUnique();

            // Configurar filtros globales para soft delete
            modelBuilder.Entity<Cliente>().HasQueryFilter(c => !c.Deleted);
            modelBuilder.Entity<Conductor>().HasQueryFilter(c => !c.Deleted);
            modelBuilder.Entity<Vehiculo>().HasQueryFilter(v => !v.Deleted);
            modelBuilder.Entity<Envio>().HasQueryFilter(e => !e.Deleted);
            modelBuilder.Entity<Factura>().HasQueryFilter(f => !f.Deleted);
            modelBuilder.Entity<Pais>().HasQueryFilter(p => !p.Deleted);
            modelBuilder.Entity<Provincia>().HasQueryFilter(p => !p.Deleted);
            modelBuilder.Entity<Localidad>().HasQueryFilter(l => !l.Deleted);
            modelBuilder.Entity<Ubicacion>().HasQueryFilter(u => !u.Deleted);
            modelBuilder.Entity<MovimientoCaja>().HasQueryFilter(m => !m.Deleted);

            // Configurar precisión decimal
            modelBuilder.Entity<Vehiculo>()
                .Property(v => v.CapacidadKg)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Envio>()
                .Property(e => e.PesoKg)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Envio>()
                .Property(e => e.CostoTotal)
                .HasPrecision(10, 2);

            // Configurar conversiones de enumeraciones
            modelBuilder.Entity<Factura>()
                .Property(f => f.Estado)
                .HasConversion<int>();

            modelBuilder.Entity<Vehiculo>()
                .Property(v => v.Estado)
                .HasConversion<int>();


            // Configurar valor por defecto en entidades con soft delete
            modelBuilder.Entity<Pais>().Property(p => p.Deleted).HasDefaultValue(false);
            modelBuilder.Entity<Provincia>().Property(p => p.Deleted).HasDefaultValue(false);
            modelBuilder.Entity<Localidad>().Property(l => l.Deleted).HasDefaultValue(false);
            modelBuilder.Entity<Cliente>().Property(c => c.Deleted).HasDefaultValue(false);
            modelBuilder.Entity<Conductor>().Property(c => c.Deleted).HasDefaultValue(false);
            modelBuilder.Entity<Vehiculo>().Property(v => v.Deleted).HasDefaultValue(false);
            modelBuilder.Entity<Envio>().Property(e => e.Deleted).HasDefaultValue(false);
            modelBuilder.Entity<Factura>().Property(f => f.Deleted).HasDefaultValue(false);
            modelBuilder.Entity<Ubicacion>().Property(u => u.Deleted).HasDefaultValue(false);
            modelBuilder.Entity<MovimientoCaja>().Property(m => m.Deleted).HasDefaultValue(false);


            // Seed data para estados
            modelBuilder.Entity<EstadoEnvio>().HasData(
                new EstadoEnvio { IdEstado = 1, Nombre = "Pendiente" },
                new EstadoEnvio { IdEstado = 2, Nombre = "En preparación" },
                new EstadoEnvio { IdEstado = 3, Nombre = "En tránsito" },
                new EstadoEnvio { IdEstado = 4, Nombre = "Entregado" },
                new EstadoEnvio { IdEstado = 5, Nombre = "Demorado" },
                new EstadoEnvio { IdEstado = 6, Nombre = "Incidencia" },
                new EstadoEnvio { IdEstado = 7, Nombre = "Cancelado" }
            );

            // Seed data para métodos de pago
            modelBuilder.Entity<MetodoPago>().HasData(
                new MetodoPago { IdMetodoPago = 1, Nombre = "Efectivo" },
                new MetodoPago { IdMetodoPago = 2, Nombre = "Transferencia"},
                new MetodoPago { IdMetodoPago = 3, Nombre = "Cheque" },
                new MetodoPago { IdMetodoPago = 4, Nombre = "Tarjeta de Crédito" },
                new MetodoPago { IdMetodoPago = 5, Nombre = "Tarjeta de Débito" }
            );
        }
    }
}
