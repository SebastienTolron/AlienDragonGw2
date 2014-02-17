using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using LightFX;
using System.Collections;

namespace AlienDragonGw2
{
    class Program
    {
        static int Main(string[] args)
        {


            // Get Data from gw2 APi

            // Event details https://api.guildwars2.com/v1/event_details.json?event_id=598CF3BD-0DF4-4FC7-97EA-AB4515497F5E&lang=fr
            // Event Active or Inactive  https://api.guildwars2.com/v1/events.json?world_id=1001&event_id=03BF176A-D59F-49CA-A311-39FC6F533F2F

            // Test :
                 // Fort ranik ID : 2102
                // Lang = fr 2102

            Console.WriteLine("Loading Gw2 datas....");
            // Print all events status
            Hashtable section = (Hashtable)ConfigurationManager.GetSection("DragonSettings/Id");
            foreach (DictionaryEntry d in section)
            {

                string worldId = "2102";
                string lang = "fr";
                try
                {
                    var json = new WebClient().DownloadString("https://api.guildwars2.com/v1/events.json?world_id=" + worldId + "&event_id=" + d.Value + "&lang=" + lang);
                    Console.WriteLine(json);
                    // Console.WriteLine("{0} ; {1}", d.Key, statusEvent);
                }
                catch (Exception e)
                    {
                        
                        Console.WriteLine("There was an error trying to get gw2 datas.");
                        Console.WriteLine("Done.\r\rPress ENTER key to finish ...");
                        Console.ReadLine();
                        return 0;

                    }
            }




            string ColorShatterer = ConfigurationManager.AppSettings["ColorShatterer"];
            string ColorJormag = ConfigurationManager.AppSettings["ColorJormag"];
            Console.WriteLine("Shatterer {0} ", ColorShatterer);
            Console.WriteLine("Jormag {0} ", ColorJormag);

            //Console.WriteLine(json);
            Console.WriteLine("Loading AlienWare libraries....");
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

                lightFX.LFX_Release();


                // When the program end , the original lighting configuration is restoreed



            }
            else
            {
                switch (result)
                {
                    case LFX_Result.LFX_Error_NoDevs:
                        Console.WriteLine("There is not AlienFX device available.");
                        Console.WriteLine("Done.\r\rPress ENTER key to finish ...");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("There was an error initializing the AlienFX device.");
                        Console.WriteLine("Done.\r\rPress ENTER key to finish ...");
                        Console.ReadLine();
                        break;
                }
            }

            return 1;
        }
    }
}
