using System;

namespace UndoStack.GenericTwoWayAction
{
    class TwoWayAction_1 : ITwoWayAction
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void RevertExecute()
        {
            throw new NotImplementedException();
        }

        public bool TryToMerge(ITwoWayAction newTwoWayAction)
        {
            throw new NotImplementedException();
        }
    }
}
