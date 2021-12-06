using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace PdaPrint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int qrCodeX = 260;
        private int textX = 190;

        private string bluetoothAddress = "";

        private void PrintQRCode_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text.Equals("")){
                MessageBox.Show("请输入打印内容!");
                this.textBox1.Focus();
                return;
            }
            int Result = 0;
            cpcl_dll dll = new cpcl_dll();

            dll.printer = cpcl_dll.PrinterCreatorS("HM-A300");
            if (0 != dll.printer)
            {
                Result = cpcl_dll.PortOpen(dll.printer, "BT," + bluetoothAddress);
                if (0 == Result)
                {
                    //设置标签大小和打印的数量
                    cpcl_dll.CPCL_AddLabel(dll.printer, 0, 230, 1);
                    //设置文本对齐方式
                    cpcl_dll.CPCL_SetAlign(dll.printer, 0);
                    if (this.textBox1.Text.Length > 44)
                    {
                        cpcl_dll.CPCL_AddText(dll.printer, 0, 7, 2, textX, 142, this.textBox1.Text.Substring(0, 22));
                        cpcl_dll.CPCL_AddText(dll.printer, 0, 7, 2, textX, 170, this.textBox1.Text.Substring(22, 22));
                        cpcl_dll.CPCL_AddText(dll.printer, 0, 7, 2, textX, 198, this.textBox1.Text.Substring(44, (this.textBox1.Text.Length - 44)));
                    }
                    else {
                        if (this.textBox1.Text.Length > 22)
                        {
                            cpcl_dll.CPCL_AddText(dll.printer, 0, 7, 2, textX, 142, this.textBox1.Text.Substring(0, 22));
                            cpcl_dll.CPCL_AddText(dll.printer, 0, 7, 2, textX, 170, this.textBox1.Text.Substring(22, (this.textBox1.Text.Length - 22)));
                        }
                        else
                        {
                            cpcl_dll.CPCL_AddText(dll.printer, 0, 7, 2, textX, 142, this.textBox1.Text);
                        }
                    }
                    byte[] qrCode = { 0x1B, 0x1C, 0x26, 0x20, 0x56, 0x31, 0x20, 0x73, 0x65, 0x74, 0x76, 0x61, 0x6C, 0x20, 0x22, 0x71, 0x72, 0x5F, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x22, 0x20, 0x22, 0x30, 0x33, 0x22, 0x0D, 0x0A };
                    int count = 0;
                    cpcl_dll.DirectIO(dll.printer, qrCode, qrCode.Length, new byte[1024], 0, ref count);
                    count = 0;
                   
                    cpcl_dll.CPCL_AddQRCode(dll.printer, 0, qrCodeX, 8, 2, 4, 1, this.textBox1.Text);

                    byte[] forms = { 0x46, 0x4F, 0x52, 0x4D, 0x0D, 0x0A };
                    cpcl_dll.DirectIO(dll.printer, forms, forms.Length, new byte[1024], 0, ref count);

                    cpcl_dll.CPCL_Print(dll.printer);
                    
                    cpcl_dll.PortClose(dll.printer);
                   
                    cpcl_dll.PrinterDestroy(dll.printer);
                    this.textBox1.Text = "";
                    this.textBox1.Focus();
                }
                else {
                    MessageBox.Show("打印机连接失败!请检查打印机蓝牙地址是否正确!");
                }
            }
            else {
                MessageBox.Show("找不到打印机!HM-A300");
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox1.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string configPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString()) + @"\Address.txt";
            if (!File.Exists(configPath))
            {
                MessageBox.Show("配置文件不存在启用默认打印机,FC58FA404EC4,二维码距离:260,文字距离:190,数越大越靠右");
                bluetoothAddress = "FC58FA404EC4";
                try{
                    using(StreamWriter writAddressAndHeight = new StreamWriter(configPath,false)){
                        writAddressAndHeight.WriteLine("FC58FA404EC4,260,190");
                        writAddressAndHeight.Close();
                    }
                }catch(Exception ex){
                    MessageBox.Show(ex.Message);
                }
            }
            else {
                try {
                    using (StreamReader redAddressAndHeight = new StreamReader(configPath))
                    {
                        string configData = redAddressAndHeight.ReadLine();
                        string[] configs = configData.Split(',');
                        bluetoothAddress = configs[0];
                        qrCodeX = int.Parse(configs[1]);
                        textX = int.Parse(configs[2]);
                        
                        redAddressAndHeight.Close();
                    }
                }catch(Exception ex){
                    MessageBox.Show("配置文件读取出错!" + ex.Message);
                }
            }
            
            this.WindowState = FormWindowState.Maximized;
            this.textBox1.Focus();
        }
    }
    public class cpcl_dll
    {
        [DllImport("CPCL_SDK")]
        public static extern int PrinterCreator(ref IntPtr printer, string model);

        [DllImport("CPCL_SDK")]
        public static extern int PrinterCreatorS(string model);

        [DllImport("CPCL_SDK")]
        public static extern int PrinterDestroy(int printer);

        [DllImport("CPCL_SDK")]
        public static extern int PortOpen(int printer, string ioSettings);

        [DllImport("CPCL_SDK")]
        public static extern int PortClose(int printer);

        [DllImport("CPCL_SDK")]
        public static extern int DirectIO(int printer, byte[] writedata, int wirteNum, byte[] readdata, int readNum, ref int preadedNum);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_AddLabel(int printer, int offSet, int height, int qty);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_SetAlign(int printer, int align);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_AddText(int printer, int rotate, int fontType, int fontSize, int xPos, int yPos, string data);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_AddBarCode(int printer, int rotate, int type, int width, int ratio, int height, int xPos, int yPos, string data);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_AddBarCodeText(int printer, int enable, int fontType, int fontSize, int offSet);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_AddQRCode(int printer, int rotate, int xPos, int yPos, int model, int unitWidth, int eccLevel, string data);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_AddPDF417(int printer, int rotate, int xPos, int yPos, int xDots, int yDots, int columns, int rows, int eccLevel, string data);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_AddImage(int handle, int rotate, int xPos, int yPos, string imagePath);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_AddBox(int printer, int xPos, int yPos, int endXPos, int endYPos, int thickness);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_AddLine(int printer, int xPos, int yPos, int endXPos, int endYPos, int thickness);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_SetFontSize(int printer, int width, int height);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_SetDensity(int printer, int density);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_SetSpeed(int printer, int speed);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_SetTextSpacing(int printer, int Spacing);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_SetTextBold(int printer, int bold);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_SetTextUnderline(int printer, int underline);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_Print(int printer);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_NextLabelPos(int printer);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_PreFeed(int printer, int distance);

        [DllImport("CPCL_SDK")]
        public static extern int CPCL_PostFeed(int printer, int distance);

        public int printer;
    }
}