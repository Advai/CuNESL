using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIR2017
{
    class Driver
    {
        static void Main(string[] args)
        {
            //algorithm is run by this call
            var data = KMeans.initializeData();
            KMeans.initializeClusters(data);
            KMeans.Cluster(data);
            //used for output
            Console.WriteLine("============================");
            Console.WriteLine("dimension = " + KMeans.dimension + "\n# points = " + KMeans.totalData + "\n#clusters = " + KMeans.clusterNum + "\n# iterations = " + KMeans.iterations);
            Console.WriteLine("============================");
            for (int i = 0; i < KMeans.clusterNum; i++)
            {
                Console.WriteLine("** Cluster " + i + " **");
                int counter = 0;
                Console.WriteLine("Center: " + KMeans.clusters[i].center.ToString());
                for (int j = 0; j < KMeans.totalData; j++)
                {
                    if (KMeans.dataset[j].cluster == i)
                    {
                        Console.WriteLine(KMeans.dataset[j].index + ": " + KMeans.dataset[j].data.ToString());
                        counter++;
                    }
                }
                Console.WriteLine();
                //Console.Write(counter + "\n");
            }
            Console.WriteLine("============================");
            ////Console.Write("Centroids converged to: \n");
            //for (int i = 0; i < KMeans.clusterNum; i++)
            //{
            //    Console.WriteLine("** Centroid " + i + " **");
            //    Console.WriteLine(KMeans.centroids[i].center.ToString());
            //}
            //Console.Write("\n");
            Console.ReadLine();
        }
    }
}
