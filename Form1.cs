using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Image buttonimage_3_0;
        public Image buttonimage_3_1;
        public Image buttonimage_3_2;
        public Image buttonimage_4_0;
        public Image buttonimage_4_1;
        public Image buttonimage_4_2;
        public Image buttonimage_5_0;
        public Image buttonimage_5_1;
        public Image buttonimage_5_2;
        public Image buttonimage_6_0;
        public Image buttonimage_6_1;
        public Image buttonimage_6_2;
        public Image buttonimage_7_0;
        public Image buttonimage_7_1;
        public Image buttonimage_7_2;
        System.Drawing.Image img_3_1 = System.Drawing.Image.FromFile(@"..\..\grp_dummy\g_btn_003_1.bmp");
        System.Drawing.Image img_3_2 = System.Drawing.Image.FromFile(@"..\..\grp_dummy\g_btn_003_2.bmp");
        System.Drawing.Image img_4_1 = System.Drawing.Image.FromFile(@"..\..\grp_dummy\g_btn_004_1.bmp");
        System.Drawing.Image img_4_2 = System.Drawing.Image.FromFile(@"..\..\grp_dummy\g_btn_004_2.bmp");
        System.Drawing.Image img_5_1 = System.Drawing.Image.FromFile(@"..\..\grp_dummy\g_btn_005_1.bmp");
        System.Drawing.Image img_5_2 = System.Drawing.Image.FromFile(@"..\..\grp_dummy\g_btn_005_2.bmp");
        System.Drawing.Image img_6_1 = System.Drawing.Image.FromFile(@"..\..\grp_dummy\g_btn_006_1.bmp");
        System.Drawing.Image img_6_2 = System.Drawing.Image.FromFile(@"..\..\grp_dummy\g_btn_006_2.bmp");
        System.Drawing.Image img_7_1 = System.Drawing.Image.FromFile(@"..\..\grp_dummy\g_btn_007_1.bmp");
        System.Drawing.Image img_7_2 = System.Drawing.Image.FromFile(@"..\..\grp_dummy\g_btn_007_2.bmp");
//        System.Drawing.Image img_7_2 = System.Drawing.Image.FromFile(@"C:\Users\Y\Desktop\doujin_game\Doujin_game_sharp\grp_dummy\g_btn_007_2.bmp");

        
        public Form1()
        {
            InitializeComponent();
            buttonimage_3_0 = 外出.Image;
            buttonimage_3_1 = img_3_1;
            buttonimage_3_2 = img_3_2;
            buttonimage_4_0 = 教会.Image;
            buttonimage_4_1 = img_4_1;
            buttonimage_4_2 = img_4_2;
            buttonimage_5_0 = 休む.Image;
            buttonimage_5_1 = img_5_1;
            buttonimage_5_2 = img_5_2;
            buttonimage_6_0 = 読書.Image;
            buttonimage_6_1 = img_6_1;
            buttonimage_6_2 = img_6_2;
            buttonimage_7_0 = ステータス.Image;
            buttonimage_7_1 = img_7_1;
            buttonimage_7_2 = img_7_2;
        }

        /* === 外出ボタン === */
        private void 外出_MouseEnter(object sender, EventArgs e)
        {
            外出.Image = buttonimage_3_1;
        }

        private void 外出_MouseDown(object sender, MouseEventArgs e)
        {
            外出.Image = buttonimage_3_2;
        }

        private void 外出_MouseUp(object sender, MouseEventArgs e)
        {
            外出.Image = buttonimage_3_1;
        }

        private void 外出_MouseLeave(object sender, EventArgs e)
        {
            外出.Image = buttonimage_3_0;
        }

        private void 外出_MouseMove(object sender, MouseEventArgs e)
        {
            if (外出.ClientRectangle.Contains(外出.PointToClient(Cursor.Position)) == false)
            {
                外出.Image = buttonimage_3_0;
            }
        }


        /* === 教会ボタン === */
        private void 教会_MouseEnter(object sender, EventArgs e)
        {
            教会.Image = buttonimage_4_1;
        }

        private void 教会_MouseDown(object sender, MouseEventArgs e)
        {
            教会.Image = buttonimage_4_2;
        }

        private void 教会_MouseUp(object sender, MouseEventArgs e)
        {
            教会.Image = buttonimage_4_1;
        }

        private void 教会_MouseLeave(object sender, EventArgs e)
        {
            教会.Image = buttonimage_4_0;
        }

        private void 教会_MouseMove(object sender, MouseEventArgs e)
        {
            if (教会.ClientRectangle.Contains(教会.PointToClient(Cursor.Position)) == false)
            {
                教会.Image = buttonimage_4_0;
            }
        }


        /* === 休むボタン === */
        private void 休む_MouseEnter(object sender, EventArgs e)
        {
            休む.Image = buttonimage_5_1;
        }

        private void 休む_MouseDown(object sender, MouseEventArgs e)
        {
            休む.Image = buttonimage_5_2;
        }

        private void 休む_MouseUp(object sender, MouseEventArgs e)
        {
            休む.Image = buttonimage_5_1;
        }

        private void 休む_MouseLeave(object sender, EventArgs e)
        {
            休む.Image = buttonimage_5_0;
        }

        private void 休む_MouseMove(object sender, MouseEventArgs e)
        {
            if (休む.ClientRectangle.Contains(休む.PointToClient(Cursor.Position)) == false)
            {
                休む.Image = buttonimage_5_0;
            }
        }


        /* === 読書 === */
        private void 読書_MouseEnter(object sender, EventArgs e)
        {
            読書.Image = buttonimage_6_1;
        }

        private void 読書_MouseDown(object sender, MouseEventArgs e)
        {
            読書.Image = buttonimage_6_2;
        }

        private void 読書_MouseUp(object sender, MouseEventArgs e)
        {
            読書.Image = buttonimage_6_1;
        }

        private void 読書_MouseLeave(object sender, EventArgs e)
        {
            読書.Image = buttonimage_6_0;
        }

        private void 読書_MouseMove(object sender, MouseEventArgs e)
        {
            if (読書.ClientRectangle.Contains(読書.PointToClient(Cursor.Position)) == false)
            {
                読書.Image = buttonimage_6_0;
            }
        }


        /* ステータスボタン */
        private void ステータス_MouseEnter(object sender, EventArgs e)
        {
            ステータス.Image = buttonimage_7_1;
        }

        private void ステータス_MouseDown(object sender, MouseEventArgs e)
        {
            ステータス.Image = buttonimage_7_2;
        }

        private void ステータス_MouseUp(object sender, MouseEventArgs e)
        {
            ステータス.Image = buttonimage_7_1;
        }

        private void ステータス_MouseLeave(object sender, EventArgs e)
        {
            ステータス.Image = buttonimage_7_0;
        }

        private void ステータス_MouseMove(object sender, MouseEventArgs e)
        {
            if (ステータス.ClientRectangle.Contains(ステータス.PointToClient(Cursor.Position)) == false)
            {
                ステータス.Image = buttonimage_7_0;
            }
        }

    }



}
