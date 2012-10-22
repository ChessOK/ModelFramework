using ChessOk.ModelFramework.Contexts;

namespace ChessOk.ModelFramework
{
    public interface IHaveContext
    {
        IContext Context { get; set; }
    }
}