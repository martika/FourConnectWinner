using ConnectFourWinner.Domain.Entities;

namespace ConnectFourWinner.Services.Interfaces
{
    public interface IServiceConnectFour
    {        
        Solution GetSolution(string input, int width, int height);
    }
}
