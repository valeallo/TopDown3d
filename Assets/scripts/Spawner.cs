using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(Spawn());
    }

    // Update is called once per frame
    
    IEnumerator Spawn() 
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}
