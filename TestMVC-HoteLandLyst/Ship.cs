using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVC_HoteLandLyst
{
    public abstract class Ship
    {
        private string name;

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        private int length;

        public int Length
        {
            get { return length; }
            private set { length = value; }
        }


        //! Du skal have en konstruktør med de her parametre hvis du arver fra den abstrakte klasse
        public Ship(string name, int length)
        {
            Name = name;
            Length = length;
        }

    }

    public class Megaplane : Ship
    {
        private string weapon;

        public string Weapon
        {
            get { return weapon; }
            set { weapon = value; }
        }

        //! ved at refere til base ved Megaplane nu hvordan den skal gøre med name parameteren
        public Megaplane(string name) : base(name)
        {
        }

        //!Denne konstruktør kalder den anden konstruktør, som kalder til sine base
        public Megaplane(string name, string weapon) : this(name)
        {
            Weapon = weapon;
        }
    }
}
