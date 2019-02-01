using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNetExample.Messages
{
    public class TransformPhoneticTextMessage 
    {
        public int MessageId { get; set; }

        public string InputText { get; private set; }
        
        public TransformPhoneticTextMessage(string inputText)
        {
            InputText = inputText;
        }

    }
}
