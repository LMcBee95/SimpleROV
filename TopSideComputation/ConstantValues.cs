using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopSideComputation
{

    // This class will contain the important constant values so that they can be easily found
    public static class ConstantValues
    {
        // The update rate dictates how often the ROV will send a packet to the Arduino
        // The value is the time in milli seconds between each transmition
        public static int RovUpdateRate => 1000;
    }
}
