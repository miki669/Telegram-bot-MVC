namespace Shop.Telegram
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TelegramBotCommandAttribute : Attribute
    {
        public string command { get; set; }
        public TelegramBotCommandAttribute(string command)
        {
            this.command = command;
        }

    }


    [AttributeUsage(AttributeTargets.Method)]
    public class TelegramBotCallbackAttribute : Attribute
    {
        public string command { get; set; }
        public TelegramBotCallbackAttribute(string command)
        {
            this.command = command;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class TelegramChanellCommandAttribute : Attribute
    {
        public string command { get; set; }
        public TelegramChanellCommandAttribute(string command)
        {
            this.command = command;
        }

    }


}
