using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Canvas mobileInputUI;

    private Rigidbody capsuleRigidbody;
    private PlayerInputActions playerInputActions;
    private float playerSpeed = 2;
    
    
    private void Awake()
    {
        capsuleRigidbody = GetComponent<Rigidbody>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        #if UNITY_ANDROID
            mobileInputUI.gameObject.SetActive(true);
        #endif
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        
        Vector3 playerDirection = capsuleRigidbody.rotation * new Vector3(inputVector.x, 0, inputVector.y);
        capsuleRigidbody.MovePosition(capsuleRigidbody.position + (playerDirection * playerSpeed * Time.fixedDeltaTime));
    }
    
}
