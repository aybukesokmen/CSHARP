using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;

using System.Drawing;
using System.Windows.Shapes;
using System.Timers;
namespace dmx_control
{
    
    public class dmx_renk_kanallar
    {
        public byte[] dmx_512 { get; set; }
        public MyColor[] fixtureColors { get; set; }
    }
    class DMXmaster
    {


        public List<Fixture> fixtures;
        private Effect effect;
        private int channelCount;
        private int mainFade = 1000;
        private double mainMaster = 1;

        private Stopwatch stopwatch;
        private long lastMilliseconds;
        private double millPerFrame;

        private string effectGroup = "Effect";
        Timer timer1 = new Timer();
        public DMXmaster()
        {
            fixtures = new List<Fixture>();
            stopwatch = new Stopwatch();
            stopwatch.Start();
            effect = new Effect(fixtures.Count);
        }

        public int update()
        {
            foreach (Fixture fix in fixtures)
            {
                fix.fade();
            }

            effect.update(millPerFrame);

            millPerFrame += ((double)(stopwatch.ElapsedMilliseconds - lastMilliseconds) - millPerFrame) * 0.1;
            lastMilliseconds = stopwatch.ElapsedMilliseconds;

            return 1000 / (int)millPerFrame;
        }

        public void addFixtures(string name, int nChannels, int startChannel, string groups, int repeat = 1)
        {

            int s = startChannel;
            for (int i = 0; i < repeat; i++)
            {
                fixtures.Add(new Fixture(name, nChannels, s, groups));

            }


            recountChannels();
        }



        volatile string renk_dmx_master = "";
        public void updateFixtures(string name, string values)//renkleri kanallara atar(0-255)
        {
            System.Drawing.Color color = ColorTranslator.FromHtml(values);

            byte[] channels = new byte[3] { color.R, color.G, color.B };

            foreach (Fixture fix in fixtures)
            {
                if (fix.name == name || fix.isInGroup(name))
                    fix.updateChannels(channels, mainFade / millPerFrame);
            }
            renk_dmx_master = new SolidBrush(color).ToString();

        }

        public void updateMainFade(int val)
        {
            mainFade = val;
        }



        private void recountChannels()
        {
            channelCount = 0;

            foreach (Fixture fix in fixtures)
            {
                channelCount += fix.nChannels;
            }

            //**//effect.size = fixtures.Count;
        }
        int sayac = 0;
        byte[] red = new byte[3] { 255, 0, 0 };
        byte[] green = new byte[3] { 0, 255, 0 };
        byte[] blue = new byte[3] { 0, 0, 255 };
        byte[] black = new byte[3] { 0, 0, 0 };
        byte[] amber = new byte[3] { 255, 248, 69 };
        byte[] white = new byte[3] { 255, 255, 255 };
        byte[] dark_yellow = new byte[3] { 255, 152, 0 };
        byte[] yellow = new byte[3] { 253, 255, 0 };
        byte[] purple = new byte[3] {128,0,128 };
        byte[] dark_blue = new byte[3] {15,16,97 };
        byte[] orange = new byte[3] { 255, 127, 0 };
        int index = 0;
        int[] yazi_iki = new int[13] { 274, 262, 211, 199, 196, 220, 250, 286, 289, 292, 241, 232, 181 };
        int[] yazi_iki_pixel = new int[16] { 73, 69, 52, 49, 33, 34, 46, 56, 64, 78, 79, 62, 59, 42, 39, 39 };
        int[] yazi_sifir_pixel = new int[19] { 73, 74, 75, 76, 77, 78, 62, 59, 42, 38, 37, 36, 35, 34, 33, 49, 82, 69, 52 };
        int[] index_array = new int[31];
        public byte[] renk = new byte[3];
        int[] emoji_yuz_beyaz = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 28, 29, 30, 31, 32, 39, 40, 41, 50, 51, 60, 61, 71, 70,80, 81, 90, 91, 92, 99, 100, 101, 102, 103, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120,121, 122, 123, 124, 125, 126, 127, 128, 129, 130 };
        int[] emoji_yuz_sari = new int[] { 24, 25, 26, 27, 33, 34, 35, 36, 37, 38, 42, 43, 44, 45, 46, 47, 48, 49, 52, 53, 54, 55, 56, 57, 58, 59, 62, 63, 64, 65, 66, 67, 68, 69, 72, 73, 74, 75, 76, 77, 78, 79, 82, 83, 84, 85, 86, 87, 88, 89, 93, 94, 95, 96, 97, 98, 104, 105, 106, 107 };

