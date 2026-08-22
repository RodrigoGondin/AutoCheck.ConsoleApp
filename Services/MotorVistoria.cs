using AutoCheck.ConsoleApp;

namespace AutoCheck.ConsoleApp.Services
{
    // Classe responsável por gerenciar as vistorias de veículos
	public class MotorVistoria
	{
        // Lista de vistorias realizadas, armazenando os veículos vistoriados
		public List<Veiculo> Vistorias { get; set; } = new List<Veiculo>();

		public void RealizarNovaVistoria()
		{
			Veiculo veiculo = CriarVeiculo();

			Console.WriteLine("\nInforme o status de cada item usando Bom, Regular ou Ruim.");

			foreach (string nomeItem in veiculo.ObterChecklistObrigatorio())
			{
				string status = LerStatus(nomeItem);
				veiculo.AdicionarItemVistoriado(nomeItem, status);
			}

			this.Vistorias.Add(veiculo);
			Console.WriteLine("\nVistoria registrada com sucesso.");
		}
        // Método para exibir o relatório de todas as vistorias realizadas
		public void ExibirRelatorioVistorias()
		{
			if (this.Vistorias.Count == 0)
			{
				Console.WriteLine("\nNenhuma vistoria realizada até o momento.");
				return;
			}

			int numeroVistoria = 1;

			foreach (Veiculo veiculo in this.Vistorias)
			{
				ExibirRelatorio(veiculo, numeroVistoria);
				numeroVistoria++;
			}
		}
        // Método privado para criar um veículo com base nas informações fornecidas pelo usuário
		private Veiculo CriarVeiculo()
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

