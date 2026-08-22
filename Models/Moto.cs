namespace AutoCheck.ConsoleApp
{
    public class Moto : Veiculo
    {
        // Construtor da classe Moto, que chama o construtor da classe base Veiculo
        public Moto(string marca, string modelo, int ano, int quilometragem) : base(marca, modelo, ano, quilometragem)
        {  
        }
        // Propriedade específica da classe Moto
        public int Cilindradas { get; set; }
        
        // Sobrescreve o método para obter o checklist obrigatório de itens a serem vistoriados para motos
        public override List<string> ObterChecklistObrigatorio()
        {
            var checklist = base.ObterChecklistObrigatorio();

            checklist.Add("Transmissão e Corrente");
            checklist.Add("Freios e Embreagem");
            checklist.Add("Apoio lateral");

            return checklist;
        }
    }
}