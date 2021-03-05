using ConnectFourWinner.Domain.Entities;

namespace ConnectFourWinner.Services.Validations
{

    /// <summary>
    /// Validate Positions that are "physically" possible
    /// </summary>
    public class PossiblePlayValidator : IPlayValidator
    {
        public ResultValidation Validate(Board board, ResultValidation result)
        {
            string[,] dataBoard = board.GetBoardData();

            for (var row = 1; row < board.Height; row++)
            {
                for (var col = 0; col < board.Width; col++)
                {
                    if (dataBoard[row, col] != Constants.PieceEmpty.ToString() && dataBoard[row - 1, col] == Constants.PieceEmpty.ToString())
                    {
                        result.IsValid = false;
                        result.Messages.Add(ResultValidation.ErrorPositionPieces);
                        return result;
                    }
                }
            }

            return result;
        }
    }
}
