using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace schedule_app {
	public partial class MainWindow {
		private readonly Database _db;
		private readonly DateTime _default_date = DateTime.Today;
		private const string _default_event_name = "Event name";

		public MainWindow() {
			InitializeComponent();

			_db = new Database(null);

			init_names([
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
			init_events();

			date_picker.SelectedDate = _default_date;
		}

		private void init_names(string[] names) {
			_db.init_values(names);
			members_combo_box.ItemsSource = names;
			members_combo_box.SelectedIndex = 0;
		}
		private void init_events() {
			_db.add_event("@everyone", new DateTime(2025, 4, 5), "Fanmeeting in Seoul");
		}

		private void filter_on_click_event(object sender, RoutedEventArgs e) {
			var status = check_box.IsChecked ?? false;
			var events = status ?
				_db.get_events((string) members_combo_box.SelectedItem, date_picker.SelectedDate ?? _default_date) :
				_db.get_events((string) members_combo_box.SelectedItem);

			list_box.Items.Clear();
			foreach(var it in events)
				list_box.Items.Add(_db.get_member_by_id(it.member_id) + " " + it.date + " " + it.event_name);
		}

		private void add_entry_on_click_event(object sender, RoutedEventArgs e) {
			if (text_box.Text == _default_event_name)
				return;

			_db.add_event((string) members_combo_box.SelectedItem, date_picker.SelectedDate ?? _default_date, text_box.Text);
			text_box.Text = _default_event_name;
		}
	}
}
