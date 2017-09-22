using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    class Prompt
    {
        private readonly string[] Get = new string[]
        {
            "TA",
            "PLOCKA UPP"
        };
        private readonly string[] Use = new string[]
        {
            "ANVÄND"
        };
        private readonly string[] Drop = new string[]
        {
            "KASTA"
        };
        private readonly string[] Look = new string[]
        {
            "TITTA",
            "TITTA PÅ",
            "UNDERSÖK",
        };
        //private readonly string[] Turn = new string[]
        //{
        //    "VÄND",
        //    "VRID"
        //};
        private readonly string[] Go = new string[]
        {
            "GÅ"
        };

        private Player player;

        private bool IsPlaying;

        private bool IsValidWord(string word)
        {
            List<string> list = new List<string>();
            list.AddRange(Get);
            list.AddRange(Use);
            list.AddRange(Drop);
            list.AddRange(Look);
            //list.AddRange(Turn);
            list.AddRange(Go);
            return list.Contains(word);
        }

        public void StartPrompt(string roomDescription, Dictionary<string, Object> roomObjects)
        {
            IsPlaying  = true;
            while (IsPlaying)
            {
                Console.WriteLine(roomDescription);
                Console.Write("Skriv vad du gör: ");
                string action;
                do
                {
                    action = Console.ReadLine().ToUpper();
                } while (!IsValidWord(action.Split(' ')[0]));
            }
        }
    }
}
