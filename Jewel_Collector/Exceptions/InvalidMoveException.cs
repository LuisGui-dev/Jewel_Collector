using System;

namespace Jewel_Collector.Exceptions
{
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException() : base("Movimento inválido. A posição está ocupada por outro item.")
        {
        }
    }
}