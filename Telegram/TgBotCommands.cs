using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Shop.Telegram
{
    public partial class TelegramBot
    {
        [TelegramBotCommand("/start")]
        async Task StartCommand(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                new []{  InlineKeyboardButton.WithCallbackData(text: "CallbackExample", callbackData: "callbackexample") }
            });

            await botClient.SendTextMessageAsync(
            chatId: update.Message.Chat.Id,
            replyMarkup: inlineKeyboard,
            text: $"Hello {update.Message.Chat.Username}! \nAuthor: White Nigga#7327",
            
            cancellationToken: cancellationToken);
           

        }
    }
}