using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Exceptions
{
    public class EntitiesNotFoundException : HumanReadableException
    {
        public EntitiesNotFoundException(string humanReadableMessage) : base(humanReadableMessage)
        {
        }

        public EntitiesNotFoundException(string message, string humanReadableMessage) : base(message, humanReadableMessage)
        {
        }
    }
}
