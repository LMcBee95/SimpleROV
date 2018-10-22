using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.XInput;
using System.Drawing;
using System.Threading;


// How to embed all .dll files into the main executable
// https://stackoverflow.com/questions/33695753/not-able-to-get-costura-fody-to-work-keeps-asking-for-the-dll

namespace XBoxController
{
    class XBoxController
    {
        static void Main(string[] args)
        {
            RockCandyController thisController = new RockCandyController();

            while (true)
            {
                Thread.Sleep(100);
                thisController.Update();
                Console.Clear();
                thisController.PrintValues();
            }
        }
    }


    class RockCandyController
    {
        Controller controller;
        Gamepad gamepad;
        public bool connected = false;
        public int deadband = 2500;
        public Point leftThumb, rightThumb = new Point(0, 0);
        public float leftTrigger = 0; 
        public float rightTrigger = 0;

        public GamepadButtonFlags ButtonFlags = new GamepadButtonFlags();

        public bool DPadUp = false;
        public bool DPadDown = false;
        public bool DPadLeft = false;
        public bool DPadRight = false;
        public bool AButton = false;
        public bool BButton = false;
        public bool XButton = false;
        public bool YButton = false;
        public bool LeftShoulder = false;
        public bool RightShoulder = false;
        public bool StartButton = false;
        public bool BackButton = false;

        public RockCandyController()
        {
            controller = new Controller(UserIndex.One);
            connected = controller.IsConnected;
        }

        public void PrintValues()
        {
            Console.WriteLine($"Left Thumb - x = {leftThumb.x} : y = {leftThumb.y}");
            Console.WriteLine($"Right Thumb - x = {rightThumb.x} : y = {rightThumb.y}");
            Console.WriteLine($"Triggers - Left = {leftTrigger} : Right = {rightTrigger}");
            Console.WriteLine($"DPad - Left = {DPadLeft} : Right = {DPadRight} : Up = {DPadUp} : Down = {DPadDown}");
            Console.WriteLine($"Buttons 1 - A = {AButton} : B = {BButton} : X = {XButton} : Y = {YButton}");
            Console.WriteLine($"Buttons 2 - Start = {StartButton} : Back = {BackButton}");
            Console.WriteLine($"Buttons 3 - Left Shoulder = {LeftShoulder} : Right Shoulder = {RightShoulder}");

        }

        // Call this method to update all class values
        public void Update()
        {
            if (!connected)
                return;

            gamepad = controller.GetState().Gamepad;

            leftThumb.x = (Math.Abs((float)gamepad.LeftThumbX) < deadband) ? 0 : (float)gamepad.LeftThumbX / short.MinValue * -100;
            leftThumb.y = (Math.Abs((float)gamepad.LeftThumbY) < deadband) ? 0 : (float)gamepad.LeftThumbY / short.MaxValue * 100;
            rightThumb.x = (Math.Abs((float)gamepad.RightThumbX) < deadband) ? 0 : (float)gamepad.RightThumbX / short.MaxValue * 100;
            rightThumb.y = (Math.Abs((float)gamepad.RightThumbY) < deadband) ? 0 : (float)gamepad.RightThumbY / short.MaxValue * 100;

            leftTrigger = gamepad.LeftTrigger;
            rightTrigger = gamepad.RightTrigger;

            ButtonFlags = gamepad.Buttons;

            DPadUp = ((ushort)ButtonFlags & (ushort)GamepadButtonFlags.DPadUp) != 0;
            DPadDown = ((ushort)ButtonFlags & (ushort)GamepadButtonFlags.DPadDown) != 0;
            DPadLeft = ((ushort)ButtonFlags & (ushort)GamepadButtonFlags.DPadLeft) != 0;
            DPadRight = ((ushort)ButtonFlags & (ushort)GamepadButtonFlags.DPadRight) != 0;
            AButton = ((ushort)ButtonFlags & (ushort)GamepadButtonFlags.A) != 0;
            BButton = ((ushort)ButtonFlags & (ushort)GamepadButtonFlags.B) != 0;
            XButton = ((ushort)ButtonFlags & (ushort)GamepadButtonFlags.X) != 0;
            YButton = ((ushort)ButtonFlags & (ushort)GamepadButtonFlags.Y) != 0;
            LeftShoulder = ((ushort)ButtonFlags & (ushort)GamepadButtonFlags.LeftShoulder) != 0;
            RightShoulder = ((ushort)ButtonFlags & (ushort)GamepadButtonFlags.RightShoulder) != 0;
            StartButton = ((ushort)ButtonFlags & (ushort)GamepadButtonFlags.Start) != 0;
            BackButton = ((ushort)ButtonFlags & (ushort)GamepadButtonFlags.Back) != 0;
        }

        public struct Point
        {
            public Point(float X, float Y) : this()
            {
                x = X;
                y = Y;
            }

            public float x { get; set; }
            public float y { get; set; }
        }
    }
}


