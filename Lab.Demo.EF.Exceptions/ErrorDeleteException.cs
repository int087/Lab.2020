using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Demo.EF.Exceptions
{
    public class ErrorDeleteException : Exception
    {
        public ErrorDeleteException() : base("Ocurrió un error al eliminar.") { }
    }
}
