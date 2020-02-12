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
        public static string PersonName;
        public List<FriendModel> Friends;
        public List<ChatModel> Chats;
        public List<CommentModel> Comments;
        public YourProfileModel Profile;

        public int MessagesSent { get; private set; }
        public int MessagesReceived { get; private set; }
        public int TotalMessages { get; private set; }
        public List<ChatModel> ChatsByTime { get; private set; }
        

        public GlobalStatisticsModel(List<FriendModel> friends, List<ChatModel> chats, List<CommentModel> comments, YourProfileModel profile)
        {
            Friends = friends;
            Chats = chats;
            Comments = comments;
            Profile = profile;
            PersonName = Profile.FullName;
            CalculateStatistics();
        }

        /// <summary>
        /// Calculates all of the statistics shown on the main page
        /// </summary>
        private void CalculateStatistics()
        {
            CalculateMessageCounts();
            ChatsByTime = Chats.OrderBy(chat => chat.GetFirstMessageTime()).ToList();
            SetFriendTagCount();
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
        /// Sets how many times a friend was tagged in your comments
        /// </summary>
        private void SetFriendTagCount()
        {
            foreach(FriendModel friend in Friends)
            {
                friend.TagCount = Comments.Count(comment => comment.IncludesFriendTag(friend));
            }
        }
    }
}
