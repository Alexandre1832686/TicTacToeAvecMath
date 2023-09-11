namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int[,] test = new int[3, 3];
            test[0, 0] = 1;
            test[1, 0] = 1;
            test[0, 2] = 0;
            test[1, 1] = 0;

            Controller.Tableau = test;

            if (Controller.Tableau == test)
            {
                Assert.Fail();
            }
        }
    }
}