using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MessengerDataVisualizer.Models
{
    /// <summary>
    /// Class for containing data of a specific group or private chat
    /// </summary>
    public class ChatModel
    {
        public string Title { get; }
        public List<MessageModel> Messages { get; }
        public List<string> Participants { get; }

        public ChatModel(string title, List<MessageModel> messages, List<string> participants)
        {
            Title = title;
            Messages = messages;
            Participants = participants;
        }

        public int GetMessageCountBySender(string sender)
        {
            return Messages.Count(message => message.Sender == sender);
        }

        public DateTime GetFirstMessageTime()
        {
            if(Messages.Count >= 2)
            {
                return Messages[Messages.Count - 2].TimeSent; //-2 since the first message sent is ussually the automatic wave to a new friend message
            }
            return DateTime.MaxValue;
        }


        /// <summary>
        /// Creates a new chat object that only contains messages from a specified time until now
        /// </summary>
        /// <param name="time">Time from which to filter the messages</param>
        /// <returns>A new ChatModel that only contains messages from a specified time until now</returns>
        public ChatModel GetMessagesFrom(DateTime time)
        {
            List<MessageModel> messages = Messages.Where(message => message.TimeSent.CompareTo(time) >= 0).ToList();
            return new ChatModel(Title, messages, Participants);
        }
    }
}
