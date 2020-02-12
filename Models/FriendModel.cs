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
    public class FriendModel
    {
        public string Name { get; }
        public DateTime FriendshipStartTime { get; }
        public int TagCount { get; set; }
        public string FriendsForTime
        {
            get
            {
                TimeSpan friendsFor = DateTime.Now.Subtract(FriendshipStartTime);
                int years = (int)(friendsFor.Days / 365.2425f);
                int months = (int)(friendsFor.Days / 30.436875f) % 12;
                int days = (int)(friendsFor.Days % 30.436875f);

                if (years != 0)
                    return string.Format("{0} years {1} months", years, months);
                else if (months != 0)
                    return string.Format("{0} months {1} days", months, days);
                else
                    return string.Format("{0} days", days);
            }
        }


        public FriendModel(string name, DateTime friendshipStartTime)
        {
            Name = name;
            FriendshipStartTime = friendshipStartTime;
        }
    }
}
