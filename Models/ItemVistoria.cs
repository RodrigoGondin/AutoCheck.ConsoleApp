namespace AutoCheck.ConsoleApp
{
    // Classe que representa um item vistoriado em um veículo
	public class ItemVistoria
	{
        // Propriedades do item vistoriado
		public string Nome { get; set; }
		public string Status { get; set; }

        // Método que verifica se o item está crítico
		public bool EhCritico()
		{
			return this.Status == "Ruim";
		}

        // Método que verifica se o item está em atenção
		public bool EhAtencao()
		{
			return this.Status == "Regular";
		}

        // Método que verifica se o status do item é válido (Bom, Regular ou Ruim)
		public static bool StatusValido(string status)
		{
			return status == "Bom" || status == "Regular" || status == "Ruim";
		}

        // Método que obtém a pontuação do item vistoriado com base no seu status
		public int ObterPontuacao()
		{
			if (this.Status == "Bom")
			{
				return 10;
			}
			else if (this.Status == "Regular")
			{
				return 5;
			}

			return 0;
		}

        // Método que calcula a pontuação total de uma lista de itens vistoriados
		public static int ObterPontuacaoTotal(List<ItemVistoria> itens)
		{
			int pontuacao = 0;
			foreach (ItemVistoria item in itens)
			{
				pontuacao += item.ObterPontuacao();
			}

			return pontuacao;
		}

        // Método que calcula o percentual de aprovação com base na pontuação total e na pontuação máxima possível
		public static double ObterPercentualAprovacao(List<ItemVistoria> itens)
		{
			int pontuacaoMaxima = itens.Count * 10;
			if (pontuacaoMaxima == 0)
			{
				return 0;
			}

			return (double)ObterPontuacaoTotal(itens) / pontuacaoMaxima * 100;
		}

        // Método que obtém a classificação da vistoria com base no percentual de aprovação
		public static string ObterClassificacao(List<ItemVistoria> itens)
		{
			double percentual = ObterPercentualAprovacao(itens);
			if (percentual >= 90)
			{
				return "APROVADO COM EXCELÊNCIA";
			}
			else if (percentual >= 60)
			{
				return "APROVADO COM APONTAMENTOS";
			}

			return "REPROVADO NA VISTORIA";
		}

        // Método que obtém os itens críticos (status "Ruim") de uma lista de itens vistoriados
		public static List<ItemVistoria> ObterCriticos(List<ItemVistoria> itens)
		{
			return ObterItensPorStatus(itens, "Ruim");
		}

        // Método que obtém os itens em atenção (status "Regular") de uma lista de itens vistoriados
		public static List<ItemVistoria> ObterEmAtencao(List<ItemVistoria> itens)
		{
			return ObterItensPorStatus(itens, "Regular");
		}

        // Método auxiliar que filtra os itens por status específico
		private static List<ItemVistoria> ObterItensPorStatus(List<ItemVistoria> itens, string status)
		{
			List<ItemVistoria> resultado = new List<ItemVistoria>();
			foreach (ItemVistoria item in itens)
			{
				if (item.Status == status)
				{
					resultado.Add(item);
				}
			}

			return resultado;
		}

        // Método que fornece recomendações com base nos itens críticos e em atenção
		public static string ObterRecomendacao(List<ItemVistoria> itens)
		{
			bool possuiCritico = ObterCriticos(itens).Count > 0;
			bool possuiAtencao = ObterEmAtencao(itens).Count > 0;

			if (possuiCritico && possuiAtencao)
			{
				return "Prioridade: executar os reparos ou trocas dos itens críticos e realizar a revisão preventiva dos itens de atenção.";
			}
			else if (possuiCritico)
			{
				return "Prioridade: executar os reparos ou trocas dos itens críticos.";
			}
			else if (possuiAtencao)
			{
				return "Prioridade: realizar a revisão preventiva dos itens de atenção.";
			}

			return "Nenhum serviço prioritário identificado.";
		}
	}
}
