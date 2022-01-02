using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject currentBottle = null;
    public GameObject bottlePrefab;
    float bottlePositionOffset = 2f;
    public List<Color> bottleColors = new List<Color>();
    public List<Bottle> bottles = new List<Bottle>();

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j < 2; j++)
        {
            for (int i=0 ; i < 6; i++)
            {
                GameObject go = Instantiate(bottlePrefab);
                go.transform.position = new Vector2(-5.2f + bottlePositionOffset * i, 1.5f - 4.0f * j);

                if (j * 5 + i >=9)
                {
                    go.transform.GetComponent<Bottle>().EmptyBottle();
                }
                bottles.Add(go.transform.GetComponent<Bottle>());
            }
        }

        foreach(Color c in bottleColors)
        {
                for(int i=0; i < 4; i++)
                {
                int randomnumber = Random.Range(0, 10);
                while (bottles[randomnumber].GetComponent<Bottle>().Fill(c) == false)
                {
                    randomnumber = Random.Range(0, 10);
                } 
            }
        }

        PlayerInput.OnBottleTouchDelegate += OnBottleTouch;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnBottleTouch(GameObject newBottle)
    {
        if (currentBottle == null)
        {
            newBottle.GetComponent<Bottle>().ToggleAnimation();
            currentBottle = newBottle;
        }
        else if (currentBottle != newBottle)
        {
            currentBottle.transform.GetComponent<Bottle>().TransferColors(newBottle.transform.GetComponent<Bottle>());
            currentBottle.transform.GetComponent<Bottle>().ToggleAnimation();
            currentBottle = newBottle;
            //currentBottle.transform.GetComponent<Bottle>().ToggleAnimation();
            currentBottle = null;
        }
        else
        {
            currentBottle.transform.GetComponent<Bottle>().ToggleAnimation();
            currentBottle = null;
        }
    }

    private void OnDisable()
    {
        PlayerInput.OnBottleTouchDelegate -= OnBottleTouch;
    }

}
