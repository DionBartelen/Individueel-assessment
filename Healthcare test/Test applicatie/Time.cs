using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare_test.test_applicatie
{
    public class Time
    {
        public int Second { get; set; }
        public int Minute { get; set; }
        public int Hour { get; set; }

        public Time(int ss, int mm, int hh)
        {
            this.Second = ss;
            this.Minute = mm;
            this.Hour = hh;

        }

        public void Timer ()
        {
            Second++;
            if (Second == 60)
            {
                Second = 0;
                Minute++;
            }

            if (Minute == 60)
            {
                Minute = 0;
                Hour++;
            }

        }


        public override String ToString()
        {
            return $"{Hour:D2}:{Minute:D2}:{Second:D2}";                     // string interpolatie
        }
    }
}
