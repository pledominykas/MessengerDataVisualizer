using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerDataVisualizer.Models
{
    /// <summary>
    /// Class for containing comment data
    /// </summary>
    public class CommentModel
    {
        public string Content { get; }
        public DateTime TimeOfComment { get; }

        public CommentModel(string content, DateTime timeOfComment)
        {
            Content = content;
            TimeOfComment = timeOfComment;
        }

        /// <summary>
        /// Checks wether the specified friend is tagged in this comment
        /// </summary>
        /// <param name="friend">Friend whos tag to look for</param>
        /// <returns>True if friend is tagged false otherwise</returns>
        public bool IncludesFriendTag(FriendModel friend)
        {
            return Content.Contains(friend.Name);
        }
    }
}
