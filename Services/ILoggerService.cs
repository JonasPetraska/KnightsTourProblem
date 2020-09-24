using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTourProblem.Services
{
    public interface ILoggerService : IDisposable
    {
        void Write(string str);
        void WriteLine(string str);

    }
}
