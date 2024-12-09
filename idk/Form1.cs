using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace idk
{
    public partial class Form1 : Form
    {
        private List<Event> events;
        public Form1()
        {
            InitializeComponent();
            InitializeEvents();
            PopulateComboBox();
        }

        private void InitializeEvents()
        {
            events = new List<Event>
            {
                new Event { Date = "2023-10-01", Title = "Concert in Seoul", Participant = "S.Coups" },
                new Event { Date = "2023-10-05", Title = "Fan Meeting", Participant = "Joshua" },
                new Event { Date = "2023-10-10", Title = "Music Show", Participant = "Woozi" },
                new Event { Date = "2023-10-15", Title = "Charity Event", Participant = "Dino" },
                new Event { Date = "2023-10-20", Title = "New Album Release", Participant = "All" }
            };
        }

        private void PopulateComboBox()
        {
            comboBoxMembers.Items.AddRange(new string[]
            {
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
                "Dino",
                "All"
            });
            comboBoxMembers.SelectedIndex = 0; // Установить значение по умолчанию
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            FilterEvents();
        }

        private void FilterEvents()
        {
            string selectedParticipant = comboBoxMembers.SelectedItem.ToString();
            string selectedDate = dateTimePicker.Value.ToString("yyyy-MM-dd");

            var filteredEvents = events.Where(e =>
                (selectedParticipant == "All" || e.Participant.Equals(selectedParticipant)) &&
                e.Date.Equals(selectedDate)).ToList();

            listBoxEvents.Items.Clear();

            if (filteredEvents.Count > 0)
            {
                foreach (var ev in filteredEvents)
                {
                    listBoxEvents.Items.Add(ev);
                }
            }
            else
            {
                listBoxEvents.Items.Add("Нет событий для выбранных параметров.");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
