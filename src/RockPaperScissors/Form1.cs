using System.Drawing;
using System.Security.Policy;

namespace RockPaperScissors
{
    enum Options
    {
        None = -1,
        Rock = 0,
        Paper = 1,
        Scissors = 2
    }

    enum Result
    {
        Tie,
        Win,
        Lose
    }

    public partial class RockPaperScissorsForm : Form
    {
        Options playerPick = Options.None;

        int seconds;
        int playerPoints;
        int compPoints;
        int rounds;

        static readonly Result[,] resulTable =
        {
            { Result.Tie, Result.Lose, Result.Win },
            { Result.Win, Result.Tie, Result.Lose },
            { Result.Lose, Result.Win, Result.Tie }
        };

        public RockPaperScissorsForm()
        {
            InitializeComponent();

            playerPickImg.Image = Properties.Resources.qq;
            compPickImg.Image = Properties.Resources.qq;

            rockButton.Enabled = false;
            scissorsButton.Enabled = false;
            paperButton.Enabled = false;
            restartButton.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds--;
            secondsLabel.Text = seconds.ToString();

            if (seconds != 0)
                return;

            
            pickTimer.Stop();

            if (playerPick == Options.None)
            {
                MessageBox.Show("You lose!\nMake a choice next time!");

                compPoints++;
                compPointsLabel.Text = compPoints.ToString();
            }
            else
            {
                Random random = new Random();

                Options compPick = (Options)random.Next(0, 3);

                switch (compPick)
                {
                    case Options.Rock:
                        compPickImg.Image = Properties.Resources.Rock;
                        break;
                    case Options.Paper:
                        compPickImg.Image = Properties.Resources.Paper;
                        break;
                    case Options.Scissors:
                        compPickImg.Image = Properties.Resources.Scissors;
                        break;
                }

                Result result = resulTable[(int)playerPick, (int)compPick];

                string playerPickName = Enum.GetName(typeof(Options), playerPick);
                string compPickName = Enum.GetName(typeof(Options), compPick);

                switch (result)
                {
                    case Result.Win:
                        MessageBox.Show($"You win!\n{playerPickName} beats {compPickName}");

                        playerPoints++;
                        playerPointsLabel.Text = playerPoints.ToString();
                        break;
                    case Result.Lose:
                        MessageBox.Show($"You lose!\n{compPickName} beats {playerPickName}");

                        compPoints++;
                        compPointsLabel.Text = compPoints.ToString();
                        break;
                    case Result.Tie:
                        MessageBox.Show($"DRAW!\n{playerPickName} is equal to {compPickName}");
                        break;
                }
            }


            rounds--;

            roundsLabel.Text = rounds.ToString();

            if (rounds == 0)
            {
                pickTimer.Stop();

                //disable buttons except restart
                rockButton.Enabled = false;
                rockButton.BackColor = SystemColors.InactiveCaption;
                scissorsButton.Enabled = false;
                scissorsButton.BackColor = SystemColors.InactiveCaption;
                paperButton.Enabled = false;
                paperButton.BackColor = SystemColors.InactiveCaption;
            }
            else //new round
            {
                playerPick = Options.None;

                playerPickImg.Image = Properties.Resources.qq;
                compPickImg.Image = Properties.Resources.qq;

                seconds = 6;

                pickTimer.Start();
            }
        }

        void SetupRound()
        {
            playerPick = Options.None;

            playerPickImg.Image = Properties.Resources.qq;
            compPickImg.Image = Properties.Resources.qq;

            seconds = 6;
            playerPoints = 0;
            compPoints = 0;
        }

        void SetupGame()
        {
            SetupRound();
            rounds = 5;
        }

        private void rockButton_Click(object sender, EventArgs e)
        {
            playerPick = Options.Rock;
            playerPickImg.Image = Properties.Resources.Rock;
        }

        private void paperButton_Click(object sender, EventArgs e)
        {
            playerPick = Options.Paper;
            playerPickImg.Image = Properties.Resources.Paper;
        }

        private void scissorsButton_Click(object sender, EventArgs e)
        {
            playerPick = Options.Scissors;
            playerPickImg.Image = Properties.Resources.Scissors;
        }

        void StartGame()
        {
            #region enable buttons
            rockButton.Enabled = true;
            rockButton.BackColor = SystemColors.ActiveCaption;
            scissorsButton.Enabled = true;
            scissorsButton.BackColor = SystemColors.ActiveCaption;
            paperButton.Enabled = true;
            paperButton.BackColor = SystemColors.ActiveCaption;
            restartButton.Enabled = true;
            #endregion

            SetupGame();

            pickTimer.Start();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartGame();

            startButton.Enabled = false;
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            StartGame();

            //reset picks and other variables
            playerPointsLabel.Text = "0";
            compPointsLabel.Text = "0";

            roundsLabel.Text = rounds.ToString();
        }
    }
}