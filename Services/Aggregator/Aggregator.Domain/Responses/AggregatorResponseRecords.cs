using Aggregator.Domain.Components.DTO;
using Prices.DTOs.Messages;

namespace Aggregator.Domain.Response
{
    public class AggregatorResponseRecords
    {
        public record ComponentResponse(
            string Component,
            string? Error);

        public record ReviewResponse(
            string Review,
            string? Error);

        public record ReviewsMetricResponse(
            double AvgLocal,
            double AvgGlobal,
            int TotalLocalReviews,
            int TotalGlobalReviews);

        public record ComponentSpecsResponse(
            Dictionary<string, Dictionary<string, string?>>? Specs);

        public record SpecGroupsResponse(
            Dictionary<string, List<string?>>? Groups);

        public record FullComponentResponse(
            ComponentDto? Component,
            LatestLowestComponentPriceMessage? LastPrice);
    }
}