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

    public bool Fill(Color fillColor)
    {
       if(currentColors.Count >= 4)
       {
          return false;
       }
        currentColors.Add(fillColor);
        transform.GetChild(4 - currentColors.Count).GetComponent<SpriteRenderer>().color = fillColor;
        return true;
    }

    public void EmptyBottle()
    {
        for(int i=0; i < transform.childCount; i++)
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

    public int TransferColors(Bottle secondBottle)
    {
        if(secondBottle.currentColors.Count < 4)
        {
            Color secondColor = secondBottle.currentColors.Count > 0 ? secondBottle.currentColors[secondBottle.currentColors.Count - 1] : Color.clear;
            int partsTransferred = 0;

            int i = currentColors.Count - 1;
            do
            {
                Color firstColor = currentColors[i];
                if (secondColor == Color.clear || firstColor == secondColor || GameController.instance.canReverse == true)
                {
                    Transfer(secondBottle);

                    if (secondColor == Color.clear)
                    {
                        secondColor = firstColor;
                    }

                    partsTransferred++;
                }
                else
                {
                    break;
                }
                i--; 
            } while ((i >= 0 && secondBottle.currentColors.Count < 4));
            return partsTransferred;
        }
        return 0;
    }

    public void Transfer(Bottle secondBottle)
    {
        secondBottle.currentColors.Add(currentColors[currentColors.Count - 1]);
        secondBottle.transform.GetChild(4 - secondBottle.currentColors.Count).GetComponent<SpriteRenderer>().color = currentColors[currentColors.Count - 1];
        transform.GetChild(4 - currentColors.Count).GetComponent<SpriteRenderer>().color = Color.clear;
        currentColors.RemoveAt(currentColors.Count - 1);
    }
}
