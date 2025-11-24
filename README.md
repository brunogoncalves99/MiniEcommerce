# 🛒 MiniEcommerce

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)

Sistema completo de e-commerce desenvolvido em **ASP.NET Core MVC** com **Entity Framework Core**, utilizando **arquitetura em camadas** e seguindo boas práticas de desenvolvimento.

---

## 📋 Índice

- [Sobre o Projeto](#-sobre-o-projeto)
- [Funcionalidades](#-funcionalidades)
- [Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [Arquitetura](#-arquitetura)
- [Pré-requisitos](#-pré-requisitos)
- [Instalação](#-instalação)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Capturas de Tela](#-capturas-de-tela)
- [Contribuindo](#-contribuindo)
- [Licença](#-licença)

---

## 🚀 Sobre o Projeto

**MiniEcommerce** é uma aplicação web completa de e-commerce que permite:

- 🛍️ **Catálogo de produtos** com busca e filtros
- 🛒 **Carrinho de compras** com gerenciamento de itens
- 🎟️ **Sistema de cupons** de desconto
- 📦 **Gestão de pedidos** para administradores
- 👥 **Autenticação e autorização** (Admin e Comprador)
- 📊 **Dashboard administrativo** para gerenciar produtos, cupons e pedidos

O projeto foi desenvolvido como demonstração de conhecimentos em **desenvolvimento web full-stack** com .NET.

---

## ✨ Funcionalidades

### 👤 Para Compradores

- ✅ **Catálogo de Produtos**
  - Visualização de produtos com imagem, preço e estoque
  - Busca e filtro por categoria
  - Informações detalhadas do produto

- ✅ **Carrinho de Compras**
  - Adicionar/remover produtos
  - Atualizar quantidades
  - Aplicar cupons de desconto
  - Cálculo automático de subtotal e total

- ✅ **Gestão de Pedidos**
  - Finalizar compra
  - Visualizar histórico de pedidos
  - Acompanhar status dos pedidos
  - Cancelar pedidos pendentes

### 🔐 Para Administradores

- ✅ **Gerenciamento de Produtos**
  - Criar, editar e deletar produtos
  - Controle de estoque
  - Upload de imagens
  - Ativar/desativar produtos

- ✅ **Gerenciamento de Cupons**
  - Criar cupons com percentual de desconto
  - Definir valor mínimo e máximo
  - Controlar validade e limite de usos
  - Detecção automática de cupons expirados

- ✅ **Gerenciamento de Pedidos**
  - Visualizar todos os pedidos
  - Atualizar status dos pedidos
  - Filtrar por status

### 🔒 Autenticação e Segurança

- ✅ Autenticação baseada em **Cookie Authentication**
- ✅ Senhas criptografadas com **SHA256**
- ✅ Autorização por perfil (Admin/Comprador)
- ✅ Sessões seguras

---

## 🛠️ Tecnologias Utilizadas

### Backend
- **[ASP.NET Core 8.0](https://dotnet.microsoft.com/)** - Framework web
- **[Entity Framework Core 8.0](https://docs.microsoft.com/ef/core/)** - ORM
- **[SQL Server](https://www.microsoft.com/sql-server)** - Banco de dados
- **C# 12** - Linguagem de programação

### Frontend
- **[Bootstrap 5.3](https://getbootstrap.com/)** - Framework CSS
- **[jQuery 3.7](https://jquery.com/)** - Biblioteca JavaScript
- **[Font Awesome 6.4](https://fontawesome.com/)** - Ícones
- **[SweetAlert2](https://sweetalert2.github.io/)** - Alertas customizados
- **Razor Pages** - Template engine

### Arquitetura e Padrões
- 🏗️ **Clean Architecture** (Domain, Application, Infrastructure, API)
- 📦 **Repository Pattern**
- 💉 **Dependency Injection**
- 🎯 **DTO (Data Transfer Objects)**
- 🧩 **Separation of Concerns**

---

## 🏛️ Arquitetura

O projeto segue a **arquitetura em camadas** (Clean Architecture):

```
MiniEcommerce/
├── Domain/                  # Entidades e interfaces do domínio
│   ├── Entities/           # Modelos de domínio
│   ├── Enums/              # Enumerações
│   └── Interfaces/         # Contratos de repositórios
│
├── Application/            # Lógica de negócio
│   ├── DTOs/              # Objetos de transferência de dados
│   ├── Interfaces/        # Contratos de serviços
│   └── Services/          # Implementação da lógica de negócio
│
├── Infrastructure/         # Implementação de infraestrutura
│   ├── Data/              # Contexto do EF Core
│   └── Repositories/      # Implementação dos repositórios
│
└── Api/                    # Camada de apresentação (MVC)
    ├── Controllers/       # Controllers MVC
    ├── Views/             # Views Razor
    ├── ViewModels/        # ViewModels
    ├── Constants/         # Constantes e mensagens
    └── wwwroot/           # Arquivos estáticos
```

### Camadas

#### 1️⃣ **Domain (Domínio)**
- Contém as **entidades** do negócio
- Define as **interfaces** dos repositórios
- **Não depende** de nenhuma outra camada

#### 2️⃣ **Application (Aplicação)**
- Implementa a **lógica de negócio**
- Define os **contratos de serviços**
- Usa **DTOs** para transferência de dados
- Depende apenas do **Domain**

#### 3️⃣ **Infrastructure (Infraestrutura)**
- Implementa o **acesso a dados** (EF Core)
- Implementa os **repositórios**
- Configuração do **banco de dados**
- Depende do **Domain** e **Application**

#### 4️⃣ **API (Apresentação)**
- **Controllers** MVC
- **Views** Razor
- **Arquivos estáticos** (CSS, JS, imagens)
- Depende de **todas as camadas**

---

## 📋 Pré-requisitos

Antes de começar, você precisará ter instalado:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server 2019+](https://www.microsoft.com/sql-server/sql-server-downloads) ou [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-editions-express)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### Ferramentas Recomendadas
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql-server/ssms/download-sql-server-management-studio-ssms)
- [Postman](https://www.postman.com/) (para testar APIs)

---

## 🔧 Instalação

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/MiniEcommerce.git
cd MiniEcommerce
```

### 2. Configure a string de conexão

Edite o arquivo `MiniEcommerce.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=MiniEcommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Ajuste conforme seu ambiente:**
- `localhost` → Seu servidor SQL
- `SQLEXPRESS` → Sua instância do SQL Server
- Se usar autenticação SQL: `User Id=seu_usuario;Password=sua_senha;`

### 3. Restaure os pacotes NuGet

```bash
dotnet restore
```

### 4. Crie o banco de dados

**Opção A: Via Package Manager Console (Visual Studio)**
```powershell
Update-Database
```

**Opção B: Via CLI**
```bash
dotnet ef database update --project MiniEcommerce.Api
```

### 5. (Opcional) Popule o banco com dados de exemplo

Execute o script `Database/SeedData.sql` no SQL Server Management Studio ou execute:

```bash
sqlcmd -S localhost\SQLEXPRESS -d MiniEcommerceDB -i Database/SeedData.sql
```

### 6. Execute o projeto

```bash
dotnet run --project MiniEcommerce.Api
```

Ou pressione **F5** no Visual Studio.

O projeto estará disponível em:
- **HTTPS**: `https://localhost:7000`
- **HTTP**: `http://localhost:5000`

---

### Estrutura do Banco de Dados

O banco possui as seguintes tabelas:

- **Usuarios** - Usuários do sistema
- **Produtos** - Catálogo de produtos
- **Cupons** - Cupons de desconto
- **Pedidos** - Pedidos realizados
- **ItensPedido** - Itens dos pedidos

### Migrações

Para criar uma nova migração:

```bash
dotnet ef migrations add NomeDaMigracao --project MiniEcommerce.Api
```

Para aplicar migrações:

```bash
dotnet ef database update --project MiniEcommerce.Api
```

---

## 📁 Estrutura do Projeto

```
MiniEcommerce/
│
├── MiniEcommerce.Domain/              # Camada de Domínio
│   ├── Entities/
│   │   ├── EntidadeBase.cs
│   │   ├── Usuario.cs
│   │   ├── Produto.cs
│   │   ├── Cupom.cs
│   │   ├── Pedido.cs
│   │   └── ItemPedido.cs
│   ├── Enums/
│   │   ├── PerfilUsuario.cs
│   │   └── StatusPedido.cs
│   └── Interfaces/
│       ├── IRepositorioBase.cs
│       ├── IRepositorioUsuario.cs
│       ├── IRepositorioProduto.cs
│       ├── IRepositorioCupom.cs
│       └── IRepositorioPedido.cs
│
├── MiniEcommerce.Application/         # Camada de Aplicação
│   ├── DTOs/
│   │   ├── UsuarioDTO.cs
│   │   ├── ProdutoDTO.cs
│   │   ├── CupomDTO.cs
│   │   ├── PedidoDTO.cs
│   │   └── ...
│   ├── Interfaces/
│   │   ├── IServicoUsuario.cs
│   │   ├── IServicoProduto.cs
│   │   ├── IServicoCupom.cs
│   │   └── IServicoPedido.cs
│   └── Services/
│       ├── ServicoUsuario.cs
│       ├── ServicoProduto.cs
│       ├── ServicoCupom.cs
│       └── ServicoPedido.cs
│
├── MiniEcommerce.Infrastructure/      # Camada de Infraestrutura
│   ├── Data/
│   │   ├── MiniEcommerceContext.cs
│   │   └── Configurations/
│   │       ├── UsuarioConfiguration.cs
│   │       ├── ProdutoConfiguration.cs
│   │       └── ...
│   └── Repositories/
│       ├── RepositorioBase.cs
│       ├── RepositorioUsuario.cs
│       ├── RepositorioProduto.cs
│       ├── RepositorioCupom.cs
│       └── RepositorioPedido.cs
│
├── MiniEcommerce.Api/                 # Camada de Apresentação
│   ├── Controllers/
│   │   ├── HomeController.cs
│   │   ├── AutenticacaoController.cs
│   │   ├── ProdutosController.cs
│   │   ├── CupomController.cs
│   │   ├── CarrinhoController.cs
│   │   └── PedidosController.cs
│   ├── Views/
│   │   ├── Home/
│   │   ├── Autenticacao/
│   │   ├── Produtos/
│   │   ├── Cupom/
│   │   ├── Carrinho/
│   │   ├── Pedidos/
│   │   └── Shared/
│   ├── ViewModels/
│   ├── Constants/
│   │   ├── Mensagens.cs
│   │   ├── Validacoes.cs
│   │   ├── Labels.cs
│   │   └── Configuracoes.cs
│   ├── wwwroot/
│   │   ├── css/
│   │   ├── js/
│   │   └── imagens/
│   ├── Program.cs
│   └── appsettings.json
│
```

---

## 📸 Capturas de Tela

### Página Inicial (Catálogo)
Interface responsiva com cards de produtos, informações de estoque e botão de adicionar ao carrinho.

### Carrinho de Compras
Gerenciamento completo de itens, atualização de quantidades e aplicação de cupons de desconto.

### Gerenciamento de Produtos (Admin)
CRUD completo de produtos com modal para criação/edição, controle de estoque e status.

### Gerenciamento de Cupons (Admin)
Sistema de cupons com cálculo em tempo real de desconto, validação de datas e limite de usos.

<img width="1895" height="1018" alt="MiniEcommerce1" src="https://github.com/user-attachments/assets/1ef73512-80bf-41c4-9b6b-d239596babb2" />
<img width="1893" height="1026" alt="MiniEcommerce2" src="https://github.com/user-attachments/assets/92d6f5ab-29cd-424e-830b-b46b13d283cd" />
<img width="1926" height="913" alt="MiniEcommerce3" src="https://github.com/user-attachments/assets/7ef4e301-ed70-46e4-980d-198a75b37eae" />
<img width="1916" height="920" alt="MiniEcommerce4" src="https://github.com/user-attachments/assets/b5f1cf02-055b-46cf-83fd-e715fa2f327f" />
<img width="1886" height="914" alt="MiniEcommerce5" src="https://github.com/user-attachments/assets/1a95b2d6-e9ab-41af-accb-3c6a8eacce20" />
<img width="1915" height="909" alt="MiniEcommerce7" src="https://github.com/user-attachments/assets/eaaf0ba8-5bcd-4c32-ba5f-9a7f716d61a0" />


---

## 🎯 Funcionalidades Técnicas

### Segurança
- ✅ Autenticação via Cookie Authentication
- ✅ Autorização baseada em perfis (Claims)
- ✅ Senhas criptografadas (SHA256)
- ✅ Proteção contra CSRF
- ✅ HTTPS habilitado

### Performance
- ✅ Lazy Loading de entidades relacionadas
- ✅ Queries otimizadas com Include
- ✅ Paginação de resultados
- ✅ Cache de sessão

### Validações
- ✅ Validações no backend (ModelState)
- ✅ Validações no frontend (JavaScript)
- ✅ Mensagens de erro amigáveis
- ✅ Feedback visual para o usuário

### Responsividade
- ✅ Design responsivo (Bootstrap)
- ✅ Funciona em desktop, tablet e mobile
- ✅ Menu adaptável

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Siga os passos:

## 📄 Licença

Este projeto está sob a licença **MIT**. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## 👨‍💻 Autor

**Bruno Gonçalves**

- GitHub: (https://github.com/brunogoncalves99)
- LinkedIn: (https://linkedin.com/in/brunogoncalveslemos)
- Email: bruno.goncalves1999@hotmail.com

---

## 🙏 Agradecimentos

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core Documentation](https://docs.microsoft.com/ef/core)
- [Bootstrap Documentation](https://getbootstrap.com/docs)
- Comunidade .NET Brasil

---

## 📞 Contato

Tem alguma dúvida ou sugestão? 

- Abra uma [Issue](https://github.com/brunogoncalves99/MiniEcommerce)
- Entre em contato: bruno.goncalves1999@hotmail.com

---

<div align="center">

### ⭐ Se este projeto foi útil, considere dar uma estrela!

**Desenvolvido com ❤️ usando .NET**

![Made with ASP.NET Core](https://img.shields.io/badge/Made%20with-ASP.NET%20Core-512BD4?style=for-the-badge&logo=dotnet)

</div>
