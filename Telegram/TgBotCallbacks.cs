using Microsoft.EntityFrameworkCore;
using Shop.Context.Table;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Shop.Telegram
{
    public partial class TelegramBot
    {

        [TelegramBotCallback("callbackexample")]
        async Task BackCallback(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            await botClient.AnswerCallbackQueryAsync(update.CallbackQuery?.Id, "callbackexample", false, cancellationToken: cancellationToken);

            await botClient.SendTextMessageAsync(
                update.CallbackQuery.From.Id,
                $"Hello! {update.CallbackQuery.From.Username}\nAuthor: White Nigga#7327",
                cancellationToken: cancellationToken);

        }
    }
}
       