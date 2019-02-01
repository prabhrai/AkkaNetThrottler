namespace akkaThrottler
{

  using Akka.Actor;

  /// <summary>The wrapper.</summary>
  /// <typeparam name="T"></typeparam>
  public class Wrapper<T>
  {
    /// <summary>Initializes a new instance of the <see cref="Wrapper{T}"/> class.</summary>
    /// <param name="message">The message.</param>
    /// <param name="destinationActor">The destination actor.</param>
    public Wrapper(T message, IActorRef destinationActor)
    {
      Message = message;
      DestinationActor = destinationActor;
    }

    /// <summary>Gets the destination actor.</summary>
    public IActorRef DestinationActor { get; }

    /// <summary>Gets the message.</summary>
    public T Message { get; }
  }
}

