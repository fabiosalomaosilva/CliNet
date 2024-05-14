using CliNet.Services;

namespace CliNet
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            switch (args.Length)
            {
                case > 0 when args[0] == "help":
                    Console.WriteLine("Comandos da CLI.Net:");
                    Console.WriteLine("------------##------------");
                    Console.WriteLine();
                    Console.WriteLine("help          Mostra os principais comandos da aplicação");
                    Console.ResetColor();
                    Console.WriteLine("");
                    break;
                case > 0 when args[0] != "help":
                    await ReturnCommands(string.Join(" ", args));
                    break;
            }
        }

        private static async Task ReturnCommands(string value)
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"CLI.Net - Gerando comandos para: {value}...");
            Console.WriteLine("-----------------------------------------------");
            Console.ResetColor();
            Console.WriteLine("");

            var commands = await GptService.CallApiGpt(value);
            if (!commands.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Não há comandos com o(s) termo(s) informado(s). Tente novamente.");
                Console.ResetColor();
                return;
            }
            foreach (var techCommand in commands)
            {
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Tecnologia: {techCommand.tech}");
                Console.WriteLine("-----------------------------------------------");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var command in techCommand.commands)
                {
                    Console.WriteLine($"Comando: {command}");
                }
                Console.ResetColor();
                Console.WriteLine("");
            }
        }
    }
}
