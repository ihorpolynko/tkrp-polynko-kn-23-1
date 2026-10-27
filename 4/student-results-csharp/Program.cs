Console.Write("Введіть прізвище студента: ");
string surname = Console.ReadLine() ?? "";

Console.Write("Введіть оцінку з першої дисципліни: ");
double grade1 = double.Parse(Console.ReadLine() ?? "0");

Console.Write("Введіть оцінку з другої дисципліни: ");
double grade2 = double.Parse(Console.ReadLine() ?? "0");

Console.Write("Введіть оцінку з третьої дисципліни: ");
double grade3 = double.Parse(Console.ReadLine() ?? "0");

double average = (grade1 + grade2 + grade3) / 3;

string level;

if (average >= 90)
{
    level = "високий";
}
else if (average >= 75)
{
    level = "достатній";
}
else if (average >= 60)
{
    level = "середній";
}
else
{
    level = "низький";
}

string result =
    $"Студент: {surname}\n" +
    $"Оцінки: {grade1}, {grade2}, {grade3}\n" +
    $"Середній бал: {average:F2}\n" +
    $"Рівень успішності: {level}\n";

Console.WriteLine("\nРезультати:");
Console.WriteLine(result);

File.WriteAllText("result.txt", result);

Console.WriteLine("Результат записано у файл result.txt");