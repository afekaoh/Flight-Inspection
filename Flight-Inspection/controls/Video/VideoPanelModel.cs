using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.Video
{
    class VideoPanelModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TimeSeries timeSeries;
        private int maxSlider;
        private Thread t;

        public VideoPanelModel()
        {
            t = new Thread(play);
            t.IsBackground = true;
            MaxSlider = 10;
        }

        public TimeSeries TimeSeries
        {
            get => timeSeries; set
            {
                timeSeries = value;
                OnPropertyChanged();
            }
        }

        private bool stop = true;

        public bool Stop
        {
            get { return stop; }
            set
            {
                stop = value;
                OnPropertyChanged();
            }
        }



        public int MaxSlider
        {
            get
            {
                return maxSlider;
            }
            set
            {
                maxSlider = value;
                OnPropertyChanged();
            }
        }

        private int currentTime;

        public int CurrentTime
        {
            get
            {
                return currentTime;
            }
            set
            {
                currentTime = value;
                OnPropertyChanged();
            }
        }

        //play after stoping!!!
        internal void StartPlay()
        {
            Stop = false;
            if (!t.IsAlive)
            {
                t.Start();
            }
        }

        private void play()
        {
            while (true)
            {
                if (!stop && CurrentTime != MaxSlider)
                {
                    CurrentTime++;
                    Thread.Sleep(100);
                }
            }

        }

        internal void Pause()
        {
            stop = true;
        }


        virtual public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }




    }
}












