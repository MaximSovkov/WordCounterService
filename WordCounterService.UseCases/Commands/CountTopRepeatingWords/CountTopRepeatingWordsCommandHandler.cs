using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WordCounterService.Domain.Exceptions;

namespace WordCounterService.UseCases.Commands.CountTopRepeatingWords
{
    /// <summary>
    /// Command handler to count the top repeating words.
    /// </summary>
    public class CountTopRepeatingWordsCommandHandler : IRequestHandler<CountTopRepeatingWordsCommand,
        IReadOnlyCollection<TopRepeatingWordsViewModel>>
    {
        private static readonly IReadOnlyCollection<string> SupportedContentTypes = new List<string>
        {
            "text/plain"
        };

        /// <summary>
        /// Count the number of repeating words in a file.
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of words with the number of occurrences sorted in descending order.</returns>
        public async Task<IReadOnlyCollection<TopRepeatingWordsViewModel>> Handle(CountTopRepeatingWordsCommand request,
            CancellationToken cancellationToken)
        {
            var textFile = request.TextFile;
            var topCount = request.TopCount;

            if (topCount < 1)
            {
                throw new WordCounterServiceException(
                    "The number of records in the top list should be at least 1.");
            }

            if (!SupportedContentTypes.Contains(textFile.ContentType))
            {
                throw new WordCounterServiceException(
                    $"The file content type is not supported. Supported content types: {string.Join(',', SupportedContentTypes)}.");
            }

            if (textFile.Length == 0)
            {
                throw new WordCounterServiceException(
                    "The file is empty.");
            }

            var fileContents = await GetFileContents(request.TextFile);
            var repeatingWordsTop = CountTopRepeatingWords(fileContents, topCount);
            if (!repeatingWordsTop.Any())
            {
                throw new WordCounterServiceException(
                    "No words were found in the file.");
            }

            return repeatingWordsTop;
        }

        private async Task<string> GetFileContents(IFormFile textFile)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(textFile.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    result.AppendLine(await reader.ReadLineAsync());
                }
            }

            return result.ToString();
        }

        private IReadOnlyCollection<TopRepeatingWordsViewModel> CountTopRepeatingWords(string fileContents, int topCount)
        {
            /*
             * Split the string into words, trim all the characters that are not letters from the ends of the words,
             * discard all empty lines that turned out after that, group result, and take the specified count.
             */

            return fileContents
                .Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(str => str.Trim(str.Where(ch => !char.IsLetter(ch)).ToArray()).ToUpperInvariant())
                .Where(str => !string.IsNullOrWhiteSpace(str))
                .GroupBy(str => str).Select(str => new TopRepeatingWordsViewModel(str.Key, str.Count()))
                .OrderByDescending(vm => vm.AppearancesCount).Take(topCount).ToList();
        }
    }
}
