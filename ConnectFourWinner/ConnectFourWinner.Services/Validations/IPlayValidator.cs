using ConnectFourWinner.Domain.Entities;

namespace ConnectFourWinner.Services.Validations
{
    public interface IPlayValidator
    {
        ResultValidation Validate(Board board, ResultValidation result);
    }
}