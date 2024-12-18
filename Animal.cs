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

    internal abstract class Animal
    {
        public bool IsAlive { get;  set; } = true;
        protected HungerLevel _hunger = HungerLevel.Feedup;
        public delegate void AccountHandler(string message);
        public event AccountHandler Notify;
        protected bool _harvestToday = false; // Признак того что сегодня собирали урожай




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
                case HungerLevel.StrongHunger: IsAlive = false; Notify?.Invoke("Животное умерло"); break;
            }
        }

        public virtual void NewDay()
        {
            _harvestToday = false;
        }

        public virtual void EndDay(out bool isDead)
        {
            HungerCalc();
            isDead = !IsAlive;
        }

        public abstract int CollectHarvest();
    }


}



