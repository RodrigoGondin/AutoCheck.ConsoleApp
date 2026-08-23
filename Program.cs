using AutoCheck.ConsoleApp;
using AutoCheck.ConsoleApp.Services;

// Ponto de entrada da aplicação
MotorVistoria motor = new MotorVistoria();
string opcao;

// Loop principal da aplicação, exibindo o menu e processando as opções escolhidas pelo usuário com do-while
do
{
    Console.WriteLine("\n=== AUTOCHECK .NET ===");
    Console.WriteLine("1 - Realizar Nova Vistoria");
    Console.WriteLine("2 - Exibir Relatório das Vistorias");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma opção: ");
    opcao = Console.ReadLine()!;

    // Processa a opção escolhida pelo usuário
    if (opcao == "1")
    {
        Veiculo veiculo = CriarVeiculo();
        Console.WriteLine("\nInforme o status de cada item usando Bom, Regular ou Ruim.");
        List<string> statusItens = new List<string>();
        foreach (string nomeItem in veiculo.ObterChecklistObrigatorio())
        {
            statusItens.Add(LerStatus(nomeItem));
        }

        motor.RealizarVistoria(veiculo, statusItens);
        Console.WriteLine("\nVistoria registrada com sucesso.");
    }
    else if (opcao == "2")
    {
        ExibirRelatorioVistorias(motor.Vistorias);
    }
    else if (opcao != "0")
    {
        Console.WriteLine("Opção inválida.");
    }
}
while (opcao != "0");

Console.WriteLine("Aplicação encerrada.");

// Função que cria um veículo com base nas informações fornecidas pelo usuário
Veiculo CriarVeiculo()
{
    Console.WriteLine("\nTipo de veículo:");
    Console.WriteLine("1 - Carro");
    Console.WriteLine("2 - Moto");
    Console.WriteLine("3 - Caminhão");
    Console.Write("Escolha: ");
    string tipo = Console.ReadLine()!;

    Console.Write("Marca: ");
    string marca = Console.ReadLine()!;
    Console.Write("Modelo: ");
    string modelo = Console.ReadLine()!;
    Console.Write("Ano: ");
    int ano = int.Parse(Console.ReadLine()!);
    Console.Write("Quilometragem: ");
    int quilometragem = int.Parse(Console.ReadLine()!);

    // Cria o veículo com base no tipo escolhido e nas informações fornecidas
    if (tipo == "1")
    {
        Console.Write("Quantidade de portas: ");
        return new Carro(marca, modelo, ano, quilometragem)
        {
            QuantidadePortas = int.Parse(Console.ReadLine()!)
        };
    }

    if (tipo == "2")
    {
        Console.Write("Cilindradas: ");
        return new Moto(marca, modelo, ano, quilometragem)
        {
            Cilindradas = int.Parse(Console.ReadLine()!)
        };
    }

    Console.Write("Quantidade de eixos: ");
    int quantidadeEixos = int.Parse(Console.ReadLine()!);
    Console.Write("Capacidade de carga em toneladas: ");
    double capacidadeCarga = double.Parse(Console.ReadLine()!);
    return new Caminhao(marca, modelo, ano, quilometragem)
    {
        QuantidadeEixos = quantidadeEixos,
        CapacidadeCargaToneladas = capacidadeCarga
    };
}

// Função que lê o status de um item vistoriado, garantindo que seja válido (Bom, Regular ou Ruim)
string LerStatus(string nomeItem)
{
    // Solicita ao usuário o status do item e valida a entrada
    string status;
    do
    {
        Console.Write($"{nomeItem}: ");
        status = Console.ReadLine()!;
        if (!ItemVistoria.StatusValido(status))
        {
            Console.WriteLine("Status inválido. Digite Bom, Regular ou Ruim.");
        }
    }
    while (!ItemVistoria.StatusValido(status));

    return status;
}

// Função que exibe o relatório de todas as vistorias realizadas, incluindo detalhes do veículo e avaliação dos itens vistoriados
void ExibirRelatorioVistorias(List<Veiculo> vistorias)
{
    // Verifica se há vistorias registradas; caso contrário, exibe uma mensagem informando que não há vistorias realizadas
    if (vistorias.Count == 0)
    {
        Console.WriteLine("\nNenhuma vistoria realizada até o momento.");
        return;
    }

    Console.WriteLine("\n===================================================================");
    Console.WriteLine("AUTOCHECK .NET - MOTOR DE VISTORIA");
    Console.WriteLine("===================================================================");

    for (int indice = 0; indice < vistorias.Count; indice++)
    {
        ExibirRelatorio(vistorias[indice], indice + 1, vistorias.Count);
    }

    Console.WriteLine("===================================================================");
    Console.WriteLine("FIM DO PROCESSAMENTO DE VISTORIAS");
    Console.WriteLine("===================================================================");
}

