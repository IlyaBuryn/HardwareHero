using Aggregator.DTOs.Components;

namespace Aggregator.DTOs.Response
{
    public class AggregatorResponseRecords
    {
        public record CreationOfManyResponse(
            Dictionary<string, string> values);

        public record ReviewsMetricResponse(
            double avgLocal,
            double avgGlobal,
            int totalLocalReviews,
            int totalGlobalReviews);

        public record ComponentSpecsResponse(
            Dictionary<string, Dictionary<string, string?>>? specs);

        public record SpecGroupsResponse(
            Dictionary<string, List<string>> groups);

        public record FullComponentResponse(
            ComponentDto component,
            decimal lastPrice);
    }
}