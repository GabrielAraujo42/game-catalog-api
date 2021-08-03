using System;

namespace GameCatalogAPI.Exceptions
{
    public class GameAlreadyRegisteredException : Exception
    {
        public GameAlreadyRegisteredException()
            : base("Game already registered in catalog.") 
        { }
    }
}
