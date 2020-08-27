using System;
using System.Collections.Generic;
using System.Linq;
using DynaJson;
using ElectronicObserver.Data;
using ElectronicObserver.Observer;
using ElectronicObserver.Utility.Mathematics;

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
			APIObserver.Instance.ResponseReceived += (apiname, data) =>
			{
				_responses[$"/kcsapi/{apiname}"] = data;
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
					return SerializeList(KCDatabase.Instance.Fleet.Fleets, (fleet, data) =>
					{
						data.api_name = fleet.Name;
						data.api_ship = fleet.Members;
						data.api_mission[0] = fleet.ExpeditionState;
						data.api_mission[1] = fleet.ExpeditionDestination;
						data.api_mission[2] = DateTimeHelper.ToAPITime(fleet.ExpeditionTime);
					});
				case "ships":
					return SerializeDict(KCDatabase.Instance.Ships, (ship, data) =>
					{
						data.api_nowhp = ship.HPCurrent;
						data.api_fuel = ship.Fuel;
						data.api_bull = ship.Ammo;
						data.api_cond = ship.Condition;
						data.api_slot = ship.Slot;
						data.api_slot_ex = ship.ExpansionSlot;
						data.api_onslot = ship.Aircraft;
						// Skipping data.api_kyouka
					});
				case "equips":
					return SerializeDict(KCDatabase.Instance.Equipments);
				case "repairs":
					return SerializeList(KCDatabase.Instance.Docks, (dock, data) =>
					{
						data.api_state = dock.State;
						data.api_ship_id = dock.ShipID;
						data.api_complete_time = DateTimeHelper.ToAPITime(dock.CompletionTime);
					});
				case "constructions":
					return SerializeList(KCDatabase.Instance.Arsenals,
						(arsenal, data) => { data.api_state = arsenal.State; });
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
					return SerializeList(KCDatabase.Instance.BaseAirCorps, (baseAirCorps, data) =>
					{
						data.api_name = baseAirCorps.Name;
						data.api_action_kind = baseAirCorps.ActionKind;
					});
				case "preSets":
					return $"{{\"api_deck\":{SerializeDict(KCDatabase.Instance.FleetPreset.Presets)}}}";
			}

			throw new NotImplementedException();
		}

		private string PrepareSortieData()
		{
			dynamic json = new JsonObject();
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
			dynamic json = new JsonObject();
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
			json.result.mapCell = battleManager.Compass?.Destination;

			string serialized = json.ToString(); // Some issue in dynamic dispatch forced us to do this :(
			return serialized;
		}

		private string PrepareMiscData()
		{
			dynamic json = new JsonObject();
			json.combinedFleet = KCDatabase.Instance.Fleet.CombinedFlag > 0;
			json.combinedFleetType = KCDatabase.Instance.Fleet.CombinedFlag;
			return json.ToString();
		}

		// Manually serialize stuff because DynamicJson does not play well with number-indexed objects.
		private static string SerializeDict<TData>(IDDictionary<TData> dictionary,
			Action<TData, dynamic> transformer = null)
			where TData : ResponseWrapper, IIdentifiable
		{
			var serialized =
				dictionary.Select(e => $"\"{e.Key}\":{SerializeWithTransformer(e.Value, transformer)}");
			return $"{{{string.Join(",", serialized)}}}";
		}

		private static string SerializeList<TData>(IDDictionary<TData> dictionary,
			Action<TData, dynamic> transformer = null)
			where TData : ResponseWrapper, IIdentifiable
		{
			var serialized = dictionary.Select(e => SerializeWithTransformer(e.Value, transformer));
			return $"[{string.Join(",", serialized)}]";
		}

		private static string SerializeDict(Dictionary<string, dynamic> dictionary)
		{
			var serialized = dictionary.Select(e => $"\"{e.Key}\":{e.Value.ToString()}");
			return $"{{{string.Join(",", serialized)}}}";
		}

		private static string SerializeWithTransformer<TData>(TData data, Action<TData, dynamic> transformer)
			where TData : ResponseWrapper, IIdentifiable
		{
			if (transformer == null)
			{
				return data.RawData.ToString();
			}

			var rawData = Plugin.Singleton.Settings.SkipCopyRawData
				? data.RawData
				: JsonObject.Parse(data.RawData.ToString());
			transformer(data, rawData);
			return rawData.ToString();
		}
	}
}
