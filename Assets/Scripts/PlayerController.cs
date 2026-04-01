using UnityEngine;
using UnityEngine.InputSystem;  

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 mousePos = Camera.main.ViewportToWorldPoint(Mouse.current.position.value);
            Debug.Log("Mouse position: " + mousePos);

            Vector2 direction = mousePos - transform.position;
            transform.up

        }
    }
}
