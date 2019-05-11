using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            new Runner().Run();
        }


    }

    class Runner
    {
        public void Run()
        {
            var temp = new UndoStack.UndoStackExecutor();

            temp.ExecuteAndAdd(null);
        }
    }
}
