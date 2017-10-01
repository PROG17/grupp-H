using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using AdventureGame.AdventureData;

namespace AdventureGame.AdventureData
{
    partial class Game
    {
        public class GameMenu
        {
            //Centrerar texten i menyn med en string och en variabel för höjden y
            public static void CenterText(string text, int y)
            {
                int leftOffSet = (Console.WindowWidth / 2) - text.Length/2;

                int topOffSet = (Console.WindowHeight / 2) - y+4;

                Console.SetCursorPosition(leftOffSet, topOffSet);
                Console.WriteLine(text);
            }

            public static void WriteOutStringToCenter(string text, int yStartPoint)
            {
                var split = text.Split('\n');
                int offSetCounter = yStartPoint;
                for (int i = 0; i < split.Length; i++)
                {
                    CenterText(split[i], offSetCounter);
                    offSetCounter--;
                }
            }

            //Skriver ut alla kontroller
            public static void HelpCommands()
            {
                Console.Clear();
                CenterText("TRYCK ENTER FÖR ATT GÅ TILLBAKA", 7);
                CenterText("[GÅ]+[NORR|SYD|ÖST|VÄST]", 5);
                CenterText("Gå i riktning.", 4);
                CenterText("Exempel: \"gå norr\"", 3);

                CenterText("[TA] + [<FÖREMÅL>]", 1);
                CenterText("Tar upp ett föremål och lägger i fickan", 0);
                CenterText("Exempel: \"ta bilnyckel\"", -1);

                CenterText("[SLÄPP] + [<FÖREMÅL>]", -3);
                CenterText("Släpper föremålet i rummet", -4);
                CenterText("Exempel: \"släpp hammare\"", -5);

                CenterText("[TITTA] | [TITTA] + [PÅ|I] [<FÖREMÅL>]", -7);
                CenterText("Tittar på uppplockat föremål, föremål i annat föremål eller på föremål i rummet", -8);
                CenterText("Exempel: \"titta\" eller \"titta i ficka\" eller \"titta på bilnyckel\"", -9);

                CenterText("[INSPEKTERA] + [<FÖREMÅL>] | [INSPEKTERA] + [<FÖREMÅL>] + [I] + [<FÖREMÅL>]", -11);
                CenterText("Ger en detaljerad beskrivning av ett föremål", -12);
                CenterText("Exempel: \"inspektera hammare\" eller \"inspektera nyckel i ficka\"", -13);

                CenterText("[ANVÄND] + [<FÖREMÅL>] + [PÅ] + [<FÖREMÅL> | [<UTGÅNG>]", -15);
                CenterText("Använder föremål på annat föremål eller utgång", -16);
                CenterText("Exempel: \"använd bilnyckel på bil\" eller \"använd nyckel på trädörr\"", -17);

                Console.ReadLine();
            }
            //metod för upspelning av musik/ljud. Parametern anger vilken ljudfil som skall köras.
            public static void MusicPlayer(int i, bool status)
            {
                if (i ==0)
                {
                    SoundPlayer music = new SoundPlayer
                    {
                        SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\BeepBox-Song.wav"
                    };
                    if (status)
                    {
                        music.Play();
                    }
                    else if (!status)
                    {
                        music.Stop();
                    }
                    
                }
               
                else if (i == 2)
                {
                    SoundPlayer boom = new SoundPlayer();
                    boom.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Explosion+11.wav";
                    if (status)
                    {
                        boom.Play();
                    }
                    else if (!status)
                    {
                        boom.Stop();
                    }
                    
                }
                else if (i == 3)
                {
                    SoundPlayer MenuMusic = new SoundPlayer();
                    MenuMusic.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\BeepBox-Song-Menu.wav";

                    if (status)
                    {
                        MenuMusic.PlayLooping();
                    }
                    else if (!status)
                    {
                        MenuMusic.Stop();
                    }
                    
                }
                

                // Hjälp metod för MenuBackground som anger vart i fönstret den skall skrivas ut.
            }
            public static void MenuBackgroundPos(int i)
            {
                
                Console.SetCursorPosition((Console.WindowWidth / 2) - 25, i);
                
            }
            // En askii art bakgrund som ändras när man startar spelet.
            public static void MenuBackground(int inp)
            {
                
                string[] array =
                {
                    " _______________     _______________", " | |_| |_| |_| |     | |_| |_| |_| |" 
                    ," | |_| |_| |_| |     | ___ ___ ___ |", " | |_| |_| |_| |     | |_| |_| |_| |",
                    " | |_| |_| |_| |     | ___ ___ ___ | "," | |_| |_| |_| |     | |_| |_| |_| |",
                    " | |_| |_| |_| |     | ___ ___ ___ |","_| |_| |_| |_| |_    | |_| |_| |_| |_________________  ",
                    "| ___  ___  ___ |    | ___ ___ ___ | ___  ___  ___  |","| |_|  |_|  |_| |    | |_| | | |_| | |_|  |_|  |_|  |",
                    "|_______________|    |_____|_|_____|________________|"
                };
                string[] array2 =
                {
                    "         _______     ________            ", "        _| |_| |     | |_| |_       ",
                    "      _|_| |_| |     | ___ _/       ","    _| |_| |_| |     | |_| |_       ",
                    "  _|_| |_| |_| |     | ___ __\\_   _  "," | |_| |_| |_| |     | |_| |_| |_| |",
                    " | |_| |_| |_| |     | ___ ___ ___ |","_| |_| |_| |_| |_    | |_| |_| |_| |       __________",
                    "| ___  ___  ___ |    | ___ ___ ___  \\    _/__  ___  |","| |_|  | |  |_| |    | |_| | | |_|   \\_ / |_|  |_|  |",
                    "|______|_|______|    |_____|_|_____|________________|"

                };
                if (inp == 0)
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        MenuBackgroundPos(i);
                        Console.Write(array[i]);
                    }
                }
                else
                {
                    MusicPlayer(2, true);
                    for (int i = 0; i < array2.Length; i++)
                    {
                        MenuBackgroundPos(i);
                        Console.Write(array2[i]);
                    }
                    
                    Thread.Sleep(25);
                }
                
                
                
            }
            //Startar huvudmenyn
            public static void DoMenu()
            {
                Console.CursorVisible = false;
                short curItem = 0, i;
                string[] menuItems = {"Starta Spelet", "Kontroller", "Higscore", "Credits"};
                MusicPlayer(3, true);

                do
                {
                    
                    MenuBackground(0);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    CenterText("NACKOPOLYPS NOW", 8);
                    Console.ForegroundColor = ConsoleColor.White;
                    CenterText("***************", 7);
                    CenterText("MENY", 5);
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
                        MusicPlayer(3, false);
                        MenuBackground(1);
                        Thread.Sleep(1000);
                        TypeWriterIntroText();
                        
                        break;
                    }
                    else if (key.Key == ConsoleKey.Enter && curItem == 1)
                    {
                        HelpCommands();
                        Console.Clear();
                    }
                    else if (key.Key == ConsoleKey.Enter && curItem == 3)
                    {
                        Credits();
                    }

                } while (curItem != 6);
            
