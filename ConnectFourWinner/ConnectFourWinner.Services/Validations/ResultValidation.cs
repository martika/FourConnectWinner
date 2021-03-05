using System.Collections.Generic;
using System.Text;

namespace ConnectFourWinner.Services.Validations
{
    public class ResultValidation
    {
        public const string ErrorInputLength = "Input not match with board size. ";
        public const string ErrorBoardMinSize = "The board has not minimum size. ";
        public const string ErrorSymbolPieces = "Valid symbol pieces ar A, B or X. ";
        public const string ErrorTeamNumberPieces = "Pieces A is not the same or exactly one more piece than the number of pieces B. ";
        public const string ErrorPositionPieces = "There positions that are physically impossible. ";       


        public IList<string> Messages { get; set; } = new List<string>();
        public bool IsValid { get; set; } = true;

        public string GetFormattedMessages()
        {
            StringBuilder messages = new StringBuilder();

            foreach (var msg in Messages)
            {
                messages.Append(msg);
            }

            return messages.ToString();
        }
    }
}
