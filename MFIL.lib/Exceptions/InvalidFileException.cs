using System;

namespace MFIL.lib.Exceptions
{
    public class InvalidFileException : Exception
    {
        public InvalidFileException(string message) : base(message)
        {
        }
    }
}