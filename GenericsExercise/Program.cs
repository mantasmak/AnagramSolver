using System;

namespace GenericsExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(EnumMapper.MapValueToEnum<Weekday, string>("40"));
        }
    }
}
