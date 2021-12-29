using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Color> currentColors = new List<Color>();
    bool bottleSelected = false;
    void Start()
    {
        
    }

    public void Initialize()
    {
        for (int i = 3; i >= 0; i--)
        {
            Color randomColor = GameController.instance.bottleColors[Random.Range(0, GameController.instance.bottleColors.Count)];
            currentColors.Add(randomColor);
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = randomColor;
        }
    }

    public void EmptyBottle()
    {
        for(int i=0; i < currentColors.Count; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.clear;
        }
        currentColors.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleAnimation()
    {
        bottleSelected = !bottleSelected;
        transform.GetComponent<Animator>().SetBool("TouchDetected", bottleSelected);
    }

    public void TransferColors(Bottle secondBottle)
    {
        if(secondBottle.currentColors.Count==0)
        {
            secondBottle.currentColors.Add(this.currentColors[currentColors.Count - 1]);
            secondBottle.transform.GetChild(4 - secondBottle.currentColors.Count).GetComponent<SpriteRenderer>().color = currentColors[currentColors.Count - 1];
            transform.GetChild(4-currentColors.Count).GetComponent<SpriteRenderer>().color = Color.clear;
            currentColors.RemoveAt(currentColors.Count - 1);
        }

        else
        {
            MatchColors(secondBottle);
        }
    }

    public void MatchColors(Bottle secondBottle)
    {
        Color firstColor = currentColors[currentColors.Count - 1];
        Color secondColor = secondBottle.currentColors[secondBottle.currentColors.Count - 1];
        if(firstColor==secondColor)
        {
            secondBottle.currentColors.Add(this.currentColors[currentColors.Count - 1]);
            secondBottle.transform.GetChild(4 - secondBottle.currentColors.Count).GetComponent<SpriteRenderer>().color = currentColors[currentColors.Count - 1];
            transform.GetChild(4 - currentColors.Count).GetComponent<SpriteRenderer>().color = Color.clear;
            currentColors.RemoveAt(currentColors.Count - 1);
        }
    }
    
}
