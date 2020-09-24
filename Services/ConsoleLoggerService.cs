using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTourProblem.Services
{
    public class ConsoleLoggerService : ILoggerService
    {

        public void Write(string str)
        {
            Console.Write(str);
        }

        public void WriteLine(string str)
        {
            Console.WriteLine(str);
        }

        public void Dispose()
        {
            return;
        }
    }
}
