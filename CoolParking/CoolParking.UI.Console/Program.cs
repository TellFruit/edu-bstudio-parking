using CoolParking.BL.Models;
using CoolParking.UI.Console;

namespace CoolParking.Ui.Console
{
    class Program
    {
        public static void Main()
        {
            new ConsoleControl(new ApiAccessConsole(new HttpClient(), Routes.BaseApiAddress)).LaunchProgram();
        }
    }
}

