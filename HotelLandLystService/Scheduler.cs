using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Timers;

namespace HotelLandLystService
{
    public partial class Scheduler : ServiceBase
    {
        Timer timer = null;
        SqlExecuter executer = null;

        public Scheduler()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            executer = new SqlExecuter();
            this.timer.Interval = 86400000;
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Library.CheckForOldReservations(executer);
        }

        protected override void OnStop()
        {
            timer.Enabled = false;
        }
    }
}
