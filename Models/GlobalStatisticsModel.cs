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
    public class GlobalStatisticsModel
    {
        public readonly string PersonName = "Dominykas Plesevicius";
        public List<FriendModel> Friends;
        public List<ChatModel> Chats;
        public List<CommentModel> Comments;

        public int MessagesSent { get; private set; }
        public int MessagesReceived { get; private set; }
        public int TotalMessages { get; private set; }
        public List<ChatModel> ChatsByTime { get; private set; }
        public List<FriendModel> FriendsByTime { get; private set; }
        public List<CommentModel> CommentsByTime { get; private set; }
        public Dictionary<FriendModel, int> FriendTagCount { get; private set; }
        

        public GlobalStatisticsModel(List<FriendModel> friends, List<ChatModel> chats, List<CommentModel> comments)
        {
            Friends = friends;
            Chats = chats;
            Comments = comments;
            CalculateStatistics();
        }

        /// <summary>
        /// Calculates all of the statistics shown on the main page
        /// </summary>
        private void CalculateStatistics()
        {
            CalculateMessageCounts();
            ChatsByTime = Chats.OrderBy(chat => chat.GetFirstMessageTime()).ToList();
            FriendsByTime = Friends.OrderBy(friend => friend.FriendshipStartTime).ToList();
            CommentsByTime = Comments.OrderBy(comment => comment.TimeOfComment).ToList();
            FindFriendTagCount();
        }

        /// <summary>
        /// Calculates sent, received and total messages
        /// </summary>
        private void CalculateMessageCounts()
        {
            MessagesSent = 0;
            MessagesReceived = 0;
            foreach(ChatModel chat in Chats)
            {
                MessagesSent += chat.GetMessageCountBySender(PersonName);
                TotalMessages += chat.Messages.Count;
            }
            MessagesReceived = TotalMessages - MessagesSent;
        }

        /// <summary>
        /// Finds how many times a friend was tagged in your comments
        /// </summary>
        private void FindFriendTagCount()
        {
            FriendTagCount = new Dictionary<FriendModel, int>();
            foreach(FriendModel friend in Friends)
            {
                FriendTagCount.Add(friend, Comments.Count(comment => comment.IncludesFriendTag(friend)));
            }
        }
    }
}
