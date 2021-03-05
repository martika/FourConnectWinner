namespace ConnectFourWinner.Domain.Entities.Enums
{
    public enum SolutionType
    {
        /// <summary>
        /// Team A has won
        /// </summary>
        A_Winner,
        /// <summary>
        /// Team B has won
        /// </summary>
        B_Winner,
        /// <summary>
        /// There is no winner yet
        /// </summary>
        Ongoing,
        /// <summary>
        /// Tie, no winner 
        /// </summary>
        Tie
    }
}
