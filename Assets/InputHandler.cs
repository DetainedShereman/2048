using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] GameField game_field;
    Cell cell;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) game_field.Left();
        if (Input.GetKeyDown(KeyCode.D)) game_field.Right();
        if (Input.GetKeyDown(KeyCode.W)) game_field.Up();
        if (Input.GetKeyDown(KeyCode.S)) game_field.Down();
    }
}