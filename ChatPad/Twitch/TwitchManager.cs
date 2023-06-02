using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatPad.Configuration;
using ChatPad.Twitch;

namespace ChatPad
{
    internal class TwitchManager
    {
        public static TwitchManager Manager { get; private set; }

        private const string URL = "irc.chat.twitch.tv";
        private const int PORT = 6667;
        private const string USER = "astro_test_bot";
        private const string oauth = "oauth:itfs6m413nrgzcg3u7hu8h0tt70h3y";

        private TcpClient twitch;
        private StreamReader reader;
        private StreamWriter writer;

        private Thread listenLoop;

        private static Random random = new Random();

        public static Stack<ChatMessage> Messages = new Stack<ChatMessage>();

        public bool IsConnected
        {
            get
            {
                return twitch != null && twitch.Connected && listenLoop != null;
            }
        }

        public TwitchManager()
        {
            Manager = this;
        }

        public void Connect(string channel)
        {
            Config.Settings.Channel = channel;

            twitch = new TcpClient(URL, PORT);
            reader = new StreamReader(twitch.GetStream());
            writer = new StreamWriter(twitch.GetStream());

            //Log In
            writer.WriteLine("PASS " + oauth);
            writer.WriteLine("NICK " + USER.ToLower());
            writer.WriteLine("JOIN #" + channel.ToLower());
            writer.Flush();
        }

        public static void Stop()
        {
            if (Manager != null && Manager.IsConnected) 
            {
                Manager.listenLoop = null;
            }
        }

        public static bool TryConnect(string channel)
        {
            Manager = new TwitchManager();

            Config.Settings.Channel = channel;

            Manager.twitch = new TcpClient(URL, PORT);
            Manager.reader = new StreamReader(Manager.twitch.GetStream());
            Manager.writer = new StreamWriter(Manager.twitch.GetStream());

            //Log In
            Manager.writer.WriteLine("PASS " + oauth);
            Manager.writer.WriteLine("NICK " + USER.ToLower());
            Manager.writer.WriteLine("JOIN #" + channel.ToLower());
            Manager.writer.Flush();

            int iterations = 0;
            while (iterations < 40)
            {
                //Check for messages
                if (Manager.twitch.Available > 0)
                {
                    if (Manager.reader.ReadLine().Contains("Welcome, GLHF!"))
                    {//Connected
                        Manager.listenLoop = new Thread(new ThreadStart(Manager.Listen));
                        Manager.listenLoop.Start();

                        Manager.writer.WriteLine("CAP REQ :twitch.tv/tags");
                        Manager.writer.Flush();

                        return true;
                    }
                }
                iterations++;
                Thread.Sleep(50);
            }

            return false;
        }

        public bool listen;
        private void Listen()
        {
            listen = true;
            long lastPing = DateTime.UtcNow.Ticks;
            while (listenLoop != null)
            {
                //Reconnect
                if (!twitch.Connected) Connect(Config.Settings.Channel);

                //PING to keep connection alive
                if (lastPing + 600000000 < DateTime.UtcNow.Ticks)
                {
                    lastPing = DateTime.UtcNow.Ticks;
                    writer.WriteLine("PING " + URL);
                    writer.Flush();
                }

                //Messages Available
                while (twitch.Available > 0)
                {
                    string msg = reader.ReadLine();

                    if (msg == null) continue;

                    if (msg.Contains("PRIVMSG"))
                    {//Chat Message!
                        ChatMessage chatMessage;
                        try
                        {
                            chatMessage = new ChatMessage(msg);
                        }
                        catch
                        {
                            continue;
                        }

                        if (chatMessage.text.Trim() == "!ahhhhh" || chatMessage.text.Trim() == "!help")
                        {//Send list of commands
                            writer.WriteLine("@reply-parent-msg-id=" + chatMessage.id + " PRIVMSG #" + Config.Settings.Channel + " : " + Config.Commands.GenerateHelpList());
                            writer.Flush();
                        }
                        /*else if (chatMessage.text.Trim() == "!where")
                        {//Easter egg
                            int rnd = random.Next(5);

                            switch (rnd)
                            {
                                case 1:
                                    writer.WriteLine("@reply-parent-msg-id=" + chatMessage.id + " PRIVMSG #" + Channel + " :Here, copy and paste this: \" \"");
                                    break;
                                case 2:
                                    string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                                    writer.WriteLine("@reply-parent-msg-id=" + chatMessage.id + " PRIVMSG #" + Channel + " :It's next to the [" + chars[random.Next(chars.Length)] + "] key");
                                    break;
                                case 3:
                                    writer.WriteLine("@reply-parent-msg-id=" + chatMessage.id + " PRIVMSG #" + Channel + " :Imagine not having a spacebar");
                                    break;
                                case 4:
                                    writer.WriteLine("@reply-parent-msg-id=" + chatMessage.id + " PRIVMSG #" + Channel + " :\"I decided to take the really big key off my keyboard and throw it away. It’s a waste of space.\" -Probably You");
                                    break;
                                default:
                                    writer.WriteLine("@reply-parent-msg-id=" + chatMessage.id + " PRIVMSG #" + Channel + " :https://www.amazon.com/Goodscious-Space-Bar-Keycaps-Keyboard/dp/B0BFXPRHSD/ref=sr_1_65?crid=1FY45AXHLYAGW&keywords=spacebar&qid=1683041955&sprefix=spacebar%2Caps%2C134&sr=8-65");
                                    break;
                            }
                            writer.Flush();
                        }*/
                        else if (chatMessage.text.StartsWith(Config.Settings.Prefix) && listen && !RuntimeOptions.Paused)
                        {//Message for bot, store
                         //Remove prefix characters
                            chatMessage.text = chatMessage.text.Remove(0, Config.Settings.Prefix.Length);
                            
                            Messages.Push(chatMessage);

                            /*TwitchVote vote;
                            if (TwitchMapper.Votes.TryGetValue(chatMessage.name, out vote))
                            {
                                vote.Update(chatMessage.text);
                            }
                            else
                            {
                                vote = new TwitchVote(chatMessage.text);
                                TwitchMapper.Votes.Add(chatMessage.name, vote);
                            }*/
                        }
                    }
                    else if (msg.Contains("PING"))
                    {//Pong
                        writer.WriteLine("PONG " + URL);
                        writer.Flush();
                    }
                }
                Thread.Sleep(25);
            }
        }

        private string[] stressCommands = { "!walk up left turnright shoot", "!swim down right throw turnleft", "!bomb jump up left tu walk", "!td swim down jump right" };
        private void StressTest(int msgCount)
        {
            for (int i = 0; i < msgCount; i++)
            {
                ChatMessage msg = new ChatMessage("twitch.tmi.tv:astro_dwarf:PRIVMSG " + DateTime.Now.Ticks + " : " + stressCommands[random.Next(0, stressCommands.Length)], true);
                Messages.Push(msg);
            }
        }
    }
}
