# 🏆 Teste Backend

![Aiko](img/logo.png)

Este teste tem como objetivo avaliar suas habilidades em refatoração de código, design de software e implementação de novas funcionalidades em uma aplicação backend. A proposta envolve trabalhar em um sistema já existente, melhorando sua testabilidade, adicionando suporte a novos requisitos e garantindo a confiabilidade da solução por meio de testes unitários. Além disso, a adoção de boas práticas de arquitetura e desenvolvimento será um diferencial.

## 📜 Apresentação e estado atual da aplicação

Essa aplicação é usada por uma companhia de teatro para gerar extratos
impressos a partir das faturas de seus clientes.

A companhia é contratada pelos clientes para múltiplas apresentações e a
cobrança é feita baseada no número de linhas de cada peça apresentada, no
tamanho da platéia e no gênero da peça. Atualmente os gêneros trabalhados pela
companhia são tragédia e comédia.

Para cada apresentação são também gerados créditos, que são um tipo de
mecanismo de fidelização que os clientes podem usar para obter descontos em
futuras apresentações. O total de créditos gerados é também mostrado no
extrato.

## ✨ Novas funcionalidades desejadas

A companhia de teatro pretende adicionar o gênero histórico ao seu repertório,
então o software deve ser capaz de calcular os valores e créditos também para
esse gênero. Provavelmente virão mais gêneros no futuro, então o design deve
estar pronto para acomodar novos gêneros sem muita dificuldade.

Também desejam que o extrato possa ser gerado como um XML, além do formato
de texto atualmente suportado. Novamente, é bom que o design facilite que
futuramente esse extrato seja emitido em novos formatos, pois certamente é uma
questão de tempo até surgir essa demanda.

## 🛠️ Especificação da atividade

Este é um exercício de refatoração. O design inicial da aplicação é pouco
testável, portanto os únicos testes que a aplicação possui no momento são os
[ApprovalTests](https://approvaltests.com/) para validar a saída final. É
esperado que você torne o código mais testável e então adicione testes
unitários que validem a aplicação de forma mais granular e que dêem segurança
para futuras refatorações e para o acréscimo das novas funcionalidades.

Também serão avaliados a abordagem para desenvolvimento da solução (Desing 
Patters, DDD, Solid, etc.) e a arquitetura utilizada (Clean Architecture, Onion
Architecture, etc.).

O projeto de testes possui três ApprovalTests.

* O teste TestStatementExampleLegacy, está passando no estado atual do
  código. Este teste servirá para te dar segurança das primeiras refatorações
  até que você escreva os testes unitários, mas ao final, com as
  funcionalidades novas implementadas, este teste se torna obsoleto.
* O teste TestTextStatementExample está implementado, porém não executa, pois o
  gênero histórico ainda não está implementado.
* O teste TestXmlStatementExample não está implementado e deve ser implementado
  por você e gerar a saída aprovada que está no projeto de testes.

O código dos testes pode ser refatorado, desde que a saída continue a
mesma e os testes continuem cumprindo o mesmo propósito. É esperado que você
implemente as novas funcionalidades pedidas para que todos os ApprovalTests
passem.

Faça commits com frequência para que sua abordagem de refatoração seja mostrada
pelo histórico de versões.

## 🚀 Extras (Opcional)

Não é mandatório, mas de maneira opcional os seguintes requisitos poderão ser
implementados:

* Implementar processamento assincrono de extratos, os dados devem ser imputados,
  enfileirados, processados assincronicamente e gerar o XML resultante em um 
  diretório
* API rest para expor os métodos para futuras integrações
  * Expor documentação da API por Swagger
* Persistencia dos dados em um banco de dados para salvar o extrato com suas
  respectivas peças

## 📜 Regras de negócio

* O valor base para a cobrança de todas as peças é o número de linhas da peça
  dividido por 10
* O número de linhas da peça considerado para o cálculo do valor base deve ser
  forçado a estar no intervalo entre 1000 e 4000
* O valor para uma peça de tragédia é igual ao valor base caso a platéia seja
  menor ou igual a 30, somando mais 10.00 para cada espectador adicional a
  esses 30
* Para uma peça de comédia, o cálculo base é sempre somado a 3.00 por
  espectador. Além disso, se a platéia for maior que 20, o valor deve ser
  aumentado em 100.00 e deve se somar mais 5.00 por espectador adicional aos 20
  de base
* Todas performances dão 1 crédito para cada espectador acima de 30, não
  valendo nenhum crédito para uma platéia menor ou igual a 30
* Existe um bônus de créditos de um quinto da platéia arredondados para baixo,
  exclusivo para peças de comédia
* As peças históricas são, por algum motivo, mais complicadas e têm o valor
  igual à soma dos valores correspondentes a uma peça de tragédia e uma de
  comédia
* A estrutura do XML deve seguir como referência a saída aprovada no
  ApprovalTest correspondente

## 📦 Entrega

Para realizar a entrega do teste, siga as instruções abaixo:

1. Fork este repositório e clone-o em sua máquina.
2. Crie uma branch com o nome `teste/[NOME]`:
    - `[NOME]`: Seu nome.
    - Exemplos: `teste/fulano-da-silva`; `teste/beltrano-primeiro-gomes`.
3. Realize um pull request da sua branch para este repositório.
* Realize o pull request da sua branch nesse repositório.
4. Envie um vídeo apresentando a aplicação e a entrega como um todo. O vídeo pode ser hospedado como não listado no YouTube ou compartilhado via Google Drive, e o link deve ser incluído no pull request ou no README do projeto.


📩 **Boa sorte! Estamos ansiosos para ver seu código e sua apresentação!** 🚀