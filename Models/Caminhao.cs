namespace AutoCheck.ConsoleApp
{
    public class Caminhao : Veiculo
    {
        // Construtor da classe Caminhao, que chama o construtor da classe base Veiculo
        public Caminhao(string marca, string modelo, int ano, int quilometragem) : base(marca, modelo, ano, quilometragem)
        {
        }

        // Propriedades específicas da classe Caminhao
        public double CapacidadeCargaToneladas { get; set; }
        public int QuantidadeEixos { get; set; }

        // Sobrescreve o método para obter o checklist obrigatório de itens a serem vistoriados para caminhões
        public override List<string> ObterChecklistObrigatorio()
        {
            var checklist = base.ObterChecklistObrigatorio();

            checklist.Add("Sistema de Freios a Ar");
            checklist.Add("Trava de longarina e Suspensão");
            checklist.Add("Tacógrafo");

            return checklist;
        }
    }
}