using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UndoStack.Interfaces;

namespace UndoStack.GenericTwoWayAction
{
    class TwoWayAction_1 : ITwoWayAction
    {
        public void ExecuteOrRedo()
        {
            throw new NotImplementedException();
        }

        public void MergeWithNextIfNeeded(ITwoWayAction nextTwoWayAction)
        {
            throw new NotImplementedException();
        }

        public void RevertOrUndo()
        {
            throw new NotImplementedException();
        }
    }
}
