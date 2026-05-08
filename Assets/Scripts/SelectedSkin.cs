using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SelectedSkin : MonoBehaviour
{
    //ships
    public static bool ShipOneSelected;
    public static bool ShipTwoSelected;
    public static bool ShipThreeSelected;
    public static bool ShipFourSelected;
    public GameObject One, Two, Three, Four;


    public void SelectOne()
    {
        ShipOneSelected = true;
        ShipTwoSelected = false;
        ShipThreeSelected = false;
        ShipFourSelected = false;
    }

    public void SelectTwo()
    {
        ShipOneSelected = false;
        ShipTwoSelected = true;
        ShipThreeSelected = false;
        ShipFourSelected = false;
    }

    public void SelectThree()
    {
        ShipOneSelected = false;
        ShipTwoSelected = false;
        ShipThreeSelected = true;
        ShipFourSelected = false;
    }

    public void SelectFour()
    {
        ShipOneSelected = false;
        ShipTwoSelected = false;
        ShipThreeSelected = false;
        ShipFourSelected = true;
    }

    public void DeselectAll()
    {
        ShipOneSelected = false;
        ShipTwoSelected = false;
        ShipThreeSelected = false;
        ShipFourSelected = false;
    }
    
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    void Start()
    {
   
    }
}