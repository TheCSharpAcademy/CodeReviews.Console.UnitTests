
namespace CodingTracker.Paul_W_Saltzman;
internal class Program
{ 
    private static void Main(string[] args)
    {
        Data.Init();
        Data.LoadData();
        Menu.TopMenu();

    }
}