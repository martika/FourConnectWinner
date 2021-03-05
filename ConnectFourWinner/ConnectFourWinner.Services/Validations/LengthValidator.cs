using ConnectFourWinner.Services.Validations;

namespace ConnectFourWinner.Services
{
    /// <summary>
    /// Validate the length input
    /// </summary>
    public class LengthValidator : IValidator
    {
        public ResultValidation Validate(int width, int height, string input, ResultValidation result)
        {
            int lengthInput = width * height;
            bool isValidLength = input.Length == lengthInput;            

            if (!isValidLength)
            {
                result.Messages.Add(ResultValidation.ErrorInputLength);                          
            }

            result.IsValid &= isValidLength;
            return result;
        }
    }
}
