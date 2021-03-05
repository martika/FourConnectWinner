using ConnectFourWinner.Services.Interfaces;
using ConnectFourWinner.Services.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ConnectFourWinner.Services.Test
{

    [TestClass]
    public class PlayValidatorTest
    {
        private IServiceConnectFour service;

        private IPlayValidator sut;

        [TestInitialize]
        public void SetUp()
        {
            CreateSut();
        }

        private void CreateSut()
        {
            sut = new PossiblePlayValidator();
            service = new ServiceConnectFour(new List<IValidator>(), sut);
        }

        [TestMethod]
        public void ErrorInputLengthTest()
        {
            //Arrange
            string message = $"{ResultValidation.ErrorPositionPieces}";

            //Act
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => service.GetSolution("ABBBXBXAABXXXXAAXXXXXAXXXXXXXXXXXXXXXXXXXX", 7, 6));

            //Assert
            Assert.AreEqual(message, ex.Message);
        }

    }
}
