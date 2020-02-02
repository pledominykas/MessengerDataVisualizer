using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MessengerDataVisualizer.Models;

namespace MessengerDataVisualizer.ViewModels
{
    public class ShellViewModel : Screen
    {
        public GlobalStatisticsModel Statistics { get; private set; }
        public List<ChatViewModel> Chats { get; private set; }

        public ShellViewModel()
        {
            Statistics = DataInput.ReadData(@"C:\Users\domiz\OneDrive\Desktop\facebook-dominykasplesevicius");
            Chats = new List<ChatViewModel>();
            foreach (ChatModel chat in Statistics.Chats)
                Chats.Add(new ChatViewModel(chat));
        }
    }
}
