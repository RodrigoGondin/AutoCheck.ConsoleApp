namespace AutoCheck.ConsoleApp
{
    public class Carro : Veiculo
    {
        // Construtor da classe Carro, que chama o construtor da classe base Veiculo
        public Carro(string marca, string modelo, int ano, int quilometragem)
            : base(marca, modelo, ano, quilometragem)
            {
            }
            
        // Propriedade específica da classe Carro
        public int QuantidadePortas { get; set; }

        // Sobrescreve o método para obter o checklist obrigatório de itens a serem vistoriados para carros
        public override List<string> ObterChecklistObrigatorio()
        {
            var checklist = base.ObterChecklistObrigatorio();
            
            checklist.Add("Estepe e Macaco");
            checklist.Add("Triângulo de Sinalização");
            checklist.Add("Ar-condicionado funcional");

            return checklist;
        }
    }
}


