using System;

namespace TestCaveGenerator {

    class Program {

        static int gridSize = 10;

        static void print(int[,] data) {
            for (int x=0; x < gridSize; ++x) {
                for (int y = 0; y<gridSize; ++y) {
                    Console.Write(data[x,y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Main(string[] args) {

            //**********test 1**********
            Console.WriteLine("**********test 1**********");
            float percent = new float[] {0.5,0.1,0.4}; //floor, mountain, brush
            NewCaveGenerator caveGenerator1 = new NewCaveGenerator(percent, 10,1);
            caveGenerator1.RandomizeMap();
            for (int i=0; i<10; ++i) {
                print(caveGenerator1.GetMap());
                Console.ReadKey();
                caveGenerator1.SmoothMap();
            }

            //**********test 2**********
            Console.WriteLine("**********test 2**********");
            float percent = new float[] {0.5,0.1,0.2,0.2}; //floor, mountain, brush,sea
            NewCaveGenerator caveGenerator2 = new NewCaveGenerator(percent, 10,1);
            caveGenerator2.RandomizeMap();
            for (int i=0; i<10; ++i) {
                print(caveGenerator2.GetMap());
                Console.ReadKey();
                caveGenerator2.SmoothMap();
            }
        }
    }
}
