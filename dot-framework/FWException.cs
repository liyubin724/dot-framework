using System;

namespace Dot.Framework
{
    public class FWException : Exception
    {
        public FWException(string message, string hint)
            : base(hint != null ? (message + "\n" + hint) : message)
        {
        }
    }
}
