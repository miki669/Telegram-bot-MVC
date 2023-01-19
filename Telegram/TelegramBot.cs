using Microsoft.EntityFrameworkCore;
using Shop.Context;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Shop.Telegram
{
    public partial class TelegramBot
    {
        public static TelegramBot Instanse { get; set; }
        delegate Task TgBotCommand(TelegramBot instance, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
        delegate Task TgBotCallback(TelegramBot instance, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args);
        delegate Task TgChanellCommand(TelegramBot instance, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

        Dictionary<string, TgBotCommand> commandHandlers = new();
        Dictionary<string, TgChanellCommand> ChanellCommandHandlers = new();
        Dictionary<string, TgBotCallback> callbackHandlers = new();
        TelegramBotClient client;
        private DbContextOptions<PrimaryDatabaseContext> _primaryDatabaseContext;
        public PrimaryDatabaseContext DbContext;
        public TelegramBot(string token)
        {
            client = new TelegramBotClient(token);
        }
        public TelegramBot(string token, DbContextOptions<PrimaryDatabaseContext> contextOptions)
        {
            primaryDatabaseContext = contextOptions;
            client = new TelegramBotClient(token);
            dbContext = new(contextOptions);
            Instanse = this;
        }
        public async Task StartAsync()
        {
            await Task.CompletedTask;

            var methods = typeof(TelegramBot).GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            foreach (var method in methods)
            {
                if (method.GetCustomAttributes(false).FirstOrDefault(p => p.GetType() == typeof(TelegramBotCommandAttribute)) is not TelegramBotCommandAttribute attr)
                {
                    continue;
                }
                TgBotCommand deleg = method.CreateDelegate<TgBotCommand>();
                commandHandlers.Add(attr.command, deleg);
            }
            foreach (var method in methods)
            {
                if (method.GetCustomAttributes(false).FirstOrDefault(p => p.GetType() == typeof(TelegramBotCallbackAttribute)) is not TelegramBotCallbackAttribute attr)
                {
                    continue;
                }
                TgBotCallback deleg = method.CreateDelegate<TgBotCallback>();
                callbackHandlers.Add(attr.command, deleg);
            }
            foreach (var method in methods)
            {
                if (method.GetCustomAttributes(false).FirstOrDefault(p => p.GetType() == typeof(TelegramChanellCommandAttribute)) is not TelegramChanellCommandAttribute attr)
                {
                    continue;
                }
                TgChanellCommand deleg = method.CreateDelegate<TgChanellCommand>();
                ChanellCommandHandlers.Add(attr.command, deleg);
            }

            client.StartReceiving(
               HandleUpdateAsync,
               HandleErrorAsync,
               new ReceiverOptions
               {
                   AllowedUpdates = { }
               });
        }
       
        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (update.Type == UpdateType.Message)
            {
                var command = update.Message?.Text?.ToLower();
                if (command == null)
                {
                    return; // Empty command
                }
                if (!commandHandlers.ContainsKey(command))
                {
                    return; // Command not found;
                }
               
                commandHandlers[command].DynamicInvoke(this, botClient, update, cancellationToken);
            }


            // Only process Callback updates
            if (update.Type == UpdateType.CallbackQuery)
            {
                

                var callback = update?.CallbackQuery?.Data?.ToLower();

                if (callback == null) return;
                if (!callbackHandlers.ContainsKey(callback))
                {
                    return; // Command not found;
                }
               
                
                var chatId = update.CallbackQuery.Message.Chat.Id;
                callbackHandlers[callback].DynamicInvoke(this, botClient, update, cancellationToken, new string[]{""});
                
            }

        }
        private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                     => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return;
        }
    }
   
}
