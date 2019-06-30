using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    public static Vector2 GetDirection()
    {
        return new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));
    }

    public static bool GetJump()
    {
        return Input.GetButton("Jump");
    }

    public static bool GetFire()
    {
        return Input.GetButton("Fire1");
    }
}
