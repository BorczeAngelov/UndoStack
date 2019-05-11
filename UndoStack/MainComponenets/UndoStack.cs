using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace UndoStack
{
    class UndoStack : IUndoStack
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly List<ITwoWayAction> _twoWayActions = new List<ITwoWayAction>();
        private ITwoWayAction _current;

        public ITwoWayAction Current
        {
            get => _current;
            private set
            {
                if (_current != value)
                {
                    _current = value;
                    OnPropertyChanged(nameof(Current));
                }
            }
        }

        public void AppendToCurrent(ITwoWayAction newTwoWayAction)
        {
            RemoveNextActionsIfNeeded();
            TryToMergeWithCurrentOrAddToUndoStack();

            #region local functions
            void RemoveNextActionsIfNeeded()
            {
                if (HasNext())
                {
                    RemoveNextActions();
                }
            }

            void TryToMergeWithCurrentOrAddToUndoStack()
            {
                bool hasMerged = Current.TryToMerge(newTwoWayAction);
                if (hasMerged == false)
                {
                    _twoWayActions.Add(newTwoWayAction);
                    Current = newTwoWayAction;
                }
            }
            #endregion
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

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
