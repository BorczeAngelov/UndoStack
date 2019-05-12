using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

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

            void RemoveNextActions()
            {
                var currentIndex = GetCurrentIndex();
                int suceddingElementsCount = _twoWayActions.Count - currentIndex;
                _twoWayActions.RemoveRange(currentIndex, suceddingElementsCount);
            }

            void TryToMergeWithCurrentOrAddToUndoStack()
            {
                bool? hasMerged = Current?.TryToMerge(newTwoWayAction);
                var hasNotMerged = hasMerged != true;
                if (hasNotMerged)
                {
                    _twoWayActions.Add(newTwoWayAction);
                    Current = newTwoWayAction;
                }
            }
            #endregion
        }


        public void MoveNext()
        {
            var currentIndex = GetCurrentIndex();
            int nextIndex = currentIndex + 1;
            Debug.Assert(nextIndex < _twoWayActions.Count);
            Current = _twoWayActions[nextIndex];
        }

        public void MovePrevious()
        {
            var currentIndex = GetCurrentIndex();
            int previousIndex = currentIndex - 1;

            var isPreviousBeforeFirstIndex = previousIndex == -1;
            if (isPreviousBeforeFirstIndex)
            {
                Current = null;
            }
            else
            {
                Current = _twoWayActions[previousIndex];
            }
        }

        public bool HasNext()
        {
            return GetCurrentIndex() < _twoWayActions.Count - 1;
        }

        public bool HasPrevious()
        {
            return GetCurrentIndex() >= 0;
        }

        private int GetCurrentIndex()
        {
            return _twoWayActions.IndexOf(Current);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
