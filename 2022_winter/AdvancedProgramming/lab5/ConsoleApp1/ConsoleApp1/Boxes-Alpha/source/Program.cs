using System;
using System.Diagnostics;
using System.Threading;

namespace BoxProject
{
    public class Program
    {
        public static void Main(string[] args)
        {            
            (string operation,  int boxNum, string boxes_file, string result_file) = Parse(args);

            if (operation == "generate"){
                Box[] Boxes = BoxesGenerator.GetBoxes(boxNum);
                Boxes.Print();
                Boxes.Write(boxes_file);
            }else if (operation == "sort"){
                RunBoxSort(boxes_file, result_file); // Run the Matryoshka Box Sort algorithm
            }else if (operation == "all"){
                Box[] Boxes = BoxesGenerator.GetBoxes(boxNum);
                Boxes.Write(boxes_file);

                RunBoxSort(boxes_file, result_file); // Run the Matryoshka Box Sort algorithm
            }else if (operation== "runbatch"){
                RunBatch();
            }
            else if(operation == "help"){
                Usage(); //
            }else{
                Usage();
            }
        }
        public static TimeSpan RunBoxSort(string boxes_file, string result_file, bool verbose=true){
            /* Program*/
            var data = BoxesIO.Read(boxes_file);
            if (verbose){
                Console.WriteLine("-------Original Data:");
                data.Print();
            }
            
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            
            data.MergeSort();
            Box[] boxSequence = data.Matryoshka();
            
            stopWatch.Stop();
            var time = stopWatch.Elapsed;

            if (verbose){
                Console.WriteLine("\n\n-------After Calculating the Matryoshka Box Sequence.");
                Console.WriteLine($"Max Sequence Length = {boxSequence.GetLength(0)}");
                boxSequence.Print();
                Console.WriteLine($"Algorithm Time Cost(excluding IO time): {time.TotalMilliseconds}ms");
            }
            boxSequence.Write(result_file);
            return time;
        }

        public static void RunBatch(){
            int i = 0;
            double[] time_records = new double[10];
            
            Console.WriteLine($"N\tAverageTime\tMinTime\tMaxTime");

            while(i<=100000){
                i+=50;
                for (int j=0;j<10;j++ ){
                    Box[] Boxes = BoxesGenerator.GetBoxes(i);
                    var stopWatch = Stopwatch.StartNew();

                    Boxes.MergeSort();
                    Box[] boxSequence = Boxes.Matryoshka();
                    stopWatch.Stop();
                    
                    var time = stopWatch.Elapsed;
                    time_records[j] = time.TotalMilliseconds;
                }
                Console.WriteLine($"{i}\t{time_records.Average()}\t{time_records.Min()}\t{time_records.Max()}");
            }
        }


        public static (string,int,string,string) Parse(string[] args){
            string operation="sort";
            int box_num=10;
            string boxes_file="./data/boxes.txt";
            string result="./data/result.txt";
            if (args.GetLength(0) !=0){
                operation = args[0];
            }
            
            foreach(string param in args){
                var paramPair = param.Split("=");
                Console.Write(paramPair[0]);
                if (paramPair[0] == "-boxes_num"){
                    box_num = int.Parse(paramPair[1]);
                }else if (paramPair[0] == "-boxes_file"){
                    boxes_file = paramPair[1];
                }else if (paramPair[0] == "-result_file"){
                    result = paramPair[1];
                }
            }

            return (operation, box_num, boxes_file, result);
        }

        public static void Usage(){
            Console.Write("Usage: ./BoxSort operation -boxes_num [box_num] -boxes_file [boxes_file] -result_file [result_file]\noperation\t\"generate\"| \"sort\"| \"all\"| \"help\"\n[box_num]\tBox numbers to generate\n[boxes_file]\tboxes filepath");
        }
        
        
    }
}