using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using LightFX;
using System.Collections;
using System.Threading;

namespace AlienDragonGw2
{


    class Program
    {


        static int Main(string[] args)
        {


            // Get Data from gw2 APi

            // Event details https://api.guildwars2.com/v1/event_details.json?event_id=598CF3BD-0DF4-4FC7-97EA-AB4515497F5E&lang=fr
            // Event Active or Inactive  https://api.guildwars2.com/v1/events.json?world_id=1001&event_id=03BF176A-D59F-49CA-A311-39FC6F533F2F

            Console.WriteLine("Loading Gw2 datas....");
            Thread.Sleep(2000);
            // On rempli la list des evenements de gw2 avec les parametre de l'application
            List<Event> listEvent = new List<Event>();
            Hashtable sectionDragon = (Hashtable)ConfigurationManager.GetSection("DragonSettings/Id");
            Hashtable sectionColor = (Hashtable)ConfigurationManager.GetSection("DragonSettings/Color");
            foreach (DictionaryEntry d in sectionDragon)
            {
                string type = "";
                string eventName = (string)d.Key;
                string ColorEvent = (string)sectionColor[eventName];

                // Defini le type de l'event 
                if (eventName.Contains("Pre"))
                {
                    Console.WriteLine("Pre Event {0]", d.Value);
                    type = "Pre";
                }
                else if (eventName.Contains("Main"))
                {
                    Console.WriteLine("Main Event {0]", d.Value);
                    type = "Main";
                }

                // ajoute l'event à la liste 
                Event temp = new Event((string)d.Key, (string)d.Value, type, ColorEvent);
                listEvent.Add(temp);

            }


            Console.WriteLine("\nLoading AlienWare libraries....");
            var lightFX = new LightFXController();
            var result = lightFX.LFX_Initialize();
            if (result == LFX_Result.LFX_Success)
            {
                Thread.Sleep(2000);
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


            Console.WriteLine("\nStarting Main Program");


            // Un thread va tourner pour remplir les valeur True ou False en fonction de si l'event est active ou pas ;
            // Le thread devrai tournée h24 pour que les valeur soit toujour a jour ;
            
             BgThread threadDragon = new BgThread(listEvent);
             Thread bgThread = new Thread(new ThreadStart(threadDragon.RunLoop));
             bgThread.IsBackground = true;
            


            // Pour allumer le clavier , il y aura une boucle infini qui va vérifier toutes les 5 secondes le status des evenements et , 
            // en fonction des event va allumer le clavier .
            while (true)
            {

                Thread.Sleep(5000);
                int i = 0;

                threadDragon.m_lock.WaitOne();
                
                for (i = 0; i < listEvent.Count; i++)
                {
                    Console.WriteLine("Event : " + listEvent.ElementAt(i).ToString());
                }

                threadDragon.m_lock.ReleaseMutex();

                // on parcourt la liste ;

                // lorsqu'on trouve un event = true ;

                // En fonction du compteur de l'event :
                // En fonction du type d'event = " contains ('pre') " ;
                // Si le compteur = 0 on applique la fonction de coloriage ( Pre event ou event Start ) puis on met à 1 

                // Si le compteur = 1 on ne fait rien 

                // Si le  type d'event = " contains Finish"

                // On remet le compteur a 0


            }


            //Console.WriteLine(json);
            /*
            Console.WriteLine("Loading AlienWare libraries....");
           // var lightFX = new LightFXController();
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
            */
            return 1;
        }
    }
}
