# CP2 — Sistema de Pedidos

## Integrantes

- Ana Clara Melo - RM559021 
- David Murillo de Oliveira Soares - RM559078 
- Lucas Serrano - RM555170 
- Yasmim Gonçalves - RM559147

## Turma

3ESPY

---

# Descrição do Sistema

O projeto consiste em uma API REST para gerenciamento de pedidos de um sistema de delivery, desenvolvida como parte do Checkpoint 2 da disciplina de SOA/Arquitetura.

A aplicação implementa os domínios:

- Pedido
- Produto
- Estoque
- Pagamento
- Notificação

O sistema foi desenvolvido seguindo princípios de arquitetura em camadas, separação de responsabilidades e baixo acoplamento.

---

# Tecnologias Utilizadas

## Back-end

- C#
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core

## Banco de Dados

- PostgreSQL
- Docker

## Documentação

- Swagger/OpenAPI

---

# Arquitetura da Aplicação

O sistema foi estruturado utilizando arquitetura em camadas:

```text
Controller
   ↓
Service
   ↓
Repository
   ↓
PostgreSQL
````

## Responsabilidade de cada camada

### Controllers

Responsáveis por:

* Receber requisições HTTP
* Validar entrada básica
* Retornar respostas HTTP

Nenhuma regra de negócio foi implementada nos controllers.

---

### Services

Responsáveis por:

* Implementar regras de negócio
* Orquestrar o fluxo do pedido
* Integrar estoque, pagamento e notificações

---

### Repositories

Responsáveis por:

* Persistência de dados
* Comunicação com PostgreSQL via Entity Framework

---

### Entities

Representam os modelos do domínio:

* Pedido
* Produto
* PedidoItem
* Pagamento
* Notificacao
* Cliente

---

# Fluxo do Pedido

Ao criar um pedido, o sistema executa:

1. Validação dos itens
2. Consulta dos produtos
3. Verificação do estoque
4. Cálculo do valor total
5. Processamento do pagamento
6. Atualização do status
7. Registro de notificação

Fluxo de status:

```text
CRIADO
↓
AGUARDANDO_PAGAMENTO
↓
PAGO
↓
FINALIZADO

ou

CANCELADO
```

---

# Regras de Negócio

## Pedido

* Não pode ser criado sem itens
* Deve possuir cliente e valor total
* Deve seguir o fluxo de status definido

## Estoque

* Produtos sem estoque não podem ser vendidos
* O estoque é reduzido após pagamento aprovado

## Pagamento

* O pedido só é finalizado após aprovação do pagamento
* Pagamentos podem ser aprovados ou recusados

## Notificação

O sistema registra notificações para:

* pagamento aprovado
* pedido finalizado
* pedido cancelado

---

# Tratamento de Exceções

O sistema possui tratamento para:

* Pedido não encontrado
* Produto não encontrado
* Estoque insuficiente
* Pagamento recusado
* Pedido sem itens

As exceptions são tratadas globalmente utilizando middleware.

---

# Endpoints da API

## Pedidos

| Método | Endpoint                 | Descrição        |
| ------ | ------------------------ | ---------------- |
| POST   | /api/pedidos             | Criar pedido     |
| GET    | /api/pedidos/{id}        | Buscar pedido    |
| PATCH  | /api/pedidos/{id}/status | Atualizar status |

---

## Produtos

| Método | Endpoint           | Descrição       |
| ------ | ------------------ | --------------- |
| GET    | /api/produtos      | Listar produtos |
| GET    | /api/produtos/{id} | Buscar produto  |

---

# Status HTTP Utilizados

| Status | Descrição              |
| ------ | ---------------------- |
| 200    | Sucesso                |
| 201    | Criado com sucesso     |
| 400    | Requisição inválida    |
| 404    | Recurso não encontrado |
| 500    | Erro interno           |

---

# Como Executar o Projeto

## Pré-requisitos

* Docker Desktop
* .NET 8 SDK

---

## 1. Clonar o repositório

```bash
git clone [LINK_REPOSITORIO]
```

---

## 2. Subir o PostgreSQL

```bash
docker compose up -d
```

---

## 3. Executar as migrations

```bash
dotnet ef database update
```

---

## 4. Executar a aplicação

```bash
dotnet run
```

---

## 5. Abrir Swagger

```text
https://localhost:xxxx/swagger
```

---

# Seed Automático

O projeto possui seed automático de:

* clientes
* produtos

Os dados são inseridos automaticamente na inicialização da aplicação caso o banco esteja vazio.

---

# Organização das Responsabilidades

A comunicação entre os componentes foi organizada utilizando separação de responsabilidades e arquitetura em camadas.

O Controller é responsável apenas pela comunicação HTTP.

O PedidoService centraliza a orquestração do fluxo do pedido, mas delega responsabilidades específicas para outros serviços:

* EstoqueService → validação e redução de estoque
* PagamentoService → processamento de pagamento
* NotificacaoService → registro de notificações

Os Repositories ficam responsáveis exclusivamente pelo acesso ao banco de dados.

Dessa forma, evitamos concentração excessiva de responsabilidades em uma única classe.

---

# Impacto da Indisponibilidade do Serviço de Pagamento

Caso o componente de pagamento fique indisponível em um cenário real, o sistema não conseguiria finalizar pedidos.

Na arquitetura atual, isso impactaria diretamente o fluxo de criação do pedido.

Para reduzir esse impacto, a solução poderia evoluir para um modelo orientado a eventos utilizando filas e mensageria.

Nesse cenário:

* O pedido poderia permanecer em estado AGUARDANDO_PAGAMENTO
* O pagamento seria processado assincronamente
* Estratégias como retry, filas e circuit breaker poderiam ser aplicadas

Essa abordagem aumentaria a resiliência e reduziria falhas em cascata.

---

# Diagrama Arquitetural

```text
Frontend
   ↓
Controllers
   ↓
Services
   ├── PedidoService
   ├── ProdutoService
   ├── EstoqueService
   ├── PagamentoService
   └── NotificacaoService
   ↓
Repositories
   ↓
PostgreSQL
```

---

# Critérios Atendidos

| Critério               | Implementação |
| ---------------------- | ------------- |
| Arquitetura em camadas | ✔             |
| Fluxo de pedidos       | ✔             |
| Regras de negócio      | ✔             |
| API REST               | ✔             |
| Tratamento de exceções | ✔             |
| README técnico         | ✔             |
| Diagrama arquitetural  | ✔             |

```
```
