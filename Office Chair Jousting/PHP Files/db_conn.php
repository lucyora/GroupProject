<?php 
	class db_conn{
    	public function __construct()
        {
            $dbHost = "localhost";
            $dbUser = "ocjsession";
            $dbPass = "c5f4eea59db9eb8905e4cf98a5d7fae4";
            $dbName = "OCJ";
     
            try
            {
                $this->_conn = new PDO("mysql:host=".$dbHost.";dbname=".$dbName, $dbUser, $dbPass);
                $this->_conn->setAttribute( PDO::ATTR_ERRMODE, PDO::ERRMODE_WARNING);
            } 
            catch(Exception $e)
            {
                die("Error: " . $e->getMessage());
            }

        }
    }

?>