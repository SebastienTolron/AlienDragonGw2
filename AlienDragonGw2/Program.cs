using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using LightFX;

namespace AlienDragonGw2
{
    class Program
    {
        static void Main(string[] args)
        {

            // Get Data from gw2 APi
            var json = new WebClient().DownloadString("https://api.guildwars2.com/v1/events.json?world_id=1001&map_id=39");

            string ColorShatterer = ConfigurationManager.AppSettings["ColorShatterer"];
            string ColorJormag = ConfigurationManager.AppSettings["ColorJormag"];
            Console.WriteLine("Shatterer {0} ", ColorShatterer);
            Console.WriteLine("Jormag {0} ", ColorJormag);
            
            //Console.WriteLine(json);
            var lightFX = new LightFXController();

            var result = lightFX.LFX_Initialize();
            if (result == LFX_Result.LFX_Success)
            {

                lightFX.LFX_Reset();

                // Light the keyboard
                var primaryColor = new LFX_ColorStruct(255, 0, 255, 0);
                lightFX.LFX_Light(LFX_Position.LFX_All, primaryColor);
                lightFX.LFX_Update();


                Console.WriteLine("Done.\r\rPress ENTER key to finish ...");
                Console.ReadLine();


                // When the program end , the original lighting configuration is restoreed
                lightFX.LFX_Release();
            }
            else
            {
                switch (result)
                {
                    case LFX_Result.LFX_Error_NoDevs:
                        Console.WriteLine("There is not AlienFX device available.");
                        break;
                    default:
                        Console.WriteLine("There was an error initializing the AlienFX device.");
                        break;
                }
            }

        }
    }
}
