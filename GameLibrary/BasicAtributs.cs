using System;

namespace GameLibrary
{
    public abstract class BasicAtributs
    {
        protected int _fullhp = 100;
        protected int _hp = 100;
        protected int _fullmp = 50;
        protected int _mp = 50;
        protected int _str = 10;
        private int _spellstr = 0;
        protected int _int = 10;
        protected int _agl = 10;
        private int _spellagl = 0;
        protected int _kill = 0;
        protected int _exp = 0;
        protected int _gopoints = 5;
        protected bool _isdead = false;
        protected bool _onmap = false;
        protected string _name = "0";
        protected int _position;
        protected HeroType _type;
        private static Random _rnd = new Random();
        private int _cooldown = 0;
        private GameHistory _history = GameHistory.GetInstance();

        //Properties
        public int FullHp { get => _fullhp; }
        public int FullMp { get => _fullmp; }
        public int Hp { get => _hp; }
        public int Mp { get => _mp; }
        public int Str { get => _str + _spellstr; }
        public int Int { get => _int; }
        public int Agl { get => _agl + _spellagl; }

        //Other
        public bool IsDead { get => _isdead; }

        public int Kill { get => _kill; }
        public int Exp { get => _exp; }
        public string Name { get => _name; }

        //ForMap
        public int Position { get => _position; }
        public HeroType Type { get => _type; }

        protected void Naming(string Name)
        {
            _name = Name;
        }
        public virtual bool Move(int ToPosition)
        {
            GameMap map = GameMap.GetInstance();
            if (ToPosition < 1 || ToPosition >= map.Size)
            {
                _history.History.Add("Targeted position out of map");
                return false;
            }
            if (Position > ToPosition)
            {
                if ((Position - _gopoints) <= ToPosition)
                    if (map[ToPosition] != "_")
                        ToPosition++;
            }
            else if ((Position + _gopoints) >= ToPosition)
                if (map[ToPosition] != "_")
                    ToPosition--;
            if (Math.Abs(Position - ToPosition) > _gopoints)
                if (Position < ToPosition)
                    ToPosition = Position - _gopoints;
                else ToPosition = Position + _gopoints;
            if (!map.Move(_position, ToPosition)) return false;
            _history.History.Add($"{_name} moved from {_position} to {ToPosition}");
            _position = ToPosition;
            if (_cooldown > 0) _cooldown--;
            else _spellagl = _spellstr = 0;
            return true;
        }

        public virtual bool StartPosition(int Position)
        {
            if (_onmap) return false;
            _position = Position;
            return _onmap = true;
        }
        public virtual bool Attack(BasicAtributs Enemie)
        {
            _history.History.Add($"{_name} attack {Enemie._name}");
            BasicAtributs _enemie = Enemie;
            if (_enemie.IsDead)
            {
                _history.History.Add($"{_enemie._name} is already dead");
                return false;
            }
            if (_enemie.Position != _position - 1 && _enemie.Position != _position + 1)
            {
                _history.History.Add($"{_enemie._name} is to far");
                return false;
            }
            _enemie.Hit(Str + _spellstr);
            if (_enemie.IsDead)
            {
                _kill++;
                _exp++;
                _hp = _fullhp;
                _mp = _fullmp;
            }
            if (_cooldown > 0) _cooldown--;
            else _spellagl = _spellstr = 0;
            return true;
        }

        public virtual bool Spell(BasicAtributs Enemie = null)
        {
            if (Mp < 30)
            {
                _history.History.Add("Not enough mana");
                return false;
            }
            if (_cooldown > 0)
            {
                _history.History.Add($"Cool-down is {_cooldown}");
                return false;
            }
            switch ((HeroType)Type)
            {
                case (HeroType.Elpf):
                    _cooldown = 3;
                    _history.History.Add($"{_name} use concentration spell");
                    _spellagl = 20 + _int;
                    _mp -= 30;
                    break;
                case (HeroType.Orc):
                    _cooldown = 3;
                    _history.History.Add($"{_name} use berserk spell");
                    _spellstr = 20 + _str;
                    _mp -= 30;
                    break;
                case (HeroType.Human):
                    _history.History.Add($"{_name} cast fireball to {Enemie._name}");
                    if (!Enemie.Hit(Int * 3, true))
                    {
                        _history.History.Add($"{Enemie._name} is already dead");
                        return false;
                    }
                    _mp -= 30;
                    break;
            }
            return true;
        }
        public virtual bool Hit(int dmg, bool magic = false)
        {
            if (_isdead) return false;
            if (!magic)
            {
                if (_rnd.Next(0, 100) > _agl + _spellagl)
                {
                    _hp -= (int)((double)dmg * ((100.0 - (double)((_str + _spellstr) * (_agl + _spellagl)) / 2.0) / 100.0));
                    _history.History.Add($"{_name} received {(int)((double)dmg * ((100.0 - (double)((_str + _spellstr) + (_agl + _spellagl)) / 2.0) / 100.0))} dmg");
                }
                else _history.History.Add("Missed");
            }
            else
            {
                _hp -= dmg;
                _history.History.Add($"{_name} received {dmg} dmg");
            }
            _isdead = _hp <= 0;
            if (_isdead)
            {
                _history.History.Add($"{_name} died");
                _hp = 0;
                RemoveFromMap();
            }
            return true;
        }

        public bool LvLUp(int x)
        {
            if (Exp <= 0) return false;
            switch (x)
            {
                case 0:
                    _fullhp += 20;
                    break;

                case 1:
                    _fullmp += 10;
                    break;

                case 2:
                    _str += 5;
                    break;

                case 3:
                    _int += 5;
                    break;

                case 4:
                    _agl += 5;
                    break;

                default: return false;
            }
            _exp--;
            return true;
        }

        public void RemoveFromMap()
        {
            GameMap map = GameMap.GetInstance();
            map.Remove(this);
        }

        public virtual void Go()
        {
            if (IsDead) return;
            GameMap _map = GameMap.GetInstance();
            HeroList _heroes = HeroList.GetInstance();
            Random rnd = new Random();
            int imin = 0;
            int min = _map.Size;
            while (Exp > 0) LvLUp(rnd.Next(0, 3));
            for (int i = 0; i < _heroes.Count; i++)
            {
                if (_heroes[i].IsDead) continue;
                if (Position - _heroes[i].Position == 0) continue;
                if (Math.Abs(Position - _heroes[i].Position) < min)
                {
                    imin = i;
                    min = Math.Abs(Position - _heroes[i].Position);
                }
            }
            if (min == 1)
                if (_type != HeroType.Human || (_type == HeroType.Human && Mp < 30))
                {
                    if (_cooldown == 0 && Mp >= 30)
                    {
                        Spell();
                        _cooldown++;
                    }
                    Attack(_heroes[imin]);
                }
                else Spell(_heroes[imin]);
            else Move(_heroes[imin].Position);
            string str = "";
        }
    }
}