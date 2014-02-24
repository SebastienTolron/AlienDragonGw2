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
using System.ComponentModel;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AlienDragonGw2
{


    class Program
    {
        private static BackgroundWorker _bw = new BackgroundWorker();
        private static List<Event> m_listEvent = new List<Event>();

        static int Main(string[] args)
        {


            // Get Data from gw2 APi

            // Event details https://api.guildwars2.com/v1/event_details.json?event_id=598CF3BD-0DF4-4FC7-97EA-AB4515497F5E&lang=fr
            // Event Active or Inactive  https://api.guildwars2.com/v1/events.json?world_id=1001&event_id=03BF176A-D59F-49CA-A311-39FC6F533F2F

            Console.WriteLine("Loading Gw2 datas....");
            Thread.Sleep(2000);
            // On rempli la list des evenements de gw2 avec les parametre de l'application

            Hashtable sectionDragon = (Hashtable)ConfigurationManager.GetSection("DragonSettings/Id");
            Hashtable sectionColor = (Hashtable)ConfigurationManager.GetSection("DragonSettings/Color");
            foreach (DictionaryEntry d in sectionDragon)
            {
                String type = "";
                string eventName = (string)d.Key;
                string ColorEvent = (string)sectionColor[eventName];

                // Defini le type de l'event 
                if (eventName.Contains("Pre"))
                {

                    type = "Pre";
                }
                else if (eventName.Contains("Main"))
                {

                    type = "Main";
                }

                // ajoute l'event à la liste 
                Event temp = new Event((string)d.Key, (string)d.Value, type, ColorEvent);
                m_listEvent.Add(temp);


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

            _bw.DoWork += bw_DoWork;
            _bw.RunWorkerAsync();

            // Pour allumer le clavier , il y aura une boucle infini qui va vérifier toutes les 5 secondes le status des evenements et , 
            // en fonction des event va allumer le clavier .

            while (true)
            {
                Thread.Sleep(3000);
                Console.WriteLine("\n ---------- Affichage de la Liste ---------");
                for (int i = 0; i < m_listEvent.Count; i++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine(m_listEvent.ElementAt(i)+"\n");
                }
                // on parcourt la liste ;

                // lorsqu'on trouve un event = true ;

                // En fonction du compteur de l'event :

                // En fonction du type d'event = " contains ('pre') " ;
                // Si le compteur = 0 on applique la fonction de coloriage ( Pre event ou event Start ) puis on met à 1 

                // Si le compteur = 1 on ne fait rien 

                // Si le  type d'event = " contains Finish"

                // On remet le compteur a 0


            }

            return 1;
        }


        // Methode qui  met à jour la liste des events;
        static void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                for (int i = 0; i < m_listEvent.Count; i++)
                {

                    Thread.Sleep(2000);                   
                    WebClient webClient = new WebClient();
                    var json = webClient.DownloadString("https://api.guildwars2.com/v1/events.json?world_id=2102&event_id=" + m_listEvent.ElementAt(i).IdEvent);

                    JObject o = JObject.Parse(json);
                    dynamic _eventG = JsonConvert.DeserializeObject(json);
                    string temp = Convert.ToString(_eventG.events);

                    dynamic _eventFinal = JsonConvert.DeserializeObject(temp.Trim( new Char[] { '{', '\n', '\r','[',']' } ));

                    m_listEvent.ElementAt(i).StatusEvent = _eventFinal.state;
                      
                }

            }
        }
    }

}
