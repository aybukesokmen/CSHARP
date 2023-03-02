using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Xml;
using System.Timers;
using System.Windows.Threading;
using System.Drawing.Drawing2D;
namespace dmx_control
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

        DMXserial serial;
        DMXmaster master;
        System.Timers.Timer timer;
        BrushConverter converter;
        int saniye = 0;
        int animasyon_sayisi = 10;
        DispatcherTimer timer1;
        public MainWindow()
        {

            InitializeComponent();
            serial = new DMXserial();
            populatePortList();
            master = new DMXmaster();
            timer = new System.Timers.Timer();
            timer.Interval = 1;
            timer.Elapsed += mainLoop;
            converter = new BrushConverter();
            data_grid.ItemsSource = master.fixtures;
            // loadXMLFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\default.xml");
        }


        private void mainLoop(Object source, ElapsedEventArgs e)
        {
            if (!serial.isConnected)
            {
                timer.Stop();
                return;
            }
            int fps = master.update(); //fps değerini gösterir

            dmx_renk_kanallar f = master.getFixtureArray();

            this.Dispatcher.Invoke(() =>
            {
                FPSmeter.Content = fps + " fps";

            });
            serial.send(f.dmx_512);
        }

        dmx_renk_kanallar fixturesAnd = new dmx_renk_kanallar();
        
        private void FixtureEditor_updateUI() //TABITEM:AYARLAR 
        {
           
            
           
            this.Dispatcher.Invoke(() =>
            {
                data_grid.Items.Refresh();
                for (int i = 0; i < master.fixtures.Count; i++)
                {
                    Ellipse myEllipse = new Ellipse();
                    myEllipse.Stroke = System.Windows.Media.Brushes.White;
                    myEllipse.Fill = System.Windows.Media.Brushes.Black;
                    myEllipse.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    myEllipse.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    myEllipse.Width = 40;
                    myEllipse.Height = 40;
                    myEllipse.Margin = new System.Windows.Thickness(5);
                  
                }
            });
        }

        private void populatePortList() //PORT SECİMİ
        {
            string[] ports = serial.ports();

            foreach (string port in ports)
            {
                COMselect.Items.Add(port);
            }
            COMselect.SelectedIndex = 2;

        }

        private void COMconnect_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(COMselect.SelectedItem);
            if (serial.connect(COMselect.SelectedItem.ToString()))
            {
                COMconnect.Background = Brushes.Green;
                timer.Start();
            }
            else
            {
                COMconnect.Background = Brushes.Red;
            }
        }

        private void FIXcreate_Click(object sender, RoutedEventArgs e)//AYARLARDAKİ TEXTBOXLAR
        {

        }


     /*  private void MAINfade_Click(object sender, RoutedEventArgs e)//Geçiş Hızları
        {
            int tag = Int32.Parse((sender as System.Windows.Controls.Button).Tag.ToString());
       //     FADEspeed_text.Text = tag.ToString();
            master.updateMainFade(tag);
   
        }
        */

        private void MAINfade_Change(object sender, RoutedEventArgs e)
        {
            int tag = Int32.Parse((sender as System.Windows.Controls.TextBox).Text);
            master.updateMainFade(tag);
        }

        int sayac = 0;
        int timer_sayac = 0;
        string[] renkler = new string[10];
        int slider_flag = 0;
        int rainbow_flag = 0;
        int su_yolu_flag = 0;
        int polis_sireni_flag = 0;
        int deger = 0;
        double azalma_miktari = 255 / 1000;
        public void timer1_Tick(object sender, EventArgs e)
        {
                 saniye++;
        
            if(saniye%1==0)
            {

       

               if(master.tek_kanal_flag==3)
                {
                    master.su_yolu_sayac++;
                    if(master.su_yolu_sayac==130)
                    {
                        master.su_yolu_sayac = 0;
                    }

                }

                if (master.tek_kanal_flag == 4)
                {
                    master.kaleydoskop_sayac++;
                    if (master.kaleydoskop_sayac == 3)
                    {
                        master.kaleydoskop_sayac = 0;
                    }

                }

                if (master.tek_kanal_flag==5)
                {
                    master.kayan_yazi_sayac++;
      
                    if (master.kayan_yazi_sayac == 4)
                    {
                        master.kayan_yazi_sayac = 0;
                    }
                }


                if (master.tek_kanal_flag == 6)
                {
                   
                 //  master.updateFixtures(tags[0], renk_tags1[master.animasyon_6_sayac]);
                  
                    if(master.animasyon_6_sayac==23)
                    {
                        master.animasyon_6_sayac = 0;
                    }
                    master.animasyon_6_sayac++;
                    
                }


                if (master.tek_kanal_flag == 7)
                {
                    master.kayan_yazi_sayac++;
                    
                    if (master.kayan_yazi_sayac ==5)
                    {
                        master.kayan_yazi_sayac = 0;
                    }
                }

                if (master.tek_kanal_flag == 8)
                {
            
                   
                        master.rainbow_sayac++;
                        master.rainbow_flag = 1;
                        if (master.rainbow_sayac == 15)
                        {
                            master.rainbow_sayac = 0;
                        }
                   

                }
                if (master.tek_kanal_flag ==9)
                {


                    master.emoji_sayac++;

                    if (master.emoji_sayac == 8)
                    {
                        master.emoji_sayac = 0;

                    }


                }

                if (master.tek_kanal_flag == 10)
                {
                   
                    if(master.turk_bayragi_animasyon==0)
                    {
                        master.turk_bayragi_sayac++;
                        if (master.turk_bayragi_sayac == 3)
                        {
                            master.turk_bayragi_sayac = 0;

                        }

                    }

                    if (master.turk_bayragi_animasyon == 1)
                    {
                        master.turk_bayragi_sayac++;
                        if (master.turk_bayragi_sayac == 13)
                        {
                            master.turk_bayragi_sayac = 0;

                        }

                    }

                }


            }

            if (saniye>=60)
            {
                saniye = 0;
            }
        }

        int btn_flag=0;
        string[] tags=new string[] {};

        byte r_byte, g_byte, b_byte;
        string[] renk_tags1 = new string[27] { "#FFFFFF", "#FF777171", "#FF6F0404", "#FF000000", "#FFC7519E", "#FF90418D", "#FF631447", "#FF3A0C69", "#FF00FFBF", "#FF1F6786", "#FF1F2186", "#FF0F1061", "#FFAEF13D", "#FF6DDA26", "#FF439A0A", "#FF255208", "#FFFEFF6E", "#FFFFB55B", "#FFFFD200", "#FFFF9800", "#FFFFA5FC", "#FFFF8AAA", "#FFFF00F6", "#FFFF2762", "#FFFF0000", "#FF0000FF" , "#FF00FF00" };


        private void Color_Click(object sender, RoutedEventArgs e)
        {
            byte [] renkler=new byte[3] { r_byte, g_byte, b_byte };
            
           tags = (sender as System.Windows.Controls.Button).Tag.ToString().Split(',');

         
                master.updateFixtures(tags[0], tags[1]);
           

            /* Color color = Color.FromRgb((byte)slider_r.Value, (byte)slider_g.Value, (byte)slider_b.Value);
             this.Background = new SolidColorBrush(color);
             renk = new SolidColorBrush(color).ToString();*/
            byte[] tag_slider_r = new byte[27] { 255, 119, 111, 0, 119,144,99,58,0,31,31,15,174,109,67,37,254,255,255,255,255,255,255,255,255,0,0};
            byte[] tag_slider_g = new byte[27] { 255, 113, 4, 0, 81, 65,20,12,255,103,33,16 ,241,218,154,82,255,181,210,152,165,138,0,39,0,0,255};
            byte[] tag_slider_b = new byte[27] { 255, 113, 4, 0, 158, 141,71,105,191,134,134,97,61,38,10,8,110,91,0,0,252,170,246,98,0,255,0};
            for (int i = 0; i < 27; i++)
            {
               if (tags[1]==renk_tags1[i])
                {
                    renkler[0] = tag_slider_r[i];
                    renkler[1]=tag_slider_g[i];
                    renkler[2]=tag_slider_b[i];

                }
            }

            slider_r.Value = renkler[0];
            slider_g.Value = renkler[1];
            slider_b.Value = renkler[2];

            master.renk[0]=renkler[0];
            master.renk[1] = renkler[1];
            master.renk[2] = renkler[2];

        }
        
        byte [] dmx_array = new byte[512];

        string renk="";
        volatile string renk_dmx_master;
        double r_slider_deger = 0, g_slider_deger = 0, b_slider_deger = 0;
        private void ColorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
          
            //btn2_renk_deneme.Background =System.Windows.Media.Brush(renk);
           
       
            Color color = Color.FromRgb((byte)slider_r.Value, (byte)slider_g.Value, (byte)slider_b.Value);

            slider_r.Maximum = 255;
            slider_g.Maximum = 255;
            slider_b.Maximum = 255;
            this.Background = new SolidColorBrush(color);
            renk=new SolidColorBrush(color).ToString();

       
            txt_r.Text=(Convert.ToInt16(slider_r.Value)).ToString();
            txt_b.Text= (Convert.ToInt16(slider_b.Value)).ToString();   
            txt_g.Text= (Convert.ToInt16(slider_g.Value)).ToString();

            slider_buyukten_kucuge_siralama();


            master.updateFixtures("Led", renk);

            slider_flag = 1;

            
        }

        private void slider_gecis_Sure_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int deger=1000;
            txt_hiz.Text = slider_gecis_Sure.Value.ToString();
            slider_gecis_Sure.Maximum = 5000;
            tag = Convert.ToInt32(slider_gecis_Sure.Value);


        }
  

        private void animasyon_rainbow_Click(object sender, RoutedEventArgs e)
        {
            sayac = 0;
            rainbow_flag = 1;
            su_yolu_flag = 0;
            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(2);
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }


        int sifirla_flag = 0;
        byte[] dmx = new byte[512];




        private void txt_r_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txt_r.Text == "")
            {
                txt_r.Text = "0";
            }
            slider_r.Value = Convert.ToDouble(txt_r.Text);
        }

        double[] slider_array2 = new double[3];
        double parlaklik_artis_miktari = 0;
        void slider_buyukten_kucuge_siralama()
        {
            int[] slider_array = new int[] {Convert.ToInt16(slider_r.Value), Convert.ToInt16(slider_g.Value), Convert.ToInt16(slider_b.Value) };

            Array.Sort(slider_array);

            for (int i = 0; i < 3; i++)
            {

                slider_array2[i] = slider_array[i];
            }
           
            slider_parlaklik.Value = slider_array2[2];

            
            

        }

        private void slider_parlaklik_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            slider_parlaklik.Minimum = 0;
            slider_parlaklik.Maximum = 255;
            txt_parlaklik.Text=(Convert.ToInt16( slider_parlaklik.Value)).ToString();
            slider_parlaklik.Value = Convert.ToDouble(txt_parlaklik.Text);
            parlaklik_artis_miktari = slider_parlaklik.Value - slider_array2[2];
            slider_r.Value = parlaklik_artis_miktari + slider_r.Value;
            slider_g.Value = parlaklik_artis_miktari + slider_g.Value;
            slider_b.Value=parlaklik_artis_miktari+slider_b.Value;
        }


        private void slider_kanal_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {


     

        }

        int pixel_deger = 0;
        int tek_kanal_flag = 0;
        private void btn_pixel_azat_Click(object sender, RoutedEventArgs e)
        {
            slider_pixel.Value = pixel_deger;
            pixel_deger--;
            txt_pixel.Text = pixel_deger.ToString();
            if(master.tek_kanal_flag==2)
            {
                master.su_yolu_sayac = pixel_deger;
            }

        }



        private void rdbtn_tek_kanal_Checked(object sender, RoutedEventArgs e)
        {
            master.tek_kanal_flag = 1;
        }

        private void rdbtn_cift_kanal_Checked(object sender, RoutedEventArgs e)
        {
            master.tek_kanal_flag = 0;
        }

        private void rdbtn_sira_Checked(object sender, RoutedEventArgs e)
        {
            master.tek_kanal_flag = 2;
            
        }

        int pixel_sayisi = 0;
        private void ayarla_click(object sender, RoutedEventArgs e)
        {
                        master.addFixtures(
                    (NewFixtureName as System.Windows.Controls.TextBox).Text,
                    Int32.Parse((NewFixtureNChannels as System.Windows.Controls.TextBox).Text),
                   (1),
                    (NewFixtureGroups as System.Windows.Controls.TextBox).Text,
                    (1)
                );
            FixtureEditor_updateUI();
            if(txt_pixel_sayisi.Text=="")
            {
                txt_pixel_sayisi.Text = "170";
            }
            pixel_sayisi=Convert.ToInt32(txt_pixel_sayisi.Text);
            master.pixel_sayisi = pixel_sayisi;
        }

        private void btn_iki_Click(object sender, RoutedEventArgs e)
        {
            sifirla();
            master.tek_kanal_flag = 3;

            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(slider_animasyon);
            timer1.Start();
            timer1.Tick += new EventHandler(timer1_Tick);
            
        }


        private void btn_dort_Click(object sender, RoutedEventArgs e)
        {
            sifirla();
            master.tek_kanal_flag = 5;
            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(slider_animasyon);
            timer1.Start();
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        private void btn_bes_Click(object sender, RoutedEventArgs e)
        {
            sifirla();
            master.tek_kanal_flag = 6;
            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(slider_animasyon);
            timer1.Start();
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        private void btn_alti_Click(object sender, RoutedEventArgs e)
        {
            sifirla();
            master.tek_kanal_flag = 4;

            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(slider_animasyon);
            timer1.Start();
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        private void btn_yedi_Click(object sender, RoutedEventArgs e)
        {
            sifirla();
            master.tek_kanal_flag =8;
            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(slider_animasyon);
            timer1.Start();
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        private void btn_sekiz_Click(object sender, RoutedEventArgs e)
        {
            sifirla();
            master.tek_kanal_flag = 9;
            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(slider_animasyon);
            timer1.Start();
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        private void btn_dokuz_Click(object sender, RoutedEventArgs e)
        {
            sifirla();
            master.tek_kanal_flag = 10;
            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(slider_animasyon);
            timer1.Start();
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        private void slider_pixel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            pixel_deger = Convert.ToInt32(slider_pixel.Value);
            txt_pixel.Text = (Convert.ToInt16(slider_pixel.Value)).ToString();
            slider_pixel.Maximum = pixel_sayisi;
            slider_pixel.Minimum =0;
            master.istenen_kanal = Convert.ToInt32(slider_pixel.Value);
            //  rdbtn_tek_kanal.IsChecked = true;
            if(master.tek_kanal_flag==2)
            {
                master.su_yolu_sayac = pixel_deger;
            }
        }

        private void txt_pixel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_pixel.Text == "")
            {
                txt_pixel.Text = "0";
            }
            slider_pixel.Value = Convert.ToDouble(txt_pixel.Text);
            tek_kanal_flag = 1;
        }

        private void btn_pixel_Arttir_Click(object sender, RoutedEventArgs e)
        {
            slider_pixel.Value = pixel_deger;
            pixel_deger++;
            txt_pixel.Text = pixel_deger.ToString();
            if (master.tek_kanal_flag == 2)
            {
                master.su_yolu_sayac=pixel_deger;
            }
        }

        double slider_animasyon = 0;
        
        private void slider_animasyon_gecis_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            slider_animasyon_gecis.Minimum = 0;
            slider_animasyon_gecis.Maximum = 5;
            slider_animasyon = slider_animasyon_gecis.Value;
            txt_animasyon_gecis_sure.Text=slider_animasyon.ToString();
           

        }

        private void txt_animasyon_gecis_sure_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            slider_animasyon = Convert.ToDouble(txt_animasyon_gecis_sure.Text);

        }

        private void rdbtn_bayrak_kırp_Click(object sender, RoutedEventArgs e)
        {
            sifirla();
            master.turk_bayragi_animasyon = 0;
                timer1 = new DispatcherTimer();
                timer1.Interval = TimeSpan.FromSeconds(0.5);
                timer1.Start();
                timer1.Tick += new EventHandler(timer1_Tick);
           

        }

        private void rdbtn_bayrak_dalga_Click(object sender, RoutedEventArgs e)
        {
            sifirla();

            master.turk_bayragi_animasyon = 1;
                timer1 = new DispatcherTimer();
                timer1.Interval = TimeSpan.FromSeconds(0.5);
                timer1.Start();
                timer1.Tick += new EventHandler(timer1_Tick);
            

        }

        void sifirla()
        {
            // timer1.Stop();
            saniye = 0;
            master.su_yolu_sayac = 0;
            master.emoji_sayac = 0;
            master.turk_bayragi_sayac = 0;
            master.rainbow_sayac = 0;
            master.kayan_yazi_sayac = 0;
            master.kaleydoskop_sayac = 0;
        }

        private void btn_reset_Click(object sender, RoutedEventArgs e)
        {
            sifirla();
            master.tek_kanal_flag = 11;
            timer1.Stop();
        }

        private void txt_g_TextChanged(object sender, TextChangedEventArgs e)
        {
            slider_g.Value=Convert.ToDouble(txt_g.Text);
        }

        private void txt_b_TextChanged(object sender, TextChangedEventArgs e)
        {
            slider_b.Value=Convert.ToDouble(txt_b.Text);
        }
        int tag=0;
        private void hiz_button_Click(object sender, RoutedEventArgs e)
        {
            tag = Int32.Parse((sender as System.Windows.Controls.Button).Tag.ToString());
            slider_gecis_Sure.Value = tag;
   
            txt_hiz.Text = tag.ToString();
            master.updateMainFade(tag);
        }


    }
}
