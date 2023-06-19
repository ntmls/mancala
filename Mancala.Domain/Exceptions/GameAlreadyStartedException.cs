using System;
using System.Runtime.Serialization;

namespace Mancala.Domain
{
    [Serializable]
    public class GameAlreadyStartedException : Exception
    {
        public GameAlreadyStartedException()
        {
        }
    }
}