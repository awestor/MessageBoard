using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Other.Exceptions
{
    public class EntititysNotVaildException : HumanReadableException
    {
        public EntititysNotVaildException(string humanReadableMessage) : base(humanReadableMessage)
        {
        }

        public EntititysNotVaildException(string message, string humanReadableMessage) : base(message, humanReadableMessage)
        {
        }
    }
}
