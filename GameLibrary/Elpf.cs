namespace GameLibrary
{
    public class Elpf : BasicAtributs
    {
        private static int ind = 0;
        public Elpf()
        {
            base.Naming("Elpf" + ind.ToString());
            ind++;
            base._agl = 20;
            base._type = HeroType.Elpf;
        }
        public Elpf(string Name)
        {
            base.Naming(Name);
            base._agl = 20;
            base._type = HeroType.Elpf;
        }
    }
}