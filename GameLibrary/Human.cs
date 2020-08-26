namespace GameLibrary
{
    public class Human : BasicAtributs
    {
        private static int ind = 0;

        public Human()
        {
            base.Naming("Human" + ind.ToString());
            ind++;
            base._int = 20;
            base._fullmp = base._mp = 60;
            base._type = HeroType.Human;
        }
        public Human(string Name)
        {
            base.Naming(Name);
            base._int = 20;
            base._fullmp = base._mp = 60;
            base._type = HeroType.Human;
        }
    }
}