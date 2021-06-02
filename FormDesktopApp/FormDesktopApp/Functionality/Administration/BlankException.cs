using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormDesktopApp.Functionality.Administration
{
    class BlankException : Exception
    {
        public BlankException(string message) : base(message)
        {

        }
    }
}
