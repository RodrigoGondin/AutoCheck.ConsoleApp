# AutoCheck .NET

## Sobre o projeto

O AutoCheck .NET é uma aplicação de console para realizar vistorias em veículos. A ideia é cadastrar um carro, uma moto ou um caminhão, verificar os itens do checklist e informar se cada item está **Bom**, **Regular** ou **Ruim**.

Depois da vistoria, o programa calcula a pontuação, mostra o percentual de aprovação, classifica o veículo e informa quais serviços podem ser necessários.

Este projeto pratica conceitos básicos de C# e programação orientada a objetos.

## Como executar

É necessário ter o SDK do .NET instalado.

No terminal, dentro da pasta do projeto, execute o comando dotnet run --project AutoCheck.ConsoleApp/AutoCheck.ConsoleApp.csproj.

Depois disso, o menu apresenta as opções 1 - Realizar Nova Vistoria, 2 - Exibir Relatório das Vistorias e 0 - Sair.

Para realizar uma vistoria, o programa pergunta os dados do veículo e depois solicita o status de cada item do checklist.

## Organização do projeto

A pasta AutoCheck.ConsoleApp possui o arquivo Program.cs, a pasta Models com as classes ItemVistoria, Veiculo, Carro, Moto e Caminhao, e a pasta Services com a classe MotorVistoria.

## O que cada classe faz

### Veiculo

É a classe base dos veículos. Ela guarda somente os dados comuns a qualquer veículo:

- Marca e Modelo: identificam o veículo.
- Ano e Quilometragem: ajudam a registrar seu estado e histórico de uso.
- VistoriaRealizada: usa uma List<ItemVistoria> porque um veículo possui vários itens avaliados.

A classe também possui o método AdicionarItemVistoriado, usado para colocar cada item avaliado dentro da lista da vistoria.

O método ObterChecklistObrigatorio é virtual, pois cada tipo de veículo pode acrescentar itens diferentes ao checklist.

### Carro, Moto e Caminhao

São classes específicas que herdam da classe genérica Veiculo. Por isso, aproveitam as propriedades e métodos comuns dos veículos.

Cada uma possui uma informação própria, necessária para descrever sua categoria:

- Carro: QuantidadePortas informa o número de portas.
- Moto: Cilindradas informa a capacidade do motor.
- Caminhao: QuantidadeEixos e CapacidadeCargaToneladas representam sua estrutura e capacidade de transporte.

Cada classe sobrescreve ObterChecklistObrigatorio com override e adiciona os itens específicos da sua categoria.

### ItemVistoria

Essa classe representa cada item verificado. As duas propriedades são suficientes para identificar o item e guardar seu resultado:

- Nome: identifica o que foi inspecionado.
- Status: guarda o resultado, que pode ser Bom, Regular ou Ruim.

A classe também possui os métodos que trabalham com as regras da vistoria:

- StatusValido: confere se o status digitado é permitido.
- ObterPontuacao: transforma o status em pontos.
- ObterPontuacaoTotal: soma a pontuação de todos os itens.
- ObterPercentualAprovacao: calcula o percentual da vistoria.
- ObterClassificacao: informa a classificação final.
- ObterCriticos: encontra os itens com status Ruim.
- ObterEmAtencao: encontra os itens com status Regular.
- ObterRecomendacao: informa a orientação para a oficina.

As verificações EhCritico e EhAtencao são métodos porque as propriedades Nome e Status foram mantidas simples, apenas com get e set.

### MotorVistoria

O motor é responsável por executar a vistoria. A lista Vistorias foi escolhida para guardar todos os veículos avaliados durante a execução. A lista statusItens recebe os resultados informados pelo usuário, e o for relaciona cada resultado ao item correto do checklist.

Depois, o veículo finalizado é guardado na lista Vistorias, que permite consultar mais de uma vistoria durante a execução do programa.

O motor não imprime o relatório. A parte visual fica no Program, deixando cada classe com uma responsabilidade mais clara.

### Program

O Program contém a interação com o usuário. A variável opcao guarda a escolha do menu, veiculo representa o veículo atual e statusItens guarda os resultados digitados. Depois, o programa exibe o relatório final.

Também é no Program que ficam os textos do relatório, pois ele representa a parte de apresentação da aplicação.

## Regras da vistoria

A pontuação de cada item é definida pelo status. O status Bom vale 10 pontos, Regular vale 5 pontos e Ruim vale 0 pontos.

O percentual é calculado dividindo a pontuação obtida pela pontuação máxima possível e multiplicando por 100:

A fórmula usada é: Percentual = (Pontuação obtida / Pontuação máxima possível) * 100.

A pontuação máxima é calculada multiplicando a quantidade de itens por 10.

A classificação segue estas faixas:

- De 90% a 100%: **Aprovado com Excelência**.
- De 60% a 89%: **Aprovado com Apontamentos**.
- De 0% a 59%: **Reprovado na Vistoria**.

Os itens Ruim aparecem como itens críticos e os itens Regular aparecem como itens de atenção.

## Conceitos de C# utilizados

- Tipos primitivos: string, int, double e bool.
- Coleções usando List<T>.
- Estruturas if/else.
- Laços for, foreach e do/while.
- Classes, objetos, propriedades e métodos.
- Construtores com parâmetros.
- Uso da palavra-chave this.
- Herança entre Veiculo e suas subclasses.
- Sobrescrita com virtual e override.
- Polimorfismo ao tratar os diferentes tipos como Veiculo.

A busca dos itens, os cálculos e os filtros são feitos com laços tradicionais, sem utilizar LINQ.

## Sobre arquitetura cliente-servidor

Este projeto não possui arquitetura cliente-servidor. Ele é uma aplicação de console que funciona localmente no terminal.

O usuário pode ser considerado a parte que envia os dados pela entrada do console, enquanto o programa processa as informações e mostra o resultado. Isso é apenas uma comparação para facilitar o entendimento; não existe um servidor ou banco de dados nesta versão.

## Vídeo de apresentação

Link do vídeo: **adicione aqui o link do Google Drive ou YouTube**.
