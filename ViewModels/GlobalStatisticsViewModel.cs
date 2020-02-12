using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerDataVisualizer.Models;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Helpers;

namespace MessengerDataVisualizer.ViewModels
{
    class GlobalStatisticsViewModel : Screen
    {
        private const int ChatsByMessageCount_DISPLAY_COUNT = 15;
        private const int FirstChats_DISPLAY_COUNT = 4;
        private const int FRIEND_TABLE_DISPLAY_COUNT = 20;

        public GlobalStatisticsModel Statistics { get; }

        public GlobalStatisticsViewModel(string archivePath)
        {
            Statistics = DataInput.ReadData(archivePath);                
        }

        #region Row 0 (message count, your information)
        public int MessagesSent
        {
            get
            {
                return Statistics.MessagesSent;
            }
        }

        public int MessagesReceived
        {
            get
            {
                return Statistics.MessagesReceived;
            }
        }

        public int TotalMessages
        {
            get
            {
                return Statistics.TotalMessages;
            }
        }

        public string FullName
        {
            get
            {
                return Statistics.Profile.FullName;
            }
        }

        public string RegistrationDate
        {
            get
            {
                return Statistics.Profile.RegistrationDate.ToString("yyyy-MM-dd");
            }
        }

        public string ProfileURL
        {
            get
            {
                return Statistics.Profile.ProfileURL;
            }
        }
        #endregion

        #region Row 1 (chats by message count)
        public SeriesCollection ChatsByMessageCount
        {
            get
            {
                IEnumerable<ChatModel> filteredChats = ChatsByMessageCountFilter();

                return new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Message count",
                        Values = filteredChats.Select(chat => chat.Messages.Count).Take(ChatsByMessageCount_DISPLAY_COUNT).AsChartValues()
                    }
                };
            }
        }

        public string[] ChatsByMessageCountLabels
        {
            get
            {
                IEnumerable<ChatModel> filteredChats = ChatsByMessageCountFilter();

                return filteredChats.Select(chat => chat.Title).Take(ChatsByMessageCount_DISPLAY_COUNT).ToArray();
            }
        }

        private bool _includeGroupChats = true;
        public bool IncludeGroupChats
        {
            get
            {
                return _includeGroupChats;
            }
            set
            {
                _includeGroupChats = value;
                NotifyOfPropertyChange(() => IncludeGroupChats);
                NotifyOfPropertyChange(() => ChatsByMessageCount);
                NotifyOfPropertyChange(() => ChatsByMessageCountLabels);
            }
        }

        public Dictionary<string, DateTime> ChatsByMessageCountTimeFrame
        {
            get
            {
                return new Dictionary<string, DateTime>
                {
                    { "Last week", DateTime.Today.AddDays(-7) },
                    { "Last 30 days", DateTime.Today.AddDays(-30) },
                    { "Last 6 months", DateTime.Today.AddMonths(-6) },
                    { "Last year", DateTime.Today.AddYears(-1) },
                    { "All time", DateTime.MinValue }
                };
            }
        }

        private KeyValuePair<string, DateTime> _chatsByMessageCountSelectedTimeFrame 
            = new KeyValuePair<string, DateTime>("All time", DateTime.MinValue);
        public KeyValuePair<string, DateTime> ChatsByMessageCountSelectedTimeFrame
        {
            get
            {
                return _chatsByMessageCountSelectedTimeFrame;
            }
            set
            {
                _chatsByMessageCountSelectedTimeFrame = value;
                NotifyOfPropertyChange(() => ChatsByMessageCountSelectedTimeFrame);
                NotifyOfPropertyChange(() => ChatsByMessageCount);
                NotifyOfPropertyChange(() => ChatsByMessageCountLabels);
            }
        }

        private IEnumerable<ChatModel> ChatsByMessageCountFilter()
        {
            IEnumerable<ChatModel> filteredChats = Statistics.Chats;
            if (_includeGroupChats == false)
                filteredChats = filteredChats.Where(chat => chat.Participants.Count == 2);

            filteredChats = filteredChats.Select(chat => chat.GetMessagesFrom(_chatsByMessageCountSelectedTimeFrame.Value)).OrderByDescending(chat => chat.Messages.Count);

            return filteredChats.Take(ChatsByMessageCount_DISPLAY_COUNT);
        }
        #endregion

        #region Row 2 (first facebook chats)

        private List<ChatViewModel> _firstChats = new List<ChatViewModel>();
        public List<ChatViewModel> FirstChats
        {
            get
            {
                if(_firstChats.Count == 0) { LoadFirstChats(); }
                return _firstChats;
            }
        }

        private void LoadFirstChats()
        {
            for (int i = 0; i < FirstChats_DISPLAY_COUNT; i++)
            {
                ChatViewModel chat = new ChatViewModel(Statistics.ChatsByTime[i]);
                chat.LoadMessages();
                _firstChats.Add(chat);
            }
        }

        public void LoadMoreMessages()
        {
            foreach (ChatViewModel chat in FirstChats)
                chat.LoadMessages();

            NotifyOfPropertyChange(() => FirstChats);
        }
        #endregion

        #region Row 3 (friend tag count, longest friends)

        public List<FriendModel> FriendsByTagCount
        {
            get
            {
                return Statistics.Friends.OrderByDescending(friend => friend.TagCount).Take(FRIEND_TABLE_DISPLAY_COUNT).Where(friend => friend.TagCount != 0).ToList();
            }
        }

        public List<FriendModel> FriendsByTime
        {
            get
            {
                return Statistics.Friends.OrderBy(friend => friend.FriendshipStartTime).Take(FRIEND_TABLE_DISPLAY_COUNT).ToList();
            }
        }

        #endregion

    }
}
