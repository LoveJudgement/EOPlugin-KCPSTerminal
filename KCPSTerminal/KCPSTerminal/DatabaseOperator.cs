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
					// TODO: This behaves differently
					return KCDatabase.Instance.Material.RawData.ToString();
				case "maps":
					// TODO: EO currently does not maintain data from api_get_member/mapinfo
					throw new NotImplementedException();
				case "sortie":
					return PrepareSortieData();
				case "battle":
					// TODO
					return PrepareBattleData();
				case "miscellaneous":
					return PrepareMiscData();
				case "landBasedAirCorps":
					return SerializeList(KCDatabase.Instance.BaseAirCorps);
				case "preSets":
					throw new NotImplementedException(); // TODO
			}

			throw new NotImplementedException();
		}

		private string PrepareSortieData()
		{
			dynamic json = new DynamicJson();
			var escapedPos = new List<int>();

			var posOffset = 0;
			foreach (var fleetData in KCDatabase.Instance.Fleet.Fleets.Values.Where(fleet => fleet.IsInSortie))
			{
				for (var i = 0; i < fleetData.Members.Count; i++)
				{
					if (fleetData.EscapedShipList.Contains(fleetData.Members[i]))
					{
						escapedPos.Add(i + posOffset);
					}
				}

				posOffset += 6;
			}

			json.escapedPos = escapedPos; // TODO: Unverified!

			// Our lifecycle seems to be the same with Poi's ;)
			json.currentNode = KCDatabase.Instance.Battle.Compass.Destination;

			return json.ToString();
		}

		private string PrepareBattleData()
		{
			dynamic json = new DynamicJson();
			json.result = new { };

			return json.toString();
		}

		private string PrepareMiscData()
		{
			dynamic json = new DynamicJson();
			json.combinedFleet = KCDatabase.Instance.Fleet.CombinedFlag > 0;
			return json.ToString();
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
