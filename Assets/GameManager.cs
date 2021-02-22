using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour, IPointerDownHandler
{
    //Initialization

    [SerializeField]
    List<GameObject> meteors=null;
    [SerializeField]
    List<GameObject> towers=null;
    [SerializeField]
    GameObject bullet=null;

    int maxMeteors=1; //How many meteors at the same time? Should increase over time for higher difficulty.

    float meteorInterval=5f; //Meteors will be fired every so many seconds if current meteors < maxMeteors

    //State
    int currentMeteors=0;

    public int CurrentMeteors { get => currentMeteors; 
        set {
            currentMeteors=value>=0?value:0;
        }
    }
    


    // Start is called before the first frame update
    void Start()
    {
        Physics.queriesHitTriggers=true;

        StartCoroutine(ThrowMeteor());
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator ThrowMeteor() {

        while (true) {

            if (meteors!=null && meteors.Count>0) {
                if (CurrentMeteors<maxMeteors) {
                    //What meteor?
                    int mIndex=Random.Range(0, meteors.Count);

                    GameObject m=Instantiate(meteors[mIndex]);
                    Meteor mScr=m.GetComponent<Meteor>();

                    m.transform.position=new Vector2(Random.Range(-9.5f, 9.5f), 6f);

                }
            }
            else
                Debug.Log("Meteors not initialized!");

            yield return new WaitForSeconds(meteorInterval);

        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 towerPos=towers[Random.Range(0, towers.Count)].transform.position;
        GameObject b=Instantiate(bullet, towerPos, Quaternion.identity);
        b.GetComponent<Bullet>().dir=(worldPosition-towerPos).normalized;
    }
}
