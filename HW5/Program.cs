using System;

namespace motor
{
    // ================== Interface ==================
    public interface IMotor
    {
        void InputInfo();     // Nhập thông tin motor
        void DisplayInfo();   // Hiển thị thông tin motor
        void ChangeInfo();    // Thay đổi thông tin motor
    }

    // ================== Lớp Motor ==================
    public class Motor : IMotor
    {
        // Thuộc tính
        private string code;        // Mã xe
        private string type;        // Tên loại xe
        private double capacity;    // Dung tích xi-lanh
        private int num;            // Kiểu truyền lực (1: số tay, 2: tự động)

        // Constructor không tham số
        public Motor() { }

        // Constructor có tham số
        public Motor(string code, string type, double capacity, int num)
        {
            this.code = code;
            this.type = type;
            this.capacity = capacity;
            this.num = num;
        }

        // Getter - Setter
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public double Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

        public int Num
        {
            get { return num; }
            set { num = value; }
        }

        // Triển khai các phương thức trong interface
        public void InputInfo()
        {
            Console.Write("Nhập mã xe: ");
            code = Console.ReadLine();

            Console.Write("Nhập tên loại xe: ");
            type = Console.ReadLine();

            Console.Write("Nhập dung tích xi-lanh (cc): ");
            capacity = Convert.ToDouble(Console.ReadLine());

            Console.Write("Kiểu truyền lực (1 - số tay, 2 - tự động): ");
            num = Convert.ToInt32(Console.ReadLine());
        }

        public void DisplayInfo()
        {
            Console.WriteLine("\n===== THÔNG TIN MOTOR =====");
            Console.WriteLine($"Mã xe: {code}");
            Console.WriteLine($"Loại xe: {type}");
            Console.WriteLine($"Dung tích xi-lanh: {capacity} cc");
            Console.WriteLine($"Kiểu truyền lực: {(num == 1 ? "Số tay" : "Tự động")}");
        }

        public void ChangeInfo()
        {
            Console.WriteLine("\n--- Cập nhật thông tin xe ---");
            Console.Write("Nhập lại tên loại xe: ");
            type = Console.ReadLine();

            Console.Write("Nhập lại dung tích xi-lanh (cc): ");
            capacity = Convert.ToDouble(Console.ReadLine());

            Console.Write("Nhập lại kiểu truyền lực (1 - số tay, 2 - tự động): ");
            num = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(">> Thông tin xe đã được cập nhật!\n");
        }
    }

    // ================== Chương trình kiểm tra ==================
    class Program
    {
        static void Main(string[] args)
        {
            Motor m = new Motor();
            m.InputInfo();
            m.DisplayInfo();

            Console.Write("\nBạn có muốn thay đổi thông tin xe? (y/n): ");
            string ans = Console.ReadLine();
            if (ans.ToLower() == "y")
            {
                m.ChangeInfo();
                m.DisplayInfo();
            }

            Console.WriteLine("\nHoàn thành chương trình!");
        }
    }
}
