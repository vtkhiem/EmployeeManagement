using System;

// 1. Phải import thư viện bạn vừa cài đặt
using BCrypt.Net;

namespace PasswordHasherUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("--- CÔNG CỤ HASH MẬT KHẨU BCRYPT ---");
            Console.WriteLine("Công cụ này sẽ tạo ra một chuỗi hash an toàn cho mật khẩu của bạn.");
            Console.WriteLine("Hãy sao chép kết quả và dán vào cột 'PasswordHash' trong DB.");
            Console.WriteLine("------------------------------------------");

            while (true)
            {
                Console.WriteLine();
                Console.Write("Nhập mật khẩu cần hash (hoặc gõ 'exit' để thoát): ");
                string? password = Console.ReadLine();

                // Thoát nếu người dùng gõ 'exit'
                if (string.Equals(password, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                // Kiểm tra nếu mật khẩu rỗng
                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Mật khẩu không được để trống. Vui lòng thử lại.");
                    Console.ResetColor();
                    continue;
                }

                try
                {
                    // 2. Đây là dòng quan trọng: Hash mật khẩu
                    // BCrypt tự động tạo "salt" (muối) và nhúng nó vào chuỗi hash
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("THÀNH CÔNG! Hash của bạn là:");
                    Console.ResetColor();

                    // In ra chuỗi hash
                    Console.WriteLine(hashedPassword);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("Đã thoát công cụ hash.");
        }
    }
}
