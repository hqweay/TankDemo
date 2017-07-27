using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TankDemo
{
    public enum MoveDiretion
    {
        Stop,Left,Right,Up,Down
    }


    public class tank
    {
        //坦克的坐标，图片，移动速度，开火状态，宽度，高度
        int x;
        int y;
        Image image;
        int speed;
        bool isShooting = false;
        int width;
        int height;
        MoveDiretion xMove;
        MoveDiretion yMove;

        MoveDiretion bulletDirection;
       
        
        public tank()
        {
            image = TankDemo.Properties.Resources.p2tankU;
            width = image.Width;
            height = image.Height;
            x = 20;
            y = 20;
            speed = 10;

            MapTest.GameForm.KeyDown += new System.Windows.Forms.KeyEventHandler(GameForm_KeyDown);
            MapTest.GameForm.KeyUp +=new System.Windows.Forms.KeyEventHandler(GameForm_KeyUp);



        }
         
        private void GameForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            { 
                case System.Windows.Forms.Keys.Up:
                    yMove = MoveDiretion.Up;
                    bulletDirection = MoveDiretion.Up;
                    break;
                case System.Windows.Forms.Keys.Down:
                    yMove = MoveDiretion.Down;
                    bulletDirection = MoveDiretion.Down;
                    break;
                case System.Windows.Forms.Keys.Left:
                    xMove = MoveDiretion.Left;
                    bulletDirection = MoveDiretion.Left;
                    break;
                case System.Windows.Forms.Keys.Right:
                    xMove = MoveDiretion.Right;
                    bulletDirection = MoveDiretion.Right;
                    break;
                case System.Windows.Forms.Keys.Space:
                    isShooting = true;
                    break;



            }
        }


        private void GameForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Up:
                case System.Windows.Forms.Keys.Down:
                    yMove = MoveDiretion.Stop;
                    break;
                case System.Windows.Forms.Keys.Left:
                case System.Windows.Forms.Keys.Right:
                    xMove = MoveDiretion.Stop;
                    break;
                case System.Windows.Forms.Keys.Space:
                    isShooting = false;
                    break;
            }
        }


        public void Draw(Graphics g)
        {
            g.DrawImage(image, x, y);

        }

//        int lastShooTime;
        public void Updata(int frame)
        {
            //水平移动；
            switch  (xMove)
            {
                case MoveDiretion.Left:
                    x-=speed;
                    break;
                case MoveDiretion.Right:
                    x+=speed;
                    break;
            
            }
            

            //竖直移动
            switch (yMove)
            {
                case MoveDiretion.Up:
                    y -= speed;
                    break;
                case MoveDiretion.Down:
                    y += speed;
                    break;
            

            }

            if (isShooting)
            {
                Bullet bullet = new Bullet(bulletDirection);
                bullet.X = this.x;
                bullet.Y = this.y;
                MapTest.planeBullets.Add(bullet);
            }
            
        }
      
    }
}
