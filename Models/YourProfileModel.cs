using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerDataVisualizer.Models
{
    public class YourProfileModel
    {
        public string FullName { get; }
        public DateTime RegistrationDate { get; }
        public DateTime BirthDate { get; }
        public string ProfileURL { get; }

        public YourProfileModel(string fullName, DateTime registrationDate, DateTime birthDate, string profileURL)
        {
            FullName = fullName;
            RegistrationDate = registrationDate;
            BirthDate = birthDate;
            ProfileURL = profileURL;
        }
    }
}
