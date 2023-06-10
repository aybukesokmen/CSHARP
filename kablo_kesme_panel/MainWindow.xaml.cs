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
using System.IO;
using System.IO.Ports;
using System.Windows.Threading;
using System.Data.OleDb;
using System.Data;
using Microsoft.Win32;
namespace kablo_kesme_panel
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        //SINIFLAR
        SerialPort serialPort1 = new SerialPort();
        DispatcherTimer timer1;
        DispatcherTimer timer2;
        int sn = 0;

        /**************/
        //**********degiskenler*********
        int kontrol_flag = 0;
        string imlec = ";";
        string CR = "\r";
        string LF = "\n";
        string veri;
        string frekans;
        int bekle_flag = 0;
        int step, tur, saniye;
        int basladi_flag = 0;
        int reset_flag = 0;
        int adim_dondurme_flag = 0;
        int yon_flag = 0; //sağ
        int step_flag = 0;
        int frekans_flag = 0;
        int saniye_flag = 0;
        int tur_flag = 0;
        int donme_miktari_sayac = 0;


        string duzenlenmis_veri;
        public MainWindow()
        {

            string[] ports = SerialPort.GetPortNames();
            cmbbx_com.ItemsSource = ports;
            cmbbx_com.SelectedIndex = 0;
            string[] baudrate = new string[3] { "9600", "57600", "115200" };
            string[] databit = new string[3] { "8", "7", "6" };
            string[] stopbits = new string[3] { "1", "2", "3" };
            string[] paritybits = new string[3] { "None", "Odd", "Even" };
            for (int i = 0; i < 3; i++)
            {
                cmbbx_stop_bits.Items.Add(stopbits[i]);
                cmbbx_baudrate.Items.Add(baudrate[i]);
                cmbbx_parity.Items.Add(paritybits[i]);
                cmbbx_data.Items.Add(databit[i]);
            }

            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
            }
            cmbbx_baudrate.SelectedIndex = 1;
            cmbbx_data.SelectedIndex = 0;
            cmbbx_parity.SelectedIndex = 0;
            cmbbx_stop_bits.SelectedIndex = 0;




        }


        private void btn_baglanti_Ac_Click(object sender, RoutedEventArgs e)
        {
            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(1);
            timer1.Tick+=new EventHandler(timer1_Tick);
            timer1.Start();
          
            timer_sayac = 0;
            basladi_flag = 1;
            try
            {
                serialPort1.PortName = cmbbx_com.Text;
                serialPort1.BaudRate = Convert.ToInt32(cmbbx_baudrate.Text);
                serialPort1.DataBits = Convert.ToInt32(cmbbx_data.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbbx_stop_bits.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cmbbx_parity.Text);
                serialPort1.Open();
                timer1.Start();
                lbl_baglanti_durum.Content = "Baglantı kuruldu";

            }
            catch (Exception err)
            {

                progresbar_1.Value = 100;

            }
            adresleme(1);
            timer2.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
      
            try
            {

                string sonuc = serialPort1.ReadExisting();
                byte[] ascii = Encoding.ASCII.GetBytes(sonuc);

                serialPort1.DiscardInBuffer();//Bufferdaki veriyi siler


                // string sonuc = serialPort1.ReadLine();

            }
            catch (Exception ex)
            {
                lbl_baglanti_durum.Content = ex.Message;
                timer1.Stop();
                throw;
            }
        }
        void adresleme(int istenen_index)
        {
           // step = comboBox_step.Text;
           // saniye = numericUpDown_saniye.Value.ToString();
           // frekans = textbox_frekans.Text;
           // tur = numericUpDown_tur.Value.ToString();


            switch (istenen_index)
            {
                case 1:
                    veri = basladi_flag.ToString("00000");
                    serialPort1.Write(imlec + istenen_index.ToString("0") + veri + CR + LF);
                    basladi_flag = 0;
                    break;
                case 2:
                    step_flag = 1;
                    step_flag = Convert.ToInt32(step);
                    veri = step_flag.ToString("00000");
                    serialPort1.Write(imlec + istenen_index.ToString("0") + veri + CR + LF);
                    break;
                case 3:
                    reset_flag = 2;

                    lbl_baglanti_durum.Content = "resetlendi";
                    lbl_baglanti_durum.Background =Brushes.Red;

                    veri = reset_flag.ToString("00000");

                    serialPort1.Write(imlec + istenen_index.ToString("0") + veri + CR + LF);
                    break;
                case 4:
                    frekans_flag = 1;
                    frekans_flag = Convert.ToInt32(frekans);
                    veri = frekans_flag.ToString("00000");
                    serialPort1.Write(imlec + istenen_index.ToString("0") + veri + CR + LF);
                    break;
                case 5:

                    saniye_flag = Convert.ToInt32(saniye);
                    veri = saniye_flag.ToString("00000");
                    serialPort1.Write(imlec + istenen_index.ToString("0") + veri + CR + LF);
                    break;
                case 6:
                    adim_dondurme_flag = 3;
                    veri = adim_dondurme_flag.ToString() + yon_flag.ToString();

                    serialPort1.Write(imlec + istenen_index.ToString("0") + veri + CR + LF);
                    break;
                case 7:
                    bekle_flag = 1;
                    veri = "*****";
                    serialPort1.Write(imlec + istenen_index.ToString("0") + veri + CR + LF);
                    break;
                case 8:

                    tur_flag = Convert.ToInt32(tur);
                    veri = tur_flag.ToString("00000");
                    serialPort1.Write(imlec + istenen_index.ToString("0") + veri + CR + LF);
                    break;
                case 9:
                    if (adim_dondurme_flag == 2)//sağ
                    {
                        veri = "0000R";
                        serialPort1.Write(imlec + istenen_index + veri + CR + LF);


                    }
                    if (adim_dondurme_flag == 1)
                    {
                        veri = "0000L";
                        serialPort1.Write(imlec + istenen_index + veri + CR + LF);

                    }
                    break;

            }
            lbl_index.Content = istenen_index.ToString();
        }


        void button_aktif_fonksiyonu(bool x)
        {
            btn_arti.IsEnabled = x;
            btn_baglanti_kapa.IsEnabled = x;
            btn_eksi.IsEnabled = x;
            btn_tam_tur.IsEnabled = x;
            btn_ayar_baslat.IsEnabled = x;
            btn_ayar_duraklat.IsEnabled = x;
            btn_ayar_durdur.IsEnabled = x;
            btnOpenFile.IsEnabled = x;
            btn_gonder.IsEnabled = x;
            btn_acil_Stop.IsEnabled = x;
        }
        private void btn_baglanti_kapa_Click(object sender, RoutedEventArgs e)
        {
            timer_sayac = 0;
            serialPort1.DiscardInBuffer();//Bufferdaki veriyi siler
            if (serialPort1.IsOpen)
            {
                timer1.Stop();
                serialPort1.Close();
                progresbar_1.Value = 0;
                lbl_baglanti_durum.Content = "Baglanti kapatıldı";
                progresbar_1.Value = 0;

            }

        }
        int i = 0;
        private void btn_acil_Stop_Click(object sender, RoutedEventArgs e)
        {

            timer1.Stop();
            saniye =0;
            timer1.Tick+=new EventHandler(timer1_Tick);

        }

        int bekleme_Suresi = 5;
        int timer_sifir_flag = 0;
        int timer_sayac = 0;

        private void timer2_Tick(object sender, EventArgs e)
        {

            timer_sifir_flag = 0;
            step_flag = 0;
            timer2.Interval=TimeSpan.FromSeconds(1);
            bekleme_Suresi = bekleme_Suresi - 1;
            lbl_saniye.Content = Convert.ToString(bekleme_Suresi);
            lbl_saniye.Background =Brushes.Red;
            if (bekleme_Suresi == 0)
            {
                if (basladi_flag == 0)
                {
                    timer_sifir_flag = 1;
                    timer_sayac++;
                    lbl_saniye.Content = timer_sayac.ToString();
                    timer2.Stop();
                    bekleme_Suresi = 5;
                    if (timer_sayac == 1)
                    {
                        adresleme(7);
                    }
                   
                    if (adim_dondurme_flag == 2)
                    {
                        if (timer_sayac == 5 || timer_sayac == 2)
                        {
                            adresleme(8);
                            timer2.Start();
                        }
                    }

                }

            }
            saniye++;
            lbl_saniye.Content = saniye.ToString();
        }

        private void btn_gonder_Click(object sender, RoutedEventArgs e)
        {
            string constr= "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+txt_dosya_ac.Text+"';Extended Properties=\"Excel 16.0;HDR=Yes;\'";
             OleDbConnection con =new OleDbConnection(constr);
            OleDbDataAdapter sda = new OleDbDataAdapter("Select *From['" + txt_dosya_Ad_2.Text + "$']", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            data_grid.DataContext = dt;

        }

        private void btn_eksi_Click(object sender, RoutedEventArgs e)
        {
            yon_flag = 1;
            donme_miktari_sayac--;
            adim_dondurme_flag = 1;
            adresleme(9);

        }

        private void btn_tam_tur_Click_1(object sender, RoutedEventArgs e)
        {
         
        }

        private void btn_istenen_tur_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
        private void btn_dosyadan_Ac_Click(object sender, RoutedEventArgs e)
        {
           
        }
        
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile=new OpenFileDialog();
            if (openfile.ShowDialog() == true)
            {
                openfile.InitialDirectory = @"c:\temp\";
                openfile.Title = "Dosya Seç";
                txt_dosya_ac.Text = openfile.FileName;
                openfile.Filter = "Excel Sayfası(*.xls)|*.xls|Tüm Dosyalar(*.*)|*.*";
                openfile.FilterIndex = 1;
                openfile.RestoreDirectory = true;
                if (openfile.ShowDialog() == true)
                {
                    txt_dosya_ac.Text = openfile.FileName;
                }
        



            }
               
        }

        private void btn_bekle_Click(object sender, RoutedEventArgs e)
        {
            if (basladi_flag == 0)
            {
                timer2.Start();
                if (lbl_saniye.Content == "0")
                {
                    bekle_flag = 0;
                    adresleme(7);
                }

            }
        }

        private void btn_arti_Click(object sender, RoutedEventArgs e)
        {
            adim_dondurme_flag = 2;

            yon_flag = 0;
            donme_miktari_sayac++;
            adresleme(9);

        }

        private void Kablo_Kesme_Closed(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
            }
        }
    }
}
