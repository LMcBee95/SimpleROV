using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopSideComputation
{
    class PostProcessor
    {
        // Constructor
        public PostProcessor()
        {

        }

        public DataToArduino Process(GamePadProcessor processedData, DataFromArduino inputData)
        {
            DataToArduino outputData = new DataToArduino();

            // Process the data here

            return outputData;
        }
    }
}
