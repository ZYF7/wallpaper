using System;
using System.Windows.Forms;



namespace wallpaper
{
    public partial class Form2 : Form
    {


        Form1 Form1 = new Form1();
        public Form2()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
        }

        //点击退出按钮
        private void button2_Click(object sender, EventArgs e)
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

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
