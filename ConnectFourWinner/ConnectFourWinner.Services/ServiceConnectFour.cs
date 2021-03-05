using ConnectFourWinner.Domain.Entities;
using ConnectFourWinner.Domain.Entities.Enums;
using ConnectFourWinner.Services.Interfaces;
using ConnectFourWinner.Services.Validations;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectFourWinner.Services
{
    public class ServiceConnectFour : IServiceConnectFour
    {
        public const int MaxHolesChainFour = 3;

        private IList<IValidator> validators { get; set; }
        private IPlayValidator possiblePlayValidator { get; set; }

        public ServiceConnectFour(IList<IValidator> validators, IPlayValidator possiblePlayValidator)
        {
            this.validators = validators;
            this.possiblePlayValidator = possiblePlayValidator;
        }

        public Solution GetSolution(string input, int width, int height)
        {
            var validation = Validate(width, height, input);

            if (!validation.IsValid)
            {
                throw new System.ArgumentException(validation.GetFormattedMessages());
            }

            Board board = new Board(GetBoardData(width, height, input));

            var possiblePlayValidation = possiblePlayValidator.Validate(board, validation);

            if (!possiblePlayValidation.IsValid)
            {
                throw new System.ArgumentException(possiblePlayValidation.GetFormattedMessages());
            }

            return GetConnectFour(board, input);
        }

        private ResultValidation Validate(int width, int height, string input)
        {
            ResultValidation result = new ResultValidation();

            foreach (IValidator validation in validators)
            {
                validation.Validate(width, height, input, result);
            }

            return result;
        }

        private string[,] GetBoardData(int width, int height, string input)
        {
            string[,] board = new string[height, width];
            List<string> aListOfPiece = input.ToCharArray().Select(c => c.ToString()).ToList();

            int count = 0;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    board[row, col] = aListOfPiece[count];
                    count = count + height;
                }
                count = row + 1;
            }

            return board;
        }

        private Solution GetConnectFour(Board board, string input)
        {
            var height = board.Height;
            var width = board.Width;

            string[,] dataBoard = board.GetBoardData();
            string showBoard = ShowBoard(board);

            for (var row = 0; row < height; row++)
            {
                for (var col = 0; col < width; col++)
                {
                    var pieceVisited = dataBoard[row, col];

                    bool hasHorizontalHoleAvailable = col < width - MaxHolesChainFour;
                    bool hasVerticalHoleAvailable = row < height - MaxHolesChainFour;

                    bool hasDiagonalRightUpHoleAvailable = row < height - MaxHolesChainFour && col < width - MaxHolesChainFour;
                    bool hasDiagonalLeftUpHoleAvailable = row < height - MaxHolesChainFour && col > MaxHolesChainFour - 1;


                    if (hasHorizontalHoleAvailable &&
                        IsFourChain(pieceVisited, dataBoard[row, col + 1], dataBoard[row, col + 2], dataBoard[row, col + 3]))
                    {
                        return new Solution(showBoard, dataBoard, GetSolutionType(pieceVisited), WinType.Horizontally);
                    }

                    if (hasVerticalHoleAvailable &&
                        IsFourChain(pieceVisited, dataBoard[row + 1, col], dataBoard[row + 2, col], dataBoard[row + 3, col]))
                    {
                        return new Solution(showBoard, dataBoard, GetSolutionType(pieceVisited), WinType.Vertically);
                    }

                    if (hasDiagonalRightUpHoleAvailable &&
                        IsFourChain(pieceVisited, dataBoard[row + 1, col + 1], dataBoard[row + 2, col + 2], dataBoard[row + 3, col + 3]))
                    {
                        return new Solution(showBoard, dataBoard, GetSolutionType(pieceVisited), WinType.DiagonallyUpForward);
                    }

                    if (hasDiagonalLeftUpHoleAvailable &&
                        IsFourChain(pieceVisited, dataBoard[row + 1, col - 1], dataBoard[row + 2, col - 2], dataBoard[row + 3, col - 3]))
                    {
                        return new Solution(showBoard, dataBoard, GetSolutionType(pieceVisited), WinType.DiagonallyUpBackward);
                    }
                }
            }

            if (IsTieSolution(input))
                return new Solution(showBoard, dataBoard, SolutionType.Tie, null);

            return new Solution(ShowBoard(board), dataBoard, SolutionType.Ongoing, null);
        }

        private bool IsFourChain(string posSelected, string pos1, string pos2, string pos3)
        {
            return (posSelected != Constants.PieceEmpty.ToString())
                && (posSelected == pos1)
                && (posSelected == pos2)
                && (posSelected == pos3);
        }

        private string ShowBoard(Board board)
        {
            IList<string> rows = new List<string>();
            string[,] data = board.GetBoardData();

            for (var row = 0; row < board.Height; row++)
            {
                StringBuilder rep = new StringBuilder();
                for (var col = 0; col < board.Width; col++)
                {
                    rep.Append(data[row, col]);
                }
                rows.Add(rep.ToString());
            }

            StringBuilder rowsBoard = new StringBuilder();
            rows.Reverse().ToList().ForEach(item => rowsBoard.AppendLine(item));
            return rowsBoard.ToString();
        }

        private SolutionType GetSolutionType(string pieceWinner)
        {
            if (pieceWinner == Constants.PieceTeamA.ToString())
                return SolutionType.A_Winner;

            if (pieceWinner == Constants.PieceTeamB.ToString())
                return SolutionType.B_Winner;          

            return SolutionType.Ongoing;
        }

        private bool IsTieSolution(string input)
        {
            int countA = input.Count(p => p == Constants.PieceTeamA);
            int countB = input.Count(p => p == Constants.PieceTeamB);

            return countA + countB == input.Length;
        }
    }
}

