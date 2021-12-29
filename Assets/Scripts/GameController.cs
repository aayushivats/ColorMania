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
        
        for(int i=0 ; i < 5; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                GameObject go = Instantiate(bottlePrefab);
                go.transform.position = new Vector2(-5.2f + bottlePositionOffset * i, 1.5f - 4.0f * j);
                go.transform.GetComponent<Bottle>().Initialize();

                if (j * 5 + i >= 8)
                {
                    go.transform.GetComponent<Bottle>().EmptyBottle();
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
            currentBottle.transform.GetComponent<Bottle>().ToggleAnimation();
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
