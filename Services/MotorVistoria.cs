using AutoCheck.ConsoleApp;

namespace AutoCheck.ConsoleApp.Services
{
    // Classe responsável por gerenciar as vistorias realizadas em veículos
	public class MotorVistoria
	{
        // Lista de veículos que passaram por vistoria
		public List<Veiculo> Vistorias { get; set; } = new List<Veiculo>();

        // Método que realiza a vistoria em um veículo, registrando o status de cada item
		public void RealizarVistoria(Veiculo veiculo, List<string> statusItens)
		{
			List<string> checklist = veiculo.ObterChecklistObrigatorio();
			for (int indice = 0; indice < statusItens.Count; indice++)
			{
				veiculo.AdicionarItemVistoriado(checklist[indice], statusItens[indice]);
			}

			this.Vistorias.Add(veiculo);
		}
	}
}
