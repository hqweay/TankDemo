using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankDemo
{
    static class GameStart
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
        
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


        //      Application.Run(new Map());
          Application.Run(new Login());

        //    Application.Run(new Welcome());

        }
    }













    public struct Point
    {
        public double x;    //横坐标
        public double y;    //纵坐标
    };

    //移动方向类型
    public struct Vector
    {
        public Point objectLocation;      //物体当前坐标
        public Point vector;     //方向向量

        public double unitX;     //横坐标增量
        public double unitY;     //纵坐标增量
    };


    class Move
    {

        private Point scale;      //物体的规模
        private Vector direction;        //移动方向
        private Point mousexy;      //鼠标点击坐标
        private const int movehDistanceUnit = 2;      //每次物体移动的距离

        public Move(Point location, Point scale)
        {
            //初始化
            this.direction.objectLocation = location;
            this.scale = scale;
            this.mousexy = location;

            this.direction.unitX = 0;
            this.direction.unitY = 0;
        }

        //设置mousex, mousey
        public void setMousexy(Point mousexy)
        {
            this.mousexy = mousexy;
            countMoveDirecton();
        }

        public Vector getMoveDirection()
        {
            return this.direction;
        }

        //计算移动方向
        private void countMoveDirecton()
        {
            int x;
            int y;
            double temp;

            //坐标整数化
            this.direction.objectLocation.y = (int)this.direction.objectLocation.y;
            this.direction.objectLocation.x = (int)this.direction.objectLocation.x;

            //确定方向向量
            x = (int)(this.mousexy.x - this.direction.objectLocation.x);
            y = (int)(this.mousexy.y - this.direction.objectLocation.y);
            this.direction.vector.x = x;
            this.direction.vector.y = y;
            x = Math.Abs(x);
            y = Math.Abs(y);

            //Math.Sqrt();
            temp = Math.Pow(((double)y / x), 2);
            temp = Math.Pow((1 + temp), 0.5);
            this.direction.unitX = (this.direction.vector.x / x) * Math.Round((movehDistanceUnit / temp), 3);
            temp = Math.Pow(((double)x / y), 2);
            temp = Math.Pow((1 + temp), 0.5);
            this.direction.unitY = (this.direction.vector.y / y) * Math.Round((movehDistanceUnit / temp), 3);

        }

        //获取碰撞方向
        public Vector getCollideDirection(Collider collider)
        {
            Vector vector = new Vector();
            /*
             *    待补充
             */
            return vector;
        }

        //获取物体新坐标
        public Point getNewxy()
        {
            Point location = new Point();

            if ((Math.Abs((this.mousexy.x - this.direction.objectLocation.x)) > Math.Abs(this.direction.unitX)) && (Math.Abs((this.mousexy.y - this.direction.objectLocation.y)) > Math.Abs(this.direction.unitY)))
            {
                this.direction.objectLocation.x += this.direction.unitX;
                this.direction.objectLocation.y += this.direction.unitY;
            }
            else
            {
                this.direction.objectLocation.x = this.mousexy.x;
                this.direction.objectLocation.y = this.mousexy.y;
            }

            location.x = (int)this.direction.objectLocation.x - scale.x / 2;
            location.y = (int)this.direction.objectLocation.y - scale.y / 2;

            Thread.Sleep(10);

            return location;
        }

        public Point getNewxy(Collider collider)
        {
            Point location = new Point();


            if (collider.isCollided)
            {
                //Vector vector;
                //vector = getCollideDirection(collider);

                if (Math.Abs(collider.x - (int)this.direction.objectLocation.x) < (collider.wide + this.scale.x))
                    this.direction.objectLocation.y += this.direction.unitY;
                else
                    this.direction.objectLocation.x += this.direction.unitX;


                this.countMoveDirecton();

            }
            else
            {
                if ((Math.Abs((this.mousexy.x - this.direction.objectLocation.x)) > Math.Abs(this.direction.unitX)) && (Math.Abs((this.mousexy.y - this.direction.objectLocation.y)) > Math.Abs(this.direction.unitY)))
                {
                    this.direction.objectLocation.x += this.direction.unitX;
                    this.direction.objectLocation.y += this.direction.unitY;
                }
                else
                {
                    this.direction.objectLocation.x = this.mousexy.x;
                    this.direction.objectLocation.y = this.mousexy.y;
                }
            }

            location.x = (int)this.direction.objectLocation.x - scale.x / 2;
            location.y = (int)this.direction.objectLocation.y - scale.y / 2;

            Thread.Sleep(10);

            return location;
        }


    }

    class Collider
    {

        public bool isCollided = false;
        public int x, y;
        public int wide, height;

        /*
                1.含碰撞物体
                2.碰撞角度
         * this.getRectangle().IntersectsWith(new Rectangle(MapTest.wallList[i].getX(), MapTest.wallList[i].getY(), 40, 40))

        
        */

        public Collider(List<Object> body)
        {

        }
        public Collider() { }
    }

    class KeyMouseMessage
    {
        public struct mouseMessage
        {
            public int x;
            public int y;
            public MouseButtons press;
            public bool isDown;
        };
        public struct keyMessage
        {
            public int keyValue;
            public bool isDown;
        };

        public mouseMessage mouse;
        public keyMessage key;
        private Form form;

        public KeyMouseMessage(Form form)
        {
            this.mouse.isDown = true;
            this.form = form;
            this.key.isDown = false;
        }

        public void setKeyPreviwe(bool value)
        {
            this.form.KeyPreview = value;
        }

        public bool getKeyPreviewValue()
        {
            return this.form.KeyPreview;
        }

        public void add()
        {
            form.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouseMove);
            form.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
            form.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPress);
            form.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);

        }
        public bool setKeyPreView(bool preView)
        {
            if (preView)
            {
                form.KeyPreview = true;
                return true;
            }
            else
            {
                form.KeyPreview = false;
                return false;
            }

        }
        private void mouseMove(object sender, MouseEventArgs e)
        {
            this.mouse.x = e.X;
            this.mouse.y = e.Y;
        }

        private void mouseDown(Object sender, MouseEventArgs e)
        {
            this.mouse.isDown = true;
            this.mouse.press = e.Button;
        }

        private void keyPress(object sender, KeyPressEventArgs e)
        {
            this.key.isDown = true;
            this.key.keyValue = (int)e.KeyChar;
            // e.handled = true;试验一下
        }
        private void keyDown(object sender, KeyEventArgs e)
        {
            this.key.isDown = true;
            this.key.keyValue = e.KeyValue;
        }

        ~KeyMouseMessage()
        {
            form.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.mouseMove);
            form.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.mouseDown);
            form.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.keyPress);
            form.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.keyDown);

        }
    }

}
