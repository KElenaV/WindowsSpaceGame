using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceGame
{
    public partial class Form1 : Form
    {
        private GameWorld _gameWorld;
        public Form1()
        {
            InitializeComponent();
            SetClientSizeCore(1024, 768);
            GameManager.Initialize(restartButton);
            _gameWorld = new GameWorld(DisplayRectangle, CreateGraphics());
        }

        public Rectangle WindowSize { get; private set; }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            _gameWorld.Update();
        }

        private void restartButton_Click(object sender, EventArgs e)
        {

        }
    }
}
