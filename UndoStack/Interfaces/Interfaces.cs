using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndoStack.Interfaces
{
    public interface ITwoWayAction
    {
        void ExecuteOrRedo();
        void RevertOrUndo();
        void MergeWithNextIfNeeded(ITwoWayAction nextTwoWayAction);
    }

    public interface IUndoStackManager
    {
        void AddAndExecute(ITwoWayAction twoWayAction);
        void Redo();
        void Undo();
        void Clear();
    }
}
