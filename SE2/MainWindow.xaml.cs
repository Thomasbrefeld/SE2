using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SE2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        static string PATH = "C:/Users/thoma/Desktop/School/Software Engineering 2/SE2/SE2";
        static DispatcherTimer timer = new DispatcherTimer();
        List<Reminder> reminders = new List<Reminder>();
        List<Event> events = new List<Event>();
        List<ToDo> toDos = new List<ToDo>();
        List<ToDo> completedToDos = new List<ToDo>();

        public MainWindow()
        {
            InitializeComponent();

            MainPageTime.Content = DateTime.Now.ToString();

            loadRemindersData();
            loadEventsData();
            loadToDoData();

            this.Closing += (s, e) => saveRemindersData();
            this.Closing += (s, e) => saveEventsData();
            this.Closing += (s, e) => saveToDoData();
        }

        private void loadRemindersData()
        {
            using (Stream stream = File.Open(PATH + "/Data/reminders.bin", FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                reminders = (List<Reminder>)bformatter.Deserialize(stream);
            }

            reminderStackPanel.Children.Clear();
            foreach (Reminder r in reminders)
            {
                Grid g = new Grid();
                ColumnDefinition gCol1 = new ColumnDefinition();
                gCol1.Width = new GridLength(180);
                g.ColumnDefinitions.Add(gCol1);
                ColumnDefinition gCol2 = new ColumnDefinition();
                gCol2.Width = new GridLength(170);
                g.ColumnDefinitions.Add(gCol2);

                Label nameLable = new Label();
                nameLable.Content = r.getName();
                nameLable.FontSize = 14;
                Grid.SetColumn(nameLable, 0);
                g.Children.Add(nameLable);

                Label timeLable = new Label();
                timeLable.Content = r.getTime();
                nameLable.FontSize = 14;
                Grid.SetColumn(timeLable, 1);
                g.Children.Add(timeLable);

                reminderStackPanel.Children.Add(g);
            }
        }

        private void saveRemindersData()
        {
            using (Stream stream = File.Open(PATH + "/Data/reminders.bin", FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, reminders);
            }
        }

        private void loadEventsData()
        {
            using (Stream stream = File.Open(PATH + "/Data/events.bin", FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                events = (List<Event>)bformatter.Deserialize(stream);
            }

            eventStackPanel.Children.Clear();
            foreach (Event r in events)
            {
                Grid g = new Grid();
                ColumnDefinition gCol1 = new ColumnDefinition();
                gCol1.Width = new GridLength(180);
                g.ColumnDefinitions.Add(gCol1);
                ColumnDefinition gCol2 = new ColumnDefinition();
                gCol2.Width = new GridLength(170);
                g.ColumnDefinitions.Add(gCol2);

                Label nameLable = new Label();
                nameLable.Content = r.getName();
                nameLable.FontSize = 14;
                Grid.SetColumn(nameLable, 0);
                g.Children.Add(nameLable);

                Label timeLable = new Label();
                timeLable.Content = r.getTime();
                nameLable.FontSize = 14;
                Grid.SetColumn(timeLable, 1);
                g.Children.Add(timeLable);

                eventStackPanel.Children.Add(g);
            }
        }

        private void saveEventsData()
        {
            using (Stream stream = File.Open(PATH + "/Data/events.bin", FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, events);
            }
        }

        private void loadToDoData()
        {
            using (Stream stream = File.Open(PATH + "/Data/todos.bin", FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                toDos = (List<ToDo>)bformatter.Deserialize(stream);
            }
            ToDoListBox.Items.Clear();
            foreach (ToDo t in toDos)
            {
                ToDoListBox.Items.Add(t.getName());
            }

            using (Stream stream = File.Open(PATH + "/Data/completedtodos.bin", FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                completedToDos = (List<ToDo>)bformatter.Deserialize(stream);
            }
            CompletedToDoListBox.Items.Clear();
            foreach (ToDo c in completedToDos)
            {
                CompletedToDoListBox.Items.Add(c.getName());
            }


        }

        private void saveToDoData()
        {
            using (Stream stream = File.Open(PATH + "/Data/todos.bin", FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, toDos);
            }
            using (Stream stream = File.Open(PATH + "/Data/completedtodos.bin", FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, completedToDos);
            }
        }

        private void newReminderBUttonClick(object sender, RoutedEventArgs e)
        {
            AddReminderWindow addReminderWindow = new AddReminderWindow(PATH, reminders);
            addReminderWindow.ShowDialog();
            loadRemindersData();
        }

        private void removeReminderBUttonClick(object sender, RoutedEventArgs e)
        {
            RemoveReminderWindow removeReminderWindow = new RemoveReminderWindow(PATH, reminders);
            removeReminderWindow.ShowDialog();
            loadRemindersData();
        }

        private void AddItemToDoButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: be able to add to-dos
        }

        private void MarkItemAsCompleted_Click(object sender, RoutedEventArgs e)
        {
            if (ToDoListBox.SelectedIndex != -1)
            {
                completedToDos.Add(toDos[ToDoListBox.SelectedIndex]);
                toDos.RemoveAt(ToDoListBox.SelectedIndex);
                CompletedToDoListBox.SelectedIndex = -1;
                saveToDoData();
                loadToDoData();
            }
        }

        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (CompletedToDoListBox.SelectedIndex != -1)
            {
                completedToDos.RemoveAt(CompletedToDoListBox.SelectedIndex);
                CompletedToDoListBox.SelectedIndex = -1;
                saveToDoData();
                loadToDoData();
            }
        }
    }
}
