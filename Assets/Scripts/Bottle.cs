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
       if(currentColors.Count>=4)
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

    public void TransferColors(Bottle secondBottle)
    {
        //if(secondBottle.currentColors.Count==0)
        //{
        //    secondBottle.currentColors.Add(this.currentColors[currentColors.Count - 1]);
        //    secondBottle.transform.GetChild(4 - secondBottle.currentColors.Count).GetComponent<SpriteRenderer>().color = currentColors[currentColors.Count - 1];
        //    transform.GetChild(4-currentColors.Count).GetComponent<SpriteRenderer>().color = Color.clear;
        //    currentColors.RemoveAt(currentColors.Count - 1);
        //}
        //else 
        if(secondBottle.currentColors.Count < 4)
        {
            MatchColors(secondBottle);
        }
    }

    public void MatchColors(Bottle secondBottle)
    {
        Color secondColor = secondBottle.currentColors.Count > 0 ? secondBottle.currentColors[secondBottle.currentColors.Count - 1] : Color.clear;
        
        int i = currentColors.Count - 1;
        do
        {
            Color firstColor = currentColors[i];
            if (secondColor == Color.clear || firstColor == secondColor)
            {
                secondBottle.currentColors.Add(firstColor);
                secondBottle.transform.GetChild(4 - secondBottle.currentColors.Count).GetComponent<SpriteRenderer>().color = currentColors[i];
                transform.GetChild(4 - currentColors.Count).GetComponent<SpriteRenderer>().color = Color.clear;
                currentColors.RemoveAt(i);

                if(secondColor == Color.clear)
                {
                    secondColor = firstColor;
                }
            }
            else
            {
                break;
            }

            i--;
        } while ((i >= 0 && secondBottle.currentColors.Count < 4));  
    }
    
}