			if (tipo == "1")
			{
				Console.Write("Quantidade de portas: ");
				int quantidadePortas = int.Parse(Console.ReadLine()!);

				return new Carro(marca, modelo, ano, quilometragem)
				{
					QuantidadePortas = quantidadePortas
				};
			}
			else if (tipo == "2")
			{
				Console.Write("Cilindradas: ");
				int cilindradas = int.Parse(Console.ReadLine()!);

				return new Moto(marca, modelo, ano, quilometragem)
				{
					Cilindradas = cilindradas
				};
			}
			else
			{
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
		}
        // Método privado para ler o status de um item da vistoria, garantindo que seja válido
		private string LerStatus(string nomeItem)
		{
			string status;

			do
			{
				Console.Write($"{nomeItem}: ");
				status = Console.ReadLine()!;

				if (status != "Bom" && status != "Regular" && status != "Ruim")
				{
					Console.WriteLine("Status inválido. Digite Bom, Regular ou Ruim.");
				}
			}
			while (status != "Bom" && status != "Regular" && status != "Ruim");

			return status;
		}
        // Método privado para calcular a pontuação total com base nos itens vistoriados
		private int CalcularPontuacao(List<ItemVistoria> itens)
		{
			int pontuacao = 0;

			foreach (ItemVistoria item in itens)
			{
				pontuacao += item.ObterPontuacao();
			}

			return pontuacao;
		}
        // Método privado para calcular o percentual de aprovação com base na pontuação obtida e na pontuação máxima possível
		private double CalcularPercentualAprovacao(List<ItemVistoria> itens)
		{
			int pontuacaoMaxima = itens.Count * 10;

			if (pontuacaoMaxima == 0)
			{
				return 0;
			}

			return (double)CalcularPontuacao(itens) / pontuacaoMaxima * 100;
		}
        // Método privado para classificar o estado do veículo com base no percentual de aprovação
		private string ClassificarEstado(List<ItemVistoria> itens)
		{
			double percentual = CalcularPercentualAprovacao(itens);

			if (percentual >= 90)
			{
				return "Aprovado com Excelência - Liberado imediatamente.";
			}
			else if (percentual >= 60)
			{
				return "Aprovado com Apontamentos - Exige desconto na compra para reparos da oficina.";
			}
			else
			{
				return "Reprovado na Vistoria - Veículo recusado pela concessionária.";
			}
		}
        // Método privado para obter a lista de itens críticos (status "Ruim") da vistoria
		private List<ItemVistoria> ObterItensCriticos(List<ItemVistoria> itens)
		{
			List<ItemVistoria> itensCriticos = new List<ItemVistoria>();

			foreach (ItemVistoria item in itens)
			{
				if (item.Status == "Ruim")
				{
					itensCriticos.Add(item);
				}
			}

			return itensCriticos;
		}
        // Método privado para obter a lista de itens de atenção (status "Regular") da vistoria
		private List<ItemVistoria> ObterItensAtencao(List<ItemVistoria> itens)
		{
			List<ItemVistoria> itensAtencao = new List<ItemVistoria>();

			foreach (ItemVistoria item in itens)
			{
				if (item.Status == "Regular")
				{
					itensAtencao.Add(item);
				}
			}

			return itensAtencao;
		}
        // Método privado para obter recomendações de serviços com base nos itens críticos e de atenção da vistoria
		private string ObterRecomendacaoServicos(List<ItemVistoria> itens)
		{
			bool possuiItemCritico = false;
			bool possuiItemAtencao = false;

			foreach (ItemVistoria item in itens)
			{
				if (item.Status == "Ruim")
				{
					possuiItemCritico = true;
				}
				else if (item.Status == "Regular")
				{
					possuiItemAtencao = true;
				}
			}

			if (possuiItemCritico && possuiItemAtencao)
			{
				return "Prioridade: executar os reparos ou trocas dos itens críticos e realizar a revisão preventiva dos itens de atenção.";
			}
			else if (possuiItemCritico)
			{
				return "Prioridade: executar os reparos ou trocas dos itens críticos.";
			}
			else if (possuiItemAtencao)
			{
				return "Prioridade: realizar a revisão preventiva dos itens de atenção.";
			}
			else
			{
				return "Nenhum serviço prioritário identificado.";
			}
		}
        // Método privado para exibir o relatório detalhado de uma vistoria específica, incluindo dados do veículo, avaliação dos itens inspecionados, resumo da pontuação e recomendações de manutenção
		private void ExibirRelatorio(Veiculo veiculo, int numeroVistoria)
		{
			int pontuacao = CalcularPontuacao(veiculo.VistoriaRealizada);
			int pontuacaoMaxima = veiculo.VistoriaRealizada.Count * 10;
			double percentual = CalcularPercentualAprovacao(veiculo.VistoriaRealizada);
			List<ItemVistoria> itensCriticos = ObterItensCriticos(veiculo.VistoriaRealizada);
			List<ItemVistoria> itensAtencao = ObterItensAtencao(veiculo.VistoriaRealizada);

			Console.WriteLine("\n===================================================================");
			Console.WriteLine($"[{numeroVistoria}] RELATÓRIO DA VISTORIA");
			Console.WriteLine("-------------------------------------------------------------------");
			Console.WriteLine("> DADOS DO VEÍCULO:");
			Console.WriteLine($"- Tipo: {veiculo.GetType().Name}");
			Console.WriteLine($"- Marca: {veiculo.Marca}");
			Console.WriteLine($"- Modelo: {veiculo.Modelo}");
			Console.WriteLine($"- Ano: {veiculo.Ano} | Quilometragem: {veiculo.Quilometragem:N0} km");
			ExibirAtributoEspecifico(veiculo);

			Console.WriteLine($"> AVALIAÇÃO DOS ITENS INSPECIONADOS ({veiculo.VistoriaRealizada.Count} ITENS):");
			foreach (ItemVistoria item in veiculo.VistoriaRealizada)
			{
				string indicador = "[OK]";

				if (item.Status == "Regular")
				{
					indicador = "[ ! ]";
				}
				else if (item.Status == "Ruim")
				{
					indicador = "[ X ]";
				}

				Console.WriteLine($"{indicador} {item.Nome} - Status: {item.Status} ({item.ObterPontuacao()} pts)");
			}

			Console.WriteLine("> RESUMO DA PONTUAÇÃO:");
			Console.WriteLine($"- Pontuação Atingida: {pontuacao} de {pontuacaoMaxima} pontos possíveis");
			Console.WriteLine($"- Percentual de Aprovação: {percentual:F1}%");
			Console.WriteLine($"- Classificação Final: {ClassificarEstado(veiculo.VistoriaRealizada)}");

			Console.WriteLine("> RELATÓRIO DE MANUTENÇÃO E RECOMENDAÇÕES DA OFICINA:");
			ExibirPendencias("ITENS CRÍTICOS / REPROVADOS", itensCriticos, "troca/reparo obrigatório");
			ExibirPendencias("ITENS DE ATENÇÃO", itensAtencao, "revisão preventiva");
			Console.WriteLine($"- {ObterRecomendacaoServicos(veiculo.VistoriaRealizada)}");
			Console.WriteLine("-------------------------------------------------------------------");
		}
        // Método privado para exibir atributos específicos de cada tipo de veículo (Carro, Moto, Caminhão)
		private void ExibirAtributoEspecifico(Veiculo veiculo)
		{
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
        // Método privado para exibir pendências de manutenção, listando os itens críticos e de atenção com suas respectivas orientações
		private void ExibirPendencias(string titulo, List<ItemVistoria> itens, string orientacao)
		{
			Console.WriteLine($"{titulo} ({orientacao}):");

			if (itens.Count == 0)
			{
				Console.WriteLine("- Nenhum item identificado.");
			}
			else
			{
				foreach (ItemVistoria item in itens)
				{
					Console.WriteLine($"- {item.Nome}");
				}
			}
		}
	}
}
