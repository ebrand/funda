using System;
namespace funda.common.auditing
{
	public interface IDeletable
	{
		bool DeleteFlag { get; set; }
	}
}