        int[] yilbasi_pixel_1=new int[]{73,69,52,49,33,34,46,56,64,78,79,62,59,42,39};
        int[] yilbasi_pixel_2 = new int[] { 93, 89, 72, 69, 53, 54, 66, 76, 84, 98, 99, 82, 79, 62, 59, 28, 27, 26, 25, 24, 23, 19, 2, 12, 9 };
        int[] yilbasi_pixel_3 = new int[] {113,109,92,89,73,74,86,96,104,118,119,102,99,82,79,48,47,46,45,44,43,39,22,19,3,4,5,6,7,8,12,29,32};
        int[] yilbasi_pixel_4 = new int[] {133,129,112,109,93,94,106,116,124,138,139,122,119,102,99,68,67,66,65,64,63,59,42,39,23,24,25,26,27,28,32,49,52};
        int[] yilbasi_pixel_5 = new int[] {132,129,113,114,126,136,139,122,119,88,87,86,85,84,83,79,62,59,43,44,45,46,47,48,52,69,72,13,9,4,18,19,2};
        int[] yilbasi_pixel_6 = new int[] {133,134,139,108,107,106,105,104,103,99,82,79,63,64,65,66,67,68,72,89,92,33,29,12,9,6,16,24,38,39,22,19,2};
        int[] yilbasi_pixel_7 = new int[] {134,135,136,137,138,139,123,120,103,98,97,96,95,94,93,110,113,130,68,52,49,32,28,27,35,45,57,63,62,59,42,39,22};
        int[] yilbasi_pixel_8 = new int[] {132,129,113,114,115,116,117,118,139,122,93,89,72,69,53,54,66,76,84,98,99,82,79,62,59,28,12,9,5,17,23,22,19,2};
        int[] yilbasi_pixel_9 = new int[] {124,125,126,127,128,129,109,93,92,89,73,74,86,96,98,104,103,100,99,82,79,48, 32, 29, 12, 8, 7, 15, 25, 37, 43, 42, 39, 22, 19, 2 };



        int[] rainbow_pixel_1 = new int[] {1,2,3,4,5,6,7,8,9,10,11,30,31,50,51,70,71,90,91,110,111,130,131,132,133,134,135,136,137,138,139,140,121,120,101,100,81,80,61,60,41,40,21,20};
        int[] rainbow_pixel_2 = new int[] {12,13,14,15,16,17,18,19,29,32,49,52,69,72,89,92,109,112,129,128,127,126,125,124,123,122,119,102,99,82,79,62,59,42,39,22};
        int[] rainbow_pixel_3 = new int[] {23,24,25,26,27,28,33,48,53,68,73,88,93,108,113,114,115,116,117,118,103,98,83,78,63,58,43,38};
        int[] rainbow_pixel_4= new int[] {34,35,36,37,44,57,64,77,84,97,104,105,106,107,94,87,74,67,54,47};
        int[] rainbow_pixel_5 = new int[] {45,46,55,56,65,66,75,76,85,86,95,96};

        byte[] blue_1 = new byte[] {0,0,255 };
        byte[] blue_2 = new byte[] { 65,105,225 };
        byte[] blue_3 = new byte[] { 135,206,250 };
        byte[] blue_4 = new byte[] { 30,144,255 };
        byte[] blue_5 = new byte[] { 173,216,230 };

