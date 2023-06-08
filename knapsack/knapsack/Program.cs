using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

class Knapsack
{
    
    static void Main(string[] args)
    {

        List<int> IncludeState = new List<int>();
        string Path = "C:\\Users\\Kasim\\Desktop\\KasımİslamTATLI\\ks_19_0.txt";
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var result = KnapsackProblem(Path,IncludeState);
        stopwatch.Stop();
        TimeSpan elapsed = stopwatch.Elapsed;
        double seconds = elapsed.TotalMilliseconds / 1000;
        Console.WriteLine("Algoritmanın çalışma süresi: {0}", seconds);
        Console.WriteLine("Optimal value değeri: " + result.Item1);
        Console.WriteLine("Optimal çözüme dahil edilen itemler: " + string.Join(",", result.Item2));

        
    }
    
    static Tuple<int, List<int>> KnapsackProblem(string filePath, List<int> includeState)
    {
        List<int> values = new List<int>();    
        List<int> weights = new List<int>();   
        includeState = new List<int>();

        int itemCount;   
        int maxCapacity; 
        

        using (StreamReader sr = new StreamReader(filePath))
        {
            string line = sr.ReadLine();
            string[] firstLine = line.Split(' ');

            itemCount = int.Parse(firstLine[0].Trim());
            maxCapacity = int.Parse(firstLine[1].Trim());

            for (int i = 0; i < itemCount; i++)
            {
                line = sr.ReadLine();
                string[] lines = line.Split(' ');
                values.Add(int.Parse(lines[0].Trim()));
                weights.Add(int.Parse(lines[1].Trim()));
            }
        }

        int size = itemCount;   

        int[][] dynamicProgTable = new int[size + 1][];   
        for (int i = 0; i < dynamicProgTable.Length; i++)
        {
            dynamicProgTable[i] = new int[maxCapacity + 1];
            
            
        }

       
        for (int i = 1; i <= size; i++)
        {
            for (int w = 1; w <= maxCapacity; w++)
            {
                if (weights[i - 1] <= w)
                {
                    
                    dynamicProgTable[i][w] = Math.Max(values[i - 1] + dynamicProgTable[i - 1][w - weights[i - 1]], dynamicProgTable[i - 1][w]);
               
                    
                }
                else
                {
                    
                    dynamicProgTable[i][w] = dynamicProgTable[i - 1][w];
                    

                }
            }
        }
        

        int optimalValue = dynamicProgTable[size][maxCapacity];   
        List<int> includedItems = new List<int>();   
        int k = maxCapacity;
        for (int i = size; i > 0 && optimalValue > 0; i--)
        {
            if (optimalValue != dynamicProgTable[i - 1][k])
            {
               

                includedItems.Add(i);
                optimalValue -= values[i - 1];
                k -= weights[i - 1];
                
                
            }
            
            
            
        }

        includedItems.Reverse();   
       
        Console.WriteLine("***Optimal Çözüme dahil edilen değerler ve ağırlıkları***" );
        for (int i = 0; i < includedItems.Count; i++)
        {
            Console.WriteLine(values[includedItems[i]-1] + " " + weights[includedItems[i]-1]);
            
        }
        Console.WriteLine("");

        for (int i = 1; i <= itemCount; i++)
        {
            if(includedItems.Contains(i))
            {
                includeState.Add(1);
            }
            else
            {
                includeState.Add(0);
            }
        }
        Console.WriteLine("durumlar:  " + string.Join(" ", includeState));

        return new Tuple<int, List<int>>(dynamicProgTable[size][maxCapacity], includedItems);
    }
}

