using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.Discord
{
    public class Discord : IDiscord
    {

        private Dictionary<string, Message> IdMessage = new Dictionary<string, Message>();

        private Dictionary<string, List<string>> ChannelIds = new Dictionary<string, List<string>>();
        private Dictionary<string, string> IdChannel = new Dictionary<string, string> ();


        public int Count => IdMessage.Count;

        public bool Contains(Message message)
        {
            return ContainsId(message.Id);
        }

        private bool ContainsId(string id)
        {
            return IdMessage.ContainsKey(id);
        }

        private void CheckMsgExists (string id)
        {
            if (!ContainsId(id))
            {
                throw new ArgumentException();
            }
        }

        private void CheckChannelExists(string channelName)
        {
            if (!ChannelIds.ContainsKey(channelName))
            {
                throw new ArgumentException();
            }
        }

        // deleter method
        public void DeleteMessage(string messageId)
        {
            CheckMsgExists(messageId);

            IdMessage.Remove(messageId);
            ChannelIds[IdChannel[messageId]].Remove(messageId);
            IdChannel.Remove(messageId);
        }

        public IEnumerable<Message> GetAllMessagesOrderedByCountOfReactionsThenByTimestampThenByLengthOfContent()
        {
            var messagesToReturn = IdMessage.Values
                 .OrderByDescending(x => x.Reactions.Count)
                 .ThenBy(x => x.Timestamp)
                 .ThenBy(x => x.Content.Length)
                 .ToList();

            return messagesToReturn;
        }

        public IEnumerable<Message> GetChannelMessages(string channel)
        {
            CheckChannelExists(channel);

            if (ChannelIds[channel].Count == 0)
            {
                throw new ArgumentException();
            }

            foreach (var msg in ChannelIds[channel])
            {
                yield return IdMessage[msg];
            }
        }

        public Message GetMessage(string messageId)
        {
            CheckMsgExists(messageId);

            return IdMessage[messageId];
        }

        public IEnumerable<Message> GetMessageInTimeRange(int lowerBound, int upperBound)
        {
           var messagesToReturn = IdMessage.Values
                .Where(x => lowerBound <= x.Timestamp && x.Timestamp <= upperBound)
                .OrderByDescending(x => ChannelIds[IdChannel[x.Id]].Count)
                .ToList();

            if (messagesToReturn.Count == 0)
            {
                return new List<Message>();
            }

            return messagesToReturn;
        }

        public IEnumerable<Message> GetMessagesByReactions(List<string> reactions)
        {
            var result = IdMessage.Values
                .Where(x => !reactions.Except(x.Reactions).Any() == true)
                .OrderByDescending(x => x.Reactions.Count)
                .ThenBy(x => x.Timestamp)
                .ToList();

            if (result.Count == 0)
            {
                return new List<Message>();
            }

            return result;
        }

        public IEnumerable<Message> GetTop3MostReactedMessages()
        {
            var result = IdMessage.Values
                .OrderByDescending(x => x.Reactions.Count)
                .Take(3)
                .ToList();

            return result;
        }

        //Modifier method
        public void ReactToMessage(string messageId, string reaction)
        {
            CheckMsgExists(messageId);

            IdMessage[messageId].Reactions.Add(reaction);

        }
        //create method
        public void SendMessage(Message message)
        {
            if (ContainsId(message.Id))
            {
                throw new ArgumentException();
            }

            IdMessage.Add(message.Id, message);
        }

        public void SendMessage(Message message, string channel)
        {
            if (ContainsId(message.Id))
            {
                throw new ArgumentException();
            }

            IdMessage.Add(message.Id, message);
            
            if (!ChannelIds.ContainsKey(channel))
            {
                 ChannelIds.Add(channel, new List<string> { message.Id });
            }
            else
            {
                ChannelIds[channel].Add(message.Id);
            }

            IdChannel.Add(message.Id, channel);
        }
    }
}
