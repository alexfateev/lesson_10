﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pack1;

namespace lesson_10
{

    enum HungerLevel
    {
        Feedup,
        LightHunger,
        MiddleHunger,
        StrongHunger
    }

    internal class Animal
    {
        public bool IsAlive { get;  set; } = true;
        private protected HungerLevel _hunger = HungerLevel.Feedup;

        public void Feedup()
        {
            _hunger = HungerLevel.Feedup;
        }

        protected void HungerCalc()
        {
            switch (_hunger)
            {
                case HungerLevel.Feedup: _hunger = HungerLevel.LightHunger; break;
                case HungerLevel.LightHunger: _hunger = HungerLevel.MiddleHunger; break;
                case HungerLevel.MiddleHunger: _hunger = HungerLevel.StrongHunger; break;
                case HungerLevel.StrongHunger: IsAlive = false; break;
            }
        }

        public virtual void NewDay(out bool isDead)
        {
            HungerCalc();
            isDead = !IsAlive;
        }
    }


}


