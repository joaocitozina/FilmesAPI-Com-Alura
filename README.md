# 🎬 Filmes API - Gestão Completa de Filmes

[![GitHub license](https://img.shields.io/github/license/joaocitozina/FilmesAPI-Com-Alura)](https://github.com/joaocitozina/FilmesAPI-Com-Alura/blob/master/LICENSE)
[![.NET](https://img.shields.io/badge/Tecnologia-.NET%206-512BD4)](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
[![Swagger](https://img.shields.io/badge/Documenta%C3%A7%C3%A3o-Swagger-85EA2D)](https://swagger.io/)

Este projeto é uma API RESTful completa para gestão de dados de filmes (CRUD), desenvolvida em **ASP.NET Core (.NET 6)** como parte do curso da **Alura**: ".NET 6: Criando uma Web API".

### 💡 Funcionalidades e Arquitetura

O foco deste projeto foi aplicar conceitos de código limpo e padronização de APIs:

* **CRUD Completo:** Criação (`POST`), Leitura (`GET`), Atualização Completa (`PUT`), Atualização Parcial (`PATCH`) e Exclusão (`DELETE`) de filmes.
* **Injeção de Dependência (DI):** Utilização do *Design Pattern* de DI no `FilmeController` para gerenciar dependências como o `FilmeContext` e o `IMapper`.
* **Mapeamento de Objetos:** Uso do **AutoMapper** para mapear DTOs (`CreateFilmeDto`, `ReadFilmeDto`, `UpdateFilmeDto`) para entidades de domínio e vice-versa.
* **Persistência de Dados:** Implementação de um banco de dados **SQLite** e gerenciamento do esquema via **Entity Framework Core Migrations**.
* **Documentação Profissional:** Implementação completa do **Swagger/OpenAPI** com inclusão de **XML Comments** em todos os *endpoints*.

---

## 🛠️ Tecnologias Utilizadas

* **Linguagem:** C#
* **Framework:** ASP.NET Core 6.0
* **Banco de Dados:** SQLite (Entity Framework Core)
* **Mapeamento:** AutoMapper
* **Documentação:** Swashbuckle (Swagger/OpenAPI)

### 📦 Pacotes NuGet Essenciais

* `Microsoft.EntityFrameworkCore.SQLite`
* `Microsoft.EntityFrameworkCore.Design`
* `AutoMapper.Extensions.Microsoft.DependencyInjection`
* `Swashbuckle.AspNetCore`

---

## ⚙️ Configuração e Execução

Para rodar o projeto localmente, siga os passos abaixo:

### Pré-requisitos

* [.NET 6 SDK ou superior](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* Uma IDE (Visual Studio ou VS Code)

### Passos de Instalação

1.  **Clone o Repositório:**
    ```bash
    git clone [https://github.com/joaocitozina/FilmesAPI-Com-Alura.git](https://github.com/joaocitozina/FilmesAPI-Com-Alura.git)
    ```
2.  **Navegue para a Pasta do Projeto:** A pasta principal é `FilmesApi`, onde está o arquivo `.csproj`.
    ```bash
    cd FilmesAPI-Com-Alura/FilmesApi
    ```
3.  **Restaurar Dependências:**
    ```bash
    dotnet restore
    ```
4.  **Aplicar Migrações (Banco de Dados):**
    ```bash
    dotnet ef database update
    ```
    *Este comando cria o banco de dados e as tabelas definidas pelas migrações.*

### Rodando o Projeto

Execute o comando a partir do diretório do projeto (`FilmesApi/`):

```bash
dotnet run



