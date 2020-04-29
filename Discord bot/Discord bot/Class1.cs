using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;

namespace Discord_bot
{
    public class Bot1
    {
        public DiscordClient Client{ get; private set; }
        public CommandsNextModule Commands { get; private set; }
        public async Task RunAsync()
        {
            var json = string.Empty;
            using (var fs = System.IO.File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);
            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };
            Client = new DSharpPlus.DiscordClient(config);    
            Client.Ready += OnClientReady;
            var commandsConfig = new CommandsNextConfiguration {
                StringPrefix =   configJson.Prefix,
            EnableMentionPrefix = true,
            
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            await Client.ConnectAsync();
            await Task.Delay(-1);
        
        
        }
        
        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
