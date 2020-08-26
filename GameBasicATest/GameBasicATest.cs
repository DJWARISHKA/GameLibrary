using GameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameBasicATest
{
    [TestClass]
    public class GameBasicATest
    {
        [TestMethod]
        public void Create()
        {
            new PrivateType(typeof(Elpf)).SetStaticField("ind", 0);
            BasicAtributs atributs = new Elpf();
            Assert.AreEqual(atributs.FullHp, 100);
            Assert.AreEqual(atributs.Hp, 100);
            Assert.AreEqual(atributs.FullMp, 50);
            Assert.AreEqual(atributs.Mp, 50);
            Assert.AreEqual(atributs.Str, 10);
            Assert.AreEqual(atributs.Int, 10);
            Assert.AreEqual(atributs.Agl, 20);
            Assert.AreEqual(atributs.Kill, 0);
            Assert.AreEqual(atributs.Name, "Elpf0");
            BasicAtributs atributs1 = new Elpf("hero");
            Assert.AreEqual(atributs1.Name, "hero");

        }

        //[TestMethod]
        //public void Hit()
        //{
        //    BasicAtributs atributs = new Elpf();
        //    atributs.Hit(10);
        //    Assert.AreEqual(atributs.Hp, 90);
        //    atributs.Hit(90);
        //    Assert.AreEqual(atributs.Hp, 0);
        //    Assert.IsTrue(atributs.IsDead);
        //    atributs.Hit(90);
        //    Assert.AreEqual(atributs.Hp, 0);
        //    Assert.IsTrue(atributs.IsDead);
        //}

        //[TestMethod]
        //public void Attack()
        //{
        //    BasicAtributs hero = new Elpf();
        //    Assert.IsTrue(hero.StartPosition(2));
        //    Elpf enemie = new Elpf();
        //    Assert.IsTrue(enemie.StartPosition(1));
        //    Assert.IsTrue(hero.Attack(enemie));
        //    Assert.AreEqual(enemie.Hp, 91);
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Assert.IsTrue(hero.Attack(enemie));
        //    }
        //    Assert.AreEqual(enemie.Hp, 0);
        //    Assert.AreEqual(hero.Kill, 1);
        //    Assert.AreEqual(hero.Exp, 1);
        //    Assert.IsTrue(enemie.IsDead);
        //    Assert.IsFalse(hero.Attack(enemie));
        //}

        [TestMethod]
        public void Spell()
        {
            Human hero = new Human();
            Elpf enemie = new Elpf();
            new PrivateType(typeof(GameMap)).SetStaticField("instance", null);
            GameMap map = GameMap.GetInstance();
            map.Add(hero, 1);
            map.Add(enemie, 2);
            Assert.IsTrue(hero.Spell(enemie));
            Assert.AreEqual(enemie.Hp, 40);
            Assert.IsTrue(hero.Spell(enemie));
            Assert.IsTrue(enemie.IsDead);
        }

        [TestMethod]
        public void Exp_LvLUp()
        {
            BasicAtributs hero = new Elpf();
            Assert.IsFalse(hero.LvLUp(0));
            PrivateObject pr = new PrivateObject(hero);
            pr.SetField("_exp", 1);
            Assert.IsTrue(hero.LvLUp(0));
            Assert.AreEqual(hero.FullHp, 120);
            Assert.AreEqual(hero.Exp, 0);
        }

        [TestMethod]
        public void Move()
        {
            new PrivateType(typeof(Elpf)).SetStaticField("ind", 0);
            new PrivateType(typeof(GameMap)).SetStaticField("instance", null);
            GameMap map = GameMap.GetInstance();
            Elpf per1 = new Elpf();
            Elpf per2 = new Elpf();
            Assert.IsTrue(map.Add(per1, 13));
            Assert.IsTrue(map.Add(per2, 12));
            Assert.IsTrue(per1.Move(14));
            Assert.IsTrue(per2.Move(14));
            Assert.AreEqual(per2.Name, map[13]);
            Elpf per3 = new Elpf();
            Assert.IsTrue(map.Add(per3, 29));
            Assert.IsFalse(per3.Move(34));
        }
    }
}