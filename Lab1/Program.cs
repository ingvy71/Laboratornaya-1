using System;

namespace Lab1OOP
{
    public class ComplexNumber
    {
        
        public double Real { get; }
        public double Imagine { get; }

        
        public ComplexNumber(double real, double imagine)
        {
            Real = real;
            Imagine = imagine;
        }

        
        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(
                a.Real + b.Real,
                a.Imagine + b.Imagine
            );
        }

        
        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(
                a.Real - b.Real,
                a.Imagine - b.Imagine
            );
        }

        
        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(
                a.Real * b.Real - a.Imagine * b.Imagine,
                a.Real * b.Imagine + a.Imagine * b.Real
            );
        }

        
        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
        {
            double denominator = b.Real * b.Real + b.Imagine * b.Imagine;

            if (denominator == 0)
            {
                throw new DivideByZeroException(
                    "Деление на нулевое комплексное число."
                );
            }

            return new ComplexNumber(
                (a.Real * b.Real + a.Imagine * b.Imagine) / denominator,
                (a.Imagine * b.Real - a.Real * b.Imagine) / denominator
            );
        }

        
        public static bool operator ==(ComplexNumber a, ComplexNumber b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null || b is null)
                return false;

            return a.Real == b.Real && a.Imagine == b.Imagine;
        }

        
        public static bool operator !=(ComplexNumber a, ComplexNumber b)
        {
            return !(a == b);
        }

        
        
        public static double operator +(ComplexNumber a)
        {
            return Math.Sqrt(a.Real * a.Real + a.Imagine * a.Imagine);
        }

        
        
        public static ComplexNumber operator -(ComplexNumber a)
        {
            return new ComplexNumber(a.Real, -a.Imagine);
        }

        
        public override bool Equals(object? obj)
        {
            if (obj is ComplexNumber other)
            {
                return this == other;
            }

            return false;
        }

        
        public override int GetHashCode()
        {
            return HashCode.Combine(Real, Imagine);
        }

        
        public override string ToString()
        {
            
            if (Imagine == 0)
                return $"{Real}";

            
            if (Real == 0)
                return $"{Imagine}i";

            
            string sign = Imagine > 0 ? "+" : "-";

            return $"{Real} {sign} {Math.Abs(Imagine)}i";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ComplexNumber a = new ComplexNumber(1, 2);
            ComplexNumber b = new ComplexNumber(3, -4);

            Console.WriteLine("=== КОМПЛЕКСНЫЕ ЧИСЛА ===");
            Console.WriteLine($"a = {a}");
            Console.WriteLine($"b = {b}");

            Console.WriteLine();

            
            Console.WriteLine("=== ОПЕРАЦИЯ СЛОЖЕНИЯ ===");
            Console.WriteLine($"({a}) + ({b})");
            Console.WriteLine($"Результат: {a + b}");

            Console.WriteLine();

            
            Console.WriteLine("=== ОПЕРАЦИЯ ВЫЧИТАНИЯ ===");
            Console.WriteLine($"({a}) - ({b})");
            Console.WriteLine($"Результат: {a - b}");

            Console.WriteLine();

            
            Console.WriteLine("=== ОПЕРАЦИЯ УМНОЖЕНИЯ ===");
            Console.WriteLine($"({a}) * ({b})");
            Console.WriteLine($"Результат: {a * b}");

            Console.WriteLine();

            
            Console.WriteLine("=== ОПЕРАЦИЯ ДЕЛЕНИЯ ===");
            Console.WriteLine($"({a}) / ({b})");
            Console.WriteLine($"Результат: {a / b}");

            Console.WriteLine();

            
            Console.WriteLine("=== ВЫЧИСЛЕНИЕ МОДУЛЯ ===");
            Console.WriteLine($"|{a}|");
            Console.WriteLine($"Результат: {+a}");

            Console.WriteLine();

            
            Console.WriteLine("=== СОПРЯЖЁННОЕ ЧИСЛО ===");
            Console.WriteLine($"Сопряжённое для числа {a}");
            Console.WriteLine($"Результат: {-a}");

            Console.WriteLine();

            
            Console.WriteLine("=== СРАВНЕНИЕ КОМПЛЕКСНЫХ ЧИСЕЛ ===");

            ComplexNumber c = new ComplexNumber(1, 2);

            Console.WriteLine($"c = {c}");

            Console.WriteLine();

            Console.WriteLine($"Проверка: a == c");
            Console.WriteLine($"Результат: {a == c}");

            Console.WriteLine();

            Console.WriteLine($"Проверка: a != b");
            Console.WriteLine($"Результат: {a != b}");

            Console.WriteLine();

            
            Console.WriteLine("=== ПРОВЕРКА ToString() ===");

            ComplexNumber d = new ComplexNumber(5, 0);
            ComplexNumber e = new ComplexNumber(0, 4);

            Console.WriteLine($"d = {d}");
            Console.WriteLine($"e = {e}");

            Console.WriteLine();

            
            Console.WriteLine("=== ПРОВЕРКА ДЕЛЕНИЯ НА НОЛЬ ===");

            try
            {
                ComplexNumber zero = new ComplexNumber(0, 0);

                Console.WriteLine($"Попытка выполнить:");
                Console.WriteLine($"{a} / {zero}");

                Console.WriteLine(a / zero);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Ошибка:");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("=== ВСЕ ПРОВЕРКИ ЗАВЕРШЕНЫ ===");
        }
    }
}