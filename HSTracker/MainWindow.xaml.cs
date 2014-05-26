﻿using System;
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

using Utility.TailThread;
using System.Reactive.Linq;

namespace HSTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EventStream eventStream;
        Deck deck;
        Library library = new Library();

        public MainWindow()
        {
            InitializeComponent();
            InitializeWindow();
            InitializeDeck();
            library.FindByFragment("fire").ForEach(x => Console.WriteLine(String.Format("{0}, {1}", x.Item1, x.Item2)));
            eventStream = new EventStream();

			this.Loaded += delegate { this.StartListening(); };
        }

        // Start window in upper-right
        private void InitializeWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            this.Height = System.Windows.SystemParameters.WorkArea.Height;
            this.Top = 0;
        }

        private void InitializeDeck()
        {
            deck = Deck.Mage();
            this.cardCollection.ItemsSource = deck.Cards;
            this.deckControl.ItemsSource = new List<Deck> { deck };
        }

        private void StartListening()
        {
            eventStream.MyDraws().Subscribe(card =>
            {
                Console.WriteLine("Draw: " + card);
                deck.Draw(card);
            });

            eventStream.MyMulligans().Subscribe(card =>
            {
                Console.WriteLine("Mulligan: " + card);
                deck.Restore(card);
            });
        }

        private void showMessage(string text, string caption = "ReadMe")
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBox.Show(text, caption, button, icon);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            InitializeDeck();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
