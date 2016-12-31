using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp.Deserializers;

namespace DominosApi
{
	public static class RestUtils
	{
		public enum ResponseBodyType
		{
			JSON,
			XML
		}

		#region SendRestRequest

		/// <summary>
		/// Send the specified RestSharp request and deserialize the response into the type provided.
		/// </summary>
		/// <returns>A populated object of type T.</returns>
		/// <param name="client">RestSharp client struct</param>
		/// <param name="request">RestSharp request struct</param>
		/// <param name="format">The expected format of the body of the response</param>
		/// <param name="log">An optional delegate, which will be applied to log messages.</param>
		/// <param name="throwOnError">Should we throw if the result of the http call was successful? If 
		/// this is false, the method simply logs the error and returns null.  If true, a 
		/// RestRequestFailureException is thrown.</param>
		/// <typeparam name="T">The response object into which we will deserialize the content of the 
		/// HTTP response.</typeparam>
		/// <exception cref="AggregateException">In the case of a non-200 response (and throwOnError==false),
		/// this method will return an AggregateException which wraps a RestRequestFailureException.</exception>
		public static async Task<T> SendRestRequest<T>(RestClient client, RestRequest request, 
			ResponseBodyType format = ResponseBodyType.JSON, Action<string> log = null, bool throwOnError = true)
		{
			return await Task.Factory.StartNew<T>( () => 
					{
						var response = SendRestRequestSyncronous(client, request, log, throwOnError);
						return DeserializeRestResponse<T>(response, format, log, throwOnError);
					}
			);
		}

		private static IRestResponse SendRestRequestSyncronous(RestClient client, RestRequest request, 
			Action<string> log, bool throwOnError)
		{
			var response = client.Execute(request);

			if(response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				var error = string.Format("HTTP request failed with status code:[{0}].  Message: [{1}]", 
					response.StatusCode, response.ErrorMessage);

				if (log != null)
					log(error);

				if (throwOnError)
					throw new RestRequestFailureException(error);
			}

			return response;
		}

		#endregion

		#region PackageRestRequest

		public static RestRequest PackageRestRequest(string requestUri, Method httpVerb, object body = null, 
			Dictionary<string, string> headers = null)
		{
			var request = new RestRequest(requestUri, httpVerb);

			if(body != null)
			{
				string serializedBody = JsonConvert.SerializeObject(body, GenerateSerializationSettings());
				request.Parameters.Clear();
				request.AddParameter("application/json", serializedBody, ParameterType.RequestBody);
			}

			if(headers != null)
				headers.Select(x => request.AddHeader(x.Key, x.Value));

			return request;
		}

		#endregion

		#region (De)Serialization

		private static T DeserializeRestResponse<T>(IRestResponse response, ResponseBodyType format, 
			Action<string> log, bool throwOnError)
		{
			try
			{
				var responsePayload = response.Content;

				if (format == ResponseBodyType.XML)
					responsePayload = JsonConvert.SerializeXNode(XDocument.Parse(response.Content));  // gross.

				return JsonConvert.DeserializeObject<T>(responsePayload, GenerateSerializationSettings());
			}
			catch(Exception ex)
			{
				var error = string.Format("Deserialization of rest response failed.  Response Content:[{0}].  " +
					"Exception Message: [{1}]", response.Content, ex.Message);

				if (log != null)
					log(error);

				if(throwOnError)
					throw new RestRequestFailureException(error, ex);

				return default(T);
			}
		}

		private static JsonSerializerSettings GenerateSerializationSettings()
		{
			var contractResolver = new CustomContractResolver();
			return new JsonSerializerSettings
			{
				ContractResolver = contractResolver
			};
		}

		/// <summary>
		/// Modifies the default Json.Net behavior to allow for deserialization of private 
		/// properties of an object.
		/// </summary>
		private class CustomContractResolver : DefaultContractResolver
		{
			protected override JsonProperty CreateProperty(MemberInfo member, 
				MemberSerialization memberSerialization)
			{
				var prop = base.CreateProperty(member, memberSerialization);

				if (!prop.Writable)
				{
					var property = member as PropertyInfo;
					if (property != null)
					{
						var hasPrivateSetter = property.GetSetMethod(true) != null;
						prop.Writable = hasPrivateSetter;
					}
				}

				return prop;
			}
		}

		#endregion
	}
}

