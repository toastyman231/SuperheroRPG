using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    public static PlayerLook camMove;
    public static PlayerMovement playerMove;
    public static PlayerInput pInput;
    public abstract void Interact(InputAction.CallbackContext context);
}

public class BaseInteractable : MonoBehaviour, IInteractable
{
    public static PlayerLook camMove;
    public static PlayerMovement playerMove;
    public static PlayerInput pInput;
    public bool interacted = false;

    public void Awake()
    {
        Setup();
    }

    public void Setup()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerMove = player.GetComponent<PlayerMovement>();
        camMove = player.GetComponent<PlayerLook>();
        pInput = player.GetComponent<PlayerInput>();
    }

    public virtual void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interacted!");
    }
}
