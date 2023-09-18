using System;
using UnityEngine;
using UnityEngine.InputSystem;

/* Script by Juno Stretmo */

public class InputManager : MonoBehaviour
{
    public static Vector2 MoveInput { get; private set; }
    public static bool JumpInput { get; private set; }

    private static Action<Vector2, bool> onMove;
    private static Action onFire;
    private static Action onInteract;
    private static Action onInventory;

    public static void AddOnMove(Action<Vector2, bool> action) => onMove += action;
    public static void RemoveOnMove(Action<Vector2, bool> action) => onMove -= action;

    public static void AddOnJump(Action action) => onFire += action;
    public static void RemoveOnJump(Action action) => onFire -= action;

    public static void AddOnInteract(Action action) => onInteract += action;
    public static void RemoveOnInteract(Action action) => onInteract -= action;

    public static void AddOnInventory(Action action) => onInventory += action;
    public static void RemoveOnInventory(Action action) => onInventory -= action;

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
        onMove?.Invoke(MoveInput, context.performed);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        JumpInput = context.ReadValue<float>() > 0;
        onFire?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        onInteract?.Invoke();
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        onInventory?.Invoke();
    }
}
