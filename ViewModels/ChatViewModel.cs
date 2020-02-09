using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MessengerDataVisualizer.Models;

namespace MessengerDataVisualizer.ViewModels
{
    public class ChatViewModel : Screen
    {
        public const int MESSAGE_LOAD_INCREMENT = 100;
        public ChatModel Chat { get; }

        public ChatViewModel(ChatModel chat)
        {
            Chat = chat;
        }

        public void LoadMessages()
        {
            int from = Chat.Messages.Count - _messages.Count - 1;
            int to = Chat.Messages.Count - _messages.Count - MESSAGE_LOAD_INCREMENT;

            for (int i = from; i > to && i > 0; i--)
                _messages.Add(new MessageViewModel(Chat.Messages[i]));

            NotifyOfPropertyChange(() => Messages);
        }

        BindableCollection<MessageViewModel> _messages = new BindableCollection<MessageViewModel>();
        public BindableCollection<MessageViewModel> Messages
        {
            get
            {
                return _messages;
            }
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
