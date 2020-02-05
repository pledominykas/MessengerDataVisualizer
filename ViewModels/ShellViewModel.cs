using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MessengerDataVisualizer.Models;

namespace MessengerDataVisualizer.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        public List<ChatViewModel> Chats { get; private set; }

        public ShellViewModel()
        {
            ActivateItem(new DragArchiveDirectoryViewModel());
        }

        /// <summary>
        /// Called when the SelectFolder button is clicked
        /// </summary>
        public void SelectFolderBtn()
        {
            
        }

        /// <summary>
        /// Called when a file is dropped on the main display area
        /// </summary>
        /// <param name="e">Drag event arguments</param>
        public void FileDropped(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                GlobalStatisticsViewModel globalStats = new GlobalStatisticsViewModel(path);
                Chats = new List<ChatViewModel>();
                foreach (ChatModel chat in globalStats.Statistics.Chats)
                    Chats.Add(new ChatViewModel(chat));

                NotifyOfPropertyChange(() => Chats);
                ActivateItem(globalStats);
            }
        }
    }
}
