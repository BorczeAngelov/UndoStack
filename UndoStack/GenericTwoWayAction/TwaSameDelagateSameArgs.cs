using System;

namespace UndoStack.GenericTwoWayAction
{
    public class TwaSameDelagateSameArgs<TArgs> : ITwoWayAction
    {
        private readonly TArgs _args;
        private readonly Action<TArgs> _execute;
        private readonly Action<TArgs> _revertExecute;
        private readonly Func<ITwoWayAction, bool> _mergeCondition;
        private readonly Action<ITwoWayAction> _mergeProcedure;

        public TwaSameDelagateSameArgs(
            TArgs args,
            Action<TArgs> execute,
            Action<TArgs> revertExecute,
            Func<ITwoWayAction, bool> mergeCondition = null,
            Action<ITwoWayAction> mergeProcedure = null)
        {
            _args = args;
            _execute = execute;
            _revertExecute = revertExecute;
            _mergeCondition = mergeCondition;
            _mergeProcedure = mergeProcedure;
        }

        public void Execute()
        {
            _execute.Invoke(_args);
        }

        public void RevertExecute()
        {
            _revertExecute.Invoke(_args);
        }

        public bool TryToMerge(ITwoWayAction newTwoWayAction)
        {
            var result = _mergeCondition?.Invoke(newTwoWayAction);
            var shouldMerge = result == true;

            if (shouldMerge)
            {
                _mergeProcedure?.Invoke(newTwoWayAction);
            }

            return shouldMerge;
        }
    }
}
