using Core.Utilities.Pagination;

namespace Auth.Core.Utilities.Pagination
{
    public class PaginationHelper
    {
        /// <summary>
        /// Calculates the offset and limit(fetch) values
        /// </summary>
        /// <param name="offset">Offset number</param>
        /// <param name="limit">Equals to the fetch number</param>
        public static (int offset, int limit) Calculate(ref PagingOptions pagingOptions) => new()
        {
            offset = pagingOptions!.Start - 1,
            limit = pagingOptions.End - pagingOptions.Start + 1
        };

        /// <summary>
        /// Prepares limit string
        /// </summary>
        /// <param name="pagingOptions">Pagination options</param>
        /// <param name="sourceType">Db source type</param>
        public static string? LimitQuery(PagingOptions? pagingOptions)
        {
            if (pagingOptions == null || (pagingOptions.Start == 0 && pagingOptions.End == 0))
                return null;

            (int offset, int limit) calculationResults = Calculate(ref pagingOptions!);

            return TSqlLimitQuery(ref calculationResults);
        }

        #region Private methods
        private static string TSqlLimitQuery(ref (int offset, int limit) calculationResults)
        {
            return $" OFFSET {calculationResults.offset} ROWS FETCH NEXT {calculationResults.limit} ROWS ONLY";
        }
        #endregion
    }
}
