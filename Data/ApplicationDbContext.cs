namespace CP2_SOA.Data;

using CP2_SOA.Entities;
using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<PedidoItem> PedidoItens { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pagamento> Pagamentos { get; set; }
    public DbSet<Notificacao> Notificacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.ToTable("pedidos");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Cliente)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(p => p.ValorTotal)
                .HasColumnType("decimal(10,2)");

            entity.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(50);

            entity.HasMany(p => p.Itens)
                .WithOne()
                .HasForeignKey(i => i.PedidoId);
        });

        modelBuilder.Entity<PedidoItem>(entity =>
        {
            entity.ToTable("pedido_itens");
            entity.HasKey(i => i.Id);

            entity.Property(i => i.PrecoUnitario)
                .HasColumnType("decimal(10,2)");

            entity.Property(i => i.Subtotal)
                .HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.ToTable("produtos");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(p => p.Preco)
                .HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.ToTable("pagamentos");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Valor)
                .HasColumnType("decimal(10,2)");

            entity.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Notificacao>(entity =>
        {
            entity.ToTable("notificacoes");
            entity.HasKey(n => n.Id);

            entity.Property(n => n.Tipo)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(n => n.Mensagem)
                .IsRequired()
                .HasMaxLength(255);
        });
    }
}
