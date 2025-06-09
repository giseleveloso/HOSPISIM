# HOSPISIM - Sistema de Gestão Hospitalar

## Descrição
API REST para gerenciamento de informações clínicas do Hospital Vida Plena, desenvolvida em ASP.NET Core com Entity Framework e SQL Server.

## Funcionalidades
- Gestão de Pacientes
- Controle de Profissionais de Saúde
- Prontuários Eletrônicos
- Atendimentos Médicos
- Prescrições Médicas
- Exames Clínicos
- Controle de Internações
- Altas Hospitalares

## Tecnologias
- .NET 8.0
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger/OpenAPI

## Como Executar

### Pré-requisitos
- .NET 8.0 SDK
- SQL Server (LocalDB ou instância completa)
- Visual Studio 2022 ou VS Code

### Passos
1. Clone o repositório
2. Navegue até o diretório do projeto
3. Execute os comandos:

```bash
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

4. Acesse https://localhost:7000/swagger para visualizar a documentação da API

## Estrutura do Banco de Dados

### Entidades Principais
- **Paciente**: Informações pessoais e médicas
- **ProfissionalSaude**: Médicos, enfermeiros e outros profissionais
- **Especialidade**: Áreas médicas (Cardiologia, Pediatria, etc.)
- **Prontuario**: Histórico médico do paciente
- **Atendimento**: Consultas, emergências e internações
- **Prescricao**: Medicamentos prescritos
- **Exame**: Exames laboratoriais e de imagem
- **Internacao**: Controle de internações hospitalares
- **AltaHospitalar**: Registro de altas médicas

### Relacionamentos
- Paciente 1:N Prontuário
- Prontuário 1:N Atendimento
- Profissional 1:N Atendimento
- Atendimento 1:N Prescrição
- Atendimento 1:N Exame
- Atendimento 0..1:1 Internação
- Internação 0..1:1 Alta Hospitalar
- Profissional N:1 Especialidade

## Endpoints da API

### Pacientes
- GET /api/pacientes - Lista todos os pacientes
- GET /api/pacientes/{id} - Busca paciente por ID
- POST /api/pacientes - Cria novo paciente
- PUT /api/pacientes/{id} - Atualiza paciente
- DELETE /api/pacientes/{id} - Remove paciente

### Atendimentos
- GET /api/atendimentos - Lista todos os atendimentos
- GET /api/atendimentos/{id} - Busca atendimento por ID
- POST /api/atendimentos - Cria novo atendimento

## Dados de Teste
A aplicação já vem com dados de exemplo (seed data) incluindo:
- 10 pacientes cadastrados
- 10 profissionais de saúde
- 10 especialidades médicas
- Prontuários, atendimentos, prescrições e exames
- Internações e altas hospitalares

## Segurança e Compliance
- Campos obrigatórios validados
- Relacionamentos com integridade referencial
- Soft deletes para preservar histórico médico
