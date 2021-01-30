using System.Device;
using System;
using System.Device.Gpio;
using System.Diagnostics;

namespace Frequency_for_1_pin
{
    class Program
    {
        static void Main(string[] args)
        {
            var gpio = new GpioController();
            gpio.OpenPin(5, PinMode.Input);

            // Warmup
            for (int idx = 0; idx < 100_000; ++idx)
                gpio.Read(5);

            // benchmark
            var stopWatch = Stopwatch.StartNew();
            var cycleCounter = 0;
            while (stopWatch.Elapsed.TotalSeconds < 5)
                for (int idx = 0; idx < 1_000_000; ++idx)
                {
                    gpio.Read(5);
                    ++cycleCounter;
                }

            Console.WriteLine($"Read Frequency: {cycleCounter / stopWatch.Elapsed.TotalSeconds / 1024 / 1024}MHz");
        }
    }
}
