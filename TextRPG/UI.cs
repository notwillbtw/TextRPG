//using Colorful;
using System.Drawing;

namespace TextRPG
{
    internal class UI
    {
        internal static CharacterHandling characterHandling = new();
        

        internal static void MainMenu()
        {
            Plotline plotline = new Plotline();

            Console.WriteLine("MAIN MENU", Color.Green);
            Console.WriteLine("------------------");

            Console.WriteLine("1: start new game");
            Console.WriteLine("2: load old character");

            string input = Console.ReadLine();

            if (input == "1")
            {  
                characterHandling.CharacterCreator();
            }
            else if (input == "2")
            {   
                characterHandling.LoadExistingCharacter();
                plotline.storyMain();    
            }

        }

        internal static void TypeWriterConsoleWrite(string textToPrint)
        {
            foreach (var item in textToPrint)
            {
                if (item == ',')
                {
                    Console.Write(item);
                    Thread.Sleep(100);
                }

                else if (item == '.' || item == '?' || item == '!')
                {
                    Console.Write(item);
                    Thread.Sleep(150);
                }

                else
                {
                    Console.Write(item);
                    Thread.Sleep(50);
                }

            }

            Console.WriteLine("");
        }
    }
}
