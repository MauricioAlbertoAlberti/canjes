using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Canjes.Models;

public partial class CanjeContext : DbContext
{
    public CanjeContext()
    {
    }

    public CanjeContext(DbContextOptions<CanjeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<Barrio> Barrios { get; set; }

    public virtual DbSet<Canje> Canjes { get; set; }

    public virtual DbSet<Conservacione> Conservaciones { get; set; }

    public virtual DbSet<DetallesCanje> DetallesCanjes { get; set; }

    public virtual DbSet<EstadosLibro> EstadosLibros { get; set; }

    public virtual DbSet<EstadosPedido> EstadosPedidos { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Idioma> Idiomas { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Notificacione> Notificaciones { get; set; }

    public virtual DbSet<PedidosCanje> PedidosCanjes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Canje;User=sa;Password=sqladmin;Trusted_Connection=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.IdAutor);

            entity.Property(e => e.IdAutor)
                .ValueGeneratedNever()
                .HasColumnName("idAutor");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Biografia)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("biografia");
            entity.Property(e => e.Generos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("generos");
            entity.Property(e => e.Libros)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("libros");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nacionalidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RutaImagen)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("rutaImagen");
        });

        modelBuilder.Entity<Barrio>(entity =>
        {
            entity.HasKey(e => e.IdBarrio);

            entity.Property(e => e.IdBarrio)
                .ValueGeneratedNever()
                .HasColumnName("idBarrio");
            entity.Property(e => e.NombreBarrio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreBarrio");
        });

        modelBuilder.Entity<Canje>(entity =>
        {
            entity.HasKey(e => e.IdCanje);

            entity.Property(e => e.IdCanje).HasColumnName("idCanje");
            entity.Property(e => e.FechaCanje)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaCanje");
            entity.Property(e => e.IdPedidoCanje).HasColumnName("idPedidoCanje");

            entity.HasOne(d => d.IdPedidoCanjeNavigation).WithMany(p => p.Canjes)
                .HasForeignKey(d => d.IdPedidoCanje)
                .HasConstraintName("FK_Canjes_PedidosCanjes");
        });

        modelBuilder.Entity<Conservacione>(entity =>
        {
            entity.HasKey(e => e.IdConservacion);

            entity.Property(e => e.IdConservacion).HasColumnName("idConservacion");
            entity.Property(e => e.DescripcionConservacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcionConservacion");
            entity.Property(e => e.Orden).HasColumnName("orden");
        });

        modelBuilder.Entity<DetallesCanje>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCanje);

            entity.Property(e => e.IdDetalleCanje).HasColumnName("idDetalleCanje");
            entity.Property(e => e.ComentarioCanje)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("comentarioCanje");
            entity.Property(e => e.IdCanje).HasColumnName("idCanje");
            entity.Property(e => e.IdLibroEntregado).HasColumnName("idLibroEntregado");
            entity.Property(e => e.IdLibroRecibido).HasColumnName("idLibroREcibido");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.ValoracionCanje).HasColumnName("valoracionCanje");

            entity.HasOne(d => d.IdCanjeNavigation).WithMany(p => p.DetallesCanjes)
                .HasForeignKey(d => d.IdCanje)
                .HasConstraintName("FK_DetallesCanjes_Canjes");
        });

        modelBuilder.Entity<EstadosLibro>(entity =>
        {
            entity.HasKey(e => e.IdEstadoLibro);

            entity.Property(e => e.IdEstadoLibro)
                .ValueGeneratedNever()
                .HasColumnName("idEstadoLibro");
            entity.Property(e => e.DescripcionEstadoLibro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcionEstadoLibro");
            entity.Property(e => e.Orden).HasColumnName("orden");
        });

        modelBuilder.Entity<EstadosPedido>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPedido);

            entity.Property(e => e.IdEstadoPedido)
                .ValueGeneratedNever()
                .HasColumnName("idEstadoPedido");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero);

            entity.Property(e => e.IdGenero)
                .ValueGeneratedNever()
                .HasColumnName("idGenero");
            entity.Property(e => e.DescripcionGenero)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcionGenero");
            entity.Property(e => e.Orden).HasColumnName("orden");
        });

        modelBuilder.Entity<Idioma>(entity =>
        {
            entity.HasKey(e => e.IdIdioma);

            entity.Property(e => e.IdIdioma).HasColumnName("idIdioma");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.IdLibro);

            entity.Property(e => e.IdLibro).HasColumnName("idLibro");
            entity.Property(e => e.AnioPublicacion).HasColumnName("anioPublicacion");
            entity.Property(e => e.Autor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("autor");
            entity.Property(e => e.Baja)
                .HasDefaultValue(false)
                .HasColumnName("baja");
            entity.Property(e => e.Bolsillo)
                .HasDefaultValue(false)
                .HasColumnName("bolsillo");
            entity.Property(e => e.Deseado).HasColumnName("deseado");
            entity.Property(e => e.Edicion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("edicion");
            entity.Property(e => e.Editorial)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("editorial");
            entity.Property(e => e.FechaResgistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaResgistro");
            entity.Property(e => e.IdConservacion).HasColumnName("idConservacion");
            entity.Property(e => e.IdEstadoLibro)
                .HasDefaultValue(1)
                .HasColumnName("idEstadoLibro");
            entity.Property(e => e.IdGenero).HasColumnName("idGenero");
            entity.Property(e => e.IdIdioma)
                .HasDefaultValue(1)
                .HasColumnName("idIdioma");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Ilustrado)
                .HasDefaultValue(false)
                .HasColumnName("ilustrado");
            entity.Property(e => e.ImagenNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("imagenNombre");
            entity.Property(e => e.ImagenRuta)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("imagenRuta");
            entity.Property(e => e.Isbn).HasColumnName("isbn");
            entity.Property(e => e.NumeroPaginas).HasColumnName("numeroPaginas");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("observaciones");
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pais");
            entity.Property(e => e.Resenia)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("resenia");
            entity.Property(e => e.TapaDura)
                .HasDefaultValue(false)
                .HasColumnName("tapaDura");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("titulo");
            entity.Property(e => e.Traduccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("traduccion");

            entity.HasOne(d => d.IdConservacionNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdConservacion)
                .HasConstraintName("FK_Libros_Conservaciones");

            entity.HasOne(d => d.IdEstadoLibroNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdEstadoLibro)
                .HasConstraintName("FK_Libros_EstadosLibros");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdGenero)
                .HasConstraintName("FK_Libros_Generos");

            entity.HasOne(d => d.IdIdiomaNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdIdioma)
                .HasConstraintName("FK_Libros_Idiomas");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Libros_Usuarios");
        });

        modelBuilder.Entity<Notificacione>(entity =>
        {
            entity.HasKey(e => e.IdNotificacion);

            entity.Property(e => e.IdNotificacion).HasColumnName("idNotificacion");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Notificacion)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("notificacion");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notificaciones_Usuarios");
        });

        modelBuilder.Entity<PedidosCanje>(entity =>
        {
            entity.HasKey(e => e.IdPedidoCanje);

            entity.Property(e => e.IdPedidoCanje).HasColumnName("idPedidoCanje");
            entity.Property(e => e.FechaPedido)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaPedido");
            entity.Property(e => e.IdEstadoPedido)
                .HasDefaultValue(1)
                .HasColumnName("idEstadoPedido");
            entity.Property(e => e.IdLibroReceptor).HasColumnName("idLibroReceptor");
            entity.Property(e => e.IdUsuarioEmisor).HasColumnName("idUsuarioEmisor");

            entity.HasOne(d => d.IdEstadoPedidoNavigation).WithMany(p => p.PedidosCanjes)
                .HasForeignKey(d => d.IdEstadoPedido)
                .HasConstraintName("FK_PedidosCanjes_EstadosPedidos");

            entity.HasOne(d => d.IdLibroReceptorNavigation).WithMany(p => p.PedidosCanjes)
                .HasForeignKey(d => d.IdLibroReceptor)
                .HasConstraintName("FK_PedidosCanjes_Libros");

            entity.HasOne(d => d.IdUsuarioEmisorNavigation).WithMany(p => p.PedidosCanjes)
                .HasForeignKey(d => d.IdUsuarioEmisor)
                .HasConstraintName("FK_PedidosCanjes_Usuarios");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.Property(e => e.IdRol)
                .ValueGeneratedNever()
                .HasColumnName("idRol");
            entity.Property(e => e.DescipcionRol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descipcionRol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreRol");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Baja)
                .HasDefaultValue(false)
                .HasColumnName("baja");
            entity.Property(e => e.Calificacion).HasColumnName("calificacion");
            entity.Property(e => e.Celular)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("celular");
            entity.Property(e => e.Comentario)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("comentario");
            entity.Property(e => e.Contacto)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("contacto");
            entity.Property(e => e.Contacto2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contacto2");
            entity.Property(e => e.Contacto3)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contacto3");
            entity.Property(e => e.CuentaConfirmada)
                .HasDefaultValue(false)
                .HasColumnName("cuentaConfirmada");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaAlta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaAlta");
            entity.Property(e => e.IdBarrio).HasColumnName("idBarrio");
            entity.Property(e => e.IdRol)
                .HasDefaultValue(1)
                .HasColumnName("idRol");
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mail");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreUsuario");
            entity.Property(e => e.Pass)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("pass");
            entity.Property(e => e.RestablecerPass)
                .HasDefaultValue(false)
                .HasColumnName("restablecerPass");
            entity.Property(e => e.Token)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("token");

            entity.HasOne(d => d.IdBarrioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdBarrio)
                .HasConstraintName("FK_Usuarios_Barrios");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK_Usuarios_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
