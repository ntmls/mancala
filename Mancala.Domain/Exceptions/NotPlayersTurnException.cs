using System;
using System.Runtime.Serialization;

namespace Mancala.Domain
{
    [Serializable]
    public class NotPlayersTurnException : Exception
    {
        public NotPlayersTurnException()
        {
        }
    }
}