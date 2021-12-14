﻿using System;
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
        //Set path_Loc debug value
        static readonly string PATH = path_Loc(true);

        //Declare timer 
        static DispatcherTimer timer = new DispatcherTimer();
        List<Reminder> reminders = new List<Reminder>();
        List<Event> events = new List<Event>();

        public MainWindow()
        {
            InitializeComponent();

            //Initialize timer.
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += timerTick;
            timer.Start();
            MainPageTime.Content = DateTime.Now.ToString();
            
            
            //reminders.Add(new Reminder("Monday Class", new DateTime(2021, 11, 28, 16, 20, 0)));
            //reminders.Add(new Reminder("Wednesday Class", new DateTime(2021, 12, 1, 16, 20, 0)));
            loadRemindersData();
            
            //events.Add(new Event("Graduation", new DateTime(2021, 12, 4, 10, 0, 0)));
            //events.Add(new Event("Final Exams", new DateTime(2021, 12, 13, 12, 0, 0)));
            //events.Add(new Event("End of Final Exams", new DateTime(2021, 12, 16, 12, 0, 0)));
            //events.Add(new Event("End of Semester", new DateTime(2021, 12, 17, 12, 0, 0)));

            loadEventsData();

            this.Closing += (s, e) => saveRemindersData();
            this.Closing += (s, e) => saveEventsData();
        }

        private static string path_Loc(bool debug)
        {
            if (debug)
                return "C:/Users/Computer-WS/source/repos/SE2/SE2";
            else return Directory.GetCurrentDirectory();
        }

        private void loadRemindersData(){

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


        public void saveRemindersData()
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

        public void saveEventsData()
        {
            using (Stream stream = File.Open(PATH + "/Data/events.bin", FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, events);
            }
        }

        public void newReminderBUttonClick(object sender, RoutedEventArgs e)
        {
            AddReminderWindow addReminderWindow = new AddReminderWindow(PATH, reminders);
            addReminderWindow.ShowDialog();
            loadRemindersData();
        }

        public void removeReminderBUttonClick(object sender, RoutedEventArgs e)
        {
            RemoveReminderWindow removeReminderWindow = new RemoveReminderWindow(PATH, reminders);
            removeReminderWindow.ShowDialog();
            loadRemindersData();
        }

        private void timerTick(object sender, EventArgs e)
        {
            //RemindersClockDate.Content = DateTime.Now.ToString("MMMM dd, yyyy");
            //RemindersClockTime.Content = DateTime.Now.ToString("HH:mm:ss");
        }

    }
}
