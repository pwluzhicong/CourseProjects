using System.Collections;

namespace BoxProject
{
    public static class BoxesGenerator 
    {
        // public int N;
        // public BoxesGenerator(int num) => this.N = num;
        public static Box[] GetBoxes(int num)
        {
            var rand = new Random();
            Box[] Boxes = new Box[num];
            for (int i = 0; i < num; i++)
            {
                Boxes[i] =  new Box(i+1, rand.Next(100, 5000) / 100.0f,   rand.Next(100, 5000) / 100.0f);
            }
            return Boxes;
        }
    }
}