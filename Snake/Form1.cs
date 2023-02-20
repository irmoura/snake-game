using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {

        string direction = "right";
        int deslocamento = 20;
        int xCount = 0;
        int yCount = 0;
        int points = 0;
        List<int> xMap = new List<int>();
        List<int> yMap = new List<int>();
        //
        List<Button> snake = new List<Button>();
        Button button1;

        public Form1()
        {
            InitializeComponent();
        }

        private void aumentar()
        {
            Button button = new Button();
            button.BackColor = System.Drawing.Color.Yellow;
            button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            button.Location = new System.Drawing.Point(button2.Location.X, button2.Location.Y);
            button.Name = "buttonX";
            button.Size = new System.Drawing.Size(20, 20);
            //button.TabIndex = 0;
            button.UseVisualStyleBackColor = false;
            this.Controls.Add(button);
        }

        private void gameOver()
        {
            timer1.Enabled = false;
            for (int i = 0; i < snake.Count; i++)
            {
                snake[i].BackColor = Color.Red;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var xPosition = snake[0].Location.X;
            var yPosition = snake[0].Location.Y;
            switch (direction)
            {
                case "up":
                    yPosition -= deslocamento;
                    break;
                case "left":
                    xPosition -= deslocamento;
                    break;
                case "down":
                    yPosition += deslocamento;
                    break;
                case "right":
                    xPosition += deslocamento;
                    break;
            }
            //
            int count = 0;
            List<int> x = new List<int>();
            List<int> y = new List<int>();
            for (int i = 0; i < snake.Count; i++)
            {
                x.Add(snake[i].Location.X);
                y.Add(snake[i].Location.Y);
            }
            //left || up || down || right
            if (xPosition < 0 || yPosition < 0 || yPosition > 1000 || xPosition > 1910)
            {
                gameOver();
            }
            else
            {
                for (int i = 0; i < snake.Count; i++)
                {
                    if (i == 0)
                    {
                        switch (direction)//!!!
                        {
                            case "up":
                                snake[i].Location = new Point(xPosition, yPosition + count);
                                break;
                            case "left":
                                snake[i].Location = new Point(xPosition + count, yPosition);
                                break;
                            case "down":
                                snake[i].Location = new Point(xPosition, yPosition - count);
                                break;
                            case "right":
                                snake[i].Location = new Point(xPosition - count, yPosition);
                                break;
                        }
                        for (int j = 1; j < snake.Count; j++)
                        {
                            if (snake[i].Location.X == snake[j].Location.X && snake[i].Location.Y == snake[j].Location.Y)
                            {
                                gameOver();
                            }
                        }
                    }
                    else
                    {
                        snake[i].Location = new Point(x[i - 1], y[i - 1]);
                    }
                    count += deslocamento;
                }
                //Console.WriteLine(xPosition);
                if (snake[0].Location == button2.Location)
                {
                    Random rX = new Random();
                    Random rY = new Random();
                    int newX = rX.Next(0, 90);
                    int newY = rY.Next(0, 50);
                    button2.Location = new Point(xMap[newX], yMap[newY]);
                    //
                    var button_ = new Button();
                    button_.BackColor = System.Drawing.Color.Cyan;
                    button_.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    switch (direction)
                    {
                        case "up":
                            button_.Location = new Point(button2.Location.X, button2.Location.Y - 20);
                            break;
                        case "left":
                            button_.Location = new Point(button2.Location.X + 20, button2.Location.Y);
                            break;
                        case "down":
                            button_.Location = new Point(button2.Location.X, button2.Location.Y + 20);
                            break;
                        case "right":
                            button_.Location = new Point(button2.Location.X - 20, button2.Location.Y);
                            break;
                    }
                    button_.Name = "buttonX";
                    button_.Size = new System.Drawing.Size(20, 20);
                    button_.UseVisualStyleBackColor = false;
                    this.Controls.Add(button_);
                    snake.Add(button_);
                    //
                    timer1.Interval = timer1.Interval > 50 ? timer1.Interval - 50 : timer1.Interval;//increase speed
                    points++;
                    label1.Text = $"Points : {points}";
                    rX = null;
                    rY = null;
                    //aumentar();
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "W":
                    if (!direction.Equals("down"))
                    {
                        direction = "up";
                    }
                    break;
                case "A":
                    if (!direction.Equals("right"))
                    {
                        direction = "left";
                    }
                    break;
                case "S":
                    if (!direction.Equals("up"))
                    {
                        direction = "down";
                    }
                    break;
                case "D":
                    if (!direction.Equals("left"))
                    {
                        direction = "right";
                    }
                    break;
                case "F5"://restart game
                    snake[0].Location = new Point(0, 0);
                    for (int i = 0; i < snake.Count; i++)
                    {
                        snake[i].BackColor = Color.Cyan;
                    }
                    direction = "right";
                    timer1.Enabled = true;
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Location = new Point(((this.Size.Width / 2) - (label1.Size.Width / 2)), label1.Location.Y);
            for (int i = 0; i < 90; i++)
            {
                xMap.Add(xCount);
                xCount += deslocamento;
            }
            for (int i = 0; i < 50; i++)
            {
                yMap.Add(yCount);
                yCount += deslocamento;
            }
            //
            button1 = new Button();
            //button1.BackColor = System.Drawing.Color.Aqua;
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            button1.Location = new System.Drawing.Point(xMap[((xMap.Count / 2) + 2)], xMap[((yMap.Count / 2) + 0)]);
            button1.Name = "buttonX";
            button1.Size = new System.Drawing.Size(20, 20);
            button1.UseVisualStyleBackColor = false;
            this.Controls.Add(button1);
            snake.Add(button1);
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            //Cursor.Hide();
        }
    }
}