                Console.CursorVisible = true;
                Console.Clear();
            }
            //Metod som skriver ut introt som en skrivmaskin. Kallar på ljudfilen med.
            public static void TypeWriterIntroText()
            {
                Console.Clear();
                MenuBackground(1);
                int leftOffSet = (Console.WindowWidth / 2) - 40;
                int topOffSet = (Console.WindowHeight / 2) - 3;
                Console.SetCursorPosition(leftOffSet, topOffSet);

                int itemCount = 0;
                int diff = 0;
                string Text = "I en galax långt långt borta, i en tid när programerare blivit utbytta av AI:s." +
                              " Krigen mellan front-end klasserna och .Net är för längesedan bortglömda... " +
                              "I en skola på utkanten till Solna börjar våran historia. " +
                              "Runtomkring ligger staden i ruiner och på avstånd hörs vrålen från dom skadade. "+
                              "Våran hjälte vaknar upp i   lobbyn till Nackademin utan några minnen. " +
                              "Du måste hitta en väg ut, fort! Gör   dig beredd för nu börjar det...";

                SoundPlayer typewriter = new SoundPlayer
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
                typewriter.Stop();
                Thread.Sleep(5000);
                Console.Clear();
                IntroText(0);
                Thread.Sleep(3000);
                IntroText(1);
                Thread.Sleep(3000);
                IntroText(2);
            }
            // Skriver ut credits.
            public static void Credits()
            {
                Console.WriteLine();
                Console.ReadLine();
                DoMenu();
            }
            //Metod för att mata in namn.
            public static void NameInput()
            {
                CenterText("Ange ditt namn", 4);
                string name = Console.ReadLine();
                //Game.SetName(name);
                TypeWriterIntroText();
            
            }

            public static void IntroText(int state)
            {
                if (state == 0)
                {
                    Console.Clear();
                    CenterText("Från skaparna av HANG THE MAN och SUPA SUDOKU kommer:", 7);
                    Thread.Sleep(1200);
                }
                else if (state == 1)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    CenterText("NACKOPOLYPS NOW", 7);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (state == 2)
                {
                    Console.Clear();
                    CenterText("Om ni som jag gillar läskiga äventyrspel, är NACKOPOLYPS NOW något för er.", 12);
                    CenterText("Det får 5 solar av 5!", 11);
                    CenterText("-David Dangerås spelrecensent Gamereaktor ", 10);
                    Thread.Sleep(1000);
                    CenterText("Såååå lääääskigt! 5 havrebollar av 5!", 8);
                    CenterText("-Julia Neriksson Aftonbladen", 7);
                    Thread.Sleep(1000);
                    CenterText("Utan några tvivel det bästa spelet den här hösten.", 5);
                    CenterText("Missa inte! 6 kaniner av 6", 4);
                    CenterText("-Jonas Hedviksson Expressens", 3);
                    Thread.Sleep(1000);
                    Thread.Sleep(3000);
                    Console.Clear();
                }
                
                
                
            }
        }
    }
}
