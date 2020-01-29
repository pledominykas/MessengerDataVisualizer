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
    class CommentModel
    {
        public string Content { get; }
        public DateTime TimeOfComment { get; }

        public CommentModel(string content, DateTime timeOfComment)
        {
            Content = content;
            TimeOfComment = timeOfComment;
        }
    }
}
