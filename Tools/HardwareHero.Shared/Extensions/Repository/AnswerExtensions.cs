using HardwareHero.Shared.Repositories.Answers;

namespace HardwareHero.Shared.Extensions.Repository
{
    public static class AnswerExtensions
    {
        /// <summary>
        /// Check exceptions and `IsSuccess` status.
        /// Doesn't affect the value itself
        /// </summary>
        public static void DataAnswerCheck<T>(
            this Answer<T> answer) where T : class
        {
            if (answer.Errors.Any())
            {
                throw answer.Errors.First();
            }

            if (answer.IsSuccess)
            {
                throw new InvalidOperationException("Find 0 errors, but answer is negative!");
            };
        }
    }
}
