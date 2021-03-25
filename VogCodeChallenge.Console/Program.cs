using System;

namespace VogCodeChallenge.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Recursive Iteration");
            QuestionClass.RecursiveIteration();

            System.Console.WriteLine();
            System.Console.WriteLine("GoTo Iteration");
            QuestionClass.GoToIteration();

            System.Console.WriteLine();
            System.Console.WriteLine(TESTModule(2));
            System.Console.WriteLine(TESTModule(6));

            try
            {
                TESTModule(-1);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            System.Console.WriteLine(TESTModule(1.0f));
            System.Console.WriteLine(TESTModule("Vog Code Challenge"));
            System.Console.WriteLine(TESTModule(4.0));
            System.Console.ReadLine();
        }

        private static object TESTModule(object value) =>
        value switch
        {
            int i when (i > 0 && i < 5) => i * 2,
            int i when i > 4 => i * 3,
            int i when i < 1 => throw new ArgumentOutOfRangeException(nameof(value), value, "Invalid argument."),
            float f when (f == 1.0f || f == 2.0f) => 3.0f,
            string s => s.ToUpper(),
            object o => o
        };
    }
}
