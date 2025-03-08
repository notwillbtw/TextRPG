namespace TextRPG
{
    internal class Plotline
    {


        internal void peasantPlotline()
        {
            encounter1();
        }

        internal void encounter1()
        {
            Console.Clear();
            UI.TypeWriterConsoleWrite("> you are in A small tavern in your village. someone seems to have taken a disliking to you \n");
            UI.TypeWriterConsoleWrite("You what?! ill kill ya you little-");
            UI.TypeWriterConsoleWrite("> the man stands up, clearly drunk and angry. What do you do?");

            UI.TypeWriterConsoleWrite(">1: defuse situation - charisma check");
            UI.TypeWriterConsoleWrite(">2: taunt him - enter combat");

            string userChoice = Console.ReadLine();


            if (userChoice == "1")
            {
                int statCheckResult = StatHandling.statCheck(StatHandling.StatTypes.charisma);

                if (statCheckResult > 1)
                {
                    UI.TypeWriterConsoleWrite("dweevf");
                }
            }
            else if (userChoice == "2")
            {
                UI.TypeWriterConsoleWrite($"({CharacterHandling.Player.PlayerName}) > \"Come on big man, lets see if you're worth it!\"");
            }
        }
    }
}
