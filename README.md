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
3. Se a ferramenta Entity Framework Core não está instalada:

```bash
dotnet tool install --global dotnet-ef
```
   
5. Execute os comandos:

```bash
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

4. Acesse https://localhost:<porta>/swagger para visualizar a documentação da API

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
#### Paciente 1:N Prontuário
- Cada Paciente possui um ou mais Prontuários. O prontuário representa o histórico de atendimentos daquele paciente, contendo a data de abertura e anotações gerais. Um paciente pode ter múltiplos prontuários em situações de reabertura de registros clínicos ou reinício de acompanhamento.

- Paciente.Id → Prontuario.PacienteId


#### Prontuário 1:N Atendimento
- Cada Prontuário pode estar vinculado a vários Atendimentos. Cada atendimento representa uma ação clínica (ex.: consulta, emergência, internação), documentando data/hora, status, tipo e local.

- Prontuario.Id → Atendimento.ProntuarioId


#### Profissional 1:N Atendimento
- Cada Profissional de Saúde pode atender múltiplos pacientes. Assim, o profissional está associado a vários Atendimentos realizados por ele.

- Profissional.Id → Atendimento.ProfissionalId


#### Atendimento 1:N Prescrição
- Cada Atendimento pode gerar diversas Prescrições, onde são registradas as orientações médicas de medicamentos, dosagens, vias de administração, duração do tratamento e eventuais reações adversas.

- Atendimento.Id → Prescricao.AtendimentoId


#### Atendimento 1:N Exame
- Cada Atendimento pode solicitar múltiplos Exames (ex.: laboratoriais, de imagem), registrando a data de solicitação, realização e o resultado do exame.

- Atendimento.Id → Exame.AtendimentoId


#### Atendimento 0..1:1 Internação
- Um Atendimento pode ou não resultar em uma Internação. Quando há necessidade de hospitalização, é criada uma internação associada ao atendimento correspondente.

- Atendimento.Id → Internacao.AtendimentoId


#### Internação 0..1:1 Alta Hospitalar
- Cada Internação pode ter, posteriormente, um registro de Alta Hospitalar, indicando a finalização da internação com dados como condição do paciente na alta e recomendações pós-alta.

- Internacao.Id → AltaHospitalar.InternacaoId


#### Profissional N:1 Especialidade
- Cada Profissional de Saúde está vinculado a uma única Especialidade (ex.: cardiologia, pediatria), mas cada especialidade pode ter vários profissionais cadastrados.

- Profissional.EspecialidadeId → Especialidade.Id


## Endpoints Principais da API

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
