
using AlienDragonGw2;
using System;
using System.Collections.Generic;
using System.Threading;

class BgThread
{
    

    public BgThread()
    {

    }

    public void RunLoop()
    {
        String threadName = Thread.CurrentThread.Name;
        
        while(true)
        {
            Thread.Sleep(2000);
            Console.WriteLine(" Test Thread");
           // this.listEvent:
        }

    }
}
