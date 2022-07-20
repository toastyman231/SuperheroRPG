using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 5f;
    public IInteractable nearestInteractable;

    public void Interact(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, interactRange) &&
            hitInfo.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable hitObject))
        {
            nearestInteractable = hitObject;
        }

        if (nearestInteractable != null)
        {
            nearestInteractable.Interact(context);
        }
    }
}
