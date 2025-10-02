using System;
using System.Text;
abstract class Phong
{
    protected int songay;
    public Phong(int songay)
    {
        this.songay = songay;
    }
    public abstract double TinhTien();
    public abstract void Hien();
}
class PhongA : Phong
{
    protected int tiendv;
    public PhongA(int songay) : base(songay)
    {
        Console.Write("Nhập tiền dịch vụ:");
        tiendv=int.Parse(Console.ReadLine());
    }
    public override double TinhTien()
    {
        if (songay < 5) return songay * 80 + tiendv;
        else return 5*80+(songay -5) *80*0.9 + tiendv;
    }
    public override void Hien()
    {
        Console.WriteLine("Dịch vụ phòng A");
        Console.WriteLine("Tiền dịch vụ:" + tiendv);
        Console.WriteLine("Tiền phòng" + TinhTien());
    }
}
class PhongB : Phong
{
    protected int tiendv;
    public PhongB(int songay) : base(songay)
    { }
    public override double TinhTien()
    {
        if (songay < 5) return songay * 60;
        else return 5 * 60 + (songay - 5) * 60 * 0.9;
    }
    public override void Hien()
    {
        Console.WriteLine("Dịch vụ phòng B");
        Console.WriteLine("Tiền phòng:" + TinhTien());
    }
}
class PhongC : Phong
{
    protected int tiendv;
    public PhongC(int songay) : base(songay)
    { }
    public override double TinhTien()
    {
        return songay * 40;
    }
    public override void Hien()
    {
        Console.WriteLine("Dịch vụ phòng C");
        Console.WriteLine("Tiền phòng:" + TinhTien());
    }
}
class HoaDonKhach
{
    private string tenkhach;
    private int songay;
    private Phong loaiphong;
    public void Nhap()
    {
        Console.WriteLine("Nhập thông tin hóa đơn khách hàng:");
        Console.Write("Họ tên:");
        tenkhach = Console.ReadLine();
        Console.Write("Số ngày ở:");
        songay=int.Parse(Console.ReadLine());
        Console.WriteLine("Cho biết loại phòng đỉnh ở A,B,C?");
        char ch = char.Parse(Console.ReadLine());
        switch (char.ToUpper(ch))
        {
            case 'A':loaiphong = new PhongA(songay); break;
            case 'B': loaiphong = new PhongB(songay); break;
            case 'C': loaiphong = new PhongC(songay); break;
        }
    }
    public void Hien()
    {
        Console.WriteLine("Thông tin hóa đơn khách hàng:");
        Console.WriteLine("Họ tên khách:" + tenkhach);
        Console.WriteLine("Số ngày ở:"+songay);
        Console.WriteLine("Khách hàng đã sử dụng");
        loaiphong.Hien();
    }
}
class App
{
    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;
        HoaDonKhach t = new HoaDonKhach();
        t.Nhap();
        Console.Clear();
        t.Hien();
        Console.ReadLine();
    }
}
