using System.Collections.Generic;
using UnityEngine;

public class TheStacks
{
    private Stack<GameObject> discStack =new Stack<GameObject>(4);
    public int GetDiscCount()
    {
        return discStack.Count;
    }
    public float HorizontalCoordinate{get;}
    public TheStacks(float horizontalCoordinate)
    {
        HorizontalCoordinate = horizontalCoordinate;
    }
    public float VerticalCoordinate
    {
        get
        {
            if(discStack.Count==3)
            {
                return 0.0233f;
            }
            else if(discStack.Count==2)   
            {
                return 0.019f;
            }
            else if(discStack.Count==1)
            {
                return 0.0122f;
            }
            else
            {
                return 0.0046f;
            }
        }
    }
    public bool PopTheDisc(TheStacks targetStack)
    {
        if(this.discStack.Count==0)//If there is no disc in the current stack, false.
        {
            return false;
        }
        if(!IsPlacable(this.discStack.Peek(),targetStack))
        {
            return true;
        }
        else if(targetStack.Place(discStack.Peek()))
        {
            discStack.Pop();
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool Place(GameObject disc)
    {
        switch(discStack.Count)
        {
            case 4:
                return false;
            default :
                disc.transform.localPosition=new Vector3(HorizontalCoordinate,VerticalCoordinate,0);//Position the disc
                discStack.Push(disc);
                return true;
        }
    }
    public bool IsPlacable(GameObject disc,TheStacks theStack)
    {
        if(theStack.discStack.Count==0)
            return true;
        //If target stack has disc that is larger than selected disc, return true.
        else if(theStack.discStack.Peek().transform.localScale.x > disc.transform.localScale.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //Reset the stack.
    public void ResetTheStack()
    {
        this.discStack.Clear();
    }
}
