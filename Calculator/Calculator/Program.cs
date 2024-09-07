using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("How Many Subject You Wish To Record : ");
        int totalSubjects;

        while (!int.TryParse(Console.ReadLine(), out totalSubjects) || totalSubjects <= 0)
        {
            Console.Write(" Marks should be positive: ");
        }

        double sum = 0;

        
        for (int i = 1; i <= totalSubjects; i++)
        {
            Console.Write($" subject {i} over  100: ");
            double mark = GetValidMark();
            sum += mark;
        }

       
        double average = sum / totalSubjects;

        Console.WriteLine("\n Your Total Result");
        Console.WriteLine($" ToT Marks: {sum}");
        Console.WriteLine($" Avg: {average:F2}");

        if (average >= 50)
            Console.WriteLine(" Status: You passed");
        else
            Console.WriteLine(" Status: You Failed");
    }

    
    static double GetValidMark()
    {
        double mark;
        string input = Console.ReadLine();

        while (!double.TryParse(input, out mark) || mark < 0 || mark > 100)
        {
            Console.Write("Enter Number Between 0 or 100 : ");
            input = Console.ReadLine();
        }

        return mark;
    }
}
// Enhanced on 2025-10-19 - Commit 2
