using AutoCheck.ConsoleApp.Services;

MotorVistoria motor = new MotorVistoria();
string opcao;

do
{
    Console.WriteLine("\n=== AUTOCHECK .NET ===");
    Console.WriteLine("1 - Realizar Nova Vistoria");
    Console.WriteLine("2 - Exibir Relatório das Vistorias");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma opção: ");
    opcao = Console.ReadLine()!;

    if (opcao == "1")
    {
        motor.RealizarNovaVistoria();
    }
    else if (opcao == "2")
    {
        motor.ExibirRelatorioVistorias();
    }
    else if (opcao != "0")
    {
        Console.WriteLine("Opção inválida.");
    }
}
while (opcao != "0");

Console.WriteLine("Aplicação encerrada.");
