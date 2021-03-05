using ConnectFourWinner.Domain.Entities.Enums;
using System.Collections.Generic;

namespace ConnectFourWinner.Domain.Entities
{
    public class Solution
    {
        public SolutionType SolutionType { get; private set; }
        public WinType? WinType { get; private set; } 
        public string Board { get; private set; }
        public string[,] BoardData { get; private set; }

        public Solution(string board, string[,] boardData, SolutionType solutionType, WinType? winType)
        {
            BoardData = boardData;
            Board = board;
            SolutionType = solutionType;
            WinType = winType;
        }
    }
}
