using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField]
    private float cameraOffset = 0f;
    [SerializeField]
    private float interpolant = 0.5f;
    
    private Transform characterTransform;

    // Start is called before the first frame update
    void Start()
    {
        characterTransform = GameObject.FindObjectOfType<PlayableCharacter>().transform;
    }

    void Update()
    {
        Vector3 destinationPosition = this.transform.position;
        destinationPosition.y = characterTransform.position.y + cameraOffset;
        this.transform.position = Vector3.Lerp(this.transform.position, destinationPosition, interpolant * Time.deltaTime);
    }
}
