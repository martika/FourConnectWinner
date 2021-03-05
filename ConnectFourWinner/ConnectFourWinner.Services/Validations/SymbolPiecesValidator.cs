using ConnectFourWinner.Domain.Entities;
using System.Linq;

namespace ConnectFourWinner.Services.Validations
{
    public class SymbolPiecesValidator : IValidator
    {
        public ResultValidation Validate(int width, int height, string input, ResultValidation result)
        {
            bool hasValidSymbolPieces = input.All(p => p == Constants.PieceTeamA || p == Constants.PieceTeamB || p == Constants.PieceEmpty);

            if (!hasValidSymbolPieces)
            {
                result.Messages.Add(ResultValidation.ErrorSymbolPieces);
            }

            result.IsValid &= hasValidSymbolPieces;

            return result;
        }
    }
}
