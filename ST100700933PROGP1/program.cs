using System;
using System.Collections.Generic;

namespace ST100700933PROGP1
{
    class program    // this code sets up the entry point for the console application
    {    
        static void Main(string[] args)
        {
            UI ui = new UI();
            ui.Run();
        }
    }
}