using ClassHomework;
using System;

namespace GameLibrary
{
    public class GameMap
    {
        private static GameMap instance;
        private int _size = 30;
        private MyList<string> _map;

        public int Size { get => _size; }
        public MyList<string> Map { get => _map; }

        private GameMap()
        {
            _map = new MyList<string>();
            for (int i = 0; i < _size; i++)
            {
                _map.Add("_");
            }
        }
        private void Clear()
        {
            _map.Clear();
            for (int i = 0; i < _size; i++)
            {
                _map.Add("_");
            }
        }

        public string this[int index]
        {
            get => _map[index];
        }

        //private GameMap(int Size)
        //{
        //     _size = Size;

        //}

        public static GameMap GetInstance()
        {
            if (instance == null)
                instance = new GameMap();
            return instance;
        }

        public bool Add(Object Personage, int Position)
        {
            if (Position < 0 || Position >= _size) return false;
            if (_map[Position] != "_") return false;
            BasicAtributs _personage = (BasicAtributs)Personage;
            if (!_personage.StartPosition(Position)) return false;
            if (_map.Contains(_personage.Name)) return false;
            _map[Position] = _personage.Name;
            return true;
        }

        public bool Remove(Object Personage)
        {
            BasicAtributs _personage = (BasicAtributs)Personage;
            if (_personage.Position < 0 || _personage.Position > _size) return false;
            if (_map[_personage.Position] != _personage.Name) return false;
            _map[_personage.Position] = "_";
            return true;
        }

        public bool RemoveAll(HeroList Heroes)
        {
            Clear();
            for (int j = 0; j < Heroes.Count; j++)
            {
                _map[Heroes[j].Position] = Heroes[j].Name;
            }
            return true;
        }

        public bool Move(int FromPosition, int ToPosition)
        {
            GameHistory history = GameHistory.GetInstance();
            if (ToPosition < 1 || ToPosition >= _size)
            {
                history.History.Add("Targeted position out of map");
                return false;
            }
            if (_map[ToPosition] != "_" || _map[FromPosition] == "_")
            {
                history.History.Add("Targeted position occupied");
                return false;
            }
            _map[ToPosition] = _map[FromPosition];
            _map[FromPosition] = "_";
            return true;
        }

        public override string ToString()
        {
            string map = "";
            for (int i = 0; i < _map.Count; i++)
            {
                map += _map[i] + "|";
            }
            return map;
        }
    }
}