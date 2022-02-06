// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;
using ROOT.CIMV2.Win32;

namespace ConsoleApp2cs

{
    class Program
    {
        static void Main(string[] args)
        {
            var usbController = USBControllerDevice.GetInstances().Cast<USBControllerDevice>();
            var pnpGeräte = PnPEntity.GetInstances().Cast<PnPEntity>().
              Where(p => usbController.Any(uc => uc.Dependent.RelativePath.
                Split('\"')[1].Replace(@"\\", @"\") == p.DeviceID) && p.Status == "OK");
            Anzeige(pnpGeräte); Console.ReadLine();
        }

        private static void Anzeige(IEnumerable<PnPEntity> pnpGeräte)
        {
            foreach (var gerät in pnpGeräte)
                Console.WriteLine("\r\nCaption: " + gerät.Caption + "\r\n" +
                  "    DeviceID: " + gerät.DeviceID + "\r\n" +
                  "    Beschreibung: " + gerät.Description);
        }
    }
}
