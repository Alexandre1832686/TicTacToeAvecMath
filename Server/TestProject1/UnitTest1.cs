namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private void CopyTableDansController()
        {
            int[,] test = new int[3, 3];
            test[0, 0] = 1;
            test[1, 0] = 1;
            test[2, 0] = 1;
            test[0, 2] = 2;
            test[0, 1] = 2;
            test[1, 1] = 2;
            test[2, 2] = 1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Controller.Tableau[i, j] = test[i, j];
                }
            }
        }


        [TestMethod]
        public void TestValidation1()
        {
            CopyTableDansController();
            if (!Controller.ValiderCoupDispo(1, 2))
            {
                Assert.Fail();
            }
            if (Controller.ValiderCoupDispo(0, 0))
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestValidation2()
        {
            CopyTableDansController();
            if (!Controller.ValiderCoupLegal(1, 2))
            {
                Assert.Fail();
            }
            if (Controller.ValiderCoupLegal(-1, 0))
            {
                Assert.Fail();
            }
            if (Controller.ValiderCoupLegal(3, 0))
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestValidation3()
        {
            CopyTableDansController();
            if (!Controller.ValiderCoupNombreDeTourCorrect(2))
            {
                Assert.Fail();
            }
            if (Controller.ValiderCoupNombreDeTourCorrect(1))
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestJouerCoup()
        {
            CopyTableDansController();
            if (Controller.JouerCoup(1, 2, 2))
            {
                if (Controller.Tableau[1, 2] == 2)
                {
                    //ok!
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }

            if (Controller.JouerCoup(1, 2, 1))
            {
                Assert.Fail();
            }

            if (!Controller.JouerCoup(2, 1, 2))
            {
                Assert.Fail();
            }

        }
    }
}