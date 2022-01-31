using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantBot.Services
{
    public class CommandHandler : DiscordClientService
    {
        private readonly DiscordSocketClient _discord;
        private readonly IServiceProvider _provider;
        private readonly CommandService _commandService;
        private readonly IConfiguration _config;

        public CommandHandler(DiscordSocketClient client, ILogger<CommandHandler> logger, IServiceProvider provider, CommandService commandService, IConfiguration config) : base(client, logger)
        {
            _discord = client;
            _provider = provider;
            _commandService = commandService;
            _config = config;
        }
        // This'll be executed during startup.
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Client.MessageReceived += HandleMessage;
            //_commandService.CommandExecuted += CommandExecutedAsync;
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        private async Task HandleMessage(SocketMessage s)
        {
            var msg = s as SocketUserMessage;     // Ensure the message is from a user/bot
            if (msg == null) return;
            if (msg.Author.Id == _discord.CurrentUser.Id) return;     // Ignore self when checking commands

            var context = new SocketCommandContext(_discord, msg);     // Create the command context

            int argPos = 0;     // Check if the message has a valid command prefix
            if (msg.HasStringPrefix(_config["prefix"], ref argPos) || msg.HasMentionPrefix(_discord.CurrentUser, ref argPos))
            {
                var result = await _commandService.ExecuteAsync(context, argPos, _provider);     // Execute the command

                if (!result.IsSuccess)     // If not successful, reply with the error.
                    await context.Channel.SendMessageAsync(result.ToString());
            }
        }
    }
}
