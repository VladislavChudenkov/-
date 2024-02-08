using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToeGame
{
    public partial class MainWindow : Window
    {
        private Button[,] buttons;
        private bool isPlayerXTurn = true;
        private bool isGameOver = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            buttons = new Button[3, 3] {
                { btn1, btn2, btn3 },
                { btn4, btn5, btn6 },
                { btn7, btn8, btn9 }
            };

            foreach (var button in buttons)
            {
                button.Content = string.Empty;
                button.Click += Button_Click;
                button.IsEnabled = true;
            }

            isPlayerXTurn = true;
            isGameOver = false;
            messageLabel.Content = "Ход игрока X";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isGameOver)
                return;

            var button = (Button)sender;
            if (button.Content.ToString() != "")
                return;

            button.Content = isPlayerXTurn ? "X" : "O";

            if (CheckForWinner())
            {
                isGameOver = true;
                messageLabel.Content = (isPlayerXTurn ? "Игрок X" : "Робот") + " выиграл!";
                DisableAllButtons();
                return;
            }

            if (CheckForDraw())
            {
                isGameOver = true;
                messageLabel.Content = "Ничья!";
                DisableAllButtons();
                return;
            }

            isPlayerXTurn = !isPlayerXTurn;

            if (!isPlayerXTurn)
                RobotMove();
            else
                messageLabel.Content = "Ход игрока X";
        }

        private bool CheckForWinner()
        {
            
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Content.ToString() != "" &&
                    buttons[i, 0].Content == buttons[i, 1].Content &&
                    buttons[i, 1].Content == buttons[i, 2].Content)
                {
                    return true;
                }
            }

            
            for (int i = 0; i < 3; i++)
            {
                if (buttons[0, i].Content.ToString() != "" &&
                    buttons[0, i].Content == buttons[1, i].Content &&
                    buttons[1, i].Content == buttons[2, i].Content)
                {
                    return true;
                }
            }

            
            if (buttons[0, 0].Content.ToString() != "" &&
                buttons[0, 0].Content == buttons[1, 1].Content &&
                buttons[1, 1].Content == buttons[2, 2].Content)
            {
                return true;
            }

            if (buttons[0, 2].Content.ToString() != "" &&
                buttons[0, 2].Content == buttons[1, 1].Content &&
                buttons[1, 1].Content == buttons[2, 0].Content)
            {
                return true;
            }

            return false;
        }

        private bool CheckForDraw()
        {
            foreach (var button in buttons)
            {
                if (button.Content.ToString() == "")
                    return false;
            }
            return true;
        }

        private void DisableAllButtons()
        {
            foreach (var button in buttons)
            {
                button.IsEnabled = false;
            }
        }

        private void RobotMove()
        {
            Random rand = new Random();
            int row, col;
            do
            {
                row = rand.Next(0, 3);
                col = rand.Next(0, 3);
            } while (buttons[row, col].Content.ToString() != "");

            buttons[row, col].Content = "O";

            if (CheckForWinner())
            {
                isGameOver = true;
                messageLabel.Content = "Робот выиграл!";
                DisableAllButtons();
                return;
            }

            if (CheckForDraw())
            {
                isGameOver = true;
                messageLabel.Content = "Ничья!";
                DisableAllButtons();
                return;
            }

            isPlayerXTurn = true;
            messageLabel.Content = "Ход игрока X";
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
        }
    }
}
