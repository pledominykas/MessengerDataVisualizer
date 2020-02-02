using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerDataVisualizer.Models;

namespace MessengerDataVisualizer.ViewModels
{
    public class ChatViewModel
    {
        public ChatModel Chat { get; }

        public ChatViewModel(ChatModel chat)
        {
            Chat = chat;
        }

        public string Title
        {
            get
            {
                return Chat.Title;
            }
        }

        public string ChatType
        {
            get
            {
                if (Chat.Participants.Count == 2)
                    return "Private";
                else
                    return "Group";
            }
        }

        public string ChatIcon
        {
            get
            {
                if (Chat.Participants.Count == 2)
                    return "/Assets/PersonIcon.png";
                else
                    return "/Assets/GroupIcon.png";
            }
        }
    }
}
