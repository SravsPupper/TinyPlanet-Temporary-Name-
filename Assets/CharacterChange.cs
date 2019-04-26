using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChange : MonoBehaviour
{
   public bool Characterone;
    public bool CharacterTwo;

    public PlayerController one;
    public PlayerController two;

    public Camera FirstCamera;
    public Camera SecondCamera;
    private void Awake()
    {
        FirstCamera.gameObject.SetActive(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Joystick Change"))
        {
            if (Characterone == true)
            {
                one.canMove = false;
                two.canMove = true;
               // SecondCamera.tag="MainCamera";
               // FirstCamera.tag = "Untagged";

                SecondCamera.gameObject.SetActive(true);
                FirstCamera.gameObject.SetActive(false);
                Characterone = false;
            }
            else if (Characterone == false)
            {
                one.canMove = true;
                two.canMove = false;
                //FirstCamera.tag = "MainCamera";
                // SecondCamera.tag ="Untagged";
                FirstCamera.gameObject.SetActive(true);
                SecondCamera.gameObject.SetActive(false);

                Characterone = true;
            }
        }
    }
}
