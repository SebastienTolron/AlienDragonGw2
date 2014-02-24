using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlienDragonGw2
{
    class Event
    {

        private String _nameEvent;
        public String NameEvent
        {
            get { return _nameEvent; }
            set { _nameEvent = value; }
        }


        private String _idEvent;
        public String IdEvent
        {
            get { return _idEvent; }
            set { _idEvent = value; }
        }
        private String _typeEvent;
        public String TypeEvent
        {
            get { return _typeEvent; }
            set { _typeEvent = value; }
        }
        private String _colorEvent;
        public String ColorEvent
        {
            get { return _colorEvent; }
            set { _colorEvent = value; }
        }
        private String _statusEvent;
        public String StatusEvent
        {
            get { return _statusEvent; }
            set { _statusEvent = value; }
        }

        public Event(String name, String id, String type, String color)
        {
            this._nameEvent = name;
            this._idEvent = id;
            this._typeEvent = type;
            this._colorEvent = color;

        }


        public override string ToString()
        {
            return this._nameEvent + " ID = " + this._idEvent + "  Type = " + this._typeEvent + " Color = " + this._colorEvent;
        }

    }
}
