using System;
using UnityEngine;
using Vuforia;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject miniDisk;
    [SerializeField]
    private GameObject smallDisk;
    [SerializeField] 
    private GameObject middleDisk;
    [SerializeField]
    private GameObject largeDisk;

    private TheStacks firstStack;
    private TheStacks secondStack;
    private TheStacks thirdStack;

    //Reference Empty Objects for Stack, Could be refactored.
    [SerializeField]    
    private GameObject sceneStack;
    [SerializeField]
    private GameObject sceneStack2;
    [SerializeField]
    private GameObject sceneStack3;
    

    [SerializeField]
    private GameObject leftB1;

    [SerializeField]
    private GameObject rightB1;

    [SerializeField]
    private GameObject leftB2;

    [SerializeField]
    private GameObject rightB2;

    [SerializeField]
    private GameObject leftB3;

    [SerializeField]
    private GameObject rightB3;

    private void Awake()
    {
        //Virtual Button Initialization
        InitializeVB();
        //Initialize the Stacks
        InitializeStack();
    }

    private void Start()
    {   
        //Initialize the first stack discs
        InitializeDiscOrder();
    }
    private void FixedUpdate()
    {
        if(thirdStack.GetDiscCount()==4)
        {
            Console.WriteLine("You solved the puzzle.");
            DeinitializeGameObject();
            thirdStack.ResetTheStack();
        }
    }
    private void Update()
    {
        //Mouse Control
        if(Input.GetMouseButtonDown(0))
        {   
            Ray mouseRay=Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if(Physics.Raycast(mouseRay,out raycastHit))
            {
                if(raycastHit.transform.CompareTag("LB1"))
                {   
                    Console.WriteLine("LB1 is pressed");
                    PopToOtherStack(firstStack,thirdStack);
                }
                else if(raycastHit.transform.CompareTag("RB1"))
                {
                    Console.WriteLine("RB1 is pressed");
                    PopToOtherStack(firstStack,secondStack);
                }
                else if(raycastHit.transform.CompareTag("LB2"))
                {
                    Console.WriteLine("LB2 is pressed");
                    PopToOtherStack(secondStack, firstStack);
                }
                else if(raycastHit.transform.CompareTag("RB2"))
                {
                    Console.WriteLine("RB2 is pressed");
                    PopToOtherStack(secondStack,thirdStack);
                }
                else if(raycastHit.transform.CompareTag("LB3"))
                {
                    Console.WriteLine("LB3 is pressed");
                    PopToOtherStack(thirdStack,secondStack);
                }
                else if(raycastHit.transform.CompareTag("RB3"))
                {
                    Console.WriteLine("RB3 is pressed");
                    PopToOtherStack(thirdStack,firstStack);
                }
            }
        }
    }
    //Virtual Button Control
    private void OnRB3Pressed(VirtualButtonBehaviour obj)
    {
        PopToOtherStack(thirdStack, firstStack);    
    }
    private void OnLB3Pressed(VirtualButtonBehaviour obj)
    {
        PopToOtherStack(thirdStack, secondStack);    
    }
    private void OnRB2Pressed(VirtualButtonBehaviour obj)
    {
        PopToOtherStack(secondStack, thirdStack);    
    }
    private void OnLB2Pressed(VirtualButtonBehaviour obj)
    {
        PopToOtherStack(secondStack, firstStack);
    }
    private void OnRB1Pressed(VirtualButtonBehaviour obj)
    {
        PopToOtherStack(firstStack, secondStack);    
    }
    private void OnLB1Pressed(VirtualButtonBehaviour obj)
    {
        PopToOtherStack(firstStack, thirdStack);    
    }
    private void PopToOtherStack(TheStacks xStack, TheStacks yStack)
    {
        //TODO: Refactor for a better logic.
        if(!xStack.PopTheDisc(yStack))
        {
            Console.WriteLine("Triggered");
        }
    }
    private void InitializeVB()
    {
        leftB1.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnLB1Pressed);
        rightB1.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnRB1Pressed);
        leftB2.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnLB2Pressed);
        rightB2.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnRB2Pressed);
        leftB3.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnLB3Pressed);
        rightB3.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnRB3Pressed);
    }
    private void InitializeStack()
    {
        firstStack=new TheStacks(sceneStack.transform.position.x);
        secondStack=new TheStacks(sceneStack2.transform.position.x);
        thirdStack=new TheStacks(sceneStack3.transform.position.x);
    }
    private void InitializeDiscOrder()
    {
        firstStack.Place(largeDisk);
        firstStack.Place(middleDisk);
        firstStack.Place(smallDisk);
        firstStack.Place(miniDisk);
    }
    private void DeinitializeGameObject()
    {
        miniDisk.gameObject.SetActive(false);
        smallDisk.gameObject.SetActive(false);
        middleDisk.gameObject.SetActive(false);
        largeDisk.gameObject.SetActive(false);
    }
}