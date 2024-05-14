using Newtonsoft.Json;

namespace CliNet.Models
{

    public class ContentResponse
    {
        public List<TerminalCommand> content { get; set; } = [];
    }

    public class TerminalCommand
    {
        [JsonProperty("tech")]
        public string? tech { get; set; }

        [JsonProperty("commands")]
        public List<string> commands { get; set; } = [];
    }
}
