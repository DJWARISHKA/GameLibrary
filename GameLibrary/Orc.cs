namespace GameLibrary
{
    internal class Orc : BasicAtributs
    {
        private static int ind = 0;

        public Orc()
        {
            base.Naming("Orc" + ind.ToString());
            ind++;
            base._str = 20;
            base._type = HeroType.Orc;
        }
        public Orc(string Name)
        {
            base.Naming(Name);
            base._str = 20;
            base._type = HeroType.Orc;
        }
    }
}