using System.ComponentModel;

namespace UndoStack
{
    public interface ITwoWayAction
    {
        void Execute();
        void RevertExecute();
        void MergeWithNextIfNeeded(ITwoWayAction nextTwoWayAction);
    }

    public interface IUndoStackExecutor : INotifyPropertyChanged
    {
        void ExecuteAndAdd(ITwoWayAction twoWayAction);
        bool Redo();
        bool Undo();

        bool CanUndo { get; }
        bool CanRedo { get; }
    }

    internal interface IUndoStack
    {
        void AppendToCurrent(ITwoWayAction twoWayAction);
        ITwoWayAction Current { get; }

        bool MoveNext();
        bool MovePrevious();

        bool HasNext();
        bool HasPrevious();
    }
}
