using System;
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

namespace Yahtzee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Settings
        int dieAmount = 5;
        float boardSize = 0.5F;
        int dieSize = 40;

        // Global
        Button[] dies;
        int[] curDieValues;
        string gameState;
        Random gRand;
        



        public MainWindow()
        {
            InitializeComponent();

            dies = new Button[dieAmount];
            curDieValues = new int[dieAmount];

        }

        private void PlayerDieClicked(object sender, RoutedEventArgs e)
        {
            int i = Convert.ToInt32( (sender as Button).Name.Substring(2) );
            SetDiePosition(i, "Inv");
        }

        private void RollDiceButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i=0; i<dies.Length; i++)
            {
                //// Give random positions
                int W = (int)DieCanvas.ActualWidth;
                //Canvas.SetLeft(dies[i], gRand.Next((int)(0.1 * W), (int)(0.9 * W)));
                int H = (int)DieCanvas.ActualHeight;
                //Canvas.SetBottom(dies[i], (int)((H - (boardSize * H)) / 2) + gRand.Next(0, (int)(boardSize * H)));

                Canvas.SetLeft(dies[i], (i+1)*50);
                Canvas.SetBottom(dies[i], H / 2 - dieSize/2 + gRand.Next((int)(-boardSize * H / 2), (int)(boardSize * H / 2)));

                curDieValues[i] = gRand.Next(1, dieAmount + 1);
                dies[i].Style = (Style)Application.Current.Resources[$"Button_Die_{curDieValues[i]}"];

                Console.WriteLine(curDieValues[i]);
            }

            // Count points


        }

        private void MoveDice(float x, float y)
        {
            throw new NotImplementedException();
        }

        private void ResetGame()
        {
            gameState = "Start";
            gRand = new Random();

            // Generate dies
            for (int i = 0; i < dieAmount; i++)
            {
                // Remove die if present
                if (dies[i] != null)
                {
                    DieCanvas.Children.Remove(dies[i]);
                }

                int j = i + 1;
                curDieValues[i] = j;
                dies[i] = new Button() { Style = (Style)Application.Current.Resources[$"Button_Die_{j}"], Name = $"D_{i}"};


                SetDiePosition(i, "Inv");
                dies[i].Click += (s,e) => 
                { 
                    PlayerDieClicked(s, e); 
                };
                DieCanvas.Children.Add(dies[i]);

                curDieValues[i] = i;
            }


        }

        private void SetDiePosition(int i, string P)
        {
            switch (P)
            {
                case "Inv":
                    int dieMargin = 10;
                    int W = (int)DieCanvas.ActualWidth;
                    int S = dieAmount * dieSize + (dieAmount - 1) * dieMargin;
                    Canvas.SetLeft(dies[i], W/2 - S/2 + i*(dieSize + dieMargin) );
                    Canvas.SetBottom(dies[i], 0);
                    break;
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }

        private void PlayerScoreClicked(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }
    }
}
