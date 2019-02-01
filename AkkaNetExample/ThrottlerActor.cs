namespace akkaThrottler
{
    using System;

    using Akka.Actor;
    using AkkaNetExample.Messages;

    /// <summary>The throttler actor.</summary>
    public partial class ThrottlerActor : ReceiveActor, IWithUnboundedStash
    {
        /// <summary>The _interval.</summary>
        private readonly TimeSpan _interval;

        /// <summary>The _number of messages.</summary>
        private readonly uint _numberOfMessages;

        /// <summary>The _cancel timer.</summary>
        private ICancelable _cancelTimer;

        /// <summary>The _counter.</summary>
        private uint _counter;

        /// <summary>Initializes a new instance of the <see cref="ThrottlerActor"/> class.</summary>
        /// <param name="interval">The interval.</param>
        /// <param name="numberOfMessages">The number of messages.</param>
        public ThrottlerActor(TimeSpan interval, uint numberOfMessages)
        {
            _interval = interval;
            _numberOfMessages = numberOfMessages;
            Receive<Wrapper<TransformPhoneticTextMessage>>(
              message =>
                {
                    InitTimerAndCounter();

                    SendMessage(message);

                    SetNextState();
                });

            Receive<MessageTimer>(t => { _cancelTimer?.Cancel(); });
        }

        public IStash Stash { get; set; }

        private void Iddling()
        {
            Receive<Wrapper<TransformPhoneticTextMessage>>(message => { Stash.Stash(); });

            Receive<MessageTimer>(
              t =>
                {
                    _counter = 0;
                    Console.WriteLine("Become(Throttling)");
                    Become(Throttling);
                    Stash.Unstash();
                });
        }

        private void InitTimerAndCounter()
        {
            _counter = 0;
            _cancelTimer?.Cancel();
            _cancelTimer = new Cancelable(Context.System.Scheduler);
            Context.System.Scheduler.ScheduleTellRepeatedly(_interval, _interval, Self, new MessageTimer(), Self, _cancelTimer);
        }

        private void SendMessage(Wrapper<TransformPhoneticTextMessage> message)
        {
            // send message
            //var x = message.DestinationActor.Ask(message.Message).Result;
            Console.WriteLine("Dispatcher - dispatching message with id " + message.Message.MessageId);
            message.DestinationActor.Tell(message.Message);
            _counter++;
        }

        private void SetNextState()
        {
            if (_numberOfMessages != _counter)
            {
                Console.WriteLine(" Become(Throttling)");
                Become(Throttling);
                Stash.Unstash();
            }
            else
            {
                Console.WriteLine(" Become(Iddling)");
                Become(Iddling);
            }
        }

        private void Throttling()
        {
            Console.WriteLine(" Became(Throttling)");
            Receive<Wrapper<TransformPhoneticTextMessage>>(
              message =>
                {
                    SendMessage(message);

                    SetNextState();
                });

            Receive<MessageTimer>(t => { UnbecomeStacked(); });
        }
    }
}
