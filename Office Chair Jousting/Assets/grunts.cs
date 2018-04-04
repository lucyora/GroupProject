using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grunts : MonoBehaviour {
    //public AudioSource malegrunts;
    public AudioSource hits;
    // Use this for initialization
    void Start () {
     //   malegrunts = GameObject.Find("MaleGrunts").GetComponent<AudioSource>();
        hits = GameObject.Find("Hits").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player1")
        {
            int ran = Random.Range(0, 4);
            switch (ran)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    switch(ran)
                    {
                        case 0:
                            break;
                    }
                    break;
            }
            Debug.Log("is In");
            //hits.Play();
          //  malegrunts.PlayDelayed(0.5f);
        }
    }
}
