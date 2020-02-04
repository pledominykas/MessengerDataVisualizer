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
        public GlobalStatisticsModel Statistics { get; private set; }
        public List<ChatViewModel> Chats { get; private set; }

        public ShellViewModel()
        {
            ActivateItem(new DragArchiveDirectoryViewModel());
        }

        /// <summary>
        /// Reads the the facebook archive and displays statistics and chats
        /// </summary>
        /// <param name="path">Path of the facebook archive</param>
        private void ReadArhive(string path)
        {
            Statistics = DataInput.ReadData(path);
            Chats = new List<ChatViewModel>();
            foreach (ChatModel chat in Statistics.Chats)
                Chats.Add(new ChatViewModel(chat));

            NotifyOfPropertyChange(() => Chats);
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
                ReadArhive(path);
            }

            ActivateItem(new GlobalStatisticsViewModel());
        }
    }
}
