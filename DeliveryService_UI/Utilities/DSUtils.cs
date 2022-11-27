using static System.Console;

public static class DSUtils
{
    public static bool isRunning = true;

    public static void PressAnyKey()
    {
        Clear();
        WriteLine("Press any Key To Continue.");
        ReadKey();
    }

}