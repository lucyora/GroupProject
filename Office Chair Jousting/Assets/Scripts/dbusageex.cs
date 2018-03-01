using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dbusageex : MonoBehaviour {
    public enum usagetype {UpdateLargestHit,NewLargestHit,GetPlayerLargestHit,GetTop10LargestHits, UpdateOttomanTime, NewOttomanTime, GetPlayerOttomanTime, GetTop10OttomanTime }
    public usagetype ut;
    public string playerid;
    public string displayname;
    public float value;

	// Use this for initialization
	void Start () {
        db_conn database = new db_conn();
        switch (ut)
        {
            case usagetype.NewLargestHit:
                StartCoroutine(database.setNewLargestHit(playerid, displayname, value));
            break;
            case usagetype.UpdateLargestHit:
                StartCoroutine(database.UpdateLargestHit(playerid, value));
            break;
            case usagetype.GetTop10LargestHits:
                StartCoroutine(database.getTopTenLargestHit());
            break;
            case usagetype.GetPlayerLargestHit:
                StartCoroutine(database.getPlayerLargestHit(playerid));
            break;


            case usagetype.NewOttomanTime:
                StartCoroutine(database.setNewOttomanTime(playerid,displayname,value));
            break;
            case usagetype.UpdateOttomanTime:
                StartCoroutine(database.UpdateOttomanTime(playerid, value));
            break;
            case usagetype.GetTop10OttomanTime:
                StartCoroutine(database.getTopTenOttomanTime());
            break;
            case usagetype.GetPlayerOttomanTime:
                StartCoroutine(database.getPlayerOttomanTme(playerid));
            break;

        }
        	
	}
}
