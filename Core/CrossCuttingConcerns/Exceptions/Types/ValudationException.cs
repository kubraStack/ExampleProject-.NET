using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Types
{
    public class ValudationException : Exception
    {
        public IEnumerable<string> Errors { get; set; }

        public ValudationException(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
