using KnightsTourProblem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTourProblem
{
    public class KnightsTourProblem : IDisposable
    {
        private readonly int _lengthOfBoard;
        private readonly int _initialX;
        private readonly int _initialY;

        private int _numberOfSquaresOnBoard => _lengthOfBoard * _lengthOfBoard;
        private int _trials = 0;
        private int _level = 1;

        private readonly ILoggerService _longFileLogger;
        private readonly ILoggerService _shortFileLogger;
        private readonly ILoggerService _consoleLogger;

        public KnightsTourProblem(int lengthOfBoard, int initialX, int initialY, ILoggerService longFileLogger, ILoggerService shortFileLogger, ILoggerService consoleLogger)
        {
            _initialX = initialX;
            _initialY = initialY;
            _lengthOfBoard = lengthOfBoard;

            if (shortFileLogger == null)
                throw new ArgumentNullException(nameof(shortFileLogger));
            _shortFileLogger = shortFileLogger;

            if (longFileLogger == null)
                throw new ArgumentNullException(nameof(longFileLogger));

            _longFileLogger = longFileLogger;

            if (consoleLogger == null)
                throw new ArgumentNullException(nameof(consoleLogger));
            _consoleLogger = consoleLogger;
        }

        public void Execute()
        {
            _trials = 0;
            _level = 0;

            WriteLine("PART 1. Data", true, true, true);
            WriteLine($"1) Board {_lengthOfBoard}x{_lengthOfBoard}.", true, true, true);
            WriteLine($"2) Initial position X={_initialX}, Y={_initialY}. L=1", true, true, true);

            WriteLine("PART 2. Trace", false, true, false);
            var solution = ExecuteInternal();

            WriteLine("", true, true, true);
            WriteLine("Part 3. Results", true, true, true);
            WriteLine(solution.Item1 ? $"1) Path is found. Trials={_trials}." : $"1) Path is not found. Trials={_trials}", true, true, true);
            WriteLine("2) Path graphically:", true, true, true);
            Print(solution.Item2);
        }

        /// <summary>
        /// Initializes variables and starts recursion
        /// </summary>
        /// <returns>(True, array) if path is found, (False, array) if path is not found.</returns>
        private (bool, int[,]) ExecuteInternal()
        {
            //Initializes variables
            int[,] solution = new int[_lengthOfBoard, _lengthOfBoard];

            //Set all cells to not visited
            for (int x = 0; x < _lengthOfBoard; x++)
                for (int y = 0; y < _lengthOfBoard; y++)
                    solution[x, y] = -1;

            //Define moves of a knight
            int[] xMove = {2, 1, -1, -2,
                           -2, -1, 1, 2};
            int[] yMove = {1, 2, 2, 1,
                           -1, -2, -2, -1};

            //Set starting position
            solution[_initialX - 1, _initialY - 1] = 0;

            //Run recursion
            if (!ExecuteInternalRec(_initialX - 1, _initialY - 1, 1, solution, xMove, yMove))
                return (false, solution);

            return (true, solution);
        }

        /// <summary>
        /// Main recursion algorithm to solve Knights problem
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="movei">Next cell number</param>
        /// <param name="solution">Solution array</param>
        /// <param name="xMove">Moves of knight in x axis</param>
        /// <param name="yMove">Moves of knight in y axis</param>
        /// <returns></returns>
        private bool ExecuteInternalRec(int x, int y, int movei,
                                     int[,] solution, int[] xMove,
                                     int[] yMove)
        {
            //Initialize variables
            int k, 
                next_x, 
                next_y;

            bool isThread = false;
            bool isBacktrack = false;

            //Algorithm finished, all cells visited.
            if (movei == _numberOfSquaresOnBoard)
                return true;

            _level++;

            //Go through each move
            for (k = 0; k < 8; k++)
            {
                //Calculate next move
                next_x = x + xMove[k];
                next_y = y + yMove[k];


                //Check if was backtracked
                if (isBacktrack)
                    Write("Backtrack.", false, true, false);

                WriteLine("", false, true, false);

                isBacktrack = false;
                isThread = false;

                _trials++;
                Write($"{string.Format("{0,8}", _trials)}) {RepeatSymbol('-', _level - 1)}R{k + 1}. U={next_x + 1}, V={next_y + 1}. L={_level + 1}. ", false, true, false);

                //Check if already visited
                if (next_x >= 0 && next_x < _lengthOfBoard &&
                    next_y >= 0 && next_y < _lengthOfBoard &&
                    solution[next_x, next_y] > -1)
                {
                    Write("Thread. ", false, true, false);
                    isThread = true;
                }
                
                //Check if safe to move - given cell is in board and hasn't been already visited
                if (IsSafe(next_x, next_y, solution))
                {
                    //Visit cell
                    solution[next_x, next_y] = movei;

                    Write($"Free. BOARD[{next_x + 1},{next_y + 1}]:={movei + 1}", false, true, false);

                    //Move to other possible cells
                    if (ExecuteInternalRec(next_x, next_y, movei + 1, solution, xMove, yMove))
                        return true;
                    else
                    //In case of failure, backtrack
                    {
                        isBacktrack = true;
                        solution[next_x, next_y] = -1;
                        _level--;
                    }
                }
                //Isn't safe - either visited already or is out of bounds of the board
                else
                {
                    if (!isThread)
                        Write($"Out. ", false, true, false);
                }
            }

            return false;
        }

        /// <summary>
        /// Utility to print result
        /// </summary>
        /// <param name="solution">Solution matrix</param>
        private void Print(int[,] solution)
        {
            var res = InverseArray(solution);

            //Compute first line
            var firstLine = new StringBuilder();
            firstLine.Append("Y,");
            firstLine.Append(" ");
            firstLine.Append("V");
            firstLine.Append(" ");
            firstLine.Append("^");

            for (int i = 3; i < _lengthOfBoard; i++)
                firstLine.Append(" ");

            WriteLine(firstLine.ToString(), true, true, true);

            //Compute rest of lines
            for(int i = 0; i < _lengthOfBoard; i++)
            {
                var line = new StringBuilder();
                line.Append("    ");
                line.Append(_lengthOfBoard - i);
                line.Append("|");

                for (int j = 0; j < _lengthOfBoard; j++)
                    line.Append(string.Format(" {0, 2} ", res[i, j] + 1));

                WriteLine(line.ToString(), true, true, true);
            }

            //Print last lines
            //Compute second to last line
            var secondToLastLine = new StringBuilder();
            secondToLastLine.Append("     ");

            for (int i = 0; i < _lengthOfBoard*4+3; i++)
                secondToLastLine.Append("-");

            secondToLastLine.Append(">");
            secondToLastLine.Append(" ");
            secondToLastLine.Append("X,U");

            WriteLine(secondToLastLine.ToString(), true, true, true);

            //Compute last line
            var lastLine = new StringBuilder();
            lastLine.Append("      ");
            for (int i = 1; i <= _lengthOfBoard; i++)
                lastLine.Append(string.Format(" {0, 2} ", i));

            WriteLine(lastLine.ToString(), true, true, true);

        }

        /// <summary>
        /// Inverses matrix to provide proper matrix for printing
        /// </summary>
        /// <param name="solution">Solution matrix</param>
        /// <returns>Inversed matrix</returns>
        private int[,] InverseArray(int[,] solution)
        {
            var res = new int[_lengthOfBoard, _lengthOfBoard];
            Array.Copy(solution, res, solution.Length);
            return ReverseY(Transpose(res));
        }

        /// <summary>
        /// Reverses Y direction of matrix columns
        /// </summary>
        /// <param name="matrix">Matrix</param>
        /// <returns>Reversed matrix</returns>
        private int[,] ReverseY(int[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            int[,] result = new int[h, w];

            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    result[_lengthOfBoard - i - 1, j] = matrix[i, j];

            return result;
        }

        /// <summary>
        /// Transposes matrix (switches columns and rows)
        /// </summary>
        /// <param name="matrix">Matrix</param>
        /// <returns>Transposed matrix</returns>
        private int[,] Transpose(int[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            int[,] result = new int[h, w];

            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    result[j, i] = matrix[i, j];

            return result;
        }


        /// <summary>
        /// Checks if it's save to move to specified x,y coordinates. 
        /// </summary>
        /// <param name="x">X to move to</param>
        /// <param name="y">Y to move to</param>
        /// <param name="solution">Solution matrix</param>
        /// <returns>True - if x,y coordinates are not visited yet. False - otherwise</returns>
        private bool IsSafe(int x, int y, int[,] solution)
        {
            return (x >= 0 && x < _lengthOfBoard &&
                    y >= 0 && y < _lengthOfBoard &&
                    solution[x, y] == -1);
        }

        /// <summary>
        /// Repeats the symbol amount of times and forms a string
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="amount">Amount of times to repeat symbol</param>
        /// <returns>String composed of symbol repeated amount of times</returns>
        private string RepeatSymbol(char symbol, int amount)
        {
            var str = new StringBuilder();
            for (int i = 0; i < amount; i++)
                str.Append(symbol);

            return str.ToString();
        }

        /// <summary>
        /// Write without new line
        /// </summary>
        /// <param name="str">string to write</param>
        /// <param name="shortFile">Write to short file?</param>
        /// <param name="longFile">Write to long file?</param>
        /// <param name="console">Write to console</param>
        private void Write(string str, bool shortFile, bool longFile, bool console)
        {
            if (shortFile)
                _shortFileLogger.Write(str);

            if (longFile)
                _longFileLogger.Write(str);

            if (console)
                _consoleLogger.Write(str);
        }

        /// <summary>
        /// Write with new line
        /// </summary>
        /// <param name="str">string to write</param>
        /// <param name="shortFile">Write to short file?</param>
        /// <param name="longFile">Write to long file?</param>
        /// <param name="console">Write to console</param>
        private void WriteLine(string str, bool shortFile, bool longFile, bool console)
        {
            if (shortFile)
                _shortFileLogger.WriteLine(str);

            if (longFile)
                _longFileLogger.WriteLine(str);

            if (console)
                _consoleLogger.WriteLine(str);
        }

        public void Dispose()
        {
            _consoleLogger.Dispose();
            _shortFileLogger.Dispose();
            _longFileLogger.Dispose();
        }
    }
}
