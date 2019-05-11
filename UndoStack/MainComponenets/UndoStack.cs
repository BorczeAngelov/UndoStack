using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UndoStack
{
    class UndoStack : IUndoStack
    {
        readonly List<ITwoWayAction> _twoWayActions = new List<ITwoWayAction>();

        public ITwoWayAction Current { get; private set; }

        public void AppendToCurrent(ITwoWayAction twoWayAction)
        {
            if (HasNext())
            {
                RemoveNextActions();
            }
            _twoWayActions.Add(twoWayAction);
            Current = twoWayAction;
        }

        private void RemoveNextActions()
        {
            var currentIndex = GetCurrentIndex();
            int suceddingElementsCount = _twoWayActions.Count - currentIndex;
            _twoWayActions.RemoveRange(currentIndex, suceddingElementsCount);
        }

        public bool MoveNext()
        {
            var result = HasNext();
            if (result)
            {
                var currentIndex = GetCurrentIndex();
                int nextIndex = currentIndex + 1;
                Debug.Assert(_twoWayActions.Count > nextIndex);
                Current = _twoWayActions[nextIndex];
            }
            return result;
        }

        public bool MovePrevious()
        {
            var result = HasPrevious();
            if (result)
            {
                var currentIndex = GetCurrentIndex();
                int previousIndex = currentIndex - 1;
                Debug.Assert(_twoWayActions.Count > previousIndex && previousIndex >= 0);
                Current = _twoWayActions[previousIndex];
            }
            return result;
        }

        public bool HasNext()
        {
            var lastElement = _twoWayActions.LastOrDefault();
            return Current != lastElement;
        }

        public bool HasPrevious()
        {
            var firstElement = _twoWayActions.FirstOrDefault();
            return Current != firstElement;
        }

        private int GetCurrentIndex()
        {
            Debug.Assert(Current != null);
            return _twoWayActions.IndexOf(Current);
        }
    }
}
