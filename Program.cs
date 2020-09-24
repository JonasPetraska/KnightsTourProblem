using KnightsTourProblem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTourProblem
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("Enter length of board: ");
			var lengthOfBoard = Console.ReadLine();

			if (!int.TryParse(lengthOfBoard, out var lengthOfBoardVal))
			{
				Console.WriteLine("Please enter a valid number.");
				Main(args);
				return;
			}

			if (lengthOfBoardVal > 10 || lengthOfBoardVal <= 0)
			{
				Console.WriteLine("Please enter a number between 1 and 10 inclusively.");
				Main(args);
				return;
			}

			Console.WriteLine("Enter initial X coordinate: ");
			var xCoordinate = Console.ReadLine();

			if (!int.TryParse(xCoordinate, out var xCoordinateVal))
			{
				Console.WriteLine("Please enter a valid number.");
				Main(args);
				return;
			}

			if (xCoordinateVal > lengthOfBoardVal || xCoordinateVal <= 0)
			{
				Console.WriteLine($"Please enter a number between 1 and {lengthOfBoardVal} inclusively.");
				Main(args);
				return;
			}

			Console.WriteLine("Enter initial Y coordinate: ");
			var yCoordinate = Console.ReadLine();

			if (!int.TryParse(yCoordinate, out var yCoordinateVal))
			{
				Console.WriteLine("Please enter a valid number.");
				Main(args);
				return;
			}

			if (yCoordinateVal > lengthOfBoardVal || yCoordinateVal <= 0)
			{
				Console.WriteLine($"Please enter a number between 1 and {lengthOfBoardVal} inclusively.");
				Main(args);
				return;
			}

			using (var obj = new KnightsTourProblem(lengthOfBoardVal, xCoordinateVal, yCoordinateVal,
												   new FileLoggerService("out-long.txt"),
												   new FileLoggerService("out-short.txt"),
												   new ConsoleLoggerService()))
			{
				obj.Execute();
				Console.ReadKey();
			}
		}
    }
}
