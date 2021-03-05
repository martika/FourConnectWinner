
namespace ConnectFourWinner.Domain.Entities
{
    public class Board
    { 
        private readonly string[,] data;
        public int Width { get;  private set; }
        public int Height { get; private set; }

        public string[,] GetBoardData()
        {
            return data;
        }

        public Board(string[,] data)
        {
            this.Width = data.GetLength(1);
            this.Height = data.GetLength(0);
            this.data = data;
        }
    }
}

