using ChessOk.ModelFramework.Scopes;

namespace ChessOk.ModelFramework
{
    public interface IHaveModelScope
    {
        IModelScope Model { get; set; }
    }
}