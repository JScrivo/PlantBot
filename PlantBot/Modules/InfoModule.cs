using Discord.Commands;

namespace PlantBot.Modules
{
    public class General : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("Pong");
        }
    }
}