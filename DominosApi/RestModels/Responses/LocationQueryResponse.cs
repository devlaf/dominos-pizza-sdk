using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DominosApi
{
	[JsonObject]
	public class LocationQueryResponse
	{
		private LocationQueryResponse() { }

		public Address Address { get; private set; }

		public string Granularity { get; private set; }

		public int Status { get; private set; }

		public List<Store> Stores { get; private set; }
	}
}

