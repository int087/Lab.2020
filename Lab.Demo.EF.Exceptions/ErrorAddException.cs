﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Demo.EF.Exceptions
{
    public class ErrorAddException : Exception
    {
        public ErrorAddException() : base("Ocurrió un error al intetar agregar el registro.") { }
    }
}
