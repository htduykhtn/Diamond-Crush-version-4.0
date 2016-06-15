﻿using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformTest;

namespace Diamond_Crush
{
    public partial class PreviewLevelScene : Form
    {
        
        CImageBase _home, _back, _play, _shop;

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            var user = CUser.Instance();
            if (CLevel._currLevel.LevelID<=7)
            {
                CLevel._currLevel.LevelID++;
            }
            CDiamondCrushSceneControl._nextScene = CDiamondCrushSceneControl.Scene.PreviewLevel;
            this.Close();
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (CLevel._currLevel.LevelID >0)
            {
                CLevel._currLevel.LevelID--;
            }
            CDiamondCrushSceneControl._nextScene = CDiamondCrushSceneControl.Scene.PreviewLevel;
            this.Close();
        }

        private void PreviewLevelScene_VisibleChanged(object sender, EventArgs e)
        {
            //if (CDiamondCrushSceneControl._nextScene == CDiamondCrushSceneControl.Scene.PickLevel&&this.Visible==false)
            //{
            //    this.SetVisibleCore(true);
            //}
        }

        private void PreviewLevelScene_MouseClick(object sender, MouseEventArgs e)
        {

            if (_home.IsClicked(e.X, e.Y))
            {
                MainMenu.Instance().SetMainMenuVisible(true);
                CDiamondCrushSceneControl._nextScene = CDiamondCrushSceneControl.Scene.MainMenu;
                this.SetVisibleCore(false);
            }
            else if (_back.IsClicked(e.X, e.Y))
            {
                CDiamondCrushSceneControl._nextScene = CDiamondCrushSceneControl.Scene.PickLevel;
                this.SetVisibleCore(false);
            }
            else if (_play.IsClicked(e.X, e.Y))
            {
                if (CUser.Instance()._lifeCount > 0)
                {
                    var f = new Form1(CLevel._currLevel.LevelID);
                    f.StartPosition = FormStartPosition.CenterParent;
                    f.ShowDialog(this);
                    if (CLevel._currLevel.LevelID<CUser.Instance()._currLevel)
                    {
                        this.pictureBox3.Visible = true;
                    }
                }
                else MessageBox.Show("You need life to play! You will have one more life per 5 minutes in game.");
            }
            else if (_shop.IsClicked(e.X, e.Y))
            {
                ShopScene.Instance().StartPosition = FormStartPosition.CenterParent;
                ShopScene.Instance().ShowDialog(this);
            }
        }

        public PreviewLevelScene(int lvID)
        {
            InitializeComponent();
            if (lvID == 8||lvID ==CUser.Instance()._currLevel)
            {
                this.pictureBox3.Visible = false;
            }
            if (lvID == 0)
            {
                this.pictureBox4.Visible = false;
            }
            PickLevelScene.Instance().SetPickLevelSceneVisible(false);
            _home = new CImageBase(Properties.Resources.but_home_75x75) { Left = 10,Top=10};
            _back = new CImageBase(Properties.Resources.but_back_75x75) { Left = 10, Top = 515 };
            ////bellow the "level" label
            for(int i = 0; i < CUser.Instance().GetLevel(lvID)._highestStar; i++)
            {
                this.Text += " *";
            }
            _play = new CImageBase(Properties.Resources.but_play_75x75)
            { Left = pictureBox1.Location.X + pictureBox1.Height / 2 + 50, Top = pictureBox1.Location.Y + 100 };
            ////Next to play but

            _shop = new CImageBase(Properties.Resources.but_shop_75x75)
            { Left = pictureBox1.Location.X + pictureBox1.Height / 2 + 150, Top = pictureBox1.Location.Y + 100 };
            this.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("_" + (lvID).ToString());
            this.pictureBox2.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("num_" + (lvID + 1).ToString()+ "_100x100");
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            _play.DrawSprite(g);
            _back.DrawSprite(g);
            _home.DrawSprite(g);
            _shop.DrawSprite(g);
            base.OnPaint(e);
        }
    }
}
