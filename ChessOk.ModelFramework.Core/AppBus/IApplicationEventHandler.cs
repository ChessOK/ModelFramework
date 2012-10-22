namespace ChessOk.ModelFramework.Messages
{
    public interface IApplicationEventHandler
    {
        bool Handle(IApplicationMessage ev);
    }

    public abstract class ApplicationEventHandler<T> : ApplicationEventHandler
        where T : class, IApplicationMessage
    {
        protected abstract bool Handle(T message);

        public sealed override bool Handle(IApplicationMessage ev)
        {
            var casted = ev as T;
            if (casted == null)
            {
                return false;
            }

            return Handle(casted);
        }
    }
}
