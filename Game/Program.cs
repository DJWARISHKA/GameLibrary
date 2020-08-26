using GameLibrary;
using System;

namespace Game
{
    internal class Program
    {
        static int _step = 0;
        static int _lastHistoryCount = 0;
        static GameHistory _history = GameHistory.GetInstance();

        private static void WriteG(GameMap map, HeroList heroes)
        {
            Console.Clear();
            Console.WriteLine(map.ToString());
            Console.WriteLine($"\nStep {_step}\nAll dead {heroes.AllDead}\n");
            for (int i = 0; i < heroes.Count; i++)
            {
                Console.WriteLine(heroes[i].Name);
                Console.WriteLine($"FullHP {heroes[i].FullHp}\tHP {heroes[i].Hp}");
                Console.WriteLine($"FullMP {heroes[i].FullMp}\tMP {heroes[i].Mp}");
                Console.WriteLine($"Attack {heroes[i].Str}\tIntellect {heroes[i].Int}");
                Console.WriteLine($"Agility {heroes[i].Agl}\tExp {heroes[i].Exp}");
                Console.WriteLine($"Position {heroes[i].Position}\tKilled {heroes[i].Kill}\n");
            }
            Console.SetCursorPosition(40, 4);
            Console.Write("Log");
            for (int i = _lastHistoryCount; i < _history.Count; i++)
            {
                Console.SetCursorPosition(40, 5 + i - _lastHistoryCount);
                Console.Write(_history[i]);
            }                
            _lastHistoryCount = _history.Count;
            Console.ReadKey();
        }
        private static void Main(string[] args)
        {
            GameMap map = GameMap.GetInstance();
            HeroList heroes = HeroList.GetInstance();
            GameMusic music = new GameMusic();
            music.Play();
            heroes.AddAuto(6);
            while (true)
            {
                _step++;
                for (int i = 0; i < heroes.Count; i++)
                    heroes[i].Go();
                //if (i++ > 1000000)
                    WriteG(map, heroes);
                heroes.RemoveDead();
                heroes.AddAuto(6 - heroes.Count);
            }
        }
    }
}