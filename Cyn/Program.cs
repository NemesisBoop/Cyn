using Discord;
using Discord.WebSocket;
using Discord.Interactions;
using Discord.Commands;
using Newtonsoft.Json;

namespace Cyn
{
    class BotConfig
    {
        public string? Token { get; set; }
        public string? Prefix { get; set; }
    }
    class Program
    {
        public static async Task Main(string[] args)
        {
            var configText = File.ReadAllText("confreak.json");
            var configJson = JsonConvert.DeserializeObject<BotConfig>(configText);

            var token = configJson!.Token;
            var prefix = configJson.Prefix;

            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.All,
                MessageCacheSize = 300,
                AuditLogCacheSize = 300,
            };
            var client = new DiscordSocketClient(config);
            var interactionService = new InteractionService(client.Rest);

            var commandService = new CommandService();
            await commandService.AddModulesAsync(typeof(Program).Assembly, null);

            client.MessageReceived += async message =>
            {
                if (message is not SocketUserMessage userMessage) return;
                if (userMessage.Author.IsBot) return;

                int argPos = 0;
                if (userMessage.HasStringPrefix(prefix, ref argPos))
                {
                    var context = new SocketCommandContext(client, userMessage);
                    await commandService.ExecuteAsync(context, argPos, null);
                }
            };

            client.Ready += async () =>
            {
                await interactionService.AddModulesAsync(typeof(Program).Assembly, null);
                await interactionService.RegisterCommandsGloballyAsync();
            };
            client.InteractionCreated += async interaction =>
            {
                var ctx = new SocketInteractionContext(client, interaction);
                await interactionService.ExecuteCommandAsync(ctx, null);
            };

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            Console.Clear();
            Console.WriteLine("Prefix Commands Initialised!");
            Console.WriteLine("Application Commands Initialised!");
            Console.WriteLine("Connected to Discord!");

            await Task.Delay(-1);
        }
    }
}