using System;

namespace GameLibrary
{
    public class AutoHero<T>
        where T : BasicAtributs,  new()  
    {
        private GameMap _map = GameMap.GetInstance();
        private HeroList _heroes = HeroList.GetInstance();
        private T _hero = new T();
        private static Random rnd = new Random();

        public T Hero { get => _hero; }

        public AutoHero()
        {
            _map.Add(Hero, rnd.Next(1, _map.Size));
        }

        
    }
}