﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTourProblem.Services
{
    public class FileLoggerService : ILoggerService
    {
        private readonly System.IO.StreamWriter file;

        public FileLoggerService(string fileName)
        {
            file = new System.IO.StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), fileName));
        }
        public void Write(string str)
        {
            file.Write(str);
        }

        public void WriteLine(string str)
        {
            file.WriteLine(str);
        }

        public void Dispose()
        {
            file.Dispose();
        }

    }
}
