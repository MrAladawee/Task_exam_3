# Task_exam_3

Задание, которое решает СЛАУ методом Гаусса.
C# приложение имеет регистрацию и авторизацию (без проверки на длину и хэш)
Отображение на сайте происходит благодаря измененному DBHelper.php и secret.php из репозитория MySite

Изменена база в MySQL.
Добавлена база values:
CREATE TABLE 'mm_site2023'.'values' ('id' SERIAL PRIMARY KEY, 'user_id' INT, 'coef' VARCHAR(255), 'coef_res' VARCHAR(255), 'result' VARCHAR(255));

Изменена база users:
Добавлено поле id, не являющееся PRIMARY KEY, но являющееся UNIQUE, NOT NULL, AUTO_INCREMENT (иными словами SERIAL)
