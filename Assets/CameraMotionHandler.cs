using UnityEngine;

public class CameraMotionHandler : MonoBehaviour
{
   private PlayerInputActions cameraRotationInputActions;
   
   [SerializeField] private Camera mainCamera;

   private Vector3 cameraVectorRotation;
   
   private float sensitivity = 5f;
   private float mainCameraRotationTopX = 50;
   private float mainCameraRotationDownX = 30;
   
   private void Awake()
   {
      cameraRotationInputActions = new PlayerInputActions();
      cameraRotationInputActions.CameraRotation.Enable();
   }

   private void Start()
   {
      Cursor.lockState = CursorLockMode.Locked;
      
      cameraVectorRotation = mainCamera.transform.eulerAngles;
   }

   private void Update()
   {
      Vector2 turn = cameraRotationInputActions.CameraRotation.LookAround.ReadValue<Vector2>();
      
      var cameraRotation = new Vector3(turn.y, turn.x, 0) * Time.deltaTime * sensitivity;
      
      transform.eulerAngles += new Vector3(0, cameraRotation.y,0);
      
      cameraVectorRotation.x += cameraRotation.x;
      cameraVectorRotation.x = Mathf.Clamp(cameraVectorRotation.x, -mainCameraRotationDownX, mainCameraRotationTopX);
      mainCamera.transform.localRotation = Quaternion.Euler(-cameraVectorRotation.x, transform.rotation.y, 0);
   }

}
