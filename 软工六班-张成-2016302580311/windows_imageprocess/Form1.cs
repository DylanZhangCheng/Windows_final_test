using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Stopwatch sw = new Stopwatch();
        Bitmap bitmap;
        Bitmap bar;
        string path;
        string name;
        //原图的宽和高  
        int w, h;
		Image image_1;
		Image image_2;
		Image image_4;

		private void button9_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{

				path = openFileDialog1.FileName;
				name = openFileDialog1.FileNames.ToString();

				FileStream file = new FileStream(path, FileMode.OpenOrCreate);


				pictureBox2.Image = Image.FromStream(file);
				image_1 = Image.FromStream(file);

				bitmap = (Bitmap)pictureBox2.Image;
				// bitmap = (Bitmap)Image.FromFile(path);
				w = bitmap.Width;
				h = bitmap.Height;
				pictureBox2.Image = bitmap.Clone() as Image;
				bar = (Bitmap)pictureBox2.Image.Clone();

				file.Close();
			}
		}

		private void button10_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{

				path = openFileDialog1.FileName;
				name = openFileDialog1.FileNames.ToString();

				FileStream file = new FileStream(path, FileMode.OpenOrCreate);


				pictureBox3.Image = Image.FromStream(file);
				image_2 = Image.FromStream(file);

				bitmap = (Bitmap)pictureBox3.Image;
				// bitmap = (Bitmap)Image.FromFile(path);
				w = bitmap.Width;
				h = bitmap.Height;
				pictureBox3.Image = bitmap.Clone() as Image;
				bar = (Bitmap)pictureBox3.Image.Clone();

				file.Close();
			}
		}


		private void button11_Click(object sender, EventArgs e)
		{
			List<Image> imageList = new List<Image>();
			imageList.Add(image_1);
			imageList.Add(image_2);

			Image final_image = JoinImage(imageList, 0);
			pictureBox4.Image = final_image;
			//if (openFileDialog1.ShowDialog() == DialogResult.OK)
			//{

			//	path = openFileDialog1.FileName;
			//	name = openFileDialog1.FileNames.ToString();

			//	FileStream file = new FileStream(path, FileMode.OpenOrCreate);


			//	pictureBox4.Image = Image.FromStream(file);
			//	image_4 = Image.FromStream(file);

			//	bitmap = (Bitmap)pictureBox4.Image;
			//	// bitmap = (Bitmap)Image.FromFile(path);
			//	w = bitmap.Width;
			//	h = bitmap.Height;
			//	pictureBox4.Image = bitmap.Clone() as Image;
			//	bar = (Bitmap)pictureBox4.Image.Clone();

			//	file.Close();
			//}
		}

		private Image JoinImage(List<Image> imageList, int a)
		{
			//图片列表
			if (imageList.Count <= 0)
				return null;
			if (a == 0)
			{
				//横向拼接
				int width = 0;
				//计算总长度
				foreach (Image i in imageList)
				{
					width += i.Width;
				}
				//高度不变
				//int height = imageList.Max(x => x.Height);
				int height = imageList[1].Height;
				//构造最终的图片白板
				Bitmap tableChartImage = new Bitmap(width, height);
				Graphics graph = Graphics.FromImage(tableChartImage);
				//初始化这个大图
				graph.DrawImage(tableChartImage, width, height);
				//初始化当前宽
				int currentWidth = 0;
				foreach (Image i in imageList)
				{
					//拼图
					graph.DrawImage(i, currentWidth, 0);
					//拼接改图后，当前宽度
					currentWidth += i.Width;
				}
				return tableChartImage;
			}
			else if (a == 1)
			{
				//纵向拼接
				int height = 0;
				//计算总长度
				foreach (Image i in imageList)
				{
					height += i.Height;
				}
				//宽度不变
				//int width = imageList.Max(x => x.Width);
				int width = imageList[1].Width;
				//构造最终的图片白板
				Bitmap tableChartImage = new Bitmap(width, height);
				Graphics graph = Graphics.FromImage(tableChartImage);
				//初始化这个大图
				graph.DrawImage(tableChartImage, width, height);
				//初始化当前宽
				int currentHeight = 0;
				foreach (Image i in imageList)
				{
					//拼图
					graph.DrawImage(i, 0, currentHeight);
					//拼接改图后，当前宽度
					currentHeight += i.Height;
				}
				return tableChartImage;
			}
			else
			{
				return null;
			}
		}

		private void button12_Click(object sender, EventArgs e)
		{
			if (pictureBox4.Image == null) { MessageBox.Show("请导入图片文件"); }
			else
			{
				saveFileDialog1.InitialDirectory = "";
				saveFileDialog1.Filter = "JPEG (*.jpg)|Bitmap (*.bmp)|*.bmp|*.jpg|EMF (*.emf)|*.emf|PNG (*.png)|*.png|GIF (*.gif)|*.gif|TIFF (*.tif)|*.tif";
				if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					string folderP = saveFileDialog1.FileName;
					Image img = image_4;
					img.Save(folderP);
				}
			}
		}

		//打开图片（采用输入流打开，防止文件被锁定而导致只能另存为无法保存）
		private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                path = openFileDialog1.FileName;
                name = openFileDialog1.FileNames.ToString();

                FileStream file = new FileStream(path, FileMode.OpenOrCreate);


                pictureBox1.Image = Image.FromStream(file);

                bitmap = (Bitmap)pictureBox1.Image;
                // bitmap = (Bitmap)Image.FromFile(path);
                w = bitmap.Width;
                h = bitmap.Height;
                pictureBox1.Image = bitmap.Clone() as Image;
                bar = (Bitmap)pictureBox1.Image.Clone();

                file.Close();
            }
                
        }




        //保存
        private void button6_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) { MessageBox.Show("请导入图片文件"); }
            else
            {
                Image img = pictureBox1.Image;
                img.Save(path);
                MessageBox.Show("保存成功");
            }
        }

        //另存为
        private void button8_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) { MessageBox.Show("请导入图片文件"); }
            else
            {
                saveFileDialog1.InitialDirectory = "";
                saveFileDialog1.Filter = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg|EMF (*.emf)|*.emf|PNG (*.png)|*.png|GIF (*.gif)|*.gif|TIFF (*.tif)|*.tif";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string folderP = saveFileDialog1.FileName;
                    Image img = pictureBox1.Image;
                    img.Save(folderP);
                }
            }
        }

        //上下翻转
        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) { MessageBox.Show("请导入图片文件"); }
            else {
                Bitmap a = new Bitmap(pictureBox1.Image);
                pictureBox1.Image = RevPic1(a) as Image;
                bar = (Bitmap)pictureBox1.Image.Clone();
            }
        }

        //左右翻转
         private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) { MessageBox.Show("请导入图片文件"); }
            
            else
            {
                Bitmap c = new Bitmap(pictureBox1.Image);
                pictureBox1.Image = RevPic2(c) as Image;
                bar = (Bitmap)pictureBox1.Image.Clone();

            }
        }

        //任意角度旋转
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == ""&& pictureBox1.Image == null) { MessageBox.Show("请导入图片文件"); }

            else if(textBox1.Text == "" && pictureBox1.Image != null) { MessageBox.Show("请输入旋转角度"); }
            else
            {
                Bitmap a = new Bitmap(pictureBox1.Image);//得到图片框中的图片  
                pictureBox1.Image = Rotate(Convert.ToInt32(textBox1.Text),a);
                bar = (Bitmap)pictureBox1.Image.Clone();
            }
        }

       //灰度化处理
        private void button5_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) { MessageBox.Show("请导入图片文件"); }
            else
            {
                Bitmap c = new Bitmap(pictureBox1.Image);
                pictureBox1.Image = GetGrayImage(c) as Image;
                bar = (Bitmap)pictureBox1.Image.Clone();
            }
        }

       //还原
        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = bitmap.Clone() as Image;
        }

        //亮度条
        private void trackBar1_Scroll(object sender, EventArgs e)
        {          
            pictureBox1.Image = Brightness(bar) as Image;

        }

        //灰度化函数
        Bitmap GetGrayImage(Bitmap image)
        {       
            Bitmap result = image.Clone() as Bitmap;
            Color c = new Color();
            int ret;
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    c = result.GetPixel(i, j);
                    // 计算点i,j的灰度值
                    ret = (int)(c.R * 0.299 + c.G * 0.587 + c.B * 0.114);

                    result.SetPixel(i, j, Color.FromArgb(ret, ret, ret));
                }
            }
            return result;
        }

        //旋转函数
        public Bitmap Rotate(int angle,Bitmap a)
        {
            angle = angle % 360;

            //弧度转换  
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);

            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));

            //目标位图  
            Bitmap dsImage = new Bitmap(W, H);
            Graphics g = Graphics.FromImage(dsImage);

            g.InterpolationMode = InterpolationMode.Bilinear;

            g.SmoothingMode = SmoothingMode.HighQuality;

            //计算偏移量  
            Point Offset = new Point((W - w) / 2, (H - h) / 2);

            //构造图像显示区域：让图像的中心与窗口的中心点一致  
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(360 - angle);

            //恢复图像在水平和垂直方向的平移  
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(a, rect);

            //重至绘图的所有变换  
            g.ResetTransform();

            g.Save();
            g.Dispose();
            return dsImage;
        }

        //左右镜像翻转函数
        public Bitmap RevPic1(Bitmap mybm)
        {
           int width = mybm.Width;
           int height  = mybm.Height;
            Bitmap bm = new Bitmap(width, height);//初始化一个记录经过处理后的图片对象
            int x, y, z;//x,y是循环次数,z是用来记录像素点的x坐标的变化的
            Color pixel;
            for (y = height - 1; y >= 0; y--)
            {
                for (x = width - 1, z = 0; x >= 0; x--)
                {
                    pixel = mybm.GetPixel(x, y);//获取当前像素的值
                    bm.SetPixel(z++, y, Color.FromArgb(pixel.R, pixel.G, pixel.B));//绘图
                }
            }
            return bm;//返回经过翻转后的图片
        }

        //上下镜像翻转函数
        public Bitmap RevPic2(Bitmap mybm)
        {
            int width = mybm.Width;
            int height = mybm.Height;
            Bitmap bm = new Bitmap(width, height);//初始化一个记录经过处理后的图片对象
            int x, y, z;//x,y是循环次数,z是用来记录像素点的x坐标的变化的
            Color pixel;
            for (x = 0; x < width; x++)
            {
                for (y = height - 1, z = 0; y >= 0; y--)
                {
                    pixel = mybm.GetPixel(x, y);//获取当前像素的值
                    bm.SetPixel(x, z++, Color.FromArgb(pixel.R, pixel.G, pixel.B));//绘图
                }
            }
            return bm;//返回经过翻转后的图片
        }

		





		//亮度函数
		private Bitmap Brightness(Bitmap map)
        {
            float grain = trackBar1.Value;
            Color c;
            Func<int, int> notOver255 = (x) => { return x > 255 ? 255 : x; };
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    c = map.GetPixel(x, y);
                    Color brightColor = Color.FromArgb(
                        notOver255((int)(c.R * grain)),
                        notOver255((int)(c.G * grain)),
                        notOver255((int)(c.B * grain)));
                    map.SetPixel(x, y, brightColor);
                }
            }
            return map;
        }
    }

	
}
