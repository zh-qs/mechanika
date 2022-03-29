using System;
using System.Collections.Generic;
using System.Text;

namespace Mechanika
{
    interface IMemorable
    {
        public IMemento GetMemento(string operationName);
        public void RestoreFrom(IMemento memento);
     
    }
}
