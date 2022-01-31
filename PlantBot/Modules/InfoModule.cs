using Discord.Commands;
using PlantBot.Pi;

namespace PlantBot.Modules
{
    public class General : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("Pong");
        }

        [Command("cam")]
        public async Task CameraAsync()
        {
            Camera camera = new Camera();

            await camera.TakePicture();

            var directory = new DirectoryInfo("/home/pi/images");

            var file = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();

            await Context.Channel.SendFileAsync(file.FullName);
        }
    }
}