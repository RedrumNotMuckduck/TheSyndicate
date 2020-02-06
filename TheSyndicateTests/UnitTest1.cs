using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheSyndicate;

namespace TheSyndicateTests
{
    [TestClass]
public class UnitTest1
    {
        [TestMethod]
        public void TestSceneConstructor()
        {
            string[] arrText = {
            "You have stayed too long and encountered firmware to remove your code. Should you move to another location in the web?",
            "You don't have much time, but it is possible to download yourself to an open bot somewhere else. Is this the path you choose?"
            };
            string[] arrDestinations = { "loveOneself", "city" };
            Scene testScene = new Scene("web",
                                        "You have uploaded yourself to The Syndicate's network. This is a dangerous place for you due to the multiple firewalls in place. It is only a matter of time before your code is located and erased, so you mustn't stay long. Choose wisely.",
                                        arrText,
                                        arrDestinations,
                                        false);

            Assert.AreEqual("web", testScene.Id);
            Assert.AreEqual(2, testScene.Destinations.Length);
            Assert.AreEqual(false, testScene.Start);

        }

       

        [TestMethod]
        public void TestPlayerPopulatesIfSaveStateFileNotFound()
        {
            Player player = Player.GetInstance();
            Assert.IsTrue(player != null);
            Assert.AreEqual(null, player.CurrentSceneId);
        }

        [TestMethod]
        public void Test()
        {

            Player player = Player.GetInstance();
            player.DecrementBatteryPowerByOne();
            Assert.AreEqual(3, player.BatteryPower);

        }

        [TestMethod]
        public void TestValidInput()
        {
            string[] arrText = {
            "You have stayed too long and encountered firmware to remove your code. Should you move to another location in the web?",
            "You don't have much time, but it is possible to download yourself to an open bot somewhere else. Is this the path you choose?"
            };
            string[] arrDestinations = { "loveOneself", "city" };
            Scene testScene = new Scene("web",
                                        "You have uploaded yourself to The Syndicate's network. This is a dangerous place for you due to the multiple firewalls in place. It is only a matter of time before your code is located and erased, so you mustn't stay long. Choose wisely.",
                                        arrText,
                                        arrDestinations,
                                        false);

            Assert.IsTrue(testScene.IsValidInput(0));
            Assert.IsTrue(testScene.IsValidInput(1));
            Assert.IsTrue(testScene.IsValidInput(2));
            Assert.IsFalse(testScene.IsValidInput(-1));
            Assert.IsFalse(testScene.IsValidInput(6));
        }

    }
}
