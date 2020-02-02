using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerDataVisualizer.Models
{
    /// <summary>
    /// Class for containing specific message data
    /// </summary>
    public class MessageModel
    {
        public string Content { get; }
        public string Sender { get; }
        public DateTime TimeSent { get; }

        public MessageModel(string content, string sender, DateTime timeSent)
        {
            Content = content;
            Sender = sender;
            TimeSent = timeSent;
        }
    }
}