// Função que exibe o relatório detalhado de uma vistoria específica, incluindo informações do veículo, avaliação dos itens e recomendações da oficina
void ExibirRelatorio(Veiculo veiculo, int numeroVistoria, int totalVistorias)
{
    List<ItemVistoria> itens = veiculo.VistoriaRealizada;
    List<ItemVistoria> itensCriticos = ItemVistoria.ObterCriticos(itens);
    List<ItemVistoria> itensAtencao = ItemVistoria.ObterEmAtencao(itens);

    Console.WriteLine($"[{numeroVistoria}/{totalVistorias}] PROCESSANDO VISTORIA");
    Console.WriteLine("-------------------------------------------------------------------");
    Console.WriteLine("> DADOS DO VEÍCULO:");
    Console.WriteLine($"- Tipo: {veiculo.GetType().Name}");
    Console.WriteLine($"- Modelo: {veiculo.Marca} {veiculo.Modelo}");
    Console.WriteLine($"- Ano: {veiculo.Ano} | Quilometragem: {veiculo.Quilometragem:N0} km");
    ExibirAtributoEspecifico(veiculo);
    Console.WriteLine($"> AVALIAÇÃO DOS ITENS INSPECIONADOS ({itens.Count} ITENS):");

    // Exibe cada item vistoriado com seu status, pontuação e indicadores visuais para itens críticos e em atenção
    foreach (ItemVistoria item in itens)
    {
        // Define o indicador visual com base no status do item (OK, Atenção ou Crítico)
        string indicador = "[OK]";
        if (item.EhAtencao())
        {
            indicador = "[ ! ]";
        }
        else if (item.EhCritico())
        {
            indicador = "[ X ]";
        }
        Console.WriteLine($"{indicador} {item.Nome} ---------------- Status: {item.Status} ({item.ObterPontuacao()} pts)");
    }

    // Calcula a pontuação total, percentual de aprovação e classificação final da vistoria, exibindo um resumo detalhado
    int pontuacao = ItemVistoria.ObterPontuacaoTotal(itens);
    Console.WriteLine("> RESUMO DA PONTUAÇÃO:");
    Console.WriteLine($"- Pontuação Atingida: {pontuacao} de {itens.Count * 10} pontos possíveis");
    Console.WriteLine($"- Percentual de Aprovação: {ItemVistoria.ObterPercentualAprovacao(itens):F1}%");
    Console.WriteLine($"- Classificação Final: [ {ItemVistoria.ObterClassificacao(itens)} ]");
    Console.WriteLine("> RELATÓRIO DE MANUTENÇÃO E RECOMENDAÇÕES DA OFICINA:");
    ExibirPendencias("- ITENS CRÍTICOS / REPROVADOS", itensCriticos, "AÇÃO IMEDIATA");
    ExibirPendencias("- ITENS DE ATENÇÃO", itensAtencao, "REVISÃO PREVENTIVA");
    if (itensCriticos.Count == 0 && itensAtencao.Count == 0)
    {
        Console.WriteLine("--Nenhuma pendência mecânica identificada. Veículo liberado para operação!");
    }
    else
    {
        Console.WriteLine($"- {ItemVistoria.ObterRecomendacao(itens)}");
    }
    Console.WriteLine("-------------------------------------------------------------------");
}

// Função que exibe o atributo específico de cada tipo de veículo (quantidade de portas para carros, cilindradas para motos e quantidade de eixos/capacidade de carga para caminhões)
void ExibirAtributoEspecifico(Veiculo veiculo)
{
    // Verifica o tipo do veículo e exibe o atributo específico correspondente
    if (veiculo is Carro carro)
    {
        Console.WriteLine($"- Atributo Específico: {carro.QuantidadePortas} portas");
    }
    else if (veiculo is Moto moto)
    {
        Console.WriteLine($"- Atributo Específico: {moto.Cilindradas} cilindradas");
    }
    else if (veiculo is Caminhao caminhao)
    {
        Console.WriteLine($"- Atributo Específico: {caminhao.QuantidadeEixos} eixos | Cap. Carga: {caminhao.CapacidadeCargaToneladas:F1} toneladas");
    }
}

// Função que exibe os itens críticos ou em atenção, fornecendo uma orientação específica para cada categoria
void ExibirPendencias(string titulo, List<ItemVistoria> itens, string orientacao)
{
    
    Console.WriteLine($"{titulo} ({orientacao}):");
    // Verifica se há itens críticos ou em atenção; caso contrário, exibe uma mensagem informando que não há itens identificados
    if (itens.Count == 0)
    {
        Console.WriteLine("- Nenhum item identificado.");
        return;
    }
    // Exibe cada item crítico ou em atenção, listando-os com um marcador para facilitar a visualização
    foreach (ItemVistoria item in itens)
    {
        Console.WriteLine($"- {item.Nome}");
    }
}
