using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SE2
{
    /// <summary>
    /// Interaction logic for AddReminderWindow.xaml
    /// </summary>
    public partial class AddReminderWindow : Window
    {
        String path;
        List<Reminder> reminders;
        public AddReminderWindow(String p, List<Reminder> r)
        {
            InitializeComponent();
            path = p;
            reminders = r;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            reminders.Add(new Reminder(addReminderName.Text, new DateTime(
                                addReminderDate.SelectedDate.Value.Year, 
                                addReminderDate.SelectedDate.Value.Month,
                                addReminderDate.SelectedDate.Value.Day,
                                Int32.Parse(addReminderTimeHour.Text),
                                Int32.Parse(addReminderTimeMin.Text), 
                                0)));
            using (Stream stream = File.Open(path + "/Data/reminders.bin", FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, reminders);
            }
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
