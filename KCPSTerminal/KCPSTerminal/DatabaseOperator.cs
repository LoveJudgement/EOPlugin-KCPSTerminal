using System;
using System.Collections.Generic;
using System.Linq;
using Codeplex.Data;
using ElectronicObserver.Data;
using ElectronicObserver.Observer;

namespace KCPSTerminal
{
	internal class DatabaseOperator
	{
		internal static readonly DatabaseOperator Singleton = new DatabaseOperator();

		private readonly Dictionary<string, dynamic> _responses = new Dictionary<string, dynamic>();

		private DatabaseOperator()
		{
		}

		internal void StartObserver()
		{
			APIObserver.Instance.ResponseReceived += (apiname, data) => { _responses[$"/kcsapi/{apiname}"] = data; };
		}

		internal string HandleResponse(string type)
		{
			return _responses[type].ToString();
		}

		internal string HandleData(string type)
		{
			switch (type)
			{
				case "const":
					throw new NotImplementedException(); // TODO
				case "basic":
					return KCDatabase.Instance.Admiral.RawData.ToString();
				case "fleets":
					return KCDatabase.Instance.Fleet.RawData.ToString();
				case "ships":
					return SerializeDict(KCDatabase.Instance.Ships);
				case "equips":
					return SerializeDict(KCDatabase.Instance.Equipments);
				case "repairs":
					return SerializeList(KCDatabase.Instance.Docks);
				case "constructions":
					return SerializeList(KCDatabase.Instance.Arsenals);
				case "resources":
					return KCDatabase.Instance.Material.RawData.ToString();
				case "maps": // EO currently does not maintain data from api_get_member/mapinfo
				case "sortie":
				case "battle":
					throw new NotImplementedException(); // TODO
				case "miscellaneous":
					dynamic json = new DynamicJson();
					json.combinedFleet = KCDatabase.Instance.Fleet.CombinedFlag > 0;
					return json.ToString();
				case "landBasedAirCorps":
					return SerializeList(KCDatabase.Instance.BaseAirCorps);
				case "preSets":
					throw new NotImplementedException(); // TODO
			}

			throw new NotImplementedException();
		}


		// Manually serialize it because DynamicJson does not play well with number-indexed objects.
		private static string SerializeDict<TData>(IDDictionary<TData> dictionary)
			where TData : ResponseWrapper, IIdentifiable
		{
			var serialized = dictionary.Select(e => $"\"{e.Key}\":{e.Value.RawData.ToString()}");
			return $"{{{string.Join(",", serialized)}}}";
		}

		// Manually serialize it because DynamicJson does not play well with number-indexed objects.
		private static string SerializeList<TData>(IDDictionary<TData> dictionary)
			where TData : ResponseWrapper, IIdentifiable
		{
			var serialized = dictionary.Select(e => e.Value.RawData.ToString());
			return $"[{string.Join(",", serialized)}]";
		}
	}
}
