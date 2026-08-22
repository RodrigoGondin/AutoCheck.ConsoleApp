namespace AutoCheck.ConsoleApp
{
    // Classe que representa um item vistoriado durante a inspeção do veículo
	public class ItemVistoria
	{
		public string Nome { get; set; } = null!;
		public string Status { get; set; } = null!;

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
			else
			{
				return 0;
			}
		}
	}
}
