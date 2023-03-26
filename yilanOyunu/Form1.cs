using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace yilanOyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        yilan yilanim = new yilan(); 
        yon yonumuz;
        PictureBox[] pb_yilanparca;
        bool yemDurumu = false;
        Random random = new Random(); //rastgele yem oluşturmak için random değişkeni oluşturdum
        PictureBox pb_yem;            //yemi koyacağım pb
        float skor = 0;    
        int sure = 0;
        float puansure=0;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            string fileName = "D:\\MetinBelgesi.txt";
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Close();
            timer3.Interval = 1000; //puan durumunu hesaplayacağım timer
            timer2.Interval = 1000; //geçen süreyi hesaplayacağım timer
            yonumuz = new yon(0, 10);
            pb_yilanparca = new PictureBox[0];
            for (int i = 0; i < 3; i++)//döngü ile boyu 3 birim olacak
            {
                Array.Resize(ref pb_yilanparca, pb_yilanparca.Length + 1);  //yılan oluşturdum
                pb_yilanparca[i] = pb_ekle();
            }
        }
        private PictureBox pb_ekle()
        {
            
            PictureBox pb = new PictureBox();
            pb.Size = new Size(10, 10);     //yılanın kalınlığı     
            pb.BackColor = Color.DarkGreen; //yılanın rengi
            pb.Location = yilanim.PozGetir(pb_yilanparca.Length-1); //bulunacağı yer
            panel1.Controls.Add(pb);
            return pb;
        }
        private void pb_guncelle()
        {
            
            for(int i = 0; i < pb_yilanparca.Length; i++) //yılan parçalarının hareketle beraber güncellenmesi
            {
                pb_yilanparca[i].Location = yilanim.PozGetir(i);
            }
            
        }
        public void timerStart()    //bu fonksiyon ile satır sayısını azaltarak oyun başlayınca timerlar başlayacak
        {
            timer1.Start();
            timer2.Start();
            timer3.Start();
        }
        public void timerStop() //oyun durduğunda veya bittiğinde kullanılacak olan fonksiyon, timerlar duracak
        {
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
        }
        public void timerSifirla()  //oyun bitince puansayacı süre ve skor sıfırlanacak
        {
            sure = 0;
            puansure = 0;
            skor = 0;
        }
        public void ButonGorunmez() //oyun başladığında panel dışındaki araçlar görünmez ve kullanılmaz olacaklar
        {
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            textBox1.Visible = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            textBox1.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            panel3.Visible = false;
        }
        public void ButonGorunur() //oyun bittiğinde panel dışındaki araçlar görünecek ve tıklanacak
        {
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            textBox1.Visible = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            textBox1.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            panel3.Visible = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.B)
            {
                timerStart(); //oyun başladığında timerlar başlayacak
 
            }
            if (e.KeyCode == Keys.Up)
            {
                if (yonumuz._y != 10)
                {
                    yonumuz = new yon(0, -10);
                }

            }
            else if (e.KeyCode == Keys.Down)
            {
                if (yonumuz._y != -10)
                {
                    yonumuz = new yon(0, 10);
                }

            }
            else if (e.KeyCode == Keys.Left)
            {
                if (yonumuz._x != 10)
                {
                    yonumuz = new yon(-10, 0);

                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (yonumuz._x != -10)
                {
                    yonumuz = new yon(10, -0);
                }
            
            }
            else if (e.KeyCode == Keys.D)
            {
                timerStop(); //oyun durunca timerlar duracak
            }


        }


        public void yemYap()    //random kullanılarak panelin herhangi bir yerine yem eklenecek
        {
            if (!yemDurumu)
            {
                PictureBox pb = new PictureBox();
                pb.BackColor = Color.White;
                pb.Size = new Size(10, 10);
                pb.Location = new Point(random.Next(panel1.Width / 10) * 10, random.Next(panel1.Height / 10) * 10);//new Point(610, 0); 
                pb_yem = pb;
                yemDurumu = true;
                panel1.Controls.Add(pb);
            }
           
        }
        public void yemYeme()
        { //yılanparçası yemin üstünden geçince yemi kendisine dahil edecek ve yemDurumu false olarak yeni yem üretecek
           
            if (yilanim.PozGetir(0) == pb_yem.Location)
            {
                if (puansure >= 100)//eğer süre 100'ü geçtiyse
                {
                    puansure = 0;//puanlamanın süresi sıfırlanacak
                }
                else if (yilanim.PozGetir(0) == pb_yem.Location && (pb_yem.Location == new Point(0, 0) || 
                    pb_yem.Location == new Point(610, 0) || pb_yem.Location == new Point(0, 350) || 
                    pb_yem.Location == new Point(610,350)))
                {//bu şartta köşelere gelmesi durumunda ödül olarak skora 100 eklemesini söyledim
                    skor = skor + 100;                   
                }
                else
                {
                    skor = skor + (100 / puansure);//geçmediyse 100'ü yediği saniye bölüp skora ekledim
                    puansure = 0;
                }
                yilanim.uza();
                Array.Resize(ref pb_yilanparca, pb_yilanparca.Length + 1);
                pb_yilanparca[pb_yilanparca.Length - 1] = pb_ekle();
                yemDurumu = false;
                panel1.Controls.Remove(pb_yem);
            }
            
           
        }
        public void yeniOyun()
        {//yeni oyun için oluşturduğum fonksiyondur
            yemDurumu = false;//yem durumunu değişerek tekrar yem oluşturmasını sağladım
            yilanim = new yilan();
            timer3.Interval = 1000;
            timer2.Interval = 1000;
            yonumuz = new yon(0, 10);
            pb_yilanparca = new PictureBox[0];
            for (int i = 0; i < 3; i++)
            {//yılanın boyunu ilk baştaki boyutuna getirdim
                Array.Resize(ref pb_yilanparca, pb_yilanparca.Length + 1);
                pb_yilanparca[i] = pb_ekle();
            }
           
            timerStart();
            ButonGorunmez();//butonlaı görünmez kılıp enabled kapattım
            
        }
        public void YilanaCarpma()
        {//yilana çarptığında oyunu bitiren fonksiyon
            for(int i = 1; i < yilanim.Yilan_Uzunluk; i++)
            {
                if (yilanim.PozGetir(0) == yilanim.PozGetir(i))
                {

                    StreamWriter SW = File.AppendText("D:\\MetinBelgesi.txt");
                    SW.WriteLine("süre: ",sure);
                    SW.WriteLine("puan: ",skor);
                    SW.Close();
                    //var olan dosyaya skoru ve puanı ekledim
                    timerStop();
                    ButonGorunur();
                    timerSifirla();
                    //oyun bittiği için butonları açıp zamanı skoru vs sıfırladım
                    panel1.Controls.Remove(pb_yem);//oyun bitince yemi ortadan kaldırdım
                    for (int j = 0; j < pb_yilanparca.Length; j++)
                    {//burada oyun bitince yılan dizisini sildim
                        panel1.Controls.Remove(pb_yilanparca[j]);
                    }//remove sayesinde yeni oyun başlayınca eski yem ve yılan olmayacak
                    DialogResult dialog = new DialogResult();
                    dialog = MessageBox.Show("Kendinde çarptın, yeni oyun başlasın mı?",
                    "OYUN BİTTİ", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {//yeni oyun sorusu olumlu yanıtlanırsa yeni oyun fonks. çağırdım
                        yeniOyun();
                    }
                    else
                    {//cevap evet değilse butonlar açılıp süre sıfırlanacak
                        timerStop();
                        ButonGorunur();
                        timerSifirla();
                        textBox1.Text = "";                    
                    }
              }
            }
      }
        public void kenarlaraCarpma()
        {
            Point p = yilanim.PozGetir(0);
            if (p.X < 0 || p.X > panel1.Width - 10 || p.Y < 0 || p.Y > panel1.Height - 10)
            {//yılanın kenarlara çarpma durumunu oluşturup oyunu bitirdim
                StreamWriter SW = File.AppendText("D:\\MetinBelgesi.txt");
                SW.WriteLine("süre: "+sure);
                SW.WriteLine("puan: "+skor);
                //skoru ve süreyi dosyaya ekledim
                SW.Close();
                timerStop();
                ButonGorunur();
                timerSifirla();
                panel1.Controls.Remove(pb_yem);
                for(int i = 0; i < pb_yilanparca.Length; i++)
                {
                    panel1.Controls.Remove(pb_yilanparca[i]);
                }  
                DialogResult dialog = new DialogResult();
                dialog = MessageBox.Show("Duvara çarptın, yeni oyun başlasın mı?", 
                "OYUN BİTTİ", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    yeniOyun();
                }
                else
                {
                    timerStop();
                    ButonGorunur();
                    timerSifirla();
                    textBox1.Text = "";
                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {//timer başladığında oyunun başlamasını sağladım
           label4.Text = sure.ToString();
            YilanaCarpma();
            kenarlaraCarpma();
            yilanim.ilerle(yonumuz);
            pb_guncelle();
            yemYap();
            yemYeme();
            label5.Text = skor.ToString();
            label4.Text = sure.ToString();
            //labellara süreyi ve skoru yazdırdım
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //yanlışlıkla panele çift tıkladım
        }
        private void panel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //yanlışlıkla
        }
        private void timer2_Tick(object sender, EventArgs e)
        {    //timer2'yi süreye atadım 
            sure++;
            label4.Text = sure.ToString();
        }
        private void label5_Click(object sender, EventArgs e)
        {
            //yanlışlıkla
        }
        

        private void timer3_Tick(object sender, EventArgs e)
        {//timer3'ü puanlamaya atadım
            puansure++;
        }

        private void button2_Click(object sender, EventArgs e)
        {//kişiyi kaydet butonuyla olan dosyaya isim ekledim
            StreamWriter SW = File.AppendText("D:\\MetinBelgesi.txt");
            SW.WriteLine(textBox1.Text);
            SW.Close();
            if (textBox1.Text == "")
            {//isim yoksa bildiirm verdim
                MessageBox.Show("lütfen bir isim giriniz");
            }
            else
            {//isim kaydedildi diyip enabled kapattım
                MessageBox.Show(textBox1.Text+" kişisi Kaydedildi :)");
                button2.Enabled = false;//enabled'ı radiobutton işaretlemesini kontrol etmek için kapadım
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {//yardım butonu
            MessageBox.Show("Bu oyun Şeyma Başgöze tarafından yazılmıştır."+
            "\nOyunu oynamak için yön tuşlarını kullanmalısınız\n"+
            "Kendinize veya Kenarlara çarparsanız oyun sonlanacaktır.\n"+
            "Başarılar dilerim, iyi eğlenceler :)");
        }

        private void button3_Click(object sender, EventArgs e)
        {//skorları görüntülemek için dosyayı okudum
            string yol = "D:\\MetinBelgesi.txt"; //txt dosyasını açmak için kullandım
            System.Diagnostics.Process yap = new System.Diagnostics.Process();
            yap.StartInfo.FileName = yol;
            yap.Start();
            if (textBox1.Text == "")
            {//isim girilmeden basılması durumunda bildirim verdim
                MessageBox.Show("lütfen bir isim giriniz");
            }        
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || button2.Enabled == true)
            {//textbox boşsa veya kişi kaydetme butonu aktifse radiobuttonları tıklamaya kapattım
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                label6.Text = "isim giriniz!";
            }
            else
            {
                label6.Text = "";
            }
            timer1.Interval = 400; //kolay mod için yılan hızını yavaşlattım
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || button2.Enabled==true)
            {//textbox boşsa veya kişi kaydetme butonu aktifse radiobuttonları tıklamaya kapattım
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                label6.Text = "isim giriniz!";
            }
            else
            {
                label6.Text = "";
            }
            timer1.Interval = 110; //zor mod için hızlandırdım        
        }

        private void radioButton1_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (e.KeyCode == Keys.B)
                {
                    ButonGorunmez();
                    timerStart();
                }
                
                if (e.KeyCode == Keys.Up)
                {
                    if (yonumuz._y != 10)
                    {
                        yonumuz = new yon(0, -10);
                    }

                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (yonumuz._y != -10)
                    {
                        yonumuz = new yon(0, 10);
                    }

                }
                else if (e.KeyCode == Keys.Left)
                {
                    if (yonumuz._x != 10)
                    {
                        yonumuz = new yon(-10, 0);

                    }
                }
                else if (e.KeyCode == Keys.Right)
                {
                    if (yonumuz._x != -10)
                    {
                        yonumuz = new yon(10, -0);
                    }

                }
                else if (e.KeyCode == Keys.D)
                {
                    timerStop();
                }
            }
            else
            {
                MessageBox.Show("lütfen isim gir");
            }
        }

        private void radioButton2_KeyDown(object sender, KeyEventArgs e)
        {

            if (textBox1.Text != "")
            {
                if (e.KeyCode == Keys.B)
                {
                    ButonGorunmez();
                    timerStart();
                }
                
                if (e.KeyCode == Keys.Up)
                {
                    if (yonumuz._y != 10)
                    {
                        yonumuz = new yon(0, -10);
                    }

                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (yonumuz._y != -10)
                    {
                        yonumuz = new yon(0, 10);
                    }

                }
                else if (e.KeyCode == Keys.Left)
                {
                    if (yonumuz._x != 10)
                    {
                        yonumuz = new yon(-10, 0);

                    }
                }
                else if (e.KeyCode == Keys.Right)
                {
                    if (yonumuz._x != -10)
                    {
                        yonumuz = new yon(10, -0);
                    }

                }
                else if (e.KeyCode == Keys.D)
                {
                    timerStop();
                }
            }
            else
            {
                MessageBox.Show("lütfen isim gir");
            }
        }
        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void label6_FontChanged(object sender, EventArgs e)
        {
            //yanlışlıkla
        }
    }
    }