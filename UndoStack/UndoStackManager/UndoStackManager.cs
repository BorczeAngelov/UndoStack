using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UndoStack.Interfaces;

namespace UndoStack.UndoStackManager
{
    class UndoStackManager : IUndoStackManager
    {
        readonly LinkedList<ITwoWayAction> _undoStack = new LinkedList<ITwoWayAction>();

        private LinkedListNode<ITwoWayAction> _traversor;


        public UndoStackManager()
        {

        }

        public void AddAndExecute(ITwoWayAction twoWayAction)
        {
            if (_traversor != _undoStack.Last)
            {
                var test = new Stack<ITwoWayAction>();
               
                //_traversor.n
            }

            _undoStack.AddLast(twoWayAction);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
