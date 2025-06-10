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
                await ReplyAsync($"`{roleName}` doesn't exist.");
                return;
            }

            await targetUser.AddRoleAsync(role);
            await ReplyAsync($"Added `{role.Name}` to {targetUser.Mention}");
        }
        [Command("delrole")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task DelRoleAsync(IGuildUser targetUser, [Remainder] string roleName)
        {
            var guild = Context.Guild;
            var role = guild.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));

            if (role == null)
            {
                await ReplyAsync($"`{roleName}` doesn't exist.");
                return;
            }

            await targetUser.RemoveRoleAsync(role);
            await ReplyAsync($"Removed `{role.Name}` from {targetUser.Mention}.");
        }
        [Command("doormat")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task DoormatAsync(IGuildUser targetUser)
        {
            var guild = Context.Guild;
            var AccessRole = guild.Roles.FirstOrDefault(r => r.Name.Equals("\"Normal\" People", StringComparison.OrdinalIgnoreCase));
            var DeniedRole = guild.Roles.FirstOrDefault(r => r.Name.Equals("fed", StringComparison.OrdinalIgnoreCase));

            await targetUser.RemoveRoleAsync(AccessRole);
            await targetUser.AddRoleAsync(DeniedRole);
            await ReplyAsync($"Doormatted {targetUser.Mention}");
        }
        [Command("undoormat")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task UndoormatAsync(IGuildUser targetUser)
        {
            var guild = Context.Guild;
            var AccessRole = guild.Roles.FirstOrDefault(r => r.Name.Equals("\"Normal\" People", StringComparison.OrdinalIgnoreCase));
            var DeniedRole = guild.Roles.FirstOrDefault(r => r.Name.Equals("fed", StringComparison.OrdinalIgnoreCase));

            await targetUser.RemoveRoleAsync(DeniedRole);
            await targetUser.AddRoleAsync(AccessRole);
            await ReplyAsync($"Undoormatted {targetUser.Mention}");
        }
        [Command("kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task KickAsync(IGuildUser targetUser)
        {
            await targetUser.KickAsync();
            await ReplyAsync($"Kicked {targetUser.Mention}");
        }
        [Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task BanAsync(IGuildUser targetUser)
        {
            await targetUser.BanAsync();
            await ReplyAsync($"Good riddance to {targetUser.Mention}");
        }
    }
}