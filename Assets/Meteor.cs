using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {
    //Initialization

    [SerializeField]
    public int hitsToBreak=0;
    [SerializeField]
    public int damage=0;

    float maxSpeed=5f;
    float minSpeed=1f;

    //State
    Vector3 dir=Vector2.zero;

    int hits=0;

     // Start is called before the first frame update
    void Start()
    {
        //Determine direction and speed
        Vector3 target=new Vector2(Random.Range(-9.5f, 9.5f), -5f);

        dir=(target-transform.position).normalized*Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        float dT=Time.deltaTime;

        //Move!
        transform.position+=dir*dT;   
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="Ground")
            Destroy(gameObject);
        else { //Hit by a bullet
            hits++;

            if (hits>=hitsToBreak)
                Destroy(gameObject);
        }
    }

}
