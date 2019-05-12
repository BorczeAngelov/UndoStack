using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UndoStack;
using UndoStack.GenericTwoWayAction;

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
            var undoStackExecutor = new UndoStackExecutor();
            int counter = 0;

            undoStackExecutor.ExecuteAndAdd(
                new TwaSameDelagateSameArgs<int>(
                    10,
                    argExecute=>
                    {
                        counter += argExecute;
                    },
                    argRevertExecute=>
                    {
                        counter -= argRevertExecute;
                    }));

            undoStackExecutor.Undo();

            undoStackExecutor.Redo();


        } 
    }
}
