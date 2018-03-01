<?php 
include 'db.php';
if ($_GET["key"] == "12345678910") {
	$database = new db();
	switch ($_GET["action"]) {
		case 'T10LH':
			$results = $database->getTop10LH();
			$results = json_encode($results);
			echo $results;
			break;
		case 'PLH':
			$results = $db->getPlayerLH($_GET["playerid"]);
			$results = json_encode($results);			
			break;
		case 'newLH':
			$database->newInsertLH($_GET["playerid"],$_GET["displayname"],$_GET["value"],$_GET["platform"]);
			echo "okay";
			break;	
		case 'insLH':
			$database->InsertLH($_GET["playerid"],$_GET["value"]);
			break;			
		case 'T10OTTO':	
			$results = $database->getTop10OTTO();
			$results = json_encode($results);
			echo $results;
			break;
		case 'PLOTTO':
			$results = $database->getPlayerOTTO($_GET["playerid"]);
			$results = json_encode($results);
			echo $results;			
			break;			
		case 'newOTTO':
			$database->newInsetOTTO($_GET["playerid"],$_GET["displayname"],$_GET["value"],$_GET["platform"]);
			break;
		case 'insOTTO':
			$database->InsertOTTO($_GET["playerid"],$_GET["value"]);
			break;		
	}

}



?>