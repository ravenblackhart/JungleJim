using UnityEngine;

public class CamMovement : MonoBehaviour
{
    // public float moveSpeed = 1f;
    public Transform targetPlayer;
    public float speedAcc;
    // public float distanceToPlayer;
    // public Transform targetStart;

    private void FixedUpdate()
    {
        if (targetPlayer.transform.position.x > transform.position.x)
        {
            followPlayer();
        }
        else Accellerate();



        // if (Vector2.Distance(targetPlayer.position, transform.position) > distanceToPlayer)
        // {
        //     followPlayer();
        // }
        // else Accellerate();
        // float traveledDistance = Vector3.Distance(targetStart.position, targetPlayer.position);
        // Debug.Log(traveledDistance);
    }



    void followPlayer()
    {
        Vector3 newPosition = new Vector3(targetPlayer.position.x, transform.position.y, transform.position.z);
        transform.position = newPosition;

        // transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);

    }

    void Accellerate()
    {
        transform.Translate(Vector3.right * speedAcc * Time.deltaTime);
    }
}
