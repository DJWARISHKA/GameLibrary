using GameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameBasicATest
{
    /// <summary>
    /// Summary description for MapTest
    /// </summary>
    [TestClass]
    public class MapTest
    {
        [TestMethod]
        public void ClassInitialize()
        {
            new PrivateType(typeof(Elpf)).SetStaticField("ind", 0);
            new PrivateType(typeof(GameMap)).SetStaticField("instance", null);
            GameMap map = GameMap.GetInstance();
            GameMap map1 = GameMap.GetInstance();
            Assert.AreEqual(map.Map.ToString(), "_".PadLeft(30, '_'));
            Elpf per1 = new Elpf();
            Assert.IsTrue(map.Add(per1, 20));
            Assert.AreEqual(map.Map[20], "Elpf0");
            Assert.AreEqual(map1.Map[20], "Elpf0");
            GameMap map2 = GameMap.GetInstance();
            Assert.AreEqual(map2.Map[20], "Elpf0");
        }

        [TestMethod]
        public void Add()
        {
            new PrivateType(typeof(Elpf)).SetStaticField("ind", 0);
            new PrivateType(typeof(GameMap)).SetStaticField("instance", null);
            GameMap map = GameMap.GetInstance();
            Elpf per1 = new Elpf();
            Elpf per2 = new Elpf();
            Assert.AreEqual(per1.Name, "Elpf0");
            Assert.AreEqual(per2.Name, "Elpf1");
            Assert.IsTrue(map.Add(per1, 20));
            Assert.IsFalse(map.Add(per1, 20));
            Assert.IsFalse(map.Add(per2, 20));
            Assert.IsFalse(map.Add(per1, 21));
            Assert.IsTrue(map.Add(per2, 21));
        }

        [TestMethod]
        public void Remove()
        {
            new PrivateType(typeof(Elpf)).SetStaticField("ind", 0);
            new PrivateType(typeof(GameMap)).SetStaticField("instance", null);
            GameMap map = GameMap.GetInstance();
            Elpf per1 = new Elpf();
            Assert.IsTrue(map.Add(per1, 20));
            Assert.IsTrue(map.Remove(per1));
            Assert.IsFalse(map.Remove(per1));
        }

        [TestMethod]
        public void Move()
        {
            new PrivateType(typeof(Elpf)).SetStaticField("ind", 0);
            new PrivateType(typeof(GameMap)).SetStaticField("instance", null);
            GameMap map = GameMap.GetInstance();
            Assert.IsFalse(map.Move(10, 20));
            Elpf per1 = new Elpf();
            Assert.IsTrue(map.Add(per1, 20));
            Assert.IsTrue(map.Move(per1.Position, 14));
            Assert.IsFalse(map.Move(per1.Position, 120));
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion Additional test attributes
    }
}