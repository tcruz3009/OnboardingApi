namespace OnboardingApi.Domain.Models.Queries
{
    public class OnboardingQuery : Query
    {
        public int? TotverId { get; set; }
        public int? PadrinhoId { get; set; }

        public OnboardingQuery(int? padrinhoId, int? totverId, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            TotverId = totverId;
            PadrinhoId = padrinhoId;

        }
    }
}