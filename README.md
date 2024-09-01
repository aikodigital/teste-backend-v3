# Jogadores Teatrais

## Descrição do Projeto

Jogadores Teatrais é uma aplicação ASP.NET Core desenvolvida para gerenciar faturas de uma companhia de teatro. A aplicação permite a geração de extratos de faturas para os clientes, levando em conta múltiplas apresentações teatrais contratadas, com cálculos baseados no número de linhas de cada peça, tamanho da plateia, e o gênero da peça (atualmente, tragédia, comédia, e história).

## Tecnologias Utilizadas

- **.NET 6**
- **ASP.NET Core**
- **Entity Framework Core**
- **SQLite** como banco de dados
- **DDD (Domain-Driven Design)** para a estrutura do projeto

## Estrutura do Projeto

O projeto segue uma abordagem de DDD, sendo dividido nos seguintes diretórios principais:

- **JogadoresTeatrais.Application**: Contém a lógica de aplicação e os serviços responsáveis por orquestrar operações de negócio.
- **JogadoresTeatrais.Domain**: Define as entidades, interfaces de repositório e regras de negócio.
- **JogadoresTeatrais.Data**: Responsável pela configuração do Entity Framework Core e acesso a dados (repositórios).
- **JogadoresTeatrais.Web**: Camada de apresentação, que inclui os controladores e as views para interação com o usuário.

## Instalação e Configuração

1. **Clone o repositório:**

   ```bash
   git clone https://github.com/Noonato/teste-GuilhermeNovais

 2. **Execute as migrações do banco de dados:**  

    No terminal, na pasta do projeto, execute os seguintes comando para criar o banco dados e aplicar as migrações:
    
        "dotnet ef database update --project JogaresTeatrais.Data -c DataContext --startup-project JogadoresTeatrais"

 3. **Inicie o servidor:**  

        dotnet run --project JogadoresTeatrais.Web


  ## Uso
  
    Gerenciamento de Faturas: Gere faturas baseadas nas apresentações realizadas pela companhia de teatro.
    Gêneros de Peças Suportados: Tragedy, Comedy, History.
    Relatórios: Imprima ou visualize os extratos das faturas.

  ## Contato
     Guilherme Nonato Novais
     Email: nonatovga10gmail.com
