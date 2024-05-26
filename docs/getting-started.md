# Começando

Para utilizar a CLI, abra uma nova instância do Windows Terminal ou do terminal de sua escolha e execute o comando desejado. 
Como exemplo, podemos usar o código a seguir:

    $ clinet pull build

E o resultado deverá ser parecido com a imagem a seguir:


![Comandos gerados](https://miro.medium.com/v2/resize:fit:720/format:webp/1*WEIV8dFRwafy72ujY3i8DQ.png "comandos")


Você poderá ainda definir a tecnologia (nuget ou npm)que usará com o comando de terminal que está pesquisando, bastando informar também como um argumento:

    $ clinet nuget install

ou 

    $ clinet nextjs create


![Comandos gerados](https://miro.medium.com/v2/resize:fit:640/format:webp/1*95jyAb2Wk7PJRfHi2gKGEA.png "comandos")


Você pode adicionar quantos termos desejar na linha de comando para refinar sua busca, como a seguir:

    $ clinet npm run docker build restore

ou

    $ clinet nextjs create install run build