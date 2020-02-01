using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerDataVisualizer.Models
{
    /// <summary>
    /// Class for containing data of your current friends
    /// </summary>
    class FriendModel
    {
        public string Name { get; }
        public DateTime FriendshipStartTime { get; }

        public FriendModel(string name, DateTime friendshipStartTime)
        {
            Name = name;
            FriendshipStartTime = friendshipStartTime;
        }
    }
}
