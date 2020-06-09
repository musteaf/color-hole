using System;
using System.Collections.Generic;

namespace Generator.Model
{
    [Serializable]
    public class Level
    {
        public int levelNumber;
        public List<Shape> board1;
        public List<Shape> board2;

        public Level(int levelNumber, List<Shape> board1, List<Shape> board2)
        {
            this.levelNumber = levelNumber;
            this.board1 = board1;
            this.board2 = board2;
        }
    }
}