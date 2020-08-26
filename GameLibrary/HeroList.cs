using System;
using ClassHomework;

namespace GameLibrary
{
    public enum HeroType
    {
        Elpf,
        Orc,
        Human
    }

    public class HeroList
    {
        private static HeroList instance;
        private MyList<BasicAtributs> _heroes = new MyList<BasicAtributs>();
        private int _allDead;
        private static Random rnd = new Random();
        bool _person = false;

        public MyList<BasicAtributs> Heroes { get => _heroes; }

        public void Add(int Type, string Name)
        {
            switch ((HeroType)Type)
            {

                case (HeroType.Elpf):
                    Heroes.Add(new Elpf(Name));
                    break;
                case (HeroType.Orc):
                    Heroes.Add(new Orc(Name));
                    break;
                case (HeroType.Human):
                    Heroes.Add(new Human(Name));
                    break;
            }
            GameMap _map = GameMap.GetInstance();
            _map.Add(Heroes[Heroes.Count - 1], rnd.Next(1, _map.Size));
            _person = true;
        }
        public void AddAuto()
        {
            switch ((HeroType)rnd.Next(0, 3))
            {
               
                case (HeroType.Elpf):
                    Heroes.Add(new Elpf());
                    break;
                case (HeroType.Orc):
                    Heroes.Add(new Orc());
                    break;
                case (HeroType.Human):
                    Heroes.Add(new Human());
                    break;
            }
            GameMap _map = GameMap.GetInstance();
            _map.Add(Heroes[Heroes.Count-1], rnd.Next(1, _map.Size));
        }

        public void AddAuto(int Count)
        {
            GameMap _map = GameMap.GetInstance();
            for (int i = 0; i < Count; i++)
            {
                switch ((HeroType)rnd.Next(0, 3))
                {

                    case (HeroType.Elpf):
                        Heroes.Add(new Elpf());
                        break;
                    case (HeroType.Orc):
                        Heroes.Add(new Orc());
                        break;
                    case (HeroType.Human):
                        Heroes.Add(new Human());
                        break;
                }                
                _map.Add(Heroes[Heroes.Count - 1], rnd.Next(1, _map.Size));
            }
        }

        private HeroList()
        {
        }

        public BasicAtributs this[int index]
        {
            get => _heroes[index];
        }

        public int Count { get => _heroes.Count; }
        public int AllDead { get => _allDead; }
        public bool Person { get => _person; }

        public static HeroList GetInstance()
        {
            if (instance == null)
                instance = new HeroList();
            return instance;
        }

        public void RemoveDead()
        {
            GameMap map = GameMap.GetInstance();
            for (int i = 0; i < _heroes.Count; i++)
            {
                if (_heroes[i].IsDead || _heroes[i].Position == 0)
                {
                    map.Remove(_heroes[i]);
                    _heroes.RemoveAt(i);
                    _allDead++;
                }
            }
            int j = 0;
            for (int i = 0; i < map.Size; i++)
            {
                if (map[i] != "_") j++;
            }
            if(j > _heroes.Count)
                map.RemoveAll(this);
        }
    }
}