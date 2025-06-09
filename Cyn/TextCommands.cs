using Discord.Commands;
using System.Threading.Tasks;

namespace Cyn
{
    public class TextCommands : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            var latency = Context.Client.Latency;
            await ReplyAsync($"Pong! {latency}ms");
        }
    }
}