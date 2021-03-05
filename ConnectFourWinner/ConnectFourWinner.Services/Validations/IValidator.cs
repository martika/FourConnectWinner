using ConnectFourWinner.Services.Validations;

namespace ConnectFourWinner.Services
{

    /// <summary>
    /// Composite pattern Validator
    /// </summary>
    public interface IValidator
    {
        ResultValidation Validate(int width, int height, string input, ResultValidation result);        
    }
}
