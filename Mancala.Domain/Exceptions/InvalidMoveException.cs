using System;
using System.Runtime.Serialization;

namespace Mancala.Domain
{
    [Serializable]
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException()
        {
        }

    }
}