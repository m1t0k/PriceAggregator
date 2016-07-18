using Akka.Actor;
using Akka.Event;

namespace Crowler.Core.Actors
{
    public abstract class BaseActor : UntypedActor
    {
        protected ILoggingAdapter Logger { get; } = Context.GetLogger();
    }
}