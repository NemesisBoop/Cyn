using Discord;
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

        [Command("addrole")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task AddRoleAsync(IGuildUser targetUser, [Remainder] string roleName)
        {
            var guild = Context.Guild;
            var role = guild.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));

            if (role == null)
            {
                await ReplyAsync($"Role `{roleName}` not found.");
                return;
            }

            await targetUser.AddRoleAsync(role);
            await ReplyAsync($"Added `{role.Name}` to {targetUser.Mention}");
        }
        [Command("doormat")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task DoormatAsync(IGuildUser targetUser)
        {
            var guild = Context.Guild;
            var role = guild.Roles.FirstOrDefault(r => r.Name.Equals("\"Normal\" People", StringComparison.OrdinalIgnoreCase));

            await targetUser.RemoveRoleAsync(role);
            await ReplyAsync($"Doormatted {targetUser.Mention}");
        }
        [Command("undoormat")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task UndoormatAsync(IGuildUser targetUser)
        {
            var guild = Context.Guild;
            var role = guild.Roles.FirstOrDefault(r => r.Name.Equals("\"Normal\" People", StringComparison.OrdinalIgnoreCase));

            await targetUser.AddRoleAsync(role);
            await ReplyAsync($"Undoormatted {targetUser.Mention}");
        }
    }
}