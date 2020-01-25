namespace WordCounterService.Web.ViewModels
{
    /// <summary>
    /// A view model for displaying a user-friendly error.
    /// </summary>
    public class DomainExceptionViewModel
    {
        /// <summary>
        /// Status code.
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Error message.
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <param name="errorMessage">Error message.</param>
        public DomainExceptionViewModel(int statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
    }
}
