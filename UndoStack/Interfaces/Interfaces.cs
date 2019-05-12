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
        void Redo();
        void Undo();

        bool CanUndo { get; }
        bool CanRedo { get; }
    }

    internal interface IUndoStack : INotifyPropertyChanged
    {
        void AppendToCurrent(ITwoWayAction twoWayAction);
        ITwoWayAction Current { get; }

        void MoveNext();
        void MovePrevious();

        bool HasNext();
        bool HasPrevious();
    }
}
