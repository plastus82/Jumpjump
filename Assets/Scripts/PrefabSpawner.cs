using UnityEngine;
using System.Collections;

public class PrefabSpawner : MonoBehaviour {

    private float nextSpawn = 0;

    public Transform prefabToSpawn;
    //public float spawnRate = 1;
    public float randomDelay = 0.25f;
    public AnimationCurve spawnCurve;
    public float curveLengthsinSeconds = 30f;
    private float startTime;
    public float randomPositionY = 0;
    public bool canSpawn = true;
    

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time > nextSpawn && canSpawn)
        {
            float moveY = Random.Range(-randomPositionY, randomPositionY);

            Instantiate(prefabToSpawn, transform.position + new Vector3(0, moveY, 0), Quaternion.identity);
           // nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);


            float curvePosition = (Time.time - startTime) / curveLengthsinSeconds;
            if(curvePosition >1f)
            {
                curvePosition = 1f;
                startTime = Time.time;

            }

            nextSpawn = Time.time + spawnCurve.Evaluate(curvePosition) + Random.Range(-randomDelay,randomDelay);
        }
	}
}
