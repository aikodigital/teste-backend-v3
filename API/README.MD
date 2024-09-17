# API de Geração de Faturas

## Visão Geral

A **API de Geração de Faturas** é uma aplicação .NET 8 projetada para gerar faturas para peças de teatro. Suporta a geração de faturas em dois formatos: XML e TXT. A API utiliza princípios de Arquitetura Limpa e segue os padrões de design SOLID.

### Recursos

- **Geração de Faturas**: Criação de faturas para peças de teatro.
- **Formatos de Arquivo**: Suporte aos formatos XML e TXT.
- **Arquitetura**: Construída com Arquitetura Limpa.
- **Padrões de Design**: Adere aos princípios SOLID.
- **Serviços em Segundo Plano**: Utiliza RabbitMQ e Hangfire para processamento em segundo plano.

## Arquitetura

A API está estruturada da seguinte forma:

- **Arquitetura Limpa**: A aplicação é organizada em diferentes camadas para promover a separação de preocupações e a manutenção.
- **RabbitMQ**: Utilizado para mensagens e para lidar com o consumo de solicitações de fatura.
- **Hangfire**: Enfileira e processa trabalhos em segundo plano para gerar arquivos de fatura.

## Pré-requisitos

Antes de executar a API, certifique-se de ter os seguintes itens instalados em sua máquina:

- **RabbitMQ**: Necessário para o enfileiramento de mensagens.
- **MS SQL Server**: Necessário para o banco de dados.

## Instruções de Configuração

Atualizar a connectionString do arquivo appsettings.

cd caminho\Domain\TS.Domain

dotnet ef database update --startup-project ..\..\Presentation\TS.Presentation\TS.Presentation.csproj

dotnet restore

dotnet build

dotnet run 

