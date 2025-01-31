using Atmosphere_prj;
using System.Reflection.Emit;
using TextFile;
namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLayerCountException()
        {
            //Testing Exception. 
            string filename = "input1.txt";
            TextFileReader reader = new TextFileReader(filename);

            // Assert that the constructor of Atmosphere throws LayerCountZero exception
            Assert.ThrowsException<Atmosphere.LayerCountZero>(() => new Atmosphere(ref reader));

        }
        [TestMethod]
        public void OneLayerTestingSingleSimulate()
        {
            string filename = "input2.txt";
            TextFileReader reader = new TextFileReader(filename);
            Atmosphere atmosphere = new Atmosphere(ref reader);
            string expOut1 = "Simulation Round 0:\nZ 5\n";
            Assert.AreEqual(expOut1, atmosphere.getState(0));
            //When we Simulate first time at Ozone its 5% thickness decreases.
            string expOut2 = "Simulation Round 1:\nZ 4.75\n";
            atmosphere.simulate();

            //Testing on max all Layers that all layers simulate and end result
            Assert.AreEqual(expOut2,atmosphere.getState(atmosphere.getLayersCount));
            
        }
        [TestMethod]
        public void OneLayeronZeroATMVTesting()
        {
            string filename = "input3.txt";
            TextFileReader reader = new TextFileReader(filename);
            Atmosphere atmosphere = new Atmosphere(ref reader);
            string expOut1 = "Simulation Round 0:\nC 5\n";
            Assert.AreEqual(expOut1, atmosphere.getState(0));

            //After First Simulation.
            atmosphere.simulate();
            string expOut2 = "Simulation Round 1:\nC 5\n";
            //Because Others Donot effect the C.
            Assert.AreEqual(expOut2, atmosphere.getState(atmosphere.getLayersCount));
        }

        [TestMethod]
        public void OneLayerTestingOverMain()
        {
            //But if we try to run it on our main program then it will not simulate because layers are less then 3
            
            string filename = "input2.txt";
            TextFileReader reader = new TextFileReader(filename);

            Atmosphere atmosphere = new Atmosphere(ref reader);

            string expOut1 = "Simulation Round 1:\nZ 5\n";
            int initialLayers = atmosphere.getLayersCount;
            for (int i = 1; atmosphere.getLayersCount >= 3 && atmosphere.getLayersCount != 3 * initialLayers; i++)
            {
                atmosphere.simulate();
            }
            //Testing on max all Layers that all layers simulate and end result
            Assert.AreEqual(expOut1, atmosphere.getState(atmosphere.getLayersCount));
        }

        [TestMethod]
        public void MultiLayerTesting()
        {
            string filename = "input.txt";
            TextFileReader reader = new TextFileReader(filename);
            Atmosphere atmosphere = new Atmosphere(ref reader);
            //Initial Input
            string expOut1 = "Simulation Round 0:\nZ 5\nX 0.8\nC 3\nX 4\n";
            Assert.AreEqual(expOut1, atmosphere.getState(0));

            //After First Simulation on 4 gases.

            atmosphere.simulate();
            string expOut2 = "Simulation Round 4:\nZ 4.75\nX 0.72\nC 3.4\nX 3.6\n";
            //Testing on max all Layers that all layers simulate and end result
            Assert.AreEqual(expOut2, atmosphere.getState(atmosphere.getLayersCount));

        }
        [TestMethod]
        public void MultiLayerTestingOverMain()
        {            
            string filename = "input.txt";
            TextFileReader reader = new TextFileReader(filename);

            Atmosphere atmosphere = new Atmosphere(ref reader);

            //At Last Only 2 Layers Left.
            string expOut1 = "Simulation Round 2:\nZ 4.468179837507224\nC 3.8377220614965553\n";
            int initialLayers = atmosphere.getLayersCount;
            for (int i = 1; atmosphere.getLayersCount >= 3 && atmosphere.getLayersCount != 3 * initialLayers; i++)
            {
                atmosphere.simulate();
            }

            Assert.AreEqual(expOut1, atmosphere.getState(atmosphere.getLayersCount));
        }

        [TestMethod]
        public void TestingSimulateWhenSimulationPerformed() 
        {
            string filename = "input.txt";
            TextFileReader reader = new TextFileReader(filename);

            Atmosphere atmosphere = new Atmosphere(ref reader);

            //Getting the State at 0 position
            string expOut1 = atmosphere.getState(0);

            atmosphere.simulate();


            //�hecking that state should be not equal to previous state after simulating
            Assert.AreNotEqual(expOut1, atmosphere.getState(0));

        }
        [TestMethod]
        public void TestSimulateWhenNoSimulationPerformed()
        {
            string filename = "input2.txt";
            TextFileReader reader = new TextFileReader(filename);
            Atmosphere atmosphere = new Atmosphere(ref reader);

            string initialState = atmosphere.getState(0);
            // No simulation performed
            string newState = atmosphere.getState(0);

            // Check that the state remains the same when no Simulation is Performed
            Assert.AreEqual(initialState, newState);
        }

        public void TestingGetStateFunctionOnInitialInput()
        {

            string filename = "input2.txt";
            TextFileReader reader = new TextFileReader(filename);
            Atmosphere atmosphere = new Atmosphere(ref reader);
            string expOut1 = "Simulation Round 0:\nZ 5\n";


            //Checking that get State, giving the output Correct or not.
            Assert.AreEqual(expOut1, atmosphere.getState(0));

        }
        [TestMethod]
        public void TestingGetStateFunctionWithMultipleLayers()
        {
            string filename = "input.txt";
            TextFileReader reader = new TextFileReader(filename);
            Atmosphere atmosphere = new Atmosphere(ref reader);

            // Expected output after the initial Simulation
            string expOut1 = "Simulation Round 0:\nZ 5\nX 0.8\nC 3\nX 4\n";
            Assert.AreEqual(expOut1, atmosphere.getState(0));
            //Runnign the Loop
            for (int i = 1; i <= 4; i++)
            {
                atmosphere.simulate();
            }

            // Expected output after multiple simulations
            string expOut2 = "Simulation Round 4:\nZ 4.07253125\nX 0.52488\nC 4.3755999999999995\nX 2.6244000000000005\n";
            Assert.AreEqual(expOut2, atmosphere.getState(atmosphere.getLayersCount));
        }

    }
}
