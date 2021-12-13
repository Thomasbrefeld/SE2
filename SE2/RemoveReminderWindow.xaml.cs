using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
    /// Interaction logic for RemoveReminderWindow.xaml
    /// </summary>
    public partial class RemoveReminderWindow : Window
    {
        String path;
        List<Reminder> reminders;
        public RemoveReminderWindow(String p, List<Reminder> r)
        {
            InitializeComponent();
            path = p;
            reminders = r;

            foreach (Reminder remidner in reminders){
                reminderListBox.Items.Add(remidner.getName());
            }
            reminderListBox.SelectedIndex = -1;
        }

        private void reminderListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedReminderName.Content = reminders[reminderListBox.SelectedIndex].getName();
                selectedReminderDate.Content = reminders[reminderListBox.SelectedIndex].getTime().Date;
                selectedReminderTime.Content = reminders[reminderListBox.SelectedIndex].getTime().TimeOfDay;
            }
            catch
            {
                selectedReminderName.Content = "";
                selectedReminderDate.Content = "";
                selectedReminderTime.Content = "";
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (reminderListBox.SelectedIndex == -1)
                return;
            reminders.RemoveAt(reminderListBox.SelectedIndex);
            reminderListBox.Items.Remove(reminderListBox.SelectedItem);
            using (Stream stream = File.Open(path + "/Data/reminders.bin", FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, reminders);
            }
            reminderListBox.SelectedIndex = -1;
        }
    }
}
