namespace schedule_app {
	internal class EventPole(uint id, DateTime date, string event_name) {
		public uint member_id = id;
		public DateTime date = date;
		public string event_name = event_name;
	}

	internal class Database {
		private readonly Dictionary<string, uint> _members_by_name;
		private readonly Dictionary<uint, string> _members_by_id;
		private readonly List<EventPole> _events;

		public Database(string[]? members_collection) {
			_members_by_name = new Dictionary<string, uint>();
			_members_by_id = new Dictionary<uint, string>();
			_events = [];

			if (members_collection is not null)
				init_values(members_collection);
		}

		public void init_values(string[] members_collection) {
			_members_by_name.Clear();
			foreach (var member in members_collection) {
				_members_by_name.Add(member, (uint) _members_by_name.Count);
				_members_by_id.Add((uint) _members_by_id.Count, member);
			}
		}

		public void add_event(string member_name, DateTime date, string event_name) {
			_events.Add(new EventPole(_members_by_name[member_name], date, event_name));
		}

		public IEnumerable<EventPole> get_events(string member_name) {
			return member_name != "@all" ? 
				_events.Where(pole => pole.member_id == _members_by_name[member_name]) : 
				_events;
		}
		public IEnumerable<EventPole> get_events(string member_name, DateTime date) {
			return member_name != "@all" ?
				_events.Where(pole => pole.member_id == _members_by_name[member_name] && check_dates(pole.date, date)) :
				_events.Where(pole => check_dates(pole.date, date));
		}

		public string get_member_by_id(uint id) {
			return _members_by_id[id];
		}

		private static bool check_dates(DateTime lhs, DateTime rhs) {
			return lhs.Year == rhs.Year && lhs.Month == rhs.Month && lhs.Day == rhs.Day;
		}
	}
}
