using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DavidsRogueClone
{
    public class Dungeon
    {
        private int width;
        private int height;
        private int numWallsX;
        private int numWallsY;

        public char[,] dungeon;
        public string[] consoleLines;
        public int[] wallPosX;
        public int[] wallPosY;
        public int[] doorPosX;
        public int[] doorPosY;

        public int Width { get; set; }
        public int Height { get; set; }
        public int NumWallsX { get; set; }
        public int NumWallsY { get; set; }

        public Dungeon()
        {
            width = 100;
            height = 22;
            numWallsX = 2;
            numWallsY = 5;
            dungeon = new char[height, width];
            consoleLines = new string[height];
            wallPosX = new int[numWallsX];
            wallPosY = new int[numWallsY];
            doorPosX = new int[numWallsX+1];
            doorPosY = new int[numWallsY+1];
        }

        public char[,] createDungeonArray()
        {
            //Create indexes for walls
            for (int i = 0; i < numWallsX; i++)
            {
                Random rand = new Random();
                int randInt = rand.Next(3, height - 3);
                //Avoid random walls from being on top of each other
                while (randInt == 0 || randInt == width || wallPosX.Contains(randInt) || wallPosX.Contains(randInt - 1) || wallPosX.Contains(randInt + 1) || wallPosX.Contains(randInt - 2) || wallPosX.Contains(randInt + 2))
                {
                    rand = new Random();
                    randInt = rand.Next((i * width / numWallsX), width);
                }
                wallPosX[i] = randInt;
            }
            for (int i = 0; i < numWallsY; i++)
            {
                Random rand = new Random();
                int randInt = rand.Next(4, width - 4);
                //Avoid random walls from being on top of each other
                while (randInt == 0 || randInt == height || wallPosY.Contains(randInt) || wallPosY.Contains(randInt - 1) || wallPosY.Contains(randInt + 1) || wallPosY.Contains(randInt - 2) || wallPosY.Contains(randInt + 2))
                {
                    rand = new Random();
                    randInt = rand.Next((i * height / numWallsY), height);
                }
                wallPosY[i] = randInt;
            }
            bubbleSort(wallPosX);
            bubbleSort(wallPosY);
            //Console.WriteLine(String.Join(",", wallPosX.Select(p => p.ToString()).ToArray()));
            //Console.WriteLine(String.Join(",", wallPosY.Select(p => p.ToString()).ToArray()));
            for (int i = 0; i <= wallPosX.Length; i++)
            {
                if(i == 0)
                {
                    doorPosX[i] = ((0 + wallPosX[i]) / 2);
                } 
                else if (i == wallPosX.Length)
                {
                    doorPosX[i] = ((wallPosX[i - 1] + height) / 2);
                } else
                {
                    doorPosX[i] = ((wallPosX[i - 1] + wallPosX[i]) / 2);
                }
            }
            for (int i = 0; i <= wallPosY.Length; i++)
            {
                if (i == 0)
                {
                    doorPosY[i] = ((0 + wallPosY[i]) / 2);
                }
                else if (i == wallPosY.Length)
                {
                    doorPosY[i] = ((wallPosY[i - 1] + width) / 2);
                }
                else
                {
                    doorPosY[i] = ((wallPosY[i - 1] + wallPosY[i]) / 2);
                }
            }
            //Console.WriteLine(String.Join(",", doorPosX.Select(p => p.ToString()).ToArray()));
            //Console.WriteLine(String.Join(",", doorPosY.Select(p => p.ToString()).ToArray()));
            return dungeon;
        }

        public String[] createConsoleString()
        {
            //Build dungeon map
            for (int x = 0; x < dungeon.GetLength(0); x++)
            {
                //Build a string to display the dungeon in console
                StringBuilder sb = new StringBuilder();
                for (int y = 0; y < dungeon.GetLength(1); y++)
                {
                    if ((doorPosX.Contains(x) || doorPosY.Contains(y)) && (wallPosX.Contains(x) || wallPosY.Contains(y)))
                    {
                        //Build doors
                        dungeon[x, y] = ',';
                    }
                    else if (x == 0 || x == height - 1 || y == 0 || y == width - 1 || wallPosX.Contains(x) || wallPosY.Contains(y))
                    {
                        //Build walls
                        dungeon[x, y] = '#';
                    }
                    else
                    {
                        //Walkable zone
                        dungeon[x, y] = '.';
                    }
                    sb.Append(dungeon[x, y]);
                }
                consoleLines[x] = sb.ToString();
            }
            return consoleLines;
        }

        public void swapIndexDungeon(int indexStartX, int indexEndX, int indexStartY, int indexEndY)
        {
            //Don't move into walls
            if(!dungeon[indexEndX,indexEndY].Equals("#"))
            {
                char temp = dungeon[indexEndX, indexEndY];
                dungeon[indexEndX, indexEndY] = dungeon[indexStartX, indexStartY];
                dungeon[indexStartX, indexStartY] = temp;
            }
        }

        public int[] bubbleSort(int[] array)
        {
            int temp;
            for (int b = 0; b <= array.Length-2; b++)
            {
                for (int i = 0; i <= array.Length-2; i++)
                {
                    if (array[i] > array[i+1])
                    {
                        temp = array[i + 1];
                        array[i + 1] = array[i];
                        array[i] = temp;
                    }
                }
            }
            return array;
        }
    }
}
