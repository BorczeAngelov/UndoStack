using System;
using System.ComponentModel;

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

        public void Redo()
        {
            if (CanRedo)
            {
                MoveToNextAndExecuteRedo();
            }

            void MoveToNextAndExecuteRedo()
            {
                _undoStack.MoveNext();
                _undoStack.Current.Execute();
            }
        }


        public void Undo()
        {
            if (CanUndo)
            {
                UndoCurrentAndMoveToPrevious();
            }

            void UndoCurrentAndMoveToPrevious()
            {
                _undoStack.Current.RevertExecute();
                _undoStack.MovePrevious();
            }
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
