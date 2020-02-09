using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerDataVisualizer.Models;

namespace MessengerDataVisualizer.ViewModels
{
    public class MessageViewModel
    {
        public MessageModel Message { get; }

        public MessageViewModel(MessageModel message)
        {
            Message = message;
        }

        public string Content
        {
            get
            {
                return Message.Content;
            }
        }

        public string TimeSent
        {
            get
            {
                return Message.TimeSent.ToString("yyyy-MM-dd H:mm");
            }
        }

        public string Color
        {
            get
            {
                if (Message.Sender == GlobalStatisticsModel.PersonName)
                    return "#00C6FF";
                else
                    return "#A9A9A9";
            }
        }

        public string Alignment
        {
            get
            {
                if (Message.Sender == GlobalStatisticsModel.PersonName)
                    return "Right";
                else
                    return "Left";
            }
        }

    }
}
