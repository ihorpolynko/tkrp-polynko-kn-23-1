file_name = input("Введіть назву файла: ").strip()
text = input("Введіть текст: ")

characters_count = len(text)
words_count = len(text.split())

result = (
f"Текст: {text}\n"
f"Кількість символів: {characters_count}\n"
f"Кількість слів: {words_count}\n"
)

with open(file_name, "w", encoding="utf-8") as file: file.write(result)

print("\nРезультати аналізу:")
print(result)
print(f"Інформацію записано у файл {file_name}")