using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using XBoxController;

namespace TopSideComputation
{
    class TopSideComputation
    {

        static void Main(string[] args)
        {
            RovController ROV = new RovController();
            ROV.StartSendingData();

            // The application will never stop
            while (true) { }

        }
    }

    class RovController
    {
        private Timer timer;

        private RockCandyController XBoxController;
        private GamePadProcessor InputProcessor;
        private PostProcessor DataProcessor;
        private ArduinoCommunication RovComms;

        // Consturctor
        public RovController()
        {
            XBoxController = new RockCandyController();
            InputProcessor = new GamePadProcessor();
            DataProcessor = new PostProcessor();
            RovComms = new ArduinoCommunication();
        }

        public void StartSendingData()
        {
            // Create a timer that will call a function every so often
            timer = new Timer(ConstantValues.RovUpdateRate);
            timer.AutoReset = true; // Make the timer happen forever
            timer.Elapsed += new ElapsedEventHandler(CalculateValues);
            timer.Start(); // Start the timer
        }

        private void CalculateValues(object sender, EventArgs e)
        {
            Console.WriteLine(DateTime.Now);

            // Receive the data from the Arduino
            DataFromArduino inputData = RovComms.GetDataFromArduino();

            // Get XBox controller Values
            XBoxController.Update();

            // Pass XBox controller values to the GamePadMapper
            InputProcessor.ProcessData(XBoxController);

            // Pass the output of the GamePadMapper to the PosProcessor
            DataToArduino outputData = DataProcessor.Process(InputProcessor, inputData);

            // Send the necessary data to the Arduino
            RovComms.SendDataToArduino(outputData);
        }
    }
}
