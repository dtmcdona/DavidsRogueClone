using System;
using System.Linq;
using System.Text;

namespace DavidsRogueClone
{
    class Program
    {
        static void Main(string[] args)
        {
            Dungeon myDungeon = new Dungeon();
            myDungeon.createDungeonArray();
            myDungeon.createConsoleString();

            //Show dungeon in console
            for (int x = 0; x < myDungeon.dungeon.GetLength(0); x++)
            {       
                Console.WriteLine(myDungeon.consoleLines[x]);
            }

            //Clear Console
            //Console.Clear();
        }
    }
}
