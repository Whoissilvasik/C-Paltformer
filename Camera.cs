using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;

    private void Awake()
    {
        if (!player)
        {
            // Ieskome objekta ssu tag "Player"
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("No 'Player'!");
            }
        }
    }

    private void Update()
    {
        if (player != null)
        {
            pos = player.position;
            pos.z = -10f;

            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
        }
    }
}
