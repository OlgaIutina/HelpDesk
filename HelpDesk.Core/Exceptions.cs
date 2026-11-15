using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Core
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }

    //если объект не найден в базе
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
