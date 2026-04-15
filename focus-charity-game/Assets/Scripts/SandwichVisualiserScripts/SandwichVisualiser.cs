using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;



public class SandwichVisualiser : MonoBehaviour
{

    //animator
    private Animator anim;


    //Prefabs of ingredients to instantiate
    public GameObject TomatoSlice; // use "Tomato" child
    public GameObject CheeseSlice; // use "Cheese" child
    public GameObject HamSlice; // use "Ham" child



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>(); // Get the animator of the object
    }


    //ALL DIFFERENT STATES OF SANDWICH can be called with the following methods:

    void SandwichStart() // Call this when the minigame starts to open the sandwich
    {
        anim.SetFloat("state",1); //Changes the animation to sw_open
    }


    void SandwichFinish() // Call this when the sandwich minigame ends to close the sandwich over
    {
        anim.SetFloat("state",2); //Changes the animation to sw_close
    }

    void SandwichReset() // Call this after the minigame to reset the animation and  deactivate all the ingredients for the next order
    {
        anim.SetFloat ("state",0); //Changes the animation to sw_empty

        // Deactivate all ingredients. Plain bread time
        TomatoSlice.SetActive(false);
        CheeseSlice.SetActive(false);
        HamSlice.SetActive(false);
    }


    //INGREDIENTS - use each of these to activate the required ingredient
        void Tomato()
    {
        TomatoSlice.SetActive(true); // tomat
    }

    void Cheese()
    {
        CheeseSlice.SetActive(true); // chess
    }

    void Ham()
    {
        HamSlice.SetActive(true); // hamburger
    }

}
