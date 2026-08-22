namespace AutoCheck.ConsoleApp
{
    public abstract class Veiculo
    // Propriedades do veículo
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public int Quilometragem { get; set; }
        public List<ItemVistoria> VistoriaRealizada { get; set; }

        // Construtor da classe Veiculo
        public Veiculo(string marca, string modelo, int ano, int quilometragem)
        {
            this.Marca = marca;
            this.Modelo = modelo;
            this.Ano = ano;
            this.Quilometragem = quilometragem;
            this.VistoriaRealizada = new List<ItemVistoria>();
        }

        // Método para adicionar um item vistoriado à lista de vistoria realizada
        public void AdicionarItemVistoriado(string nome, string status)
        {
            this.VistoriaRealizada.Add(new ItemVistoria
            {
                Nome = nome,
                Status = status
            });
        }

        // Método para obter o checklist obrigatório de itens a serem vistoriados
        public virtual List<string> ObterChecklistObrigatorio()
        {
            return new List<string>
            {
                "Motor e Transmissão",
                "Suspensão e Freios",
                "Luzes e Sinalização",
                "Pneus e Rodas",
                "Sistema Elétrico",
                "Funilaria e Estrutura",
            };
        }
    }
}