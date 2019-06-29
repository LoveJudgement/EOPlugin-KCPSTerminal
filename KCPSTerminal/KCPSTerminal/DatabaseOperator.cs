using System;
using System.Collections.Generic;
using System.Linq;
using ElectronicObserver.Data;
using ElectronicObserver.Observer;

namespace KCPSTerminal
{
	internal class DatabaseOperator
	{
		internal static DatabaseOperator Singleton = new DatabaseOperator();

		private readonly Dictionary<string, dynamic> _responses = new Dictionary<string, dynamic>();

		private DatabaseOperator()
		{
		}

		internal void StartObserver()
		{
			APIObserver.Instance.ResponseReceived += (apiname, data) => { _responses[$"/kcsapi/{apiname}"] = data; };
		}

		internal string HandleData(string type)
		{
			switch (type)
			{
				case "fleets":
					return KCDatabase.Instance.Fleet.RawData.ToString();
				case "ships":
					// Manually serialize it because DynamicJson does not play well with number-indexed objects.
					var serializedShips =
						KCDatabase.Instance.Ships.Select(ship => $"\"{ship.Key}\":{ship.Value.RawData.ToString()}");
					return $"{{{string.Join(",", serializedShips)}}}";
				case "equips":
					// Manually serialize it because DynamicJson does not play well with number-indexed objects.
					var serializedEquipments = KCDatabase.Instance.Equipments.Select(equipment =>
						$"\"{equipment.Key}\":{equipment.Value.RawData.ToString()}");
					return $"{{{string.Join(",", serializedEquipments)}}}";
				case "repairs":
					// Manually serialize it too.
					var serializedDocks = KCDatabase.Instance.Docks.Select(dock => dock.Value.RawData.ToString());
					return $"[{string.Join(",", serializedDocks)}]";
			}

			throw new NotImplementedException();
		}

		internal string HandleResponse(string type)
		{
			return _responses[type].ToString();
		}
	}
}
