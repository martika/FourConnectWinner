using ConnectFourWinner.Domain.Entities;
using System.Linq;

namespace ConnectFourWinner.Services.Validations
{
    public class TeamNumberPiecesValidator : IValidator
    {
        public ResultValidation Validate(int width, int height, string input, ResultValidation result)
        { 
            int countA = input.Count(p => p == Constants.PieceTeamA);
            int countB = input.Count(p => p == Constants.PieceTeamB);

            bool validPieces = countA == countB || countA == countB + 1;           
            if (!validPieces)
            {
                result.Messages.Add(ResultValidation.ErrorTeamNumberPieces);
            }
            
            result.IsValid &= validPieces;

            return result;
        }
    }
}