        int[] bayrak_beyaz = new int[] { 63, 79, 82, 99, 103, 117, 116, 115, 114, 108, 92, 89, 72, 68, 34, 46, 26, 35, 36, 44, 24 };
        int [] bayrak_kirmizi=new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 25, 27, 28, 29, 30, 31, 32, 33, 37, 38, 39, 40, 41, 42, 43, 45, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 64, 65, 66, 67, 69, 70, 71, 73, 74, 75, 76, 77, 78, 80, 81, 83, 84, 85, 86, 87, 88, 90, 91, 93, 94, 95, 96, 97, 98, 100, 101, 102, 104, 105, 106, 107, 109, 110, 111, 112, 113, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130 };

        int[] kaleydoskop_1 = new int[] { 66 };
        int[] kaleydoskop_2 = new int[] { 54, 55, 56, 65, 76, 75, 74, 67 };
        int[] kaleydoskop_3 = new int[] { 44, 45, 46, 47, 48, 53, 68, 73, 88, 87, 86, 85, 84, 77, 64, 57 };
        int[] kaleydoskop_4 = new int[] { 32, 33, 34, 35, 36, 37, 38, 43, 58, 63, 78, 83, 98, 97, 96, 95, 94, 93, 92, 89, 72, 69, 52, 49 };
        int[] kaleydoskop_5 = new int[] { 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 50, 51, 70, 71, 90, 91, 110, 109, 108, 107, 106, 105, 104, 103, 102, 99, 82, 79, 62, 59, 42, 39 };
        int[] kaleydoskop_6 = new int[] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,40,41,60,61,80,81,100,101,111,112,113,114,115,116,117,118,119,120,130,129,128,127,126,125,124,123,122,121
};
        dmx_renk_kanallar f = new dmx_renk_kanallar();
        public  int istenen_kanal=0;


        void buffer_temizle()
        {
            for(int i = 0;i < f.dmx_512.Length-1;i++)
            {
                f.dmx_512[i] = 0;
            }
        }


        int saniye = 0;

        public int kanal;
        public int tek_kanal_flag;
        int z =70;
        public int a = 1;
        int x =71;
        public int m = 1;
        public int y = 0;

        int index1, index3;
        public int kayan_yazi_sayac = 0;
        public int rainbow_sayac = 0;
        public int rainbow_flag = 0;
        int[] gokkusagi_1 = new int[140];
        public int emoji_sayac = 0;
        public int turk_bayragi_sayac = 0;
        public int su_yolu_sayac = 0;
        public int kaleydoskop_sayac = 0;
        public int turk_bayragi_animasyon = 0;
         int[] animasyon_6 = new int[53];
        public int animasyon_6_sayac = 0;
        public int pixel_sayisi = 170;
    
            
        


        void parlaklik_degistirme(byte [] renk_array,byte azaltma)
        {
            for(int i=0;i<3;i++)
            {
                if(renk_array[i]<0)
                {
                    renk_array[i] = 0;
                }
            }
            renk_array[0] -= azaltma;
            renk_array[1] -= azaltma;
            renk_array[2] -= azaltma;
            f.dmx_512[index3 - 2] = renk_array[0];
            f.dmx_512[index3 - 1] = renk_array[1];
            f.dmx_512[index3] = renk_array[2];

        }
        public dmx_renk_kanallar getFixtureArray()
            {
           

            f.dmx_512 = new byte[512];
            f.fixtureColors = new MyColor[fixtures.Count];

            int c = -1; // total channel count
            int t = 0; // total light count


     

            foreach (Fixture fix in fixtures)
            {
                byte[] renkler = (byte[])fix.currentChannels.Clone();

                if (tek_kanal_flag == 0)
                {

                    for (int sayac = 0; sayac < pixel_sayisi*3; sayac = sayac + 3)
                    {


                        f.dmx_512[sayac] = renkler[0];
                        f.dmx_512[sayac + 1] = renkler[1];
                        f.dmx_512[sayac + 2] = renkler[2];



                    }
                    if (tek_kanal_flag == 1)
                    {
                        buffer_temizle();
                    }
                }
                else if (tek_kanal_flag == 1)
                {
                    
                     index = ((3 * istenen_kanal) - 1);

                     if (istenen_kanal == 0)
                     {
                         f.dmx_512[0] = renkler[0];
                         f.dmx_512[1] = renkler[1];
                         f.dmx_512[2] = renkler[2];
                     }
                     else
                     {
                         f.dmx_512[index - 2] = renkler[0];
                         f.dmx_512[index - 1] = renkler[1];
                         f.dmx_512[index] = renkler[2];
                     }


                }
                else if (tek_kanal_flag == 2)
                {
                    reset();
                    for (int i = 0; i <pixel_sayisi+1; i++)
                    {
                        su_yolu_fonksiyon(i, renkler);
 
                    }
                }

                else if (tek_kanal_flag == 3) //sırası ile yak
                {
                    reset();
                    for (int i = 0; i < pixel_sayisi + 1; i++)
                    {
                        su_yolu_fonksiyon(i, purple);
                        if (i%2==0)
                        {
                            su_yolu_fonksiyon(i, blue_5);
                        }

                        if (i % 3== 0)
                        {
                            su_yolu_fonksiyon(i, red);
                        }
                        if (i % 5 == 0)
                        {
                            su_yolu_fonksiyon(i, orange);
                        }
                        if (i % 11 == 0)
                        {
                            su_yolu_fonksiyon(i, yellow);
                        }
                    }


                }

                else if (tek_kanal_flag == 4)
                {
                    reset();
                    m = 0;
                    kaleydoskop_fonksiyon(kaleydoskop_6, dark_blue);
                    kaleydoskop_fonksiyon(kaleydoskop_5, white);
                    kaleydoskop_fonksiyon(kaleydoskop_4, dark_blue);
                    kaleydoskop_fonksiyon(kaleydoskop_3, white);
                    kaleydoskop_fonksiyon(kaleydoskop_2, dark_blue);
                    kaleydoskop_fonksiyon(kaleydoskop_1, white);
                    if(kaleydoskop_sayac==1)
                    {
                        kaleydoskop_fonksiyon(kaleydoskop_1,dark_blue);
                        kaleydoskop_fonksiyon(kaleydoskop_2,white);
                        kaleydoskop_fonksiyon(kaleydoskop_3, dark_blue);
                        kaleydoskop_fonksiyon(kaleydoskop_4, white);
                        kaleydoskop_fonksiyon(kaleydoskop_5,dark_blue);
                        kaleydoskop_fonksiyon(kaleydoskop_6, white);
                    }


                }
                else if (tek_kanal_flag == 5)
                {
                    reset();

                    if (kayan_yazi_sayac==0)
                    {
                        m = 0;
                        for (int j = 0; j < yazi_iki_pixel.Length ; j++)
                        {
                            m = yazi_iki_pixel[j];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = renkler[0];
                            f.dmx_512[index3 - 1] = renkler[1];
                            f.dmx_512[index3] = renkler[2];

                        }
                    }
                    else if(kayan_yazi_sayac == 1)
                    {
                        reset();
                        m = 0;
                        for (int k = 0; k <= yazi_sifir_pixel.Length - 1; k++)
                        {
                            m = yazi_sifir_pixel[k];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = renkler[0];
                            f.dmx_512[index3 - 1] = renkler[1];
                            f.dmx_512[index3] = renkler[2];

                        }
                    }

                    else if (kayan_yazi_sayac == 2)
                    {
                        reset();
                        m = 0;
                        for (int p = 0; p< yazi_iki_pixel.Length - 1; p++)
                        {
                            m = yazi_iki_pixel[p];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = renkler[0];
                            f.dmx_512[index3 - 1] = renkler[1];
                            f.dmx_512[index3] = renkler[2];

                        }
                    }
                    else if (kayan_yazi_sayac == 3)
                    {
                        reset();
                        m = 0;
                        for (int s = 0; s < yazi_iki_pixel.Length - 1; s++)
                        {
                            m = yazi_iki_pixel[s];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = renkler[0];
                            f.dmx_512[index3 - 1] = renkler[1];
                            f.dmx_512[index3] = renkler[2];
                        }
                    }
                }
               
                else if(tek_kanal_flag==6)
                {
                    byte[] tag_slider_r = new byte[24] { 255, 119, 111, 0, 119, 144, 99, 58, 0, 31, 31, 15, 174, 109, 67, 37, 254, 255, 255, 255, 255, 255, 255, 255 };
                    byte[] tag_slider_g = new byte[24] { 255, 113, 4, 0, 81, 65, 20, 12, 255, 103, 33, 16, 241, 218, 154, 82, 255, 181, 210, 152, 165, 138, 0, 39 };
                    byte[] tag_slider_b = new byte[24] { 255, 113, 4, 0, 158, 141, 71, 105, 191, 134, 134, 97, 61, 38, 10, 8, 110, 91, 0, 0, 252, 170, 246, 98 };
                    for(int i=1;i<170;i++)
                    {
                        m =i;
                        index3 = ((3 * m) - 1);
                        f.dmx_512[index3 - 2] = tag_slider_r[animasyon_6_sayac];
                        f.dmx_512[index3 - 1] = tag_slider_g[animasyon_6_sayac];
                        f.dmx_512[index3] = tag_slider_b[animasyon_6_sayac];
                    }
                }
                else if(tek_kanal_flag==7)//rainbow
                {
                    reset();
                    if (kayan_yazi_sayac == 0)
                    {
                        m = 0;
                        for (int w = 0; w <= rainbow_pixel_1.Length - 1; w++)
                        {
                            m = rainbow_pixel_1[w];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = blue_1[0];
                            f.dmx_512[index3 - 1] = blue_1[1];
                            f.dmx_512[index3] = blue_1[2];

                        }
                    }
                    else if (kayan_yazi_sayac == 1)
                    {
                        m = 0;
                        for (int w = 0; w <= rainbow_pixel_1.Length - 1; w++)
                        {
                            m = rainbow_pixel_1[w];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = blue_2[0];
                            f.dmx_512[index3 - 1] = blue_2[1];
                            f.dmx_512[index3] = blue_2[2];

                        }
                        for (int w = 0; w <= rainbow_pixel_2.Length - 1; w++)
                        {
                            m = rainbow_pixel_2[w];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = blue_1[0];
                            f.dmx_512[index3 - 1] = blue_1[1];
                            f.dmx_512[index3] = blue_1[2];

                        }
                    }
                   else if (kayan_yazi_sayac == 2)
                    {
                        reset();
                        m = 0;
                        for (int w = 0; w <= rainbow_pixel_1.Length - 1; w++)
                        {
                            m = rainbow_pixel_1[w];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = blue_3[0];
                            f.dmx_512[index3 - 1] = blue_3[1];
                            f.dmx_512[index3] = blue_3[2];

                        }
                        for (int w = 0; w <= rainbow_pixel_2.Length - 1; w++)
                        {
                            m = rainbow_pixel_2[w];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = blue_2[0];
                            f.dmx_512[index3 - 1] = blue_2[1];
                            f.dmx_512[index3] = blue_2[2];

                        }
                        for (int w = 0; w <= rainbow_pixel_3.Length - 1; w++)
                        {
                            m = rainbow_pixel_3[w];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = blue_1[0];
                            f.dmx_512[index3 - 1] = blue_1[1];
                            f.dmx_512[index3] = blue_1[2];

                        }
                    }
                   else if (kayan_yazi_sayac == 3)
                    {
                        reset();
                        m = 0;
                        for (int w = 0; w <= rainbow_pixel_1.Length - 1; w++)
                        {
                            m = rainbow_pixel_1[w];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = blue_4[0];
                            f.dmx_512[index3 - 1] = blue_4[1];
                            f.dmx_512[index3] = blue_4[2];

                        }
                        for (int w = 0; w <= rainbow_pixel_2.Length - 1; w++)
                        {
                            m = rainbow_pixel_2[w];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = blue_3[0];
                            f.dmx_512[index3 - 1] = blue_3[1];
                            f.dmx_512[index3] = blue_3[2];

                        }
                        for (int w = 0; w <= rainbow_pixel_3.Length - 1; w++)
                        {
                            m = rainbow_pixel_3[w];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = blue_2[0];
                            f.dmx_512[index3 - 1] = blue_2[1];
                            f.dmx_512[index3] = blue_2[2];

                        }
                        for (int w = 0; w <= rainbow_pixel_4.Length - 1; w++)
                        {
                            m = rainbow_pixel_4[w];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = blue_1[0];
                            f.dmx_512[index3 - 1] = blue_1[1];
                            f.dmx_512[index3] = blue_1[2];

                        }
                    }
                    else if (kayan_yazi_sayac == 4)
                    {
                        reset();
                        m = 0;
                        for (int w = 0; w <= rainbow_pixel_1.Length - 1; w++)
                        {
                            m = rainbow_pixel_1[w];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = blue_5[0];
                            f.dmx_512[index3 - 1] = blue_5[1];
                            f.dmx_512[index3] = blue_5[2];

                        }
                        for (int w = 0; w <= rainbow_pixel_2.Length - 1; w++)
                        {
                            m = rainbow_pixel_2[w];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = blue_4[0];
                            f.dmx_512[index3 - 1] = blue_4[1];
                            f.dmx_512[index3] = blue_4[2];

                        }
                        for (int w = 0; w <= rainbow_pixel_3.Length - 1; w++)
                        {
                            m = rainbow_pixel_3[w];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = blue_3[0];
                            f.dmx_512[index3 - 1] = blue_3[1];
                            f.dmx_512[index3] = blue_3[2];

                        }
                        for (int w = 0; w <= rainbow_pixel_4.Length - 1; w++)
                        {
                            m = rainbow_pixel_4[w];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = blue_2[0];
                            f.dmx_512[index3 - 1] = blue_2[1];
                            f.dmx_512[index3] = blue_2[2];

                        }
                        for (int w = 0; w <= rainbow_pixel_5.Length - 1; w++)
                        {
                            m = rainbow_pixel_5[w];
                            index3 = ((3 * m) - 1);
                            f.dmx_512[index3 - 2] = blue_1[0];
                            f.dmx_512[index3 - 1] = blue_1[1];
                            f.dmx_512[index3] = blue_1[2];

                        }
                    }


                }
               
                
                if (tek_kanal_flag==8)//sütun rainbow
                {
                    reset();

                    index = 0;
               
                        for (int i = 1; i < 140; i++)
                        {
                      
                            gokkusagi_1[i-1] = i;
                        }
                
                        m = 0;


                        rainbow_fonksiyon(0, 10, purple);
                        rainbow_fonksiyon(120,130, purple);
                   

                    if(rainbow_sayac==1)
                    {
                        m = 0;
                        rainbow_fonksiyon(0, 10, purple);
                        rainbow_fonksiyon(10, 20, dark_blue);
                        rainbow_fonksiyon(110, 120, dark_blue);
                        rainbow_fonksiyon(120, 130, purple);
                    }
                    else if (rainbow_sayac == 2)
                    {
                        m = 0;


                        rainbow_fonksiyon(0, 10, purple);
                        rainbow_fonksiyon(10, 20, dark_blue);
                        rainbow_fonksiyon(20, 30, blue);
                        rainbow_fonksiyon(100, 110, blue);
                        rainbow_fonksiyon(110, 120, dark_blue);
                        rainbow_fonksiyon(120, 130, purple);
                    }
                    else if (rainbow_sayac == 3)
                    {
                        m = 0;
                        rainbow_fonksiyon(0, 10, purple);
                        rainbow_fonksiyon(10, 20, dark_blue);
                        rainbow_fonksiyon(20, 30, blue);
                        rainbow_fonksiyon(30, 40, green);
                        rainbow_fonksiyon(100,110, green);
                        rainbow_fonksiyon(110, 120, blue);
                        rainbow_fonksiyon(120, 130, dark_blue);
                        rainbow_fonksiyon(130, 139, purple);

                    }
                    else if (rainbow_sayac == 4)
                    {
                        m = 0;
                        rainbow_fonksiyon(0, 10, purple);
                        rainbow_fonksiyon(10, 20, dark_blue);
                        rainbow_fonksiyon(20, 30, blue);
                        rainbow_fonksiyon(30, 40, green);
                        rainbow_fonksiyon(40, 50, yellow);
                        rainbow_fonksiyon(90, 100, yellow);
                        rainbow_fonksiyon(100, 110, green);
                        rainbow_fonksiyon(110, 120, blue);
                        rainbow_fonksiyon(120, 130, dark_blue);
                        rainbow_fonksiyon(130, 139, purple);

                    }

                    else if (rainbow_sayac == 5)
                    {
                        m = 0;
                        
                        rainbow_fonksiyon(0, 10, purple);
                        rainbow_fonksiyon(10, 20, dark_blue);
                        rainbow_fonksiyon(20, 30, blue);
                        rainbow_fonksiyon(30, 40, green);
                        rainbow_fonksiyon(40, 50, yellow);
                        rainbow_fonksiyon(50, 60, orange);
                        rainbow_fonksiyon(60, 70, red);
                        rainbow_fonksiyon(70, 80, red);
                        rainbow_fonksiyon(80, 90, orange);
                        rainbow_fonksiyon(90, 100, yellow);
                        rainbow_fonksiyon(100, 110, green);
                        rainbow_fonksiyon(110, 120, blue);
                        rainbow_fonksiyon(120, 130, dark_blue);
                        rainbow_fonksiyon(130, 139, purple);
                    }

                    else if (rainbow_sayac == 6)
                    {
                        m = 0;

                        rainbow_fonksiyon(0, 10, red);
                        rainbow_fonksiyon(10, 20, purple);
                        rainbow_fonksiyon(20, 30, dark_blue);
                        rainbow_fonksiyon(30, 40, blue);
                        rainbow_fonksiyon(40, 50, green);
                        rainbow_fonksiyon(50, 60, yellow);
                        rainbow_fonksiyon(60, 70, orange);
                        rainbow_fonksiyon(70, 80, red);
                        rainbow_fonksiyon(80, 90, purple);
                        rainbow_fonksiyon(90, 100, dark_blue);
                        rainbow_fonksiyon(100, 110, blue);
                        rainbow_fonksiyon(110, 120, green);
                        rainbow_fonksiyon(120, 130, yellow);
                        rainbow_fonksiyon(130, 139, orange);
                    }

                    else if (rainbow_sayac == 7)
                    {
                        m = 0;

                        rainbow_fonksiyon(0, 10, orange);
                        rainbow_fonksiyon(10, 20, red);
                        rainbow_fonksiyon(20, 30, purple);
                        rainbow_fonksiyon(30, 40, dark_blue);
                        rainbow_fonksiyon(40, 50, blue);
                        rainbow_fonksiyon(50, 60, green);
                        rainbow_fonksiyon(60, 70, yellow);
                        rainbow_fonksiyon(70, 80, orange);
                        rainbow_fonksiyon(80, 90, dark_blue);
                        rainbow_fonksiyon(90, 100, blue);
                        rainbow_fonksiyon(100, 110, green);
                        rainbow_fonksiyon(110, 120, yellow);
                        rainbow_fonksiyon(120, 130, orange);
                        rainbow_fonksiyon(130, 139, red);
                    }


                }
              
                if(tek_kanal_flag==9)
                {
                    reset();
                    int[] goz= new int[] { 87, 47 };
                    int[] agiz_gulen = new int[] { 84, 78, 63, 58, 44 };
                    int[] gozyası_1 = new int[] { 95, 35 };
                    int[] gozyası_2 = new int[] { 25, 105 };
                    int[] agiz_uzgun = new int[] { 84, 76, 65, 56, 44 };
                    int[] kalp = new int[] { 45, 46, 47, 48, 52, 53, 54, 55, 57, 63, 64, 65, 66, 67, 68, 74, 75, 76, 77, 78, 79, 83, 84, 85, 86, 87, 88, 92, 93, 94, 95, 96, 97, 105, 106, 107, 108 };
                    emoji_fonksiyon();

                    emoji_fonksiyon_1(goz, dark_blue);
                    emoji_fonksiyon_1(agiz_gulen,dark_blue);


                    if (emoji_sayac==1)
                    {
                        reset();
                        emoji_fonksiyon();

                        emoji_fonksiyon_1(goz, dark_blue);
                        emoji_fonksiyon_1(agiz_uzgun, dark_blue);

                    }
                    if(emoji_sayac==2)
                    {
                        reset();
                        emoji_fonksiyon();

                        emoji_fonksiyon_1(goz, red);
                        emoji_fonksiyon_1(agiz_gulen, dark_blue);
                    }

                    if (emoji_sayac == 3)
                    {
                        reset();
                        emoji_fonksiyon();

                        emoji_fonksiyon_1(goz, black);
                        emoji_fonksiyon_1(agiz_uzgun, dark_blue);
                        emoji_fonksiyon_1(gozyası_1, blue);

                    }


                    if (emoji_sayac == 4)
                    {
                        reset();

                        emoji_fonksiyon();

                        emoji_fonksiyon_1(goz, black);
                        emoji_fonksiyon_1(agiz_uzgun, dark_blue);
                        emoji_fonksiyon_1(gozyası_1, blue);
                        emoji_fonksiyon_1(gozyası_2, blue);
                    }

                    if (emoji_sayac == 5)
                    {
                        reset();
                        for(int i = 0; i < 512;i++)
                        {
                            f.dmx_512[i] = 255;
                        }

                        emoji_fonksiyon_1(kalp, red);

                    }


                }

                if(tek_kanal_flag==10)
                {
                    reset();
                    index = 0;

                    for (int w = 0; w <=bayrak_beyaz.Length - 1; w++)
                    {
                        m = bayrak_beyaz[w];
                        index3 = ((3 * m) - 1);
                        f.dmx_512[index3 - 2] = white[0];
                        f.dmx_512[index3 - 1] = white[1];
                        f.dmx_512[index3] = white[2];

                    }

                    for (int w = 0; w <= bayrak_kirmizi.Length - 1; w++)
                    {
                        m = bayrak_kirmizi[w];
                        index3 = ((3 * m) - 1);
                        f.dmx_512[index3 - 2] = red[0];
                        f.dmx_512[index3 - 1] = red[1];
                        f.dmx_512[index3] = red[2];

                    }
                    for (int i = 1; i < 140; i++)
                    {

                        gokkusagi_1[i - 1] = i;
                    }

               
                        if (turk_bayragi_animasyon == 0)
                        {
                          
                            if(turk_bayragi_sayac==1)
                            {
                                m = 0;

                                rainbow_fonksiyon(0, 130, black);
                            }




                        }
                        if(turk_bayragi_animasyon==1)
                        {
                          

                           if (turk_bayragi_sayac==1)
                            {

                                m = 0;

                                rainbow_fonksiyon(10, 20, black);
                            }
                             if (turk_bayragi_sayac == 2)
                             {
                                  m = 0;

                                  rainbow_fonksiyon(20, 30, black);

                             }
                            if (turk_bayragi_sayac == 3)
                            {
                                m = 0;

                                rainbow_fonksiyon(30, 40, black);

                            }
                            if (turk_bayragi_sayac == 4)
                            {
                                m = 0;

                                rainbow_fonksiyon(40, 50, black);

                            }
                            if (turk_bayragi_sayac == 5)
                            {
                                m = 0;

                                rainbow_fonksiyon(50, 60, black);

                            }
                            if (turk_bayragi_sayac == 6)
                            {
                                m = 0;

                                rainbow_fonksiyon(60, 70, black);

                            }
                            if (turk_bayragi_sayac == 7)
                            {
                                m = 0;

                                rainbow_fonksiyon(70, 80, black);

                            }
                            if (turk_bayragi_sayac == 8)
                            {
                                m = 0;

                                rainbow_fonksiyon(80, 90, black);
                            }

                            if (turk_bayragi_sayac == 9)
                            {
                                    m = 0;

                                    rainbow_fonksiyon(90, 100, black);

                             }
                            if (turk_bayragi_sayac == 10)
                            {
                                    m = 0;

                                    rainbow_fonksiyon(100, 110, black);

                             }
                            if (turk_bayragi_sayac == 11)
                            {
                                    m = 0;

                                    rainbow_fonksiyon(110, 120, black);

                             }
                            if (turk_bayragi_sayac == 12)
                            {
                                m = 0;

                                rainbow_fonksiyon(120, 130, black);

                            }




                        }


                

                 
            

  

                   /* if (turk_bayragi_sayac == 2)
                    {
                        m = 0;

                        rainbow_fonksiyon(10, 20, black);

                    }
                    if (turk_bayragi_sayac == 3)
                    {
                        m = 0;

                        rainbow_fonksiyon(20, 30, black);

                    }
                    if (turk_bayragi_sayac == 4)
                    {
                        m = 0;

                        rainbow_fonksiyon(30, 40, black);

                    }
                    if (turk_bayragi_sayac == 5)
                    {
                        m = 0;

                        rainbow_fonksiyon(40, 50, black);

                    }
                    if (turk_bayragi_sayac == 6)
                    {
                        m = 0;

                        rainbow_fonksiyon(50, 60, black);

                    }
                    if (turk_bayragi_sayac == 7)
                    {
                        m = 0;

                        rainbow_fonksiyon(60, 70, black);

                    }
                    if (turk_bayragi_sayac == 8)
                    {
                        m = 0;

                        rainbow_fonksiyon(70, 80, black);

                    }
                    if (turk_bayragi_sayac == 9)
                    {
                        m = 0;

                        rainbow_fonksiyon(80, 90, black);

                    }
                    if (turk_bayragi_sayac == 10)
                    {
                        m = 0;

                        rainbow_fonksiyon(90, 100, black);

                    }
                    if (turk_bayragi_sayac == 11)
                    {
                        m = 0;

                        rainbow_fonksiyon(100, 110, black);

                    }
                    if (turk_bayragi_sayac == 12)
                    {
                        m = 0;

                        rainbow_fonksiyon(110, 120, black);

                    }
                    if (turk_bayragi_sayac == 13)
                    {
                        m = 0;

                        rainbow_fonksiyon(120, 130, black);

                    }*/

                }
                if(tek_kanal_flag==11)
                {
                    reset();
                }
                // istenen_kanal_fonksiyonu();


                f.fixtureColors[t] = new MyColor(renkler);
                t++;
            }

            return f;
        }

        void rainbow_fonksiyon(int alt_limit,int ust_limit,byte[]array)
        {
            for (int w = alt_limit; w <= ust_limit-1; w++)
            {
                m = gokkusagi_1[w];
                index3 = ((3 * m) - 1);
                if(index3<0)
                {
                    index3 = 2;

                }
                f.dmx_512[index3 - 2] = array[0];
                f.dmx_512[index3 - 1] = array[1];
                f.dmx_512[index3] = array[2];

            }
        }
      
        void su_yolu_fonksiyon(int sayac_degeri, byte[]array)
        {
            int[] dmx_512 = new int[512];
            for (int k=0;k<=511;k++)
            {
                dmx_512[k] =k + 1;
            }
            if (su_yolu_sayac == sayac_degeri)
            {
                for (int i = 0; i <= sayac_degeri; i++)
                {
                    
                    index3 = ((3 * dmx_512[i]) - 1);
                    if(index3<=0)
                    {
                        index3 = 2;
                    }
                    f.dmx_512[index3 - 2] = array[0];
                    f.dmx_512[index3 - 1] = array[1];
                    f.dmx_512[index3] = array[2];
                }
            }
        }

        void kaleydoskop_fonksiyon(int []array,byte[]renk_array)
        {
            for (int j = 0; j < array.Length; j++)
            {
                m = array[j];
                index3 = ((3 * m) - 1);
                f.dmx_512[index3 - 2] = renk_array[0];
                f.dmx_512[index3 - 1] = renk_array[1];
                f.dmx_512[index3] = renk_array[2];

            }
        }

        void emoji_fonksiyon()
        {
            m = 0;
            for (int w = 0; w <= emoji_yuz_beyaz.Length - 1; w++)
            {
                m = emoji_yuz_beyaz[w];
                index3 = ((3 * m) - 1);
                f.dmx_512[index3 - 2] = white[0];
                f.dmx_512[index3 - 1] = white[1];
                f.dmx_512[index3] = white[2];

            }
            for (int j = 91; j < 140; j++)
            {
                index3 = ((3 * j) - 1);
                f.dmx_512[index3 - 2] = white[0];
                f.dmx_512[index3 - 1] = white[1];
                f.dmx_512[index3] = white[2];

            }

            for (int w = 0; w <= emoji_yuz_sari.Length - 1; w++)
            {
                m = emoji_yuz_sari[w];
                index3 = ((3 * m) - 1);
                f.dmx_512[index3 - 2] = yellow[0];
                f.dmx_512[index3 - 1] = yellow[1];
                f.dmx_512[index3] = yellow[2];

            }
        }

        void reset()
        {
            for (int i = 0; i < 512; i++)
            {
                f.dmx_512[i] = 0;
            }
        }
        void emoji_fonksiyon_1(int []array,byte [] renk_array)
        {
           

            for (int w = 0; w <= array.Length - 1; w++)
            {
                m = array[w];
                index3 = ((3 * m) - 1);
                f.dmx_512[index3 - 2] = renk_array[0];
                f.dmx_512[index3 - 1] = renk_array[1];
                f.dmx_512[index3] = renk_array[2];

            }
        }
     

    }
}
