using Discord;
using Discord.Interactions;

namespace Cyn
{
    public class Commands : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("ping", "replies with latency.")]
        public async Task Ping()
        {
            var latency = Context.Client.Latency;
            await RespondAsync($"Ping: {latency}ms");
        }
    }
}