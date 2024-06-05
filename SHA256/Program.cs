// Метод для генерации случайной соли
using System.Security.Cryptography;
using System.Text;


// Метод для создания соли
 static string GenerateSalt(int length)
{
    // Создаем случайный объект для генерации соли
    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

    byte[] saltBytes = new byte[length];

    rng.GetBytes(saltBytes);

    // Преобразуем байты соли в строку шестнадцатеричного представления
    return BitConverter.ToString(saltBytes).Replace("-", "");
}

// Метод для хеширования пароля с использованием соли
 static string HashPassword(string password)
{
    // Генерация случайной соли
    string salt = GenerateSalt(16); // Например, 16 байт для соли
    using (SHA256 sha256Hash = SHA256.Create())
    {
        // Объединяем пароль и соль
        string saltedPassword = password + salt;

        // Преобразуем строку в массив байтов
        byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

        // Преобразуем массив байтов в строку шестнадцатеричного представления
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            stringBuilder.Append(data[i].ToString("x2")); // Преобразование каждого байта в двузначное шестнадцатеричное число
        }

        return stringBuilder.ToString();
    }
}

// Ввод пароля от пользователя
Console.WriteLine("Введите пароль:");
string password = Console.ReadLine();




// Хеширование пароля с использованием соли
string hashedPassword = HashPassword(password);

// Вывод информации о хешированном пароле 
Console.WriteLine("Хешированный пароль: " + hashedPassword);
