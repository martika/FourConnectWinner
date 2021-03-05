using ConnectFourWinner.Domain.Entities;
using ConnectFourWinner.Domain.Entities.Enums;
using ConnectFourWinner.Services.Interfaces;
using ConnectFourWinner.Services.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace ConnectFourWinner.Services.Test
{
    [TestClass]
    public class ServiceConnectFourTest
    {
        private IServiceConnectFour sut;
        private IList<IValidator> validators;        
        private Mock<IPlayValidator> possiblePlayValidator;

        [TestInitialize]
        public void SetUp()
        {
            validators = new List<IValidator>();
            possiblePlayValidator = new Mock<IPlayValidator>();
            CreateSut();
            possiblePlayValidator.Setup(v => v.Validate(It.IsAny<Board>(), It.IsAny<ResultValidation>())).Returns(new ResultValidation() { IsValid = true });
        }

        private void CreateSut()
        {
            sut = new ServiceConnectFour(validators, possiblePlayValidator.Object);            
        }

        [TestMethod]
        public void ShowEmptyBoardTest()
        {
            //Arrange
            string result =
               "XXXXXXX\r\n" +
               "XXXXXXX\r\n" +
               "XXXXXXX\r\n" +
               "XXXXXXX\r\n" +
               "XXXXXXX\r\n" +
               "XXXXXXX\r\n";

            //Act
            string board = sut.GetSolution("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", 7, 6).Board;           

            //Assert
            Assert.AreEqual(result, board);
        }


        [TestMethod]
        public void ShowBoardTest_1()
        {
            //Arrange
            string result =
               "XXXXXXX\r\n" +
               "XXXXXXX\r\n" +
               "XXXXXXX\r\n" +
               "XXXXBXX\r\n" +
               "XXXXBXX\r\n" +
               "AAAABXX\r\n";

            //Act
            string board = sut.GetSolution("AXXXXXAXXXXXAXXXXXAXXXXXBBBXXXXXXXXXXXXXXX", 7, 6).Board;

            //Assert
            Assert.AreEqual(result, board);
        }

        [TestMethod]
        public void ShowBoardTest_2()
        {
            //Arrange
            string result =
                "XXXXXXX\r\n" +
                "XXXXXXX\r\n" +
                "XXXXXXX\r\n" +
                "XXXXXXX\r\n" +
                "ABXXXXX\r\n" +
                "ABXXXXX\r\n";

            //Act
            string board = sut.GetSolution("AAXXXXBBXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", 7, 6).Board;

            //Assert
            Assert.AreEqual(result, board);
        }

        [TestMethod]
        public void ShowBoardTest_3()
        {
            //Arrange
            string result =
                 "AAXXXXX\r\n" +
                 "XXXXXXX\r\n" +
                 "BXXXXXX\r\n" +
                 "ABXXXXX\r\n" +
                 "ABBXXXX\r\n" +
                 "ABABXXX\r\n";

            //Act
            string board = sut.GetSolution("AAABXABBBXXAABXXXXBXXXXXXXXXXXXXXXXXXXXXXX", 7, 6).Board;

            //Assert
            Assert.AreEqual(result, board);
        }

        [TestMethod]
        public void EmptyBoardOngoingResultTest()
        {
            //Arrange
            Solution solutionExpected = new Solution(null, null, SolutionType.Ongoing, null);           


            //Act
            Solution solution = sut.GetSolution("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", 7, 6);

            //Assert
            AssertSolution(solutionExpected, solution);
        }

        [TestMethod]
        public void ATeamWinsHorizontallyResultTest()
        {
            //Arrange
            Solution solutionExpected = new Solution(null, null, SolutionType.A_Winner,  WinType.Horizontally );

            //Act
            Solution solution = sut.GetSolution("AXXXXXAXXXXXAXXXXXAXXXXXBBBXXXXXXXXXXXXXXX", 7, 6);

            //Assert
            AssertSolution(solutionExpected, solution);
        }

        [TestMethod]
        public void BTeamWinsVerticallyResultTest()
        {
            //Arrange
            Solution solutionExpected = new Solution(null, null, SolutionType.B_Winner,  WinType.Vertically);            

            //Act
            Solution solution = sut.GetSolution("AXXXXXAAXXXXAXXXXXXXXXXXBBBBXXXXXXXXXXXXXX", 7, 6);

            //Assert
            AssertSolution(solutionExpected, solution);
        }       

        [TestMethod]
        public void ATeamWinsVerticallyResultTest()
        {
            //Arrange            
            Solution solutionExpected = new Solution(null, null, SolutionType.A_Winner , WinType.Vertically );

            //Act
            Solution solution = sut.GetSolution("BXXXXXBBXXXXBXXXXXXXXXXXAAAAXXXXXXXXXXXXXX", 7, 6);

            //Assert
            AssertSolution(solutionExpected, solution);
        }

        [TestMethod]
        public void ATeamWinsDiagonallyAndHorizontallyResultTest()
        {
            //Arrange
            Solution solutionExpected = new Solution(null, null, SolutionType.A_Winner,  WinType.Horizontally ); //Also Diagonally but visit first row and found winner           

            //Act
            Solution solution = sut.GetSolution("ABBAXXABAXXXAAXXXXAXXXXXBBBXXXXXXXXXXXXXXX", 7, 6);

            //Assert
            AssertSolution(solutionExpected, solution);
        }

        [TestMethod]
        public void ATeamWinsDiagonalupBackwardResultTest()
        {
            //Arrange
            Solution solutionExpected = new Solution(null, null, SolutionType.A_Winner, WinType.DiagonallyUpBackward); 

            //Act
            Solution solution = sut.GetSolution("XXXXXXBABAXXBBAXXXAAXXXXAXXXXXBXXXXXXXXXXX", 7, 6);

            //Assert
            AssertSolution(solutionExpected, solution);
        }

        [TestMethod]
        public void BTeamWinsDiagonalupForwardResultTest()
        {
            //Arrange
            Solution solutionExpected = new Solution(null, null, SolutionType.B_Winner, WinType.DiagonallyUpForward);

            //Act
            Solution solution = sut.GetSolution("XXXXXXXXXXXXBXXXXXABXXXXAABXXXBAABXXXXXXXX", 7, 6);

            //Assert
            AssertSolution(solutionExpected, solution);
        }
        

        [TestMethod]
        public void ATeamWinTwoHorizontallyResultTest()
        {
            //Arrange
            Solution solutionExpected = new Solution(null, null, SolutionType.A_Winner, WinType.Horizontally );

            //Act
            Solution solution = sut.GetSolution("ABXXXXABXXXXABXXXXAXXXXXABXXXXABXXXXABXXXX", 7, 6);

            //Assert
            AssertSolution(solutionExpected, solution);
        }

        [TestMethod]
        public void TieResultTest()
        {
            //Arrange
            Solution solutionExpected = new Solution(null, null, SolutionType.Tie, null);

            //Act
            Solution solution = sut.GetSolution("ABABABBABABABABABABABABAABABABABABABABABAB", 7, 6);

            //Assert
            AssertSolution(solutionExpected, solution);
        }

        [TestMethod]
        public void OngoingResultTest()
        {
            //Arrange
            Solution solutionExpected = new Solution(null, null, SolutionType.Ongoing, null);

            //Act
            Solution solution = sut.GetSolution("AAXXXXBBXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", 7, 6);

            //Assert
            AssertSolution(solutionExpected, solution);
        }

        private void AssertSolution(Solution solutionExpected, Solution solution)
        {                  
            Assert.AreEqual(solutionExpected.SolutionType, solution.SolutionType);
            Assert.AreEqual(solutionExpected.WinType, solution.WinType);
        }

    }
}
