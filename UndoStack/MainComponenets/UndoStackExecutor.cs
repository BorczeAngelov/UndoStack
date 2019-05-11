using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace UndoStack
{
    public class UndoStackExecutor : IUndoStackExecutor
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly IUndoStack _undoStack = new UndoStack();


        public bool CanUndo { get => _undoStack.HasPrevious(); }

        public bool CanRedo { get => _undoStack.HasNext(); }

        public UndoStackExecutor()
        {
            _undoStack.PropertyChanged += OnNewCurrentRefreshCanProperties;

            #region local Function
            void OnNewCurrentRefreshCanProperties(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(_undoStack.Current))
                {
                    OnPropertyChanged(nameof(CanUndo));
                    OnPropertyChanged(nameof(CanRedo));
                }
            }
            #endregion
        }

        public void ExecuteAndAdd(ITwoWayAction twoWayAction)
        {
            twoWayAction.Execute();
            _undoStack.AppendToCurrent(twoWayAction);
        }

        public bool Redo()
        {
            var canPreform = _undoStack.MoveNext();
            if (canPreform)
            {
                _undoStack.Current.Execute();
            }
            return canPreform;
        }

        public bool Undo()
        {
            var canPreform = _undoStack.MovePrevious();
            if (canPreform)
            {
                _undoStack.Current.RevertExecute();
            }
            return canPreform;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
