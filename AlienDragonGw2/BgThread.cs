
using AlienDragonGw2;
using System;
using System.Collections.Generic;
using System.Threading;

class BgThread
{
    List<Event> listEvent;

    public BgThread(List<Event> listEvent)
    {
        
    }

    public void RunLoop()
    {
        String threadName = Thread.CurrentThread.Name;
        
        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine("{0} count: {1}",
                threadName, i.ToString());
            System.Threading.Thread.Sleep(1500);
        }
        Console.WriteLine("{0} finished counting.", threadName);
    }
}
