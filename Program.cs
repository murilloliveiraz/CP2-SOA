using CP2_SOA.Data;
using CP2_SOA.Entities;
using CP2_SOA.Exceptions;
using CP2_SOA.Repositories;
using CP2_SOA.Repositories.Interfaces;
using CP2_SOA.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseSnakeCaseNamingConvention()
);

builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
builder.Services.AddScoped<INotificacaoRepository, NotificacaoRepository>();

builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<EstoqueService>();
builder.Services.AddScoped<PagamentoService>();
builder.Services.AddScoped<NotificacaoService>();

var app = builder.Build();

// ==========================
// MIGRATION + SEED
// ==========================
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    context.Database.Migrate();

    // CLIENTES
    if (!context.Clientes.Any())
    {
        context.Clientes.AddRange(
            new Cliente
            {
                Nome = "Ana Souza",
                Email = "ana@email.com"
            },
            new Cliente
            {
                Nome = "Bruno Lima",
                Email = "bruno@email.com"
            },
            new Cliente
            {
                Nome = "Carla Mendes",
                Email = "carla@email.com"
            }
        );
    }

    // PRODUTOS
    if (!context.Produtos.Any())
    {
        context.Produtos.AddRange(
            new Produto
            {
                Nome = "Hamburguer Artesanal",
                Descricao = "Pao, carne, queijo e molho especial",
                Preco = 32.90m,
                QuantidadeEstoque = 10
            },
            new Produto
            {
                Nome = "Pizza Calabresa",
                Descricao = "Pizza media de calabresa",
                Preco = 49.90m,
                QuantidadeEstoque = 5
            },
            new Produto
            {
                Nome = "Refrigerante 2L",
                Descricao = "Refrigerante cola 2 litros",
                Preco = 12.00m,
                QuantidadeEstoque = 20
            },
            new Produto
            {
                Nome = "Batata Frita",
                Descricao = "Porcao media de batata frita",
                Preco = 18.50m,
                QuantidadeEstoque = 8
            }
        );
    }

    context.SaveChanges();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        switch (error)
        {
            case ProdutoNotFoundException:
                context.Response.StatusCode = 404;
                break;

            case EstoqueInsuficienteException:
                context.Response.StatusCode = 400;
                break;

            case PagamentoRecusadoException:
                context.Response.StatusCode = 400;
                break;

            default:
                context.Response.StatusCode = 500;
                break;
        }

        await context.Response.WriteAsync(error?.Message ?? "Erro interno");
    });
});

app.UseAuthorization();

app.MapControllers();

app.Run();
