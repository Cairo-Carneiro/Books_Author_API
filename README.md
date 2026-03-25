# 📚 Books and Authors API (.NET)

Uma API RESTful robusta desenvolvida em **ASP.NET Core** para o gerenciamento de Livros e Autores. Construída seguindo princípios de arquitetura limpa, injetando dependências e utilizando estruturação em Serviços (Services e Interfaces) para isolar completamente a regra de negócio.

## 🚀 Sobre o Projeto
Essa aplicação foi projetada para organizar relacionamentos entre Acervos (Livros) e seus criadores (Autores), oferecendo endpoints totalmente documentados e testáveis pelo **Swagger**. Ela opera junto a um banco de dados relacional (SQL Server) garantindo persistência estruturada utilizando a abstração do Entity Framework Core. O sistema também adota fortemente o padrão **DTO** (Data Transfer Object) para blindar as propriedades no tráfego da rede.

## 🛠 Tecnologias Utilizadas
- **[C# / .NET / ASP.NET Core](https://dotnet.microsoft.com/)** - Linguagem moderna em conjunto com o framework backend web da Microsoft.
- **[Entity Framework Core](https://learn.microsoft.com/ef/core/)** - ORM (Object-Relational Mapper) completo, utilizado como ponte entre o código em C# e o servidor SQL atráves de Migrations (`Code-First`).
- **[SQL Server](https://www.microsoft.com/sql-server)** - Banco de dados relacional de alta performance escolhido para o armazenamento das entidades.
- **[Swagger / OpenAPI](https://swagger.io/)** - Fornecendo uma interface visual amigável e rica para testes, prototipação documentando cada rota e contrato da API.

## ⚙️ Arquitetura e Padrões
- **Design de Contratos (Interfaces)**: Inversão de controle utilizando classes de Serviço e Interfaces injetadas via Service Container (ex: `IAutorinterface`).
- **Service Layer Pattern**: Separação clara de responsabilidades isolando a `Controllers` das dezenas de validações e queries para o `Services`.
- **Relacionamentos via EF Core**: Definição segura de estrutura conectando objetos complexos (um autor pertence a vários livros etc).
- **Data Transfer Objects (DTO)**: Separação minuciosa dos retornos da API. Mantendo modelos intocados e entregando apenas ViewModels essenciais.

## 🚀 Como Executar Localmente

**1. Clone o Repositório**
```bash
git clone https://github.com/Cairo-Carneiro/Books_Author_API.git
cd Books_Author_API/API_NEW
```

**2. Configure o Banco de Dados (Connection String)**
Abra o arquivo `appsettings.json` ou `appsettings.Development.json` e verifique a sua string de conexão correspondente a `"DefaulConnections"`. Altere-a caso seja necessário apontar para seu SQL Server local específico (via autenticação Windows/SSMS ou um Docker).

**3.  Gere as Tabelas via Migrations**
Pelo console de linha de comando apontando para dentro do projeto (ou via Package Manager do Visual Studio):
```bash
dotnet ef database update
```
*(Isso garantirá que o EF Core leia a pasta `Migrations` informada e levante todas as tabelas e tipagens no seu SQL Server antes de você começar)*

**4. Inicie o Servidor Host**
No prompt rode:
```bash
dotnet run
```
No navegador, simplesmente adote a porta recém levantada anexando `/swagger` (ex. `https://localhost:7153/swagger`) para interagir visualmente e popular o banco de dados enviando novas requisições visuais!

---
✨ Criado para explorar boas práticas de clean code e manipulação relacional no rico ecossistema .NET.
