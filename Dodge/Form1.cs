using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Dodge
{
    public partial class Form1 : Form
    {
        Rectangle player = new Rectangle(325, 300, 30, 30);


        int playerXSpeed = 8;
        int playerYSpeed = 8;

        int ballSize = 10;

        List<Rectangle> ballList = new List<Rectangle>();
        List<int> ballSpeeds = new List<int>();
        List<int> ballSizes = new List<int>();

        bool leftPressed = false;
        bool rightPressed = false;
        bool upPressed = false;
        bool downPressed = false;

        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Random randGen = new Random();
        int randValue = 0;

        int timer = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //Move player
            if (upPressed == true && player.Y >= 0)
            {
                player.Y -= playerYSpeed;
            }
            if (downPressed == true && player.Y <= this.Height - player.Height)
            {
                player.Y += playerYSpeed;
            }
            if (leftPressed == true && player.X >= 0)
            {
                player.X -= playerXSpeed;
            }
            if (rightPressed == true && player.X <= this.Width - player.Width)
            {
                player.X += playerXSpeed;
            }

            //Draw balls down the screen
            for (int i = 0; i < ballList.Count; i++)
            {
                int y = ballList[i].Y + ballSpeeds[i];

                ballList[i] = new Rectangle(ballList[i].X, y, ballSize, ballSize);
            }

            //Create new ball if it is time
            randValue = randGen.Next(0, 100);

            if (randValue < 50)
            { 
                    randValue = randGen.Next(0, this.Width - 10);
               
                    Rectangle ball = new Rectangle(randValue, 0, ballSize, ballSize);
                    ballList.Add(ball);
                
                    ballSpeeds.Add(randGen.Next(5, 15));
                    ballSizes.Add(randGen.Next(5, 15));
            }
              
            //Remove ball if it has gone off the screen
            for (int i = 0; i < ballList.Count; i++)
            {
                if (ballList[i].Y > this.Height)
                {
                    ballList.RemoveAt(i);
                    ballSpeeds.RemoveAt(i);
                    ballSizes.Add(randGen.Next(5, 15));
                }
            }

            //Check for collision between ball and player
            for (int i = 0; i < ballList.Count; i++)
            {
                if (ballList[i].IntersectsWith(player))
                {
                    gameTimer.Stop();
                }
            }

            timer++;
            timerLabel.Text = $"timer: {timer}";

            Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;

            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;

            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(yellowBrush, player);
            for (int i = 0; i < ballList.Count(); i++)
            {
                e.Graphics.FillEllipse(whiteBrush, ballList[i]);
            }
        }
    }
}