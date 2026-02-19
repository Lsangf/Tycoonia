using Tycoonia.Application.ApplicationExceptions;

namespace Tycoonia.Presentation.UI
{
    public class ConsoleInput
    {
        public static int ConsoleChoice()
        {
            int choice;
            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine()!;
                if (int.TryParse(input, out choice))
                {
                    break;
                }
                else
                {
                    throw new InputException();
                }
            }
            return choice;
        }
    }
}
