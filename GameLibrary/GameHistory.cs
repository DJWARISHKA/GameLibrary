using System;
using System.Collections.Generic;
using System.Text;
using ClassHomework;

namespace GameLibrary
{
    public class GameHistory
    {
        private static GameHistory instance;

        private GameHistory() { }
        public static GameHistory GetInstance()
        {
            if (instance == null)
                instance = new GameHistory();
            return instance;
        }

        MyList<string> _history = new MyList<string>();

        public MyList<string> History { get => _history; set => _history = value; }

        public string this[int index] { get => _history[index]; }

        public int Count { get => _history.Count; }
    }
}
