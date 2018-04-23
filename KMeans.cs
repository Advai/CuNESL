using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Utility;

namespace SIR2017
{
    class KMeans
    {
        //constants
        public const int clusterNum = 3;
        public static string filepath = "color10.txt";
        public static int totalData;
        public static int dimension;
        public static List<DataPoint> dataset = new List<DataPoint>();
        public static List<Cluster> clusters = new List<Cluster>();
        public static int iterations = 0;

        public static void Cluster(List<List<double>> data)
        {
            double largevalue = Math.Pow(10, 10);
            double min = largevalue;
            double dist = 0.0;
            int cluster = 0;
            bool isStillMoving = true;
            DataPoint newData = null;
            int count = 0;
            //this loop assigns each data point to a cluster
            while (dataset.Count < totalData)
            {
                newData = new DataPoint(data[count]);
                dataset.Add(newData);
                min = largevalue;
                //Console.WriteLine("** Datapoint " + count + " **");
                for (int i = 0; i < clusterNum; i++)
                {
                    dist = distSq(newData, clusters[i]);
                    //Console.WriteLine(newData.data.ToString() + "\nsubtracted from \n" + centroids[i].center.ToString() + "\n= " + dist);
                    //Console.Write("\n");
                    if (dist < min)
                    {
                        min = dist;
                        cluster = i;
                    }
                }
                newData.cluster = cluster;
                count++;
            }
            //this loops calculates the centroids once all the points are assigned a cluster
            for (int i = 0; i < clusterNum; i++)
            {
                VectorND sums = new VectorND(new double[dimension], dimension);
                int inCluster = 0;
                for (int j = 0; j < dataset.Count; j++)
                {
                    if (dataset[j].cluster == i)
                    {
                        sums += dataset[j].data;
                        inCluster++;
                    }
                }
                if (inCluster > 0)
                {
                    clusters[i].center = sums / inCluster;
                }
            }
            //checks if points can be assigned to a closer cluster and recalculates the centers until they converge
            while (isStillMoving)
            {
                //re calculates centers based on what happens in inner for loop below
                for (int i = 0; i < clusterNum; i++)
                {
                    VectorND sums = new VectorND(new double[dimension], dimension);
                    int inCluster = 0;
                    for (int j = 0; j < dataset.Count; j++)
                    {
                        if (dataset[j].cluster == i)
                        {
                            sums += dataset[j].data;
                            inCluster++;
                        }
                    }
                    if (inCluster > 0)
                    {
                        clusters[i].center = sums / inCluster;
                    }
                }
                isStillMoving = false;
                //checks if each datapoint could be closer to a different centroid and assigns it to that centroid accordingly
                for (int i = 0; i < dataset.Count; i++)
                {
                    DataPoint tempData = dataset[i];
                    min = largevalue;
                    for (int j = 0; j < clusterNum; j++)
                    {
                        dist = distSq(tempData, clusters[j]);
                        if (dist < min)
                        {
                            min = dist;
                            cluster = j;
                        }
                    }
                    tempData.cluster = cluster;
                    if (tempData.cluster != cluster)
                    {
                        tempData.cluster = cluster;
                        isStillMoving = true;
                    }
                }
                iterations++;
            }
        }

        public static void initializeClusters(List<List<double>> data)
        {
            /**
             * this code was used in testing against Dr. Reppy's C++ code
             */
            Console.WriteLine("============================");
            Console.WriteLine("Initial Centroid Data");
            clusters.Add(new Cluster(data[1]));
            Console.Write("\n");
            clusters.Add(new Cluster(data[5]));
            Console.Write("\n");
            clusters.Add(new Cluster(data[0]));


            //initializes n clusters to n random points in dataset
            //int randomseed = 0;
            //Random random = new Random();
            //Centroid newCluster = null;
            //for (int i = 0; i < clusterNum; i++)
            //{
            //    newCluster = new Centroid(data[random.Next(data.Count)]);
            //    centroids.Add(newCluster);
            //    Console.WriteLine(newCluster.center.ToString());
            //}
            Console.WriteLine("============================");
        }
        
        private static double distSq(DataPoint point, Cluster c)
        {
            //calculates distance between datapoint and centroid using VectorND operations
            return (c.center - point.data).Mag2();
        }
        
        public static List<List<double>> initializeData()
        {
            //reads in all the data from the text file and puts it into a list of a list of doubles
            List<List<double>> fakeDataset = new List<List<double>>();
            StreamReader reader = File.OpenText(filepath);
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLine();
                //weird syntax i had to use to make it split by space properly
                var data = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                List<double> listData = new List<double>();
                for (int i = 0; i < data.Length; i++)
                {
                    listData.Add(Double.Parse(data[i]));
                }
                fakeDataset.Add(listData);
            }
            dimension = fakeDataset[0].Count - 1;
            totalData = fakeDataset.Count;
            return fakeDataset;
            /**
             * The code below is used when you don't want the Cluster method to move the centers
             * once all the datapoints have been assigned a centroid
             * To use this code, do not call the Cluster or initializeCluster method and just uncomment this part 
             */ 
            //initializeClusters(fakeDataset);
            //double largevalue = Math.Pow(10, 10);
            //double min = largevalue;
            //double dist = 0.0;
            //int cluster = 0;
            //int count = 0;
            //DataPoint newData = null;
            //while (dataset.Count < totalData)
            //{
            //    newData = new DataPoint(fakeDataset[count]);
            //    dataset.Add(newData);
            //    min = largevalue;
            //    Console.WriteLine("Datapoint " + count);
            //    for (int i = 0; i < clusterNum; i++)
            //    {
            //        dist = distance(newData, centroids[i]);
            //        Console.WriteLine(newData.data.ToString() + " subtracted from \n" + centroids[i].center.ToString() + "\n= " + dist);
            //        if (dist < min)
            //        {
            //            min = dist;
            //            cluster = i;
            //        }
            //    }
            //    newData.cluster = cluster;
            //    //this inner loops recalculates cluster centers as datapoints are being assigned to it
            //    count++;
            //}
            //for (int i = 0; i < clusterNum; i++)
            //{
            //    VectorND sums = new VectorND(new double[dimension], dimension);
            //    int inCluster = 0;
            //    for (int j = 0; j < dataset.Count; j++)
            //    {
            //        if (dataset[j].cluster == i)
            //        {
            //            sums += dataset[j].data;
            //            inCluster++;
            //        }
            //    }
            //    if (inCluster > 0)
            //    {
            //        centroids[i].center = sums / inCluster;
            //    }
            //}
        }
    }
}
