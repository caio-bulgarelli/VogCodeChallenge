using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VogCodeChallenge.Console
{
    public static class QuestionClass
    {
        static List<string> NamesList = new List<string>()
        {
            "Jimmy",
            "Jeffrey",
            "John",
        };

        public static void RecursiveIteration(int count = 0)
        {
            if (count == NamesList.Count) return;

            System.Console.WriteLine(NamesList[count]);
            RecursiveIteration(count + 1);
        }

        public static void GoToIteration()
        {
            int count = 0;

            Start:
            if (count < NamesList.Count)
            {
                System.Console.WriteLine(NamesList[count]);
                count++;
                goto Start;
            }
        }
    }

}
