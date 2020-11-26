using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace digiview.iOS {
    public class Application {

        static void Main(string[] args) {
            try {
                UIApplication.Main(args, null, "AppDelegate");
            } catch (Exception ex) {
                Console.WriteLine("Domain Exception from  {0}", ex.ToString());
            }
        }

    }
}
