using System;
using System.Threading;
using Discord;
using Discord.Gateway;

namespace MessageLogger
{
    internal class Program
    {
        private static void Main()
        {
            var client = new DiscordSocketClient();
            client.OnLoggedIn += Client_OnLoggedIn;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnMessageDeleted += Client_OnMessageDeleted;
            client.OnMessageEdited += Client_OnMessageEdited;
            client.OnRelationshipAdded += Client_OnRelationshipAdded;
            client.OnRelationshipRemoved += Client_OnRelationshipRemoved;

            Console.Write("Token: ");
            client.Login(Console.ReadLine());
            Thread.Sleep(-1);
        }

        private static void Client_OnRelationshipRemoved(DiscordSocketClient client, RemovedRelationshipEventArgs args)
        {
            Console.WriteLine($"FRIEND REMOVED | [{args.UserId}]");
        }

        private static void Client_OnRelationshipAdded(DiscordSocketClient client, RelationshipEventArgs args)
        {
            Console.WriteLine($"FRIEND ADDED | [{args.Relationship.User.Username} | {args.Relationship.User.Id}]");
        }

        private static void Client_OnMessageEdited(DiscordSocketClient client, MessageEventArgs args)
        {
            string from;
            DiscordChannel channel = client.GetChannel(args.Message.Channel.Id);
            if (channel.InGuild)
                from = $"#{channel.Name} / {client.GetCachedGuild(((TextChannel)channel).Guild.Id).Name}";
            else
            {
                var privChannel = (PrivateChannel)channel;
                from = privChannel.Name ?? privChannel.Recipients[0].ToString();
            }
            Console.WriteLine($"EDITED | [{from}] {args.Message.Author} > {args.Message.Content} | {args.Message.Id}");
        }

        private static void Client_OnMessageDeleted(DiscordSocketClient client, MessageDeletedEventArgs args)
        {
            string from;
            DiscordChannel channel = client.GetChannel(args.DeletedMessage.Channel.Id);
            if (channel.InGuild)
                from = $"#{channel.Name} / {client.GetCachedGuild(((TextChannel)channel).Guild.Id).Name}";
            else
            {
                var privChannel = (PrivateChannel)channel;
                from = privChannel.Name ?? privChannel.Recipients[0].ToString();
            }
            Console.WriteLine($"DELETED | [{from}] {args.DeletedMessage.Id}");
        }

        private static void Client_OnMessageReceived(DiscordSocketClient client, MessageEventArgs args)
        {
            string from;
            DiscordChannel channel = client.GetChannel(args.Message.Channel.Id);
            if (channel.InGuild)
                from = $"#{channel.Name} / {client.GetCachedGuild(((TextChannel)channel).Guild.Id).Name}";
            else
            {
                var privChannel = (PrivateChannel)channel;
                from = privChannel.Name ?? privChannel.Recipients[0].ToString();
            }
            Console.WriteLine($"RECEIVED | [{from}] {args.Message.Author} > {args.Message.Content} | {args.Message.Id}");
        }

        private static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            Console.WriteLine("Logged into token");
        }
    }
}