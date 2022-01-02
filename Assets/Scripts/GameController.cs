using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public class Steps
    {
        public Steps(Bottle a, Bottle b, int p)
        {
            firstBottle = a;
            secondBottle = b;
            partsCount = p;
        }
        public Bottle firstBottle;
        public Bottle secondBottle;
        public int partsCount;
    }
    public static GameController instance;
    public GameObject currentBottle = null;
    public GameObject bottlePrefab;
    float bottlePositionOffset = 2f;
    public List<Color> bottleColors = new List<Color>();
    public List<Bottle> bottles = new List<Bottle>();
    List<List<Color>> bottlesinitialState = new List<List<Color>>();
    public List<Steps> stepsHistory = new List<Steps>();
    public bool canReverse = false;
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

        foreach(var b in bottles)
        {
            bottlesinitialState.Add(new List<Color>(b.currentColors));
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
            int pCount = currentBottle.transform.GetComponent<Bottle>().TransferColors(newBottle.transform.GetComponent<Bottle>());
            if (pCount > 0 && canReverse == false)
            {
                stepsHistory.Add(new Steps(currentBottle.GetComponent<Bottle>(), newBottle.GetComponent<Bottle>(), pCount));
            }
            currentBottle.transform.GetComponent<Bottle>().ToggleAnimation();
            currentBottle = newBottle;
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

    public void UndoLastStep()
    {
        if (stepsHistory.Count > 0)
        {
            for (int i = 0; i < stepsHistory[stepsHistory.Count - 1].partsCount; i++)
            {
                stepsHistory[stepsHistory.Count - 1].secondBottle.Transfer(stepsHistory[stepsHistory.Count - 1].firstBottle);
            }
            stepsHistory.RemoveAt(stepsHistory.Count - 1);
        }
        canReverse = false;
    }

    public void Reset()
    {
        for (int i = 0; i < bottlesinitialState.Count - 2; i++)
        {
            bottles[i].currentColors = new List<Color>();
            for (int j = 0; j < 4; j++)
            {
                bottles[i].Fill(bottlesinitialState[i][j]);
            }
        }

        bottles[bottles.Count - 1].EmptyBottle();
        bottles[bottles.Count - 2].EmptyBottle();
    }
}
