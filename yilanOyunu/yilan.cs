using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace yilanOyunu
{
    class yilan
    {
        yilanParca[] yilan_parca;
        int yilanBuyuklugu = 3;
        yon yonumuz;
        public yilan()
        {
            yilan_parca = new yilanParca[3];
            yilan_parca[0] = new yilanParca(150,150);
            yilan_parca[1] = new yilanParca(160,150);
            yilan_parca[2] = new yilanParca(170, 150);


        }

        public int Yilan_Uzunluk
        {
            get
            {
                return yilanBuyuklugu;
            }
        }

        public void ilerle(yon yone)
        {
            yonumuz = yone;
            if (yone._x==0&& yone._y == 0)
            {

            }
            else
            {
                for(int i=yilan_parca.Length-1; i>0; i--)
                {
                    yilan_parca[i] = new yilanParca(yilan_parca[i - 1].x_, yilan_parca[i - 1].y_);

                }
                yilan_parca[0] = new yilanParca(yilan_parca[0].x_ + yone._x, yilan_parca[0].y_ + yone._y);
            }
        }
        public void uza()
        {
            Array.Resize(ref yilan_parca, yilan_parca.Length + 1);
            yilan_parca[yilan_parca.Length - 1] = new yilanParca(yilan_parca[yilan_parca.Length - 2].x_-yonumuz._x, yilan_parca[yilan_parca.Length - 2].y_-yonumuz._y);
            yilanBuyuklugu++;
        }
        public Point PozGetir(int number)
        {
            return new Point(yilan_parca[number].x_, yilan_parca[number].y_);
        }
    }

    class yilanParca
    {
        public  int x_;
        public  int y_;
        public readonly int buyukluk_x;
        public readonly int buyukluk_y;

        public yilanParca(int x, int y)
        {
            x_ = x;
            y_ = y;
            buyukluk_x = 10;
            buyukluk_y = 10;
        }
    }
   

    class yon
    {
        public readonly int _x;
        public readonly int _y;
        public yon(int x, int y) 
        {
            _x = x;
            _y = y;

        }
    }
}
