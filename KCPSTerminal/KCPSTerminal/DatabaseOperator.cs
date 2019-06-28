using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codeplex.Data;
using ElectronicObserver.Data;
using ElectronicObserver.Observer;

namespace KCPSTerminal
{
	class DatabaseOperator
	{
		public static DatabaseOperator Singleton = new DatabaseOperator();

		private Dictionary<string, dynamic> _responses;

		private DatabaseOperator()
		{
			_responses = new Dictionary<string, dynamic>();
		}

		public void StartObserver()
		{
			APIObserver.Instance.ResponseReceived += (apiname, data) => { _responses[$"/kcsapi/{apiname}"] = data; };
		}

		public string HandleData(string type)
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

		public string HandleResponse(string type)
		{
			return _responses[type].ToString();
		}
	}
}