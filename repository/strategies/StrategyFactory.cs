using System;

namespace funda.repository.strategies
{
	public class StrategyFactory<T> : IStrategyFactory<T>
	{
		public ICreateStrategy<T> Create { get; set; }
		public IReadAllStrategy<T> ReadAll { get; set; }
		public IReadStrategy<T> Read { get; set; }
		public IUpdateStrategy<T> Update { get; set; }
		public IDeleteStrategy<T> Delete { get; set; }

		public StrategyFactory(
			ICreateStrategy<T> createStrategy,
			IReadAllStrategy<T> readAllStrategy,
			IReadStrategy<T> readStrategy,
			IUpdateStrategy<T> updateStrategy,
			IDeleteStrategy<T> deleteStrategy
		)
		{
			this.Create  = createStrategy;
			this.ReadAll = readAllStrategy;
			this.Read    = readStrategy;
			this.Update  = updateStrategy;
			this.Delete  = deleteStrategy;
		}
	}
}