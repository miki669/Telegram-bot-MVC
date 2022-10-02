using Telegram.Bot;
using Telegram.Bot.Types;

namespace Shop.Telegram
{
    public partial class TelegramBot
    {
        public int Article { get; set; }
        public int ParceProducId { get; set; }

        [TelegramChanellCommand("")]
        async Task ExampleChanellPost(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
           
        }
    }
}
