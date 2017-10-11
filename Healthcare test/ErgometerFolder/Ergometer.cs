using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare_test
{
    public abstract class Ergometer
    {
        abstract public ErgometerData GetData();
        abstract public void ErgometerCommandMode();
        abstract public Boolean IsConnected();
        abstract public void SetDistance(double distance);
        abstract public void SetTime(int time);
        abstract public void SetPower(int power);
        abstract public void Close();
        abstract public void Reset();
    }
}
