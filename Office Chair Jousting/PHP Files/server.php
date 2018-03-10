<?php 
include 'db.php';
$results = "";
if ($_GET["key"] == "12345678910") {
	$database = new db();
	switch ($_GET["action"]) {
		case 'T10LH':
			$dbresults = $database->getTop10LH();
			//$results = json_encode($results);
			//echo $results;

			$json = $json.'{"dbresults": [';
			foreach ($dbresults as $player) 
			{ 
				$json = $json.'{';
				$json = $json.'"playerdisplayname":'.'"'.$player["playerdisplayname"].'",';
				$json = $json.'"value":'.'"'.$player["value"].'",';
				$json = $json.'"platform":'.'"'.$player["platform"].'"';
				$json = $json.'}';
				if (!($player === end($dbresults))) 
				{
					$json = $json.",";
				}
			}
			$json = $json."]}";
			echo $json;
			break;
		case 'PLH':
			$dbresults = $database->getPlayerLH($_GET["playerid"]);
			$json = $json.'{"dbresults": [';
			foreach ($dbresults as $player) 
			{ 
				$json = $json.'{';
				$json = $json.'"playerdisplayname":'.'"'.$player["playerdisplayname"].'",';
				$json = $json.'"value":'.'"'.$player["value"].'",';
				$json = $json.'"platform":'.'"'.$player["platform"].'"';
				$json = $json.'}';
				if (!($player === end($dbresults))) 
				{
					$json = $json.",";
				}
			}
			$json = $json."]}";
			echo $json;		
			break;
		case 'newLH':
			$database->newInsertLH($_GET["playerid"],$_GET["displayname"],$_GET["value"],$_GET["platform"]);
			break;	
		case 'insLH':
			$database->InsertLH($_GET["playerid"],$_GET["value"]);
			break;			
		case 'T10OTTO':	
			$dbresults = $database->getTop10OTTO();
			$json = $json.'{"dbresults": [';
			foreach ($dbresults as $player) 
			{ 
				$json = $json.'{';
				$json = $json.'"playerdisplayname":'.'"'.$player["playerdisplayname"].'",';
				$json = $json.'"value":'.'"'.$player["value"].'",';
				$json = $json.'"platform":'.'"'.$player["platform"].'"';
				$json = $json.'}';
				if (!($player === end($dbresults))) 
				{
					$json = $json.",";
				}
			}
			$json = $json."]}";
			echo $json;
			break;
		case 'PLOTTO':
			$dbresults = $database->getPlayerOTTO($_GET["playerid"]);
			$json = $json.'{"dbresults": [';
			foreach ($dbresults as $player) 
			{ 
				$json = $json.'{';
				$json = $json.'"playerdisplayname":'.'"'.$player["playerdisplayname"].'",';
				$json = $json.'"value":'.'"'.$player["value"].'",';
				$json = $json.'"platform":'.'"'.$player["platform"].'"';
				$json = $json.'}';
				if (!($player === end($dbresults))) 
				{
					$json = $json.",";
				}
			}
			$json = $json."]}";
			echo $json;			
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