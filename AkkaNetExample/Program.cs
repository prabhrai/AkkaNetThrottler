using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka;
using Akka.Actor;
using AkkaNetExample.Actors;
using akkaThrottler;
using AkkaNetExample.Messages;
using System.Threading;

namespace AkkaNetExample
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // create an actor system
            // created only once per logical application
            var actorSystem = Akka.Actor.ActorSystem.Create("ExampleSystem");

            IActorRef phonetizerActor = actorSystem.ActorOf<AkkaNetExample.Actors.PhonetizerActor>();

            // using props
            //IActorRef phonetizerActor = actorSystem.ActorOf(Props.Create(() => new AkkaNetExample.Actors.PhonetizerActor())) ;
            //var res = phonetizerActor.Ask(new AkkaNetExample.Messages.TransformPhoneticTextMessage("test"));

            //for(int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine(phonetizerActor.Ask(new TransformPhoneticTextMessage("test")).Result + " " + i); 
            //}

            IActorRef throttledPhonetizerActor = actorSystem.ActorOf(Props.Create(() => new ThrottlerActor(TimeSpan.FromSeconds(3), 20)));

            for (int i = 0; i < 1000; i++)
            {

                var textMessage = new AkkaNetExample.Messages.TransformPhoneticTextMessage("test");
                //message = new ThrottlerActor<string>(_testProbe, "bbbb");
                var messageToSend = new Wrapper<TransformPhoneticTextMessage>(textMessage, phonetizerActor);

                Console.WriteLine("telling " + i + " from program");
                messageToSend.Message.MessageId = i;
                //var s = throttledPhonetizerActor.Ask(message1).Result;

                throttledPhonetizerActor.Tell(messageToSend);

                if (i % 20 == 0)
                {
                    Thread.Sleep(4000);
                }
            }

            while (true)
            {
                string quit = Console.ReadLine();
                if (quit.Equals("qq"))
                    return;
            }
        }
    }
}
