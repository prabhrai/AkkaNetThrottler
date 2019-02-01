using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkkaNetExample.Messages;
using AkkaNetExample;

namespace AkkaNetExample.Actors
{
    public class PhonetizerActor : Akka.Actor.ReceiveActor
    {
        public PhonetizerActor()
        {
            Receive<TransformPhoneticTextMessage>(m => OnTransformPhoneticTextMessage(m));
        }

        private void OnTransformPhoneticTextMessage(TransformPhoneticTextMessage m)
        {
            Console.WriteLine("Actor:PhonetizerActor - started handling message with id " + m.MessageId);
            string response = LetterPhonetizer.getPhoneticLetter(m.InputText);
            Console.WriteLine("Actor:PhonetizerActor - result " + response);
            Sender.Tell(response, Self);
        }
    }
}
