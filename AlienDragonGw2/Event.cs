using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienDragonGw2
{
    class Event
    {
        string nameEvent;
        String idEvent;
        String typeEvent;
        String colorEvent;
        string statusEvent;

        public Event(String name ,String id , String type , String color )
        {
            this.nameEvent = name;
            this.idEvent = id;
            this.typeEvent = type;
            this.colorEvent = color;

        }

       
        public override string ToString()
        {
            return this.nameEvent+" ID = " + this.idEvent + "  Type = " + this.typeEvent + " Color = " + this.colorEvent;
        }

    }
}
