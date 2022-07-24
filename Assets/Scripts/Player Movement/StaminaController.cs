using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    private PlayerMovement playerMove;
    private Slider staminaBar;

    public void Start()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        staminaBar = GetComponent<Slider>();
    }

    public void LateUpdate()
    {
        staminaBar.value = Mathf.Floor(Mathf.Lerp(staminaBar.value, playerMove.GetStamina(), Time.deltaTime));
        

        if (staminaBar.value <= 0)
        {
            staminaBar.transform.GetChild(1).gameObject.SetActive(false);
        } else
        {
            staminaBar.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
