using ConnectFourWinner.Domain.Entities;
using ConnectFourWinner.Services.Interfaces;
using ConnectFourWinner.Services.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ConnectFourWinner.Services.Test
{
    [TestClass]
    public class ValidatorTest
    {
        private IServiceConnectFour service;
        private IList<IValidator> sut;
        private Mock<IPlayValidator> possiblePlayValidator;

        [TestInitialize]
        public void SetUp()
        {
            possiblePlayValidator = new Mock<IPlayValidator>();
            CreateSut();
            possiblePlayValidator.Setup(v => v.Validate(It.IsAny<Board>(), It.IsAny<ResultValidation>())).Returns(new ResultValidation() { IsValid = true });
        }

        private void CreateSut()
        {
            sut = new List<IValidator>();
            sut.Add(new LengthValidator());
            sut.Add(new BoardValidator());
            sut.Add(new TeamNumberPiecesValidator());
            sut.Add(new SymbolPiecesValidator());                  
            service = new ServiceConnectFour(sut, possiblePlayValidator.Object);           
        }

        [TestMethod]
        public void ErrorInputLengthTest()
        {
            //Arrange
            string message = $"{ResultValidation.ErrorInputLength}";

            //Act
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => service.GetSolution("AAXXXXBBXXXXXXXXXXXXXXXXXXXX", 7, 6));

            //Assert
            Assert.AreEqual(message, ex.Message);
        }

        [TestMethod]
        public void ErrorMinSizeBoardTest()
        {
            //Arrange
            string message = $"{ResultValidation.ErrorBoardMinSize}";

            //Act
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => service.GetSolution("XXXXXXXXXXXXXXXX", 4, 4));

            //Assert
            Assert.AreEqual(message, ex.Message);
        }

        [TestMethod]
        public void ErrorTeamNumberOfPiecesTest()
        {
            //Arrange
            string message = $"{ResultValidation.ErrorTeamNumberPieces}";

            //Act
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => service.GetSolution("AAAAXXBBXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", 7, 6));

            //Assert
            Assert.AreEqual(message, ex.Message);
        }


        [TestMethod]
        public void ErrorSymbolTest()
        {
            //Arrange
            string message = $"{ResultValidation.ErrorSymbolPieces}";

            //Act
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => service.GetSolution("AAXXXXBCXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", 7, 6));

            //Assert
            Assert.AreEqual(message, ex.Message);
        }

        [TestMethod]
        public void ShowBoardTest_All_Invalid()
        {
            //Arrange
            string message1 = $"{ResultValidation.ErrorTeamNumberPieces}";
            string message2 = $"{ResultValidation.ErrorBoardMinSize}";
            string message3 = $"{ResultValidation.ErrorInputLength}";
            string message4 = $"{ResultValidation.ErrorSymbolPieces}";

            //Act
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => service.GetSolution("AAAAXXBBCXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", 4, 4));

            //Assert
            Assert.IsTrue(ex.Message.Contains(message1));
            Assert.IsTrue(ex.Message.Contains(message2));
            Assert.IsTrue(ex.Message.Contains(message3));
            Assert.IsTrue(ex.Message.Contains(message4));
        }


    }
}
