using ConnectFourWinner.Domain.Entities;


namespace ConnectFourWinner.Services.Validations
{
    public class BoardValidator : IValidator
    {
        public ResultValidation Validate(int width, int height, string input, ResultValidation result)
        {           

            bool isValidMinSize = width >= Constants.MinBoardColumns
                && height >= Constants.MinBoardRows;            

            if (!isValidMinSize)
            {
                result.Messages.Add(ResultValidation.ErrorBoardMinSize);
            }

            result.IsValid &= isValidMinSize;

            return result;
        }
    }
}
