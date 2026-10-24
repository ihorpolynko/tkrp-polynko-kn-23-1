surname = input("Введіть прізвище студента: ").strip()

grade1 = float(input("Введіть оцінку з першої дисципліни: "))
grade2 = float(input("Введіть оцінку з другої дисципліни: "))
grade3 = float(input("Введіть оцінку з третьої дисципліни: "))

average = (grade1 + grade2 + grade3) / 3

if average >= 90:
    level = "високий"
elif average >= 75:
    level = "достатній"
elif average >= 60:
    level = "середній"
else:
    level = "низький"

result = (
    f"Студент: {surname}\n"
    f"Оцінки: {grade1}, {grade2}, {grade3}\n"
    f"Середній бал: {average:.2f}\n"
    f"Рівень успішності: {level}\n"
)

print("\nРезультати:")
print(result)

with open("result.txt", "w", encoding="utf-8") as file:
    file.write(result)

print("Результат записано у файл result.txt")