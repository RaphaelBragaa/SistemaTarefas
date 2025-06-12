# API de Gerenciamento de Usuários e Tarefas

Uma API RESTful desenvolvida em ASP.NET Core para gerenciamento de usuários, autenticação e tarefas, seguindo as melhores práticas do setor, com autenticação JWT, documentação Swagger/OpenAPI e arquitetura extensível para produção e testes.

---

## Índice

- [API de Gerenciamento de Usuários e Tarefas](#api-de-gerenciamento-de-usuários-e-tarefas)
  - [Índice](#índice)
  - [Visão Geral](#visão-geral)
  - [Tecnologias Utilizadas](#tecnologias-utilizadas)
  - [Arquitetura](#arquitetura)
  - [Como Começar](#como-começar)
    - [Pré-requisitos](#pré-requisitos)
    - [Passos](#passos)
    - [Com Docker](#com-docker)
  - [Uso da API](#uso-da-api)
    - [Principais Endpoints](#principais-endpoints)
  - [Autenticação](#autenticação)

---

## Visão Geral

Esta API oferece endpoints para:

- Cadastro e autenticação de usuários
- Gerenciamento completo de tarefas (CRUD)
- Controle de acesso seguro via JWT

O objetivo é fornecer uma base robusta para aplicações que exijam controle de usuários e tarefas, com foco em segurança, escalabilidade e facilidade de integração[^2][^7].

---

## Tecnologias Utilizadas

- **ASP.NET Core** – Framework principal da aplicação
- **Entity Framework Core** – ORM para acesso a dados
- **Swagger/OpenAPI** – Documentação e testes interativos dos endpoints
- **JWT (JSON Web Token)** – Autenticação e autorização segura[^12]
- **BCrypt** – Hash de senhas para segurança dos usuários[^12]
- **Docker** – Conteinerização para ambientes de desenvolvimento e produção[^13]
- **SQL Server** (ou outro banco relacional) – Persistência dos dados

---

## Arquitetura

A API segue o padrão RESTful, com separação clara entre controladores, serviços, repositórios e modelos. O projeto está preparado para escalar, testar e manter facilmente, adotando princípios SOLID e injeção de dependência.

Endpoints principais:

- `/api/Conta/login` – Autenticação de usuários
- `/api/User` – CRUD de usuários
- `/api/User/register` – Registro de novos usuários
- `/api/Task` – CRUD de tarefas

---

## Como Começar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (opcional, para ambiente isolado)[^13]
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (ou outro banco relacional)


### Passos

1. Clone o repositório:

```bash
git clone https://github.com/seu-usuario/seu-repositorio.git
cd seu-repositorio
```

2. Configure as variáveis de ambiente no arquivo `appsettings.json` (conexão com banco, chave JWT, etc).
3. Execute as migrações do banco:

```bash
dotnet ef database update
```

4. Rode a aplicação:

```bash
dotnet run
```

5. Acesse a documentação interativa:

```
http://localhost:5000/swagger
```


### Com Docker

```bash
docker-compose up --build
```

Acesse o Swagger normalmente.

---

## Uso da API

### Principais Endpoints

| Método | Rota | Descrição |
| :-- | :-- | :-- |
| POST | /api/Conta/login | Login de usuário |
| POST | /api/User/register | Cadastro de usuário |
| GET | /api/User | Listar usuários |
| GET | /api/User/{id} | Detalhar usuário |
| PUT | /api/User/{id} | Atualizar usuário |
| DELETE | /api/User/{id} | Remover usuário |
| GET | /api/Task | Listar tarefas |
| POST | /api/Task | Criar tarefa |
| GET | /api/Task/{id} | Detalhar tarefa |
| PUT | /api/Task/{id} | Atualizar tarefa |
| DELETE | /api/Task/{id} | Remover tarefa |

Consulte o Swagger para exemplos de requisições e respostas.

---

## Autenticação

A API utiliza autenticação JWT. Para acessar os endpoints protegidos, faça login via `/api/Conta/login` e inclua o token JWT retornado no header `Authorization` das requisições subsequentes:

```
Authorization: Bearer {seu_token_jwt}
```

A documentação Swagger está configurada para aceitar tokens JWT, facilitando os testes interativos[^2][^12].

---
