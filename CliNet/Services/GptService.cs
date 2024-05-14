using CliNet.Credentials;
using CliNet.Models;
using Newtonsoft.Json;
using OpenAI_API.Chat;
using OpenAI_API.Models;

namespace CliNet.Services
{
    public static class GptService
    {
        public static async Task<List<TerminalCommand>> CallApiGpt(string value)
        {
            var prompt = $@"Retorne um ou mais comandos de terminal utilizados por Desenvolvedores de Software que contenha obrigatoriamente os seguintes termos: {value.ToUpper()}. " +
                         "Modelo da LISTA de objetos JSON: [content: [tech: string, commands: string[]]]. " +
                         "Legenda: " +
                         "tech = tecnologia do comando (exemplo de tecnologia: git)" +
                         "commands = lista de comandos (exemplo: git clone [source])";

            var apiKey = CredentialManager.GetCredential();
            if (string.IsNullOrEmpty(apiKey))
            {
                var newApyValid = false;
                Console.WriteLine("---------------");
                Console.WriteLine("");
                Console.ResetColor();
                while (!newApyValid)
                {
                    Console.WriteLine("");
                    Console.Write("Por favor, insira uma nova chave da API da OpenAI: ");
                    var newApiKey = Console.ReadLine();
                    if (string.IsNullOrEmpty(newApiKey) || newApiKey.Length < 45)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("API Key inválida. Insira um API válida");
                    }
                    else
                    {
                        CredentialManager.SaveCredential(newApiKey);
                        apiKey = newApiKey;
                        newApyValid = true;
                        Console.ResetColor();
                    }
                }
            }

            try
            {
                var api = new OpenAI_API.OpenAIAPI(apiKey);
                var result = await api.Chat.CreateChatCompletionAsync(new ChatRequest
                {
                    Model = Model.ChatGPTTurbo_1106,
                    Temperature = 0.0,
                    MaxTokens = 250,
                    ResponseFormat = ChatRequest.ResponseFormats.JsonObject,
                    Messages =
                    [
                        new ChatMessage(ChatMessageRole.System, "You are a helpful assistant designed to output LIST of JSON objects."),
                        new ChatMessage(ChatMessageRole.User, prompt)
                    ]
                });

                var jsonResult = result?.Choices[0].Message.TextContent;
                if (jsonResult == null) return [];
                var contentResponse = JsonConvert.DeserializeObject<ContentResponse>(jsonResult, new JsonSerializerSettings { Formatting = Formatting.None }) ?? new ContentResponse();
                return contentResponse.Content;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
