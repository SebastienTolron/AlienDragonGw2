
using AlienDragonGw2;
using System;
using System.Collections.Generic;
using System.Threading;

class BgThread
{
    private List<Event> m_listEvent = null;
    public Mutex m_lock = new Mutex();

    public BgThread(List<Event> listEvent)
    {
        m_listEvent = listEvent;
    }

    public void RunLoop()
    {
        String threadName = Thread.CurrentThread.Name;
        
        while(true)
        {
            Thread.Sleep(2000);
            Console.WriteLine(" Test Thread");

            m_lock.WaitOne();
            
                // this.listEvent:

            
            m_lock.ReleaseMutex();
           
        }

    }
}
