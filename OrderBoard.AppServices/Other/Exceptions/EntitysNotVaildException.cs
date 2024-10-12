using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Other.Exceptions
{
    public class EntitysNotVaildException : HumanReadableException
    {
        public EntitysNotVaildException(string humanReadableMessage) : base(humanReadableMessage)
        {
        }

        public EntitysNotVaildException(string message, string humanReadableMessage) : base(message, humanReadableMessage)
        {
        }
    }
}
