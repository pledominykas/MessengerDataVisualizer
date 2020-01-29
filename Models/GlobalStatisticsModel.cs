using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MessengerDataVisualizer.Models
{
    /// <summary>
    /// Class for containing and calculating all of the statistics shown on the main (front) page
    /// </summary>
    class GlobalStatisticsModel
    {
        public ReadOnlyCollection<FriendModel> Friends;
        public ReadOnlyCollection<ChatModel> Chats;
        public ReadOnlyCollection<CommentModel> Comments;

        public GlobalStatisticsModel(List<FriendModel> friends, List<ChatModel> chats, List<CommentModel> comments)
        {
            Friends = new ReadOnlyCollection<FriendModel>(friends);
            Chats = new ReadOnlyCollection<ChatModel>(chats);
            Comments = new ReadOnlyCollection<CommentModel>(comments);
            CalculateStatistics();
        }

        /// <summary>
        /// Calculates all of the statistics shown on the main page in one iteration
        /// </summary>
        private void CalculateStatistics()
        {

        }
    }
}
