using System.Windows;

namespace schedule_app {
	public partial class MainWindow {
		private readonly Database _db;
		private static readonly DateTime DefaultDate = DateTime.Today;
		private const string DefaultEventName = "Event name";

		public MainWindow() {
			InitializeComponent();

			_db = new Database(null);

			InitNames([
				"@all",
				"@everyone",
				"S.Coups",
				"Jeonghan",
				"Joshua",
				"Jun",
				"Hoshi",
				"Wonwoo",
				"Woozi",
				"The8",
				"Mingyu",
				"DK",
				"Seungkwan",
				"Vernon",
				"Dino"
			]);
			InitEvents();

			DatePicker.SelectedDate = DefaultDate;
		}

		private void InitNames(string[] names) {
			_db.InitValues(names);
			MembersComboBox.ItemsSource = names;
			MembersComboBox.SelectedIndex = 0;
		}

		private void InitEvents() {
			_db.AddEvent("@everyone", new DateTime(2025, 4, 5), "Fanmeeting in Seoul");
		}

		private void FilterOnClickEvent(object sender, RoutedEventArgs e) {
			var status = CheckBox.IsChecked ?? false;
			var events =
				(status
					? _db.GetEvents((string)MembersComboBox.SelectedItem, DatePicker.SelectedDate ?? DefaultDate)
					: _db.GetEvents((string)MembersComboBox.SelectedItem)).ToList();

			ListBox.Items.Clear();
			foreach (var it in events)
				ListBox.Items.Add(_db.GetMemberById(it.MemberId) + " " + it.Date + " " + it.EventName);
		}

		private void AddEntryOnClickEvent(object sender, RoutedEventArgs e) {
			if (TextBox.Text == DefaultEventName)
				return;

			_db.AddEvent((string)MembersComboBox.SelectedItem, DatePicker.SelectedDate ?? DefaultDate,
				TextBox.Text);
			TextBox.Text = DefaultEventName;
		}
	}
}
