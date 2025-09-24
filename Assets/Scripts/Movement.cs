using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public PlayerInput inputActions;    
    public Rigidbody2D rb;
    [Range(0f, 10f)]
    public float movementSpeed;

    void Start()
    {
        inputActions = new();
        inputActions.Player.Enable();
    }


    public void OnDestroy()
    {
        inputActions.Player.Disable();
    }

    void FixedUpdate()
    {
        if(ShopManager.Instance.shopActive == true || GameManager.Instance.deathScreen.activeSelf == true)
        {
            return;
        }
        Vector2 move = inputActions.Player.Move.ReadValue<Vector2>();
        rb.MovePosition(rb.position + move * movementSpeed * Time.deltaTime);
        
    }
}
