using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using JetBrains.Annotations; //For UI text

public class Mission5 : MonoBehaviour
{
    public GameObject silver_cin;
    public GameObject gold_coin;
    public GameObject Star;

    private GameObject[] requiredItems;
    private GameObject[] randomItems;
    private GameObject itemChecker;
    private int arrayChange;
    private int currentItemIndex = 0;
    private float delay = 2.0f;
      //For time delay to reset.
    private bool isDelayed = false;
    public bool missionComplete = false; //Tracking whether mission completed
    public TextMeshProUGUI missionStatus; //Ref to TextMeshPro

    private List<int> val = new List<int>();
    private int storage = 0;

    IEnumerator StartResetting()
    {
        // Set resetting flag to true to prevent multiple coroutines running simultaneously
        isDelayed = true;
        
        // Wait for the specified delay before resetting the text
        yield return new WaitForSeconds(delay);
        missionStatus.text = null;
        
        // Reset the text
        isDelayed = false;
    }

    // //For collecting items in order
    // void OnTriggerEnter(Collider other)
    // {
    //     //Make a UI, to show fail or success. Looking for textsmashpro(TMPro)

    //     //In a certain order...
    //     if (!missionComplete){
    //         if (other.gameObject == requiredItems[currentItemIndex]){
    //             currentItemIndex++;

    //             if (currentItemIndex >= requiredItems.Length)
    //             {
    //                 missionComplete = true;
    //                 missionStatus.text = "Success!";
    //                 StartCoroutine(StartResetting());

    //             }
    //         }
    //         else{
    //             missionStatus.text = "Failed!";
    //             StartCoroutine(StartResetting());

    //         }
    //     }
    // }

    private GameObject[] ShuffleArray(GameObject[] array)
    {
        GameObject[] newArray = array;
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            GameObject temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }

        return newArray;
    }

    // Start is called before the first frame update
    void Start()
    {
        requiredItems = new GameObject[] { silver_cin, gold_coin, Star };

        randomItems = ShuffleArray(requiredItems);

        string missionDescription = "Collect in order: ";

        foreach (GameObject item in randomItems)
        {
            missionDescription += item.name + " ";
        }

        missionStatus.text = missionDescription;
        Debug.Log(missionDescription);
        
        arrayChange = requiredItems.Length;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentItemIndex >= arrayChange )
            {
                missionComplete = true;
                missionStatus.text = "Success!";
                StartCoroutine(StartResetting());
            }
        //In a certain order...
        if (!missionComplete){
            
            // Check if the current required item is inactive
            if (requiredItems[currentItemIndex].activeSelf == false)
            {
                currentItemIndex++;
            }
            else
            {
                // Check for any item that is collected out of order
                for (int i = currentItemIndex + 1; i < requiredItems.Length; i++)
                {
                    if (!requiredItems[i].activeSelf)
                    {
                        missionComplete = true;
                        missionStatus.text = "Failed!";
                        StartCoroutine(StartResetting());
                        return;
                    }
                }
            }
    
        }
    }

}

