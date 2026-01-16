using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for makeRandom
/// </summary>
/// 
namespace Dal
{
    public class makeRandom
    {
        public makeRandom()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string RandomString(int size, bool lowerCase)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        /// <summary>
        /// trả về mảng chuỗi số bất kỳ {viết bằng chữ (String), số tương ứng} 
        /// </summary>
        public String MakeRanDom()
        {

            Random rd = new Random();            
            String random =RandomString(15,false)+ Guid.NewGuid();
            String randomE = Ultil.Encyption.Encrypt(random);
            return Ultil.StringHelper.ToURLgach(randomE);
        }
        /// <summary>
        /// trả về một mảng ký tự
        /// </summary
        public string[] RanDomText()
        {
            Random rd = new Random();
            double r1 = rd.Next(1, 9);
            double r2 = rd.Next(1, 9);
            double r3 = rd.Next(1, 9);
            double r4 = rd.Next(1, 9);

            int r11 = (int)(r1 * 1000) % 9;
            int r22 = (int)(r2 * 1000) % 9;
            int r33 = (int)(r3 * 1000) % 9;
            int r44 = (int)(r4 * 1000) % 9;


            //make a String
            String s1 = null;
            String s2 = null;
            String s3 = null;
            String s4 = null;
            switch (r11)
            {
                case 0:
                    s1 = "zezo";
                    break;
                case 1:
                    s1 = "one";
                    break;
                case 2:
                    s1 = "two";
                    break;
                case 3:
                    s1 = "three";
                    break;

                case 4:
                    s1 = "for";
                    break;

                case 5:
                    s1 = "five";
                    break;
                case 6:
                    s1 = "six";
                    break;
                case 7:
                    s1 = "seven";
                    break;
                case 8:
                    s1 = "eigth";
                    break;
                case 9:
                    s1 = "nice";
                    break;
            }

            switch (r22)
            {
                case 0:
                    s2 = "zezo";
                    break;
                case 1:
                    s2 = "one";
                    break;
                case 2:
                    s2 = "two";
                    break;
                case 3:
                    s2 = "three";
                    break;
                case 4:
                    s2 = "for";
                    break;
                case 5:
                    s2 = "five";
                    break;
                case 6:
                    s2 = "six";
                    break;
                case 7:
                    s2 = "seven";
                    break;
                case 8:
                    s2 = "eigth";
                    break;
                case 9:
                    s2 = "nice";
                    break;
            }
            switch (r33)
            {
                case 0:
                    s3 = "zezo";
                    break;

                case 1:
                    s3 = "one";
                    break;
                case 2:
                    s3 = "two";
                    break;
                case 3:
                    s3 = "three";
                    break;
                case 4:
                    s3 = "for";
                    break;
                case 5:
                    s3 = "five";
                    break;
                case 6:
                    s3 = "six";
                    break;
                case 7:
                    s3 = "seven";
                    break;
                case 8:
                    s3 = "eigth";
                    break;
                case 9:
                    s3 = "nice";
                    break;
            }

            switch (r44)
            {
                case 0:
                    s4 = "zezo";
                    break;

                case 1:
                    s4 = "one";
                    break;
                case 2:
                    s4 = "two";
                    break;
                case 3:
                    s4 = "three";
                    break;
                case 4:
                    s4 = "for";
                    break;
                case 5:
                    s4 = "five";
                    break;
                case 6:
                    s4 = "six";
                    break;
                case 7:
                    s4 = "seven";
                    break;
                case 8:
                    s4 = "eigth";
                    break;
                case 9:
                    s4 = "nice";
                    break;
            }

            String ChuoiKyTu = s1 + "-" + s2 + "-" + s3 + "-" + s4;
            String r111 = r11.ToString();
            String r222 = r22.ToString();
            String r333 = r33.ToString();
            String r444 = r44.ToString();
            String chuoiSo = r111 + r222 + r333 + r444;
            //System.out.println(rd2);
            String[] s = new String[] { ChuoiKyTu, chuoiSo };
            return s;
        }
        /// <summary>
        /// trả về mảng chuỗi số bất kỳ {viết bằng chữ (String), số tương ứng} 
        /// </summary>
        public String[] RanDomTextVi()
        {
            Random rd = new Random();
            double r1 = rd.Next(1, 9);
            double r2 = rd.Next(1, 9);
            double r3 = rd.Next(1, 9);
            double r4 = rd.Next(1, 9);

            int r11 = (int)(r1 * 1000) % 9;
            int r22 = (int)(r2 * 1000) % 9;
            int r33 = (int)(r3 * 1000) % 9;
            int r44 = (int)(r4 * 1000) % 9;


            //make a String
            String s1 = null;
            String s2 = null;
            String s3 = null;
            String s4 = null;
            switch (r11)
            {
                case 0:
                    s1 = "Không";
                    break;
                case 1:
                    s1 = "Một";
                    break;
                case 2:
                    s1 = "Hai";
                    break;
                case 3:
                    s1 = "Ba";
                    break;

                case 4:
                    s1 = "Bốn";
                    break;

                case 5:
                    s1 = "Năm";
                    break;
                case 6:
                    s1 = "sáu";
                    break;
                case 7:
                    s1 = "Bảy";
                    break;
                case 8:
                    s1 = "Tám";
                    break;
                case 9:
                    s1 = "Chín";
                    break;
            }

            switch (r22)
            {
                case 0:
                    s2 = "Không";
                    break;
                case 1:
                    s2 = "Một";
                    break;
                case 2:
                    s2 = "Hai";
                    break;
                case 3:
                    s2 = "Ba";
                    break;
                case 4:
                    s2 = "Bốn";
                    break;
                case 5:
                    s2 = "Năm";
                    break;
                case 6:
                    s2 = "Sáu";
                    break;
                case 7:
                    s2 = "Bảy";
                    break;
                case 8:
                    s2 = "Tám";
                    break;
                case 9:
                    s2 = "Chín";
                    break;
            }
            switch (r33)
            {
                case 0:
                    s3 = "KhÔng ";
                    break;

                case 1:
                    s3 = "mỘt";
                    break;
                case 2:
                    s3 = "HaI";
                    break;
                case 3:
                    s3 = "bA";
                    break;
                case 4:
                    s3 = "BốN";
                    break;
                case 5:
                    s3 = "NĂM";
                    break;
                case 6:
                    s3 = "SÁU";
                    break;
                case 7:
                    s3 = "BẢY";
                    break;
                case 8:
                    s3 = "TÁM";
                    break;
                case 9:
                    s3 = "CHÍN";
                    break;
            }

            switch (r44)
            {
                case 0:
                    s4 = "KHÔNG";
                    break;

                case 1:
                    s4 = "MỘT";
                    break;
                case 2:
                    s4 = "H-AI";
                    break;
                case 3:
                    s4 = "B:A";
                    break;
                case 4:
                    s4 = "BỐN";
                    break;
                case 5:
                    s4 = "NĂM";
                    break;
                case 6:
                    s4 = "SA'U";
                    break;
                case 7:
                    s4 = "BẢY";
                    break;
                case 8:
                    s4 = "TÁM";
                    break;
                case 9:
                    s4 = "chín";
                    break;
            }

            String ChuoiKyTu = s1 + "-" + s2 + "-" + s3 + "-" + s4;
            String r111 = r11.ToString();
            String r222 = r22.ToString();
            String r333 = r33.ToString();
            String r444 = r44.ToString();
            String chuoiSo = r111 + r222 + r333 + r444;
            //System.out.println(rd2);
            String[] s = new String[] { ChuoiKyTu, chuoiSo };
            return s;
        }
    }
}