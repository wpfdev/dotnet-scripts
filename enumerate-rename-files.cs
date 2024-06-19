//1. Мы используем метод Directory.GetFiles, чтобы получить список всех файлов в указанной директории.
//2. Создаем экземпляр класса Random для генерации случайных чисел.
//3. Для каждого файла в директории генерируем случайное число.
//3.1 Если в директории уже есть файл с определённым префиксом, то следующий файл не должен получать такой же префикс
//4. Формируем новое имя файла, добавляя префикс (случайное число) перед текущим именем файла.
//5. Используем метод File.Move для переименования файла. Если файл не может быть перемещен (например, если он используется другим процессом), возникает исключение, и мы выводим сообщение об ошибке.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
      
        string directoryPath = "r:\\music"; // Укажите путь к директории, где находятся файлы
        Random random = new Random(987654);
        Dictionary<string, bool> usedPrefixes = new Dictionary<string, bool>();
        
        var length = Directory.GetFiles(directoryPath).Length + 1000; //Максимально число для префикса
        foreach (var file in Directory.GetFiles(directoryPath))
        {
            string prefix = null;
            do
            {
                int randomNumber = random.Next(1, length); // Генерация случайного числа от 1 до количества файлов в директории
                prefix = $"{randomNumber}_";
            } while (usedPrefixes.ContainsKey(prefix)); // Проверяем, что префикс ещё не использован
        
            usedPrefixes[prefix] = true; // Отмечаем префикс как использованный
        
            string newFileName = $"{prefix}{Path.GetFileName(file)}"; // Формирование нового имени файла с префиксом и случайным номером
        
            try
            {
                File.Move(file, Path.Combine(Path.GetDirectoryName(file), newFileName)); // Переименование файла
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при переименовании файла: {ex.Message}");
            }
        }
    }
}
