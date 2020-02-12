using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MessengerDataVisualizer.Models;
using Newtonsoft.Json.Linq;

namespace MessengerDataVisualizer
{
    static class DataInput
    {
        private const string COMMENT_FILE = @"\comments\comments.json";
        private const string INBOX_DIRECTORY = @"\messages\inbox";
        private const string FRIEND_FILE = @"\friends\friends.json";
        private const string PROFILE_FILE = @"\profile_information\profile_information.json";


        /// <summary>
        /// Reads all of the facebook archive data
        /// </summary>
        /// <param name="archiveDirectory">Path to the facebook archive directory</param>
        /// <returns>A GlobalStatisticsModel object</returns>
        public static GlobalStatisticsModel ReadData(string archiveDirectory)
        {
            List<CommentModel> comments = ReadComments(archiveDirectory);
            List<FriendModel> friends = ReadFriends(archiveDirectory);
            List<ChatModel> chats = ReadChats(archiveDirectory);
            YourProfileModel profile = ReadProfile(archiveDirectory);

            return new GlobalStatisticsModel(friends, chats, comments, profile);
        }

        /// <summary>
        /// Reads all chats from the inbox
        /// </summary>
        /// <param name="archiveDirectory">Path to the facebook archive directory</param>
        /// <returns>List of ChatModel objects</returns>
        private static List<ChatModel> ReadChats(string archiveDirectory)
        {
            List<ChatModel> chats = new List<ChatModel>();
            string inboxDirectory = archiveDirectory + INBOX_DIRECTORY;

            foreach(string chatDirectory in Directory.GetDirectories(inboxDirectory))
            {
                List<MessageModel> messages = new List<MessageModel>();
                List<string> participants = new List<string>();
                string title = "";

                foreach(string messageFile in Directory.GetFiles(chatDirectory))
                {
                    string json = File.ReadAllText(messageFile);
                    JObject parsedJson = JObject.Parse(json);
                    ReadMessages(parsedJson, ref messages);
                    ReadParticipants(parsedJson, ref participants);
                    title = parsedJson.Value<string>("title");
                }
                chats.Add(new ChatModel(title, messages, participants));
            }

            return chats;
        }

        /// <summary>
        /// Reads messages from a parsed message file
        /// </summary>
        /// <param name="parsedMessageFile">Json message file parsed to a JObject</param>
        /// <param name="messageList">List of messages where the new messages will be added</param>
        private static void ReadMessages(JObject parsedMessageFile, ref List<MessageModel> messageList)
        {
            foreach (JToken message in parsedMessageFile.Value<JToken>("messages").Children())
            {
                try
                {
                    string content = message.Value<string>("content");
                    string sender = message.Value<string>("sender_name");
                    DateTime timeSent = ParseUtils.GetDateTimeFromTimeStampMs(message.Value<long>("timestamp_ms"));
                    messageList.Add(new MessageModel(content, sender, timeSent));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Reads participants from a parsed message file
        /// </summary>
        /// <param name="parsedMessageFile">Json message file parsed to a JObject</param>
        /// <param name="participantList">List of participants where the new participants will be added</param>
        private static void ReadParticipants(JObject parsedMessageFile, ref List<string> participantList)
        {
            foreach (JToken participant in parsedMessageFile.Value<JToken>("participants").Children())
            {
                try
                {
                    string participantName = participant.Value<string>("name");
                    if (!participantList.Contains(participantName))
                    {
                        participantList.Add(participantName);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Reads comments from the comments.json file
        /// </summary>
        /// <param name="archiveDirectory">Path to the facebook archive directory</param>
        /// <returns>A list of CommentModel objects</returns>
        private static List<CommentModel> ReadComments(string archiveDirectory)
        {
            List<CommentModel> comments = new List<CommentModel>();
            string commentFile = archiveDirectory + COMMENT_FILE;
            string json = File.ReadAllText(commentFile);
            JObject parsedJson = JObject.Parse(json);

            foreach(JToken comment in parsedJson.First.First.Children())
            {
                try
                {
                    JToken commentData = comment.Value<JToken>("data").First.Value<JToken>("comment");
                    DateTime timeOfComment = ParseUtils.GetDateTimeFromTimeStamp(commentData.Value<long>("timestamp"));
                    string content = commentData.Value<string>("comment");
                    comments.Add(new CommentModel(content, timeOfComment));
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return comments;
        }

        /// <summary>
        /// Reads friends from the friends.json file
        /// </summary>
        /// <param name="archiveDirectory">Path to the facebook archive directory</param>
        /// <returns>A list of FriendModel objects</returns>
        private static List<FriendModel> ReadFriends(string archiveDirectory)
        {
            List<FriendModel> friends = new List<FriendModel>();
            string friendFile = archiveDirectory + FRIEND_FILE;
            string json = File.ReadAllText(friendFile);
            JObject parsedJson = JObject.Parse(json);

            foreach (JToken friend in parsedJson.First.First.Children())
            {
                try
                {
                    string friendName = friend.Value<string>("name");
                    DateTime startOfFriendship = ParseUtils.GetDateTimeFromTimeStamp(friend.Value<long>("timestamp"));
                    friends.Add(new FriendModel(friendName, startOfFriendship));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return friends;
        }

        /// <summary>
        /// Reads profile information from the profile_information.json file
        /// </summary>
        /// <param name="archiveDirectory">Path to the facebook archive directory</param>
        /// <returns>A YourProfileModel object</returns>
        private static YourProfileModel ReadProfile(string archiveDirectory)
        {
            string profileFile = archiveDirectory + PROFILE_FILE;
            string json = File.ReadAllText(profileFile);
            JObject parsedJson = JObject.Parse(json);

            string name = parsedJson.SelectToken("profile.name.full_name").ToString();
            DateTime registrationDate = ParseUtils.GetDateTimeFromTimeStamp(
                long.Parse(parsedJson.SelectToken("profile.registration_timestamp").ToString()));
            DateTime birthDate = new DateTime(
                int.Parse(parsedJson.SelectToken("profile.birthday.year").ToString()),
                int.Parse(parsedJson.SelectToken("profile.birthday.month").ToString()),
                int.Parse(parsedJson.SelectToken("profile.birthday.day").ToString()));
            string url = parsedJson.SelectToken("profile.profile_uri").ToString();

            return new YourProfileModel(name, registrationDate, birthDate, url);
        }
    }
}
