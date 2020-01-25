namespace WordCounterService.UseCases.Commands.CountTopRepeatingWords
{
    /// <summary>
    /// View model for returning top to end-user.
    /// </summary>
    public class TopRepeatingWordsViewModel
    {
        /// <summary>
        /// Word.
        /// </summary>
        public string Word { get; }

        /// <summary>
        /// Count of appearances.
        /// </summary>
        public int AppearancesCount { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="word">Word.</param>
        /// <param name="appearancesCount">Count of appearances.</param>
        public TopRepeatingWordsViewModel(string word, int appearancesCount)
        {
            Word = word;
            AppearancesCount = appearancesCount;
        }
    }
}
