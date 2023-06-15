<?php

require_once "common/Page.php";
use common\Page;
use common\DbHelper;
class secret extends Page
{
	

    protected function showContent()
    {
        /*$name = DbHelper::getInstance()->getUserName($_SESSION['login']);
        print "<div>Приветствуем, ".$name."</div>";
        print "<div>Личный кабинет...</div>";*/
		
		$values = DbHelper::getInstance()->showResult($_SESSION['login']);
		
		// вывод заголовка таблицы
		echo "<table border='1'>";
		echo "<tr><th>User ID</th><th>Coef</th><th>Coef Res</th><th>Result</th></tr>";

		// вывод данных в ячейки таблицы
		foreach ($values as $row) {
			echo "<tr>";
			echo "<td>" . $row["user_id"] . "</td>";
			echo "<td>" . $row["coef"] . "</td>";
			echo "<td>" . $row["coef_res"] . "</td>";
			echo "<td>" . $row["result"] . "</td>";
			echo "</tr>";
		}

		// закрытие таблицы
		echo "</table>";
	
    }
}

(new secret())->show();