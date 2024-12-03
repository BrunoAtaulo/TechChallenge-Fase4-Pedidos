# TechChallenge FIAP - G24 FASE 4

## API PEDIDOS


## Grupo 24 - Integrantes
💻 *<b>RM355456</b>*: Franciele de Jesus Zanella Ataulo </br>
💻 *<b>RM355476</b>*: Bruno Luis Begliomini Ataulo </br>
💻 *<b>RM355921</b>*: Cesar Pereira Moroni </br>

## Nome Discord:
Franciele RM 355456</br>
Bruno - RM355476</br>
Cesar P Moroni RM355921</br>


Este repositório é dedicado ao microsserviço de pedidos. Neste foi utilizado o mysql no RDS como banco de dados

O deploy deste foi feito Utilizando aws Lambda - serveless análise de código e cobertura de testes utilizando SonarCloud são realizados via Github Actions.



## Desenho da Arquitetura

Quando disparamos a Github Action, é realizado o build da aplicação e deploy na LAMBDA . Desenho com detalhes da infraestrutura do software

![image](assets/arquitetura.png)


## Sonar

Utilizamos a ferramenta SonarCloud para análise de código e cobertura de testes. Para este microsserviço, atingimos acima de 80% de cobertura, conforme abaixo:

![image1](assets/cobertura.png)

## Teste unitário

Utilizamos a ferramenta xUnit para realizar os testes unitários

![image2](assets/image2.png)
![image3](assets/image3.png)


