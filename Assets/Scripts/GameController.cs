using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<GameObject> balls;
    [SerializeField] private Material[] materials;
    [SerializeField] private GameObject spawnPlane;
    [SerializeField] private Text text;
    private List<GameObject> array = new List<GameObject>();
    private List<GameObject> list = new List<GameObject>();
    private void Start()
    {
        foreach (var b in balls)
        {
            list.Add(b);
        }

        int lengthList = list.Count;

        for (int i = 0; i < lengthList; i++)
        {
            int index = Random.Range(0, list.Count - 1);
            list[index].GetComponent<Renderer>().material = materials[Random.Range(0,materials.Length-1)];
            list[index].SetActive(true);
            list[index].transform.position = balls[i].transform.position;
            list.RemoveAt(index);
        }
    }

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
                        ball.SetActive(true);
                        ball.transform.position = hit.point;
                        ball.GetComponent<BallBehaviour>().isInPlace = hit.collider.tag == "Plane" ? false : true;
                        array.RemoveAt(0);
                    }
                }
            }
        }

        if (CheckAnswer.IsRight == IsRightAnswer.True)
        {
            text.text = "You're right!";
            StartCoroutine(PauseToRestart());
        }
        else if (CheckAnswer.IsRight == IsRightAnswer.False)
        {
            text.text = "Try another one";
            StartCoroutine(PauseToRestart());
        }
        else
        {
            text.text = string.Empty;
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

    private IEnumerator PauseToRestart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }

}
