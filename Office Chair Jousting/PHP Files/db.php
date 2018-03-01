
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
			$query = $db_con->_conn->query("INSERT INTO `Largesthit`(`hit_id`, `hit_playerid`, `hit_playerdisplayname`, `hit_value`, `hit_platform`) VALUES (DEFAULT, '".$playerid."','".$playerdisplayname."',".$hitvalue.",".$platform.")");
			$query->fetch();

		}
		public function newInsertOTTO($playerid,$playerdisplayname,$time,$platform)
		{
			$db_con = new db_conn();
			$query = $db_con->_conn->query("INSERT INTO `Ottoman`(`otto_id`, `otto_playerid`, `otto_playerdisplayname`, `otto_value`, `otto_platform`)  VALUES (DEFAULT, '".$playerid."','".$playerdisplayname."',".$hitvalue.",".$platform.")");
			$query->fetch();
			
		}

		public function getTop10LH()
		{
			$db_con = new db_conn();
			$query = $db_con->_conn->query("SELECT  `hit_playerdisplayname` ,  `hit_value` ,  `hit_platform` FROM  `Largesthit` ORDER BY  `hit_value` ASC LIMIT 10");
			$results = $query->fetchAll();
			return $results;
		}

		public function getTop10OTTO()
		{
			$db_con = new db_conn();
			$query = $db_con->_conn->query("SELECT  `otto_playerdisplayname`, `otto_value`, `otto_platform` FROM `Ottoman` ORDER BY `otto_value` LIMIT 10");
			$results = $query->fetchAll();
			return $results;
		}


		public function InsertLH($playerid,$hitvalue)
		{
			$db_con = new db_conn();
			$query = $db_con->_conn->query("UPDATE `Largesthit` SET `hit_value`=".$hitvalue." WHERE hit_playerid`=".$playerid);
			$query->fetch();

		}

		public function InsertOTTO($playerid,$time)
		{
			$db_con = new db_conn();			
			$query = $db_con->_conn->query("UPDATE `Ottoman` SET `otto_value`=".$time." WHERE otto_playerid`=".$playerid);
			$query->fetch();
			
		}
		public function getPlayerLH($playerid)
		{
			$db_con = new db_conn();
			$query = $db_con->_conn->query("SELECT * FROM `Largesthit` WHERE hit_playerid`=".$playerid);
			$results = $query->fetch();
			return $results;

		}
		public function getPlayerOTTO($playerid)
		{
			$db_con = new db_conn();
			$query = $db_con->_conn->query("SELECT * FROM `Ottoman` WHERE otto_playerid`=".$playerid);
			$results = $query->fetch();
			return $results;

		}		


	}


?>