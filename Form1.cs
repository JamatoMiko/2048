using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

/*
2
4
8
16
32
64
128
256
512
1024
2048
4096
8192
16384
32768
65536
131072(理論上の最大値)
*/
namespace _2048;

public partial class Form1 : Form
{
    Random random = new Random();
    int[,] cell = new int[4, 4];//セルの配列
    int n_cell;//セルの個数
    bool[,] baby = new bool[4,4];//このターンにできたか
    public int cellsize = 108;//セルの大きさ
    int fontsize;//フォントサイズ
    //数字の色
    int n_r;
    int n_g;
    int n_b;
    //セルの色
    int c_r;
    int c_g;
    int c_b;
    int ali_x;//数字のx座標をいくつずらすか
    int ali_y;//y座標をいくつずらすか
    int a_move;//移動量
    bool moved;//移動したか
    //コンストラクタ
    public Form1()
    {
        InitializeComponent();
        this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        //最初の2つを生成
        Generate();
        Generate();
    }
    //セルを生成
    public void Generate()
    {
        if (n_cell < 16)
        {
            while (true)
            {
                int x = random.Next(0, 4);
                int y = random.Next(0, 4);
                if (cell[x, y] == 0)
                {
                    //90%の確率で2、10%の確率で4
                    int n = random.Next(0,10);
                    if (n == 9)
                    {
                        n = 4;
                    }
                    else
                    {
                        n = 2;
                    }
                    cell[x, y] = n;
                    n_cell += 1;
                    break;
                }
            }
        }
    }
    //キー入力
    void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        //←
        if (e.KeyCode == Keys.Left)
        {
            //this.Text = "←";
            for (int y = 0; y < 4; y++)
            {
                for (int x = 1; x < 4; x++)
                {
                    if (cell[x, y] > 0)
                    {
                        a_move = 0;
                        //x座標分繰り返す
                        for (int i = 0; i < x; i++)
                        {
                            //違う数字
                            if (cell[x-(i+1), y] > 0 && cell[x-(i+1), y] != cell[x, y])
                            {
                                break;
                            }
                            //同じ数字
                            if (cell[x-(i+1), y] == cell[x, y])
                            {
                                //このターンにできた数字か
                                if (baby[x-(i+1), y] == true)
                                {
                                    break;
                                }
                                else
                                {
                                    //合成
                                    cell[x-(i+1), y] = cell[x, y]*2;
                                    baby[x-(i+1), y] = true;
                                    cell[x, y] = 0;
                                    moved = true;
                                    n_cell -= 1;
                                    //得点
                                    break;
                                }
                            }
                            a_move++;
                        }
                        if (a_move > 0)
                        {
                            cell[x-a_move, y] = cell[x, y];
                            cell[x, y] = 0;
                            moved = true;
                        }
                    }
                }
            }
        }
        //↑
        if (e.KeyCode == Keys.Up)
        {
            //s.Text = "↑";
            for (int x = 0; x < 4; x++)
            {
                for (int y = 1; y < 4; y++)
                {
                    if (cell[x, y] > 0)
                    {
                        a_move = 0;
                        //y座標分繰り返す
                        for (int i = 0; i < y; i++)
                        {
                            //違う数字
                            if (cell[x, y-(i+1)] > 0 && cell[x, y-(i+1)] != cell[x, y])
                            {
                                break;
                            }
                            //同じ数字
                            if (cell[x, y-(i+1)] == cell[x, y])
                            {
                                //このターンにできた数字か
                                if (baby[x, y-(i+1)] == true)
                                {
                                    break;
                                }
                                else
                                {
                                    //合成
                                    cell[x, y-(i+1)] = cell[x, y]*2;
                                    baby[x, y-(i+1)] = true;
                                    cell[x, y] = 0;
                                    moved = true;
                                    n_cell -= 1;
                                    //得点
                                    break;
                                }
                            }
                            a_move++;
                        }
                        if (a_move > 0)
                        {
                            cell[x, y-a_move] = cell[x, y];
                            cell[x, y] = 0;
                            moved = true;
                        }
                    }
                }
            }
        }
        //→
        if (e.KeyCode == Keys.Right)
        {
            //this.Text = "→";
            for (int y = 0; y < 4; y++)
            {
                for (int x = 2; x >= 0; x--)
                {
                    if (cell[x, y] > 0)
                    {
                        a_move = 0;
                        //3-x座標分繰り返す
                        for (int i = 0; i < (3-x); i++)
                        {
                            //違う数字
                            if (cell[x+(i+1), y] > 0 && cell[x+(i+1), y] != cell[x, y])
                            {
                                break;
                            }
                            //同じ数字
                            if (cell[x+(i+1), y] == cell[x, y])
                            {
                                //このターンにできた数字か
                                if (baby[x+(i+1), y] == true)
                                {
                                    break;
                                }
                                else
                                {
                                    //合成
                                    cell[x+(i+1), y] = cell[x, y]*2;
                                    baby[x+(i+1), y] = true;
                                    cell[x, y] = 0;
                                    moved = true;
                                    n_cell -= 1;
                                    //得点
                                    break;
                                }
                            }
                            a_move++;
                        }
                        if (a_move > 0)
                        {
                            cell[x+a_move, y] = cell[x, y];
                            cell[x, y] = 0;
                            moved = true;
                        }
                    }
                }
            }
        }
        //↓
        if (e.KeyCode == Keys.Down)
        {
            //this.Text = "↓";
            for (int x = 0; x < 4; x++)
            {
                for (int y = 2; y >= 0; y--)
                {
                    if (cell[x, y] > 0)
                    {
                        a_move = 0;
                        //3-y座標分繰り返す
                        for (int i = 0; i < (3-y); i++)
                        {
                            //違う数字
                            if (cell[x, y+(i+1)] > 0 && cell[x, y+(i+1)] != cell[x, y])
                            {
                                break;
                            }
                            //同じ数字
                            if (cell[x, y+(i+1)] == cell[x, y])
                            {
                                //このターンにできた数字か
                                if (baby[x, y+(i+1)] == true)
                                {
                                    break;
                                }
                                else
                                {
                                    //合成
                                    cell[x, y+(i+1)] = cell[x, y]*2;
                                    baby[x, y+(i+1)] = true;
                                    cell[x, y] = 0;
                                    moved = true;
                                    n_cell -= 1;
                                    //得点
                                    break;
                                }
                            }
                            a_move++;
                        }
                        if (a_move > 0)
                        {
                            cell[x, y+a_move] = cell[x, y];
                            cell[x, y] = 0;
                            moved = true;
                        }
                    }
                }
            }
        }
        if (moved == true)
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    baby[x, y] = false;
                }
            }
            moved = false;
            Generate();
            Invalidate();
        }
    }
    //描画処理
    protected override void OnPaint(PaintEventArgs e)
    {
        //this.Text = ""+n_cell;
        base.OnPaint(e);
        //外枠描画
        SolidBrush brush1 = new SolidBrush(Color.FromArgb(0x9C, 0x8B, 0x7C));
        e.Graphics.FillRectangle(brush1, 0, 0, cellsize*4+12*4+12, cellsize*4+12*4+12);
        brush1.Dispose();
        //下地描画
        SolidBrush brush2 = new SolidBrush(Color.FromArgb(0xBD, 0xAC, 0x97));
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                e.Graphics.FillRectangle(brush2, cellsize*x+12*x+12, cellsize*y+12*y+12, cellsize, cellsize);
            }
        }
        brush2.Dispose();
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (cell[x, y] > 0)
                {
                    switch (cell[x, y])
                    {
                        case 2:
                            fontsize = 36;
                            n_r = 0x75;
                            n_g = 0x64;
                            n_b = 0x52;
                            c_r = 0xEE;
                            c_g = 0xE4;
                            c_b = 0xDA;
                            ali_x = 30;
                            ali_y = 20;
                            break;
                        case 4:
                            fontsize = 36;
                            n_r = 0x75;
                            n_g = 0x64;
                            n_b = 0x52;
                            c_r = 0xEB;
                            c_g = 0xD8;
                            c_b = 0xB6;
                            ali_x = 30;
                            ali_y =20;
                            break;
                        case 8:
                            fontsize = 36;
                            n_r = 0xFF;
                            n_g = 0xFF;
                            n_b = 0xFF;
                            c_r = 0xF2;
                            c_g = 0xAF;
                            c_b = 0x74;
                            ali_x = 30;
                            ali_y =20;
                            break;
                        case 16:
                            fontsize = 36;
                            n_r = 0xFF;
                            n_g = 0xFF;
                            n_b = 0xFF;
                            c_r = 0xF5;
                            c_g = 0x8F;
                            c_b = 0x5A;
                            ali_x = 12;
                            ali_y = 20;
                            break;
                        case 32:
                            fontsize = 36;
                            n_r = 0xFF;
                            n_g = 0xFF;
                            n_b = 0xFF;
                            c_r = 0xF5;
                            c_g = 0x76;
                            c_b = 0x58;
                            ali_x = 12;
                            ali_y = 20;
                            break;
                        case 64:
                            fontsize = 36;
                            n_r = 0xFF;
                            n_g = 0xFF;
                            n_b = 0xFF;
                            c_r = 0xF5;
                            c_g = 0x5A;
                            c_b = 0x36;
                            ali_x = 12;
                            ali_y = 20;
                            break;
                        case 128:
                            fontsize = 28;
                            n_r = 0xFF;
                            n_g = 0xFF;
                            n_b = 0xFF;
                            c_r = 0xF2;
                            c_g = 0xCF;
                            c_b = 0x56;
                            ali_x = 10;
                            ali_y = 28;
                            break;
                        case 256:
                            fontsize = 28;
                            n_r = 0xFF;
                            n_g = 0xFF;
                            n_b = 0xFF;
                            c_r = 0xF3;
                            c_g = 0xCB;
                            c_b = 0x49;
                            ali_x = 10;
                            ali_y = 28;
                            break;
                        case 512:
                            fontsize = 28;
                            n_r = 0xFF;
                            n_g = 0xFF;
                            n_b = 0xFF;
                            c_r = 0xF0;
                            c_g = 0xDC;
                            c_b = 0x50;
                            ali_x = 10;
                            ali_y = 28;
                            break;
                        case 1024:
                            fontsize = 16;
                            n_r = 0xFF;
                            n_g = 0xFF;
                            n_b = 0xFF;
                            c_r = 0xFF;
                            c_g = 0xDC;
                            c_b = 0x6E;
                            ali_x = 22;
                            ali_y = 40;
                            break;
                        case 2048:
                            fontsize = 16;
                            n_r = 0xFF;
                            n_g = 0xFF;
                            n_b = 0xFF;
                            c_r = 0xF0;
                            c_g = 0xF0;
                            c_b = 0x32;
                            ali_x = 22;
                            ali_y = 40;
                            break;
                        case 4096:
                            fontsize = 16;
                            n_r = 0xFF;
                            n_g = 0xFF;
                            n_b = 0xFF;
                            c_r = 0x00;
                            c_g = 0x00;
                            c_b = 0x00;
                            ali_x = 22;
                            ali_y = 40;
                            break;
                        case 8192:
                            fontsize = 16;
                            n_r = 0xFF;
                            n_g = 0xFF;
                            n_b = 0xFF;
                            c_r = 0x00;
                            c_g = 0x00;
                            c_b = 0x00;
                            ali_x = 22;
                            ali_y = 40;
                            break;
                        case 16384:
                            fontsize = 16;
                            n_r = 0xFF;
                            n_g = 0xFF;
                            n_b = 0xFF;
                            c_r = 0x00;
                            c_g = 0x00;
                            c_b = 0x00;
                            ali_x = 14;
                            ali_y = 40;
                            break;
                        case 32768:
                            fontsize = 16;
                            n_r = 0xFF;
                            n_g = 0xFF;
                            n_b = 0xFF;
                            c_r = 0x00;
                            c_g = 0x00;
                            c_b = 0x00;
                            ali_x = 14;
                            ali_y = 40;
                            break;
                        case 65536:
                            fontsize = 16;
                            n_r = 0xFF;
                            n_g = 0xFF;
                            n_b = 0xFF;
                            c_r = 0x00;
                            c_g = 0x00;
                            c_b = 0x00;
                            ali_x = 14;
                            ali_y = 40;
                            break;
                        case 131072://(理論上の最大値)
                            fontsize = 16;
                            n_r = 0xFF;
                            n_g = 0xFF;
                            n_b = 0xFF;
                            c_r = 0x00;
                            c_g = 0x00;
                            c_b = 0x00;
                            ali_x = 8;
                            ali_y = 40;
                            break;
                    }
                    //セル描画
                    SolidBrush brush3 = new SolidBrush(Color.FromArgb(c_r, c_g, c_b));
                    e.Graphics.FillRectangle(brush3, cellsize*x+12*x+12, cellsize*y+12*y+12, cellsize, cellsize);
                    brush3.Dispose();
                    //文字描画
                    SolidBrush brush4 = new SolidBrush(Color.FromArgb(n_r, n_g, n_b));
                    Font font = new Font("メイリオ", fontsize, FontStyle.Bold);
                    e.Graphics.DrawString(""+cell[x, y], font, brush4, cellsize*x+12*x+12+ali_x, cellsize*y+12*y+12+ali_y);
                    brush4.Dispose();
                    font.Dispose();
                }
            }
        }
    }
}
