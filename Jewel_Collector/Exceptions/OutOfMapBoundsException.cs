using System;

namespace Jewel_Collector.Exceptions
{
    public class OutOfMapBoundsException : Exception
    {
        public OutOfMapBoundsException(): base("A posição está fora dos limites do mapa.")
        {
            
        }
    }
}