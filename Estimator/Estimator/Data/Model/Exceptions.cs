using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estimator.Data.Model
{
    public class Exceptions
    {
        public bool IsExceptionAvailable { get; set; }
        public string ExceptionMessage { get; set; }

        public Exceptions()
        {
            this.IsExceptionAvailable = false;
            this.ExceptionMessage = String.Empty;
        }

        public Exceptions(bool isExceptionAvailable, string exceptionMessage)
        {
            this.IsExceptionAvailable = isExceptionAvailable;
            this.ExceptionMessage = exceptionMessage;
        }

    }
}
