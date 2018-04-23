using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace SIR2017
{
    public class Cluster
    {        
        public VectorND center { get; set; }
        public Cluster(List<double> centeri)
        {
            //my quick and dirty solution to removing the first element from the list passed in by constructor
            //the list passes objects by reference so i had to make sure not to alter the original list
            double[] arr = new double[centeri.Count];
            centeri.CopyTo(arr);
            double[] finalArr = new double[centeri.Count - 1];
            int arrIndex = 1;
            for(int i = 0; i < centeri.Count - 1; i++)
            {
                finalArr[i] = arr[arrIndex];
                arrIndex++;
            }
            center = new VectorND(finalArr, finalArr.Length);
            Console.WriteLine(center.ToString());
        }
    }
}
