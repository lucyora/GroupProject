
<?php
//The File containing all queries to the database. The connection is done in a seperate file for the sake of reusability
	include 'db_conn.php';
	class db
	{
		public function __construct(){
			$this->db_cont = new db_conn();
		}


		public function __destruct(){
			php_unset($this->db_cont); 
		}

		public function newInsertLH($playerid,$playerdisplayname,$hitvalue,$platform)
		{
			$db_con = new db_conn();
			$query = $db_con->_conn->query("INSERT INTO `Largesthit`(`id`, `playerid`, `playerdisplayname`, `value`, `platform`) VALUES (DEFAULT, '".$playerid."','".$playerdisplayname."',".$hitvalue.",".$platform.")");
			$query->fetch();

		}
		public function newInsertOTTO($playerid,$playerdisplayname,$time,$platform)
		{
			$db_con = new db_conn();
			$query = $db_con->_conn->query("INSERT INTO `Ottoman`(`id`, `playerid`, `playerdisplayname`, `value`, `platform`)  VALUES (DEFAULT, '".$playerid."','".$playerdisplayname."',".$hitvalue.",".$platform.")");
			$query->fetch();
			
		}

		public function getTop10LH()
		{
			$db_con = new db_conn();
			$query = $db_con->_conn->query("SELECT  `playerdisplayname` ,  `value` ,  `platform` FROM  `Largesthit` ORDER BY  `value` DESC LIMIT 10");
			$results = $query->fetchAll();
			return $results;
		}

		public function getTop10OTTO()
		{
			$db_con = new db_conn();
			$query = $db_con->_conn->query("SELECT  `playerdisplayname`, `value`, `platform` FROM `Ottoman` ORDER BY `value` DESC LIMIT 10");
			$results = $query->fetchAll();
			return $results;
		}


		public function InsertLH($playerid,$hitvalue)
		{
			$db_con = new db_conn();
			$query = $db_con->_conn->query("UPDATE `Largesthit` SET `value`=".$hitvalue." WHERE `playerid`=".$playerid);
			$query->fetch();

		}

		public function InsertOTTO($playerid,$time)
		{
			$db_con = new db_conn();			
			$query = $db_con->_conn->query("UPDATE `Ottoman` SET `value`=".$time." WHERE `playerid`=".$playerid);
			$query->fetch();
			
		}
		public function getPlayerLH($playerid)
		{
			$db_con = new db_conn();
			$query = $db_con->_conn->query("SELECT * FROM `Largesthit` WHERE `playerid`=".$playerid);
			$results = $query->fetch();
			return $results;

		}
		public function getPlayerOTTO($playerid)
		{
			$db_con = new db_conn();
			$query = $db_con->_conn->query("SELECT * FROM `Ottoman` WHERE `playerid`=".$playerid);
			$results = $query->fetch();
			return $results;

		}		


	}


?>