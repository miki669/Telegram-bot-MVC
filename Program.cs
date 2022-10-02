using Microsoft.EntityFrameworkCore;
using Shop.Context;
using Shop.Telegram;

namespace Shop
{
    internal class Program
    {
        
        const string Token = "YourToken";
        
        static async Task Main()
        {

            #region DBData
            string primaryConnStr = string.Format
               ("Server={0};" +
                "Port={1};" +
                "Database={2};" +
                "User ID={3};" +
                "Password={4};" +
                "Pooling=true;Connection Lifetime=0;SslMode=Disable;SslMode=Disable;",
                "localhost",
                "YourPort",
                "YourDataBase",
                "YourUserName",
                "YourPassword");
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            #endregion


            var connectionOption = new DbContextOptionsBuilder<PrimaryDatabaseContext>().UseNpgsql(primaryConnStr).Options;
            /*"if you are using a database, uncomment the following"*/
            TelegramBot bot = new(Token/*"connectionOption"*/);
            await bot.StartAsync();
            Console.Read();
        }
    }
}
