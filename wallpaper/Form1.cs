using System;
using System.Diagnostics;
using System.Drawing;

using System.Windows.Forms;


namespace wallpaper
{
    public partial class Form1 : Form
    {

        string URL = "";
        //控件变量

        int x = 0;
        int y = 0;
        //当前时间
        //DateTime mytime = DateTime.Now;

        private IntPtr Ptr = IntPtr.Zero;
        public void FormBackGround()
        {
            InitializeComponent();
            Ptr = this.Handle;
            SetBackGroud();
        }


        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            //启动按钮层级
            this.Controls.SetChildIndex(button1, 10);
            //img 层级
            this.Controls.SetChildIndex(pictureBox1, 1);
            //获取屏幕分辨率
            //int xWidth3 = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            //int yHeight3 = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            //设置窗体尺寸
            this.Width = 1000;
            this.Height = 600;
            //图片控件
            //this.pictureBox1.Width = xWidth3;
            //this.pictureBox1.Height = yHeight3;
            this.pictureBox1.Width = 200;
            this.pictureBox1.Height = 200;

        }
        private void SetBackGroud()
        {
            Ptr = Win32.FindWindow("Progman", null);
            //  Ptr = Win32.FindWindow(null, "Form1");
            if (Ptr != IntPtr.Zero)
            {
                IntPtr resultIntPtr = IntPtr.Zero;
                Win32.SendMessageTimeout(Ptr, 0x52c, IntPtr.Zero, IntPtr.Zero, 0, 0x3e8, resultIntPtr);
                Win32.EnumWindows((hwnd, IProgress) =>
                {
                    if (Win32.FindWindowEx(hwnd, IntPtr.Zero, "SHELLDLL_DefView", null) != IntPtr.Zero)
                    {
                        IntPtr tempHwnd = Win32.FindWindowEx(IntPtr.Zero, hwnd, "WorkerW", null);
                        Win32.ShowWindow(tempHwnd, 0);
                    }
                    return true;
                }, IntPtr.Zero);
            }
            // 窗口置父，设置背景窗口的父窗口为 Program Manager 窗口
            Win32.SetParent(this.Handle, Ptr);
        }

        //点击启动
        #region 点击启动
        private void button1_Click(object sender, EventArgs e)
        {
            Process current = Process.GetCurrentProcess();//当前新启动的线程

            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            // MessageBox.Show(processes.Length.ToString());
            //  MessageBox.Show(processes[0].Id.ToString());


            for (int l = 0; l < processes.Length; l++)
            {

                //MessageBox.Show(current.Id.ToString()+";;;"+ processes[l].Id.ToString());
                if (processes[l].Id != current.Id)
                {

                    processes[l].Kill();
                }
            }



            //窗口位置
            this.Location = new System.Drawing.Point(0, 0);
            //this.WindowState = FormWindowState.Maximized;
            //设置图片控件尺寸
            int xWidth3 = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int yHeight3 = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            //this.pictureBox1.Width = xWidth3;
            //this.pictureBox1.Height = yHeight3;
            //设置窗体尺寸
            this.Width = xWidth3;
            this.Height = yHeight3;
            //设置图片组件
            this.pictureBox1.Width = x;
            this.pictureBox1.Height = y;
            //上图
            try
            {
                pictureBox1.Load(URL);
            }
            catch (Exception ex)
            {
                //显示本地默认图片
            }
            //图片组件位置
            pictureBox1.Left = 0;
            pictureBox1.Top = 0;
            //隐藏一系列组件 
            label1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            //隐藏启动按钮
            button1.Visible = false;
            //放底层
            SetBackGroud();

            //启动控制器====
            Form2 f2 = new Form2();
            f2.ShowDialog();

        }

        #endregion
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        //选择文件夹：点击【浏览】，选择文件
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {


                //其他判断
                if (this.openFileDialog1.FileName != null)
                    this.label1.Text = this.openFileDialog1.FileName;
                //预览
                pictureBox1.Load(this.openFileDialog1.FileName);

                //SafeFileName文件名
                string Fname = this.openFileDialog1.SafeFileName;
                string[] sArray = Fname.Split('.');
                //允许的图片类型
                if (sArray[1] == "jpg" || sArray[1] == "png" || sArray[1] == "gif" || sArray[1] == "jpeg" || sArray[1] == "jfif" || sArray[1] == "BMP" || sArray[1] == "bmp")
                {
                    //引用地址赋值
                    URL = this.openFileDialog1.FileName;
                    //this.label1.Text =  width.ToString();
                    //获取尺寸
                    Bitmap pic = new Bitmap(URL);
                    int width = pic.Size.Width;   // 图片的宽度
                    int height = pic.Size.Height;   // 图片的高度
                                                    //屏幕尺寸
                    int xWidth7 = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                    int yHeight7 = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                    //通过判断显示图片·
                    //计算宽高比判断
                    //图片宽高比
                    double picScale = (double)width / (double)height;
                    double ScreenScale = (double)xWidth7 / (double)yHeight7;
                    if (picScale < ScreenScale)
                    {
                        x = Convert.ToInt32(xWidth7);
                        double aa = (double)height / (double)width * xWidth7;
                        double abcc = Math.Floor(aa);
                        y = Convert.ToInt32(abcc);
                    }
                    else
                    {
                        y = Convert.ToInt32(yHeight7);
                        double aa = (double)width / (double)height * yHeight7;
                        double abcc = Math.Floor(aa);
                        x = Convert.ToInt32(abcc);

                    }


                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        //退出程序
        private void button3_Click(object sender, EventArgs e)
        {
            //关闭程序
            System.Diagnostics.Process[] myProcesses = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process myProcess in myProcesses)
            {
                if ("wallpaper" == myProcess.ProcessName)
                {
                    myProcess.Kill();
                }
            }
        }
    }
}


