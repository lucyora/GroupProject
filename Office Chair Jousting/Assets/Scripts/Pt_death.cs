using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pt_death : MonoBehaviour
{
    public ParticleSystem ef_death;
    public Transform target_death;
    public Player player;
    private bool bPt_switch;

    // Use this for initialization
    void Start()
    {
        
        ef_death.loop = false;
        ef_death.enableEmission = false;
        ef_death.Stop();

        bPt_switch = true;
        //gameObject.active = false;

    }
    // Update is called once per frame
    void Update()
    {
        transform.position = target_death.position;

        if (player.isAlive == false && bPt_switch == true)
        {
            triggerDeath();
        }
    }
    public void triggerDeath()
    {
        gameObject.active = true;
        ef_death.enableEmission = true;

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {        
        //print(Time.time);
        yield return new WaitForSeconds(3.5f);
        gameObject.active = false;
        ef_death.enableEmission = false;
        ef_death.Stop();
        bPt_switch = false;
    }
}