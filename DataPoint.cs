using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace SIR2017
{
    public class DataPoint
    {

        public int index { get; set; } //index of point in file
        public VectorND data {get;set;} //coordinates of point
        public int cluster { get; set; } //cluster it is assigned to

        public DataPoint(List<double> valuesi)
        {
            //all this code converts the List into an array and sets the index/initializes the data vector
            index = (int)valuesi[0];
            valuesi.Remove(valuesi[0]);
            var values = valuesi.ToArray();
            data = new VectorND(values, values.Length);
            cluster = 0;
        }
    }
}
