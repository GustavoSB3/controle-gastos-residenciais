# Controle de Gastos Residenciais

Aplicação full stack para cadastrar pessoas e suas receitas e despesas, além de apresentar um resumo financeiro individual e geral.

## Tecnologias

- Backend: .NET 8, ASP.NET Core Web API, C#, Entity Framework Core, SQLite e Swagger.
- Frontend: React, TypeScript, Vite e CSS responsivo.

## Como executar

Pré-requisitos: SDK .NET 8 e Node.js 20 ou superior.

### Backend

```bash
cd backend
dotnet restore
dotnet run
```

A API usa por padrão `http://localhost:5000`, conforme `Properties/launchSettings.json`. A documentação Swagger fica em `http://localhost:5000/swagger`. O banco `controle-gastos.db` é criado automaticamente na pasta do backend e mantém os dados entre execuções.

### Frontend

Em outro terminal:

```bash
cd frontend
npm install
npm run dev
```

Acesse `http://localhost:5173`. Se a API estiver em outro endereço, copie `.env.example` para `.env` e altere `VITE_API_URL`.

## Estrutura

```text
backend/
├── Controllers/  # Endpoints HTTP
├── Data/         # Contexto e configuração do banco
├── DTOs/         # Contratos de entrada e saída
├── Models/       # Entidades e enumerações
└── Services/     # Regras de negócio e acesso a dados
frontend/src/
├── components/   # Componentes reutilizáveis
├── pages/        # Telas de pessoas, transações e totais
├── services/     # Comunicação com a API
└── types/        # Tipos TypeScript
```

## Regras de negócio

- O identificador de pessoas e transações é gerado automaticamente.
- A pessoa vinculada à transação deve existir.
- Pessoas com menos de 18 anos só podem registrar despesas.
- O valor de uma transação deve ser maior que zero.
- Ao excluir uma pessoa, suas transações são excluídas em cascata pelo banco.
- Não há edição ou exclusão de transações.
- O saldo é a soma das receitas menos a soma das despesas, por pessoa e no total geral.

## Endpoints

- `GET /api/pessoas`, `POST /api/pessoas`, `DELETE /api/pessoas/{id}`
- `GET /api/transacoes`, `POST /api/transacoes`
- `GET /api/totais`

## Preparação para GitHub

O repositório inclui `.gitignore`, documentação de execução e separação entre backend e frontend. Arquivos gerados, dependências e o banco SQLite local não devem ser versionados.
