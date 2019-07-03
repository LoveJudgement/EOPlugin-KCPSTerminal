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

		private readonly Dictionary<string, dynamic> _presets = new Dictionary<string, dynamic>();

		private DatabaseOperator()
		{
		}

		internal void StartObserver()
		{
			APIObserver.Instance.ResponseReceived += (apiname, data) =>
			{
				_responses[$"/kcsapi/{apiname}"] = data;

				switch (apiname)
				{
					case "api_get_member/preset_deck":
						_presets.Clear();
						foreach (var elem in data.api_deck)
						{
							_presets[elem.Value.api_preset_no.ToString()] = elem.Value;
						}

						break;
					case "api_req_hensei/preset_register":
						_presets[data.api_preset_no.ToString()] = data;
						break;
				}
			};

			APIObserver.Instance.RequestReceived += (apiname, data) =>
			{
				switch (apiname)
				{
					case "api_req_hensei/preset_delete":
						_presets.Remove(data["api_preset_no"]);
						break;
				}
			};
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
					return PrepareBattleData();
				case "miscellaneous":
					return PrepareMiscData();
				case "landBasedAirCorps":
					return SerializeList(KCDatabase.Instance.BaseAirCorps);
				case "preSets":
					return $"{{\"api_deck\":{SerializeDict(_presets)}}}";
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
			json.currentNode = KCDatabase.Instance.Battle?.Compass?.Destination;

			string serialized = json.ToString();
			return serialized;
		}

		private string PrepareBattleData()
		{
			dynamic json = new DynamicJson();
			json.result = new { };

			var battleManager = KCDatabase.Instance.Battle;

			var battle = battleManager.SecondBattle ?? battleManager.FirstBattle;
			if (battle != null)
			{
				// TODO: Verify if this works for combined fleet.
				var friendCount = battle.Initial.FriendFleet.Members.Count +
				                  (battle.IsFriendCombined ? battle.Initial.FriendFleetEscort.Members.Count : 0);
				json.result.deckHp = battle.ResultHPs.Take(friendCount).Select(i => i < 0 ? 0 : i).ToArray();

				// Enemy fleet
				var enemyShips = battle.Initial.EnemyMembers.Where(i => i > 0).ToList();
				var enemyHp = battle.ResultHPs.Skip(12).Take(enemyShips.Count).ToList();

				// Enemy escort fleet
				var enemyEscortShips =
					battle.IsEnemyCombined
						? battle.Initial.EnemyMembersEscort.Where(i => i > 0).ToList()
						: new List<int>();
				var enemyEscortHp = battle.ResultHPs.Skip(18).Take(enemyEscortShips.Count).ToList();

				json.result.enemyShipId = enemyShips.Concat(enemyEscortShips).ToArray();
				json.result.enemyHp = enemyHp.Concat(enemyEscortHp).ToArray();
			}

			// TODO: our lifecycle is different than Poi! This is updated in api_req_map/start and api_req_map/next.
			json.result.mapCell = battleManager?.Compass?.Destination;

			string serialized = json.ToString(); // Some issue in dynamic dispatch forced us to do this :(
			return serialized;
		}

		private string PrepareMiscData()
		{
			dynamic json = new DynamicJson();
			json.combinedFleet = KCDatabase.Instance.Fleet.CombinedFlag > 0;
			return json.ToString();
		}

		// Manually serialize stuff because DynamicJson does not play well with number-indexed objects.
		private static string SerializeDict<TData>(IDDictionary<TData> dictionary)
			where TData : ResponseWrapper, IIdentifiable
		{
			var serialized = dictionary.Select(e => $"\"{e.Key}\":{e.Value.RawData.ToString()}");
			return $"{{{string.Join(",", serialized)}}}";
		}

		private static string SerializeList<TData>(IDDictionary<TData> dictionary)
			where TData : ResponseWrapper, IIdentifiable
		{
			var serialized = dictionary.Select(e => e.Value.RawData.ToString());
			return $"[{string.Join(",", serialized)}]";
		}

		private static string SerializeDict(Dictionary<string, dynamic> dictionary)
		{
			var serialized = dictionary.Select(e => $"\"{e.Key}\":{e.Value.ToString()}");
			return $"{{{string.Join(",", serialized)}}}";
		}
	}
}
