using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject spawnPlane;
    private List<GameObject> array = new List<GameObject>();
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Ball")
                {
                    array.Add(hit.collider.gameObject);
                    hit.collider.gameObject.GetComponent<BallBehaviour>().isInPlace = false;
                    hit.collider.gameObject.SetActive(false);
                }
                else if (hit.collider.tag == "Plane" || hit.collider.tag == "Pull")
                {
                    GameObject ball = array.Count > 0 ? array[0] : null;
                    if (ball != null)
                    {
                        Vector3 vect = new Vector3(hit.transform.position.x + Random.Range(-0.3f,0.3f),
                            hit.transform.position.y, hit.transform.position.z + Random.Range(-0.3f, 0.3f));
                        ball.SetActive(true);
                        ball.transform.position = vect;
                        ball.GetComponent<BallBehaviour>().isInPlace = hit.collider.tag == "Plane" ? false : true;
                        array.RemoveAt(0);
                    }
                }
            }
        }
        
    }
    public void BackToPull()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var item in balls)
        {
            if (!item.GetComponent<BallBehaviour>().isInPlace)
            {
                Vector3 vect = new Vector3(spawnPlane.transform.position.x + Random.Range(-0.2f, 0.2f),
            spawnPlane.transform.position.y + 0.5f,
            spawnPlane.transform.position.z + Random.Range(-0.5f, 0.5f));
                item.gameObject.SetActive(true);
                item.transform.position = vect;
            }
            
        }
    }

}
