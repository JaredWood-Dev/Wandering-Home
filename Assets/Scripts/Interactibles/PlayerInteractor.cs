using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    public IInteractible CurrentInteractible;
    private InputAction _interactAction;

    private void Start()
    {
        _interactAction = InputSystem.actions.FindAction("Interact");
    }

    private void Update()
    {
        if (_interactAction.WasPressedThisFrame() && CurrentInteractible != null)
        {
            CurrentInteractible.Interact();
            CurrentInteractible = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractible i = other.GetComponent<IInteractible>();
        if (CurrentInteractible == null)
        {
            CurrentInteractible = i;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractible i = other.GetComponent<IInteractible>();
        if (CurrentInteractible != null && CurrentInteractible == i)
        {
            CurrentInteractible = null;
        }
    }
}
