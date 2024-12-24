namespace schedule_app {
	internal struct EventPole(uint id, DateTime date, string eventName) {
		public uint MemberId = id;
		public DateTime Date = date;
		public string EventName = eventName;
	}

	internal class Database {
		private readonly Dictionary<string, uint> _membersByName;
		private readonly Dictionary<uint, string> _membersById;
		private readonly List<EventPole> _events;

		public Database(string[]? membersCollection) {
			_membersByName = new Dictionary<string, uint>();
			_membersById = new Dictionary<uint, string>();
			_events = [];

			if (membersCollection is not null)
				InitValues(membersCollection);
		}

		public void InitValues(string[] membersCollection) {
			_membersByName.Clear();
			foreach (var member in membersCollection) {
				_membersByName.Add(member, (uint)_membersByName.Count);
				_membersById.Add((uint)_membersById.Count, member);
			}
		}

		public void AddEvent(string memberName, DateTime date, string eventName) {
			_events.Add(new EventPole(_membersByName[memberName], date, eventName));
		}

		public IEnumerable<EventPole> GetEvents(string memberName) {
			return memberName != "@all" ? _events.Where(pole => pole.MemberId == _membersByName[memberName]) : _events;
		}

		public IEnumerable<EventPole> GetEvents(string memberName, DateTime date) {
			return memberName != "@all"
				? _events.Where(pole => pole.MemberId == _membersByName[memberName] && CheckDates(pole.Date, date))
				: _events.Where(pole => CheckDates(pole.Date, date));
		}

		public string GetMemberById(uint id) {
			return _membersById[id];
		}

		private static bool CheckDates(DateTime lhs, DateTime rhs) {
			return lhs.Year == rhs.Year && lhs.Month == rhs.Month && lhs.Day == rhs.Day;
		}
	}
}
