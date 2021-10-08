using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public Transform targetPlayer;
    [SerializeField]
    private float speedAcc = 1f;
    [SerializeField]
    private float speedAccInc = 0.02f;
    [SerializeField]
    private float smoothSpeeed = 0.6f;
    [SerializeField]
    private float camOffset = 10f;

    private void FixedUpdate()

        // If player is in front of the camera the camera will follow the player otherwise it will accellerate.
    {

        if (targetPlayer.transform.position.x > transform.position.x + camOffset)
        {
            followPlayer();
        }
        else Accellerate();

        //Cam accelleration value
        speedAcc += speedAccInc * Time.deltaTime;

    }



    void followPlayer()
    {
        Vector3 newPosition = new Vector3(targetPlayer.position.x + camOffset, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, newPosition, smoothSpeeed * Time.deltaTime);
        transform.position = smoothedPosition;
        // transform.position = newPosition;



    }

    void Accellerate()
    {
        transform.Translate(Vector3.right * speedAcc * Time.deltaTime);
    }
}
