namespace TextRPG
{
    internal class UI
    {
        internal static void MainMenu()
        {

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
