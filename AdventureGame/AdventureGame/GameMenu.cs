using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using AdventureGame.AdventureData;

namespace AdventureGame
{
    class GameMenu : Game
    {
        public static void CenterText(int y)
        {
            
        }
        //Centrerar texten i menyn med en string och en variabel för höjden y
        public static void CenterText(string text, int y)
        {
            int leftOffSet = (Console.WindowWidth / 2) - text.Length/2;

            int topOffSet = (Console.WindowHeight / 2) - y;

            Console.SetCursorPosition(leftOffSet, topOffSet);
            Console.WriteLine(text);
        }

        //Skriver ut alla kontroller
        public static void HelpCommands()
        {
            Console.Clear();
            CenterText("TRYCK ENTER FÖR ATT KOMMA TILLBAKA TILL MENYN", 4);
            CenterText("Fyll i alla helpcommands här", 3);
            var key = Console.ReadKey();
            while (key.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                DoMenu();
            }
            
        }
        //Startar huvudmenyn
        public static void DoMenu()
        {
            Console.CursorVisible = false;
            short curItem = 0, i;
            string[] menuItems = {"Starta Spelet", "Kontroller", "Higscore", "Credits",};

            SoundPlayer MenuMusic = new SoundPlayer();
            MenuMusic.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\BeepBox-Song-Menu.wav";
            MenuMusic.PlayLooping();
            do
            {
                
                CenterText("MENY", 6);
                for (i = 0; i < menuItems.Length; i++)
                {
                    int t = 4 - i;
                    if (curItem == i)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        CenterText(menuItems[i], t);
                        Console.ResetColor();
                    }
                    else
                    {
                        CenterText(menuItems[i], t);
                    }

                }
                var key = Console.ReadKey(true);

                if (key.Key.ToString() == "DownArrow")
                {
                    curItem++;
                    if (curItem > menuItems.Length - 1) curItem = 0;

                }
                else if (key.Key.ToString() == "UpArrow")
                {
                    curItem--;
                    if (curItem < 0) curItem = Convert.ToInt16(menuItems.Length - 1);
                    
                }
                
                if (key.Key == ConsoleKey.Enter && curItem == 0)
                {
                    TypeWriterIntroText();
                    MenuMusic.Stop();
                    break;
                }
                else if (key.Key == ConsoleKey.Enter && curItem == 1)
                {
                    HelpCommands();

                    break;
                }
                else if (key.Key == ConsoleKey.Enter && curItem == 3)
                {
                    Credits();
                }

            } while (curItem != 6);
            
            Console.CursorVisible = true;
        }
        public static void TypeWriterIntroText()
        {
            Console.Clear();
            int leftOffSet = (Console.WindowWidth / 2) - 40;
            int topOffSet = (Console.WindowHeight / 2) - 3;
            Console.SetCursorPosition(leftOffSet, topOffSet);

            int itemCount = 0;
            int diff = 0;
            string Text = "I en galax långt långt borta, i en tid när programerare blivit utbytta av AI:s. " +
                          "Krigen mellan front-end klasserna och .Net är för längesedan bortglömda " +
                          "I en skola på utkanten till Solna börjar våran historia. Våran hjälte " +
                          "vaknar upp i lobbyn till Nackademin, utan något minne av hur hen kom dit." +
                          "Du måste nu hjälpa hen att hitta ut. Gör dig beredd för nu börjar det...";

            System.Media.SoundPlayer typewriter = new System.Media.SoundPlayer
            {
                SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\typewriter-1.wav"
            };
            typewriter.Play();

            
            foreach (var item in Text)
            {
                itemCount++;
                Console.Write(item);
                Thread.Sleep(100);
                if (itemCount == 80)
                {
                    Console.WriteLine();
                    diff++;
                    itemCount = 0;
                    typewriter.Stop();
                    typewriter.Play();
                    Console.SetCursorPosition(leftOffSet, topOffSet+diff);
                    
                }
                
            }
            Console.Clear();
        }

        public static void Credits()
        {
            Console.WriteLine();
            Console.ReadLine();
            DoMenu();
        }

        public static void NameInput()
        {
            CenterText("Ange ditt namn", 4);
            string name = Console.ReadLine();
            Game.SetName(name);
            TypeWriterIntroText();
            
        }
    }
}
