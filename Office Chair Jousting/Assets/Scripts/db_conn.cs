using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db_conn{


    public IEnumerator setNewLargestHit(string playerid,string displayname, float value)
    {
        string url = "http://zackweinstein.ca/ocj/server.php?key=12345678910&action=newLH&playerid="+playerid+"&displayname="+displayname+"&value="+value.ToString()+"&platform=0";
        WWWForm form = new WWWForm();
        WWW dataResult = new WWW(url, form);
        yield return dataResult; 
        string data = dataResult.text;
        Debug.Log(data);
    }
    public IEnumerator UpdateLargestHit(string playerid, float value)
    {
        string url = "http://zackweinstein.ca/ocj/server.php?key=12345678910&action=insLH&playerid="+playerid+"&value="+value.ToString()+"&platform=0";
        WWWForm form = new WWWForm();
        WWW dataResult = new WWW(url, form);
        yield return dataResult;
        string data = dataResult.text;
        Debug.Log(data);
    }
    public IEnumerator getTopTenLargestHit()
    {
        string url = "http://zackweinstein.ca/ocj/server.php?key=12345678910&action=T10LH";
        WWWForm form = new WWWForm();
        WWW dataResult = new WWW(url, form);
        yield return dataResult;
        string data = dataResult.text;
        Debug.Log(data);
    }
    public IEnumerator getPlayerLargestHit(string playerid)
    {
        string url = "http://zackweinstein.ca/ocj/server.php?key=12345678910&action=PHL&playerid="+playerid+"&platform=0";
        WWWForm form = new WWWForm();
        WWW dataResult = new WWW(url, form);
        yield return dataResult;
        string data = dataResult.text;
        Debug.Log(data);
    }




    public IEnumerator setNewOttomanTime(string playerid, string displayname, float value)
    {
        string url = "http://zackweinstein.ca/ocj/server.php?key=12345678910&action=newOTTO&playerid="+playerid+"&displayname="+displayname+"&value="+value.ToString()+"&platform=0";
        WWWForm form = new WWWForm();
        WWW dataResult = new WWW(url, form);
        yield return dataResult;
        string data = dataResult.text;
        Debug.Log(data);
    }
    public IEnumerator UpdateOttomanTime(string playerid, float value)
    {
        string url = "http://zackweinstein.ca/ocj/server.php?key=12345678910&action=insOTTO&playerid="+playerid+"&value="+value.ToString()+"&platform=0";
        WWWForm form = new WWWForm();
        WWW dataResult = new WWW(url, form);
        yield return dataResult;
        string data = dataResult.text;
        Debug.Log(data);
    }
    public IEnumerator getTopTenOttomanTime()
    {
        string url = "http://zackweinstein.ca/ocj/server.php?key=12345678910&action=T10OTTO";
        WWWForm form = new WWWForm();
        WWW dataResult = new WWW(url, form);
        yield return dataResult;
        string data = dataResult.text;
        Debug.Log(data);
    }
    public IEnumerator getPlayerOttomanTme(string playerid)
    {
        string url = "http://zackweinstein.ca/ocj/server.php?key=12345678910&action=PLOTTO&playerid="+playerid+"&platform=0";
        WWWForm form = new WWWForm();
        WWW dataResult = new WWW(url, form);
        yield return dataResult;
        string data = dataResult.text;
        Debug.Log(data);
    }


}
