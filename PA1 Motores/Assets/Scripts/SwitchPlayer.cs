using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchPlayer : MonoBehaviour
{
    [SerializeField] private GameObject[] Players;
    [SerializeField] private Camera mainCamera;
    private int currentPlayerIndex = 0;

    private void Start()
    {
        UpdatePlayerVisibility();
        UpdateCamera();
    }
    public void SwitchPrevious(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            TogglePlayer(currentPlayerIndex, false);

            currentPlayerIndex = (currentPlayerIndex - 1 + Players.Length) % Players.Length;

            TogglePlayer(currentPlayerIndex, true);
            UpdateCamera();
        }
    }
    public void SwitchToNextPlayer(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TogglePlayer(currentPlayerIndex, false);

            currentPlayerIndex = (currentPlayerIndex + 1) % Players.Length;

            TogglePlayer(currentPlayerIndex, true);
            UpdateCamera();
        }
    }

    private void UpdatePlayerVisibility()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            TogglePlayer(i, i == currentPlayerIndex);
        }
    }
    private void TogglePlayer(int index, bool isActive)
    {
        if (index >= 0 && index < Players.Length)
        {
            Players[index].SetActive(isActive);

            Player_Controller playerMovement = Players[index].GetComponent<Player_Controller>();
            if (playerMovement != null)
            {
                playerMovement.enabled = isActive;
            }
        }
    }
    private void UpdateCamera()
    {
        if (mainCamera != null && Players.Length > 0)
        {
            mainCamera.transform.position = Players[currentPlayerIndex].transform.position + new Vector3(0, 10, -10);
            mainCamera.transform.LookAt(Players[currentPlayerIndex].transform);
        }
    }
}