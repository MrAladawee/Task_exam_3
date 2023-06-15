<?php

namespace common;

use Exception;
use mysqli;

class DbHelper
{
    private const dbName = "mm_site2023";
    private static ?DbHelper $instance = null;
    private $conn;

    public static function getInstance($host = null, $port = null, $user = null, $pass = null): DbHelper {
        if (self::$instance === null) self::$instance = new DbHelper($host, $port, $user, $pass);
        return self::$instance;
    }

    private function __construct(
        $host, $port, $user, $pass
    ){
        $this->conn = new mysqli();
        $this->conn->connect(
            $host,
            $user,
            $pass,
            self::dbName,
            $port
        );
    }

    public function getTitle($url): string{
        $sql = "SELECT title FROM pages WHERE url=? or alias=?";
        $this->conn->begin_transaction();
        $stmt = $this->conn->prepare($sql);
        $stmt->bind_param("ss", $url, $url);
        $stmt->execute();
        $result = $stmt->get_result();
        $row = $result->fetch_row();
        $stmt->close();
        $this->conn->commit();
        return ($row !== null && $row !== false) ? $row[0] : "";
    }

    public function getPagesInfo(): array{
        $sql = "SELECT * FROM pages";
        $this->conn->begin_transaction();
        $result = $this->conn->query($sql);
        $res_arr = $result->fetch_all(MYSQLI_ASSOC);
        $result->free_result();
        $this->conn->commit();
        return $res_arr;
    }

    public function getUserPassword(string $user): ?string{
        $sql = "SELECT password FROM users WHERE login = ?";
        $this->conn->begin_transaction();
        $stmt = $this->conn->prepare($sql);
        $stmt->bind_param("s", $user);
        $stmt->execute();
        $result = $stmt->get_result();
        $row = $result->fetch_assoc();
        $stmt->close();
        $this->conn->commit();
        return ($row === null) ? $row : $row['password'];
    }

    public function isSecure(string $page){
        $sql = "SELECT secure FROM pages WHERE url=? or alias=?";
        $this->conn->begin_transaction();
        $stmt = $this->conn->prepare($sql);
        $stmt->bind_param("ss", $page, $page);
        $stmt->execute();
        $result = $stmt->get_result();
        $row = $result->fetch_assoc();
        $stmt->close();
        $this->conn->commit();
        return $row !== null && $row['secure'] == 1;
    }

    public function getUserName(string $user){
        $sql = "SELECT `name` FROM users WHERE login = ?";
        $this->conn->begin_transaction();
        $stmt = $this->conn->prepare($sql);
        $stmt->bind_param("s", $user);
        $stmt->execute();
        $result = $stmt->get_result();
        $row = $result->fetch_assoc();
        $stmt->close();
        $this->conn->commit();
        return ($row === null) ? $row : $row['name'];
    }

    public function saveUser(string $login, string $password, string $name): bool
    {
        $sql = "INSERT INTO `users` (login, password, name) VALUES(?, ?, ?)";
        try {
            $this->conn->begin_transaction();
            $stmt = $this->conn->prepare($sql);
            $stmt->bind_param("sss", $login, $password, $name);
            if (!$stmt->execute()) throw new Exception("Ошибка добавления пользователя");
            $this->conn->commit();
            return true;
        } catch (\Throwable $ex){
            $this->conn->rollback();
            return false;
        }
    }
	
	public function showImage() {
	$imageId = 2;
    $query = "SELECT image FROM `Images` WHERE id = ?";
    $stmt = $this->conn->prepare($query);
    $stmt->bind_param('i', $imageId);
    $stmt->execute();
    $stmt->bind_result($imageData);
    $stmt->fetch();
    $stmt->close();

    // Отображение изображения на странице
    echo '<img src="data:image/png;base64,' . base64_encode($imageData) . '">';
	}
	
	public function showResult(string $login) {
		
	$sql = "SELECT id FROM `users` WHERE login = ?";
    $stmt_id = $this->conn->prepare($sql);
    $stmt_id->bind_param("s", $login);
    $stmt_id->execute();
	$result = $stmt_id->get_result();
	
	
	// отладочный вывод
	//printf("Number of rows returned: %d<br>", $result->num_rows);
	
	$result_row = $result->fetch_assoc();
    $user_id = $result_row["id"];

	//echo "User ID: $user_id<br>";

    // подготовка и выполнение запроса к таблице values с параметрами
    $sql = "SELECT * FROM `values` WHERE user_id = ?";
    $stmt_val = $this->conn->prepare($sql);
    $stmt_val->bind_param("i", $user_id);
	$stmt_val->execute();
	
	// отладочный вывод
	//printf("Number of rows returned: %d<br>", $stmt_val->num_rows);

    // обработка результата запроса
    $result = $stmt_val->get_result();
	
    $values_array = array();
    while ($row = $result->fetch_assoc()) {
        $values_array[] = $row;
    }

    // закрытие соединения с базой данных
    $stmt_id->close();
    $stmt_val->close();
    $this->conn->commit();
	
	//echo "Values array: ";
    //print_r($values_array);
	
    // возвращение результата
    return $values_array;
	}


	
	
}

