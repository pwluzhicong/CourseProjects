using System;
using System.Text;

namespace BoxProject
{
    public static class BoxesIO
    {
        public static (int, float, float) SplitLine(this string line, char splitChar)
        {
            string[] widthAndLength = line.Split(" ", 3);
            int index = int.Parse(widthAndLength[0]);
            float width = float.Parse(widthAndLength[1]);
            float length = float.Parse(widthAndLength[2]);
            return (index, width, length);
        }
        public static Box[] Read(string filename)
        {
            string[] boxesData = File.ReadAllLines(filename);
            Box[] boxesArray;

            int boxNumber = int.Parse(boxesData[0]);
            boxesArray = new Box[boxNumber];

            for (int i = 1; i <= boxNumber; i++)
            {
                string line = boxesData[i];
                var (index, width, length)  = line.SplitLine(' ');
                if(width > length){
                    var tmp = width;
                    width = length;
                    length = tmp;
                }
                boxesArray[i-1] = new Box(index, width, length);
            }
            return boxesArray;
        }
        public static void Write(this Box[] boxes, string filename){
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{boxes.GetLength(0)}\n");
            foreach(Box b in boxes){
                stringBuilder.Append($"{b.Index} {b.Width} {b.Length}\n");
            }
            File.WriteAllText(filename, stringBuilder.ToString());
        }
        public static void Print(this Box[] boxes){
            
             foreach(Box b in boxes){
                Console.WriteLine($"{b.Index}:\t{b.Width}\t{b.Length}");
            }
        }
    }
}