using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public Transform targetPlayer;
    public float speedAcc = 1f;
    public float speedAccInc = 0.01f;

    private void FixedUpdate()

        // If player is in front of the camera the camera will follow the player otherwise it will accellerate.
    {
        if (targetPlayer.transform.position.x > transform.position.x)
        {
            followPlayer();
        }
        else Accellerate();

        //Cam accelleration value
        speedAcc += speedAccInc * Time.deltaTime;

    }



    void followPlayer()
    {
        Vector3 newPosition = new Vector3(targetPlayer.position.x, transform.position.y, transform.position.z);
        transform.position = newPosition;



    }

    void Accellerate()
    {
        transform.Translate(Vector3.right * speedAcc * Time.deltaTime);
    }
}
