using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class MouseSimulation : MonoBehaviour
{
    private Mouse m_Mouse;

    private void OnEnable()
    {
        m_Mouse = InputSystem.AddDevice<Mouse>();
    }

    private void OnDisable()
    {
        InputSystem.RemoveDevice(m_Mouse);
        m_Mouse = null;
    }

    private void Update()
    {
        var touchscreen = Touchscreen.current;
        if (touchscreen == null)
            return;

        var position = touchscreen.position.ReadValue();
        var delta = touchscreen.delta.ReadValue();
        var button = touchscreen.press.isPressed;

        InputState.Change(m_Mouse, new MouseState
        {
            position = position,
            delta = delta
        }.WithButton(MouseButton.Left, button));
    }
}
