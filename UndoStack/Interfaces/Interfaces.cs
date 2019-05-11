using System.ComponentModel;

namespace UndoStack
{
    public interface ITwoWayAction
    {
        void Execute();
        void RevertExecute();
        bool TryToMerge(ITwoWayAction newTwoWayAction);
    }

    public interface IUndoStackExecutor : INotifyPropertyChanged
    {
        void ExecuteAndAdd(ITwoWayAction twoWayAction);
        bool Redo();
        bool Undo();

        bool CanUndo { get; }
        bool CanRedo { get; }
    }

    internal interface IUndoStack : INotifyPropertyChanged
    {
        void AppendToCurrent(ITwoWayAction twoWayAction);
        ITwoWayAction Current { get; }

        bool MoveNext();
        bool MovePrevious();

        bool HasNext();
        bool HasPrevious();
    }
}
