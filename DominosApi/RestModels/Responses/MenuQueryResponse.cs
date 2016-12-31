using System;
using System.Collections.Generic;

namespace DominosApi
{
	/// <remarks>
	/// The menu is the most dynamic and least intuitive part of the Dominos API.  It's dangerous
	/// to invest a lot of energy into this, as will likely change at the whim of the engineers over
	/// at dominoes (which is completely fair, as this is not actually a public, documented api.) 
	/// The implementation here is due for a major overhaul, but for now I'm gonna focus on the 
	/// "Preconfigured Products" section.
	/// </remarks>
	public class MenuQueryResponse
	{
		private MenuQueryResponse() { }

		public Dictionary<string, PreconfiguredProduct> PreconfiguredProducts { get; private set; }
	}

	public class PreconfiguredProduct
	{
		public int? SortQeq { get; private set; }
		public string Code { get; private set; }
		public string Tags { get; private set; }
		public string ReferenceProductCode { get; private set; }
		public string PreconfiguredProductOptions { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
	}
}

