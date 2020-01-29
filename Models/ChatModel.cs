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
    class ChatModel
    {
        public string Title { get; }
        public ReadOnlyCollection<MessageModel> Messages { get; }
        public ReadOnlyCollection<string> Participants { get; }

        public ChatModel(string title, List<MessageModel> messages, List<string> participants)
        {
            Title = title;
            Messages = new ReadOnlyCollection<MessageModel>(messages);
            Participants = new ReadOnlyCollection<string>(participants);
        }
    }
}
