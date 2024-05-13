namespace OnboardingApi.Domain.Dtos
{
	public record QueryResultList<T>
	{
		public required int TotalItems { get; init; } = 0;
        public required List<T> Items { get; init; } = [];
	}
}