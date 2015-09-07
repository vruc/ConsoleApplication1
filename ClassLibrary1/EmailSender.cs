using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    [Export(typeof(IMessageSender))]
    class EmailSender : IMessageSender
    {
        public void Send(string message)
        {
            Console.WriteLine(message);
        }
    }
}
