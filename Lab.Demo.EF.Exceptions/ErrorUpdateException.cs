using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Demo.EF.Exceptions
{
    public class ErrorUpdateException : Exception
    {
        public ErrorUpdateException() : base("Ocurrió un error al actualizar los datos.") { }
    }
}
