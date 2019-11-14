using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TGBot
{
    class Program
    {
        static ITelegramBotClient botClient;
        /// <summary>
        /// Bot Token
        /// </summary>
        private static string _telegramBotToken = "1053095447:AAHEREMKtIVJZYLFdtOBWlM3GfXaz5SZW3U";
        /// <summary>
        /// TG頻道名稱
        /// </summary>
        private static string _telegramChannelName = "@error_monitor_group";

        /// <summary>
        /// 監聽TG機器人事件
        /// </summary>
        /// 1. 參考網址 https://blog.holey.cc/2017/08/30/csharp-send-messages-by-telegram-bot/
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            botClient = new TelegramBotClient(_telegramBotToken);

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");

            botClient.OnMessage += Bot_OnMessage;
            botClient.OnMessageEdited += Bot_OnEditMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        /// <summary>
        /// 訊息修改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Bot_OnEditMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                if (e.Message.Text.Equals("sticker"))
                { 
                }
                else
                {
                    Console.WriteLine($"接收到修改的訊息，ID:{e.Message.Chat.Id}.");
                    botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "你修改的消息:\n" + e.Message.Text);
                }
            }
        }

        /// <summary>
        /// 接收訊息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                if (e.Message.Text.Equals("sticker"))
                {
                    Console.WriteLine($"接收到貼圖，ID:{e.Message.Chat.Id}.");
                    botClient.SendStickerAsync(chatId: e.Message.Chat, sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-dali.webp");
                }
                else if (e.Message.Text.Equals("video"))
                {
                    Console.WriteLine($"接收到影片，ID:{e.Message.Chat.Id}.");
                    botClient.SendVideoAsync(chatId: e.Message.Chat, video: "https://github.com/TelegramBots/book/raw/master/src/docs/video-bulb.mp4");
                }
                else
                {
                    Console.WriteLine($"接收到訊息，ID:{e.Message.Chat.Id}.");
                    botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "你傳的消息:\n" + e.Message.Text);
                    botClient.SendTextMessageAsync(chatId: _telegramChannelName, text: $"公告消息:{e.Message.Text}");
                }
            }
        }

    }
}
