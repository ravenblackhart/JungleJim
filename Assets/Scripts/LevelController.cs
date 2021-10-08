using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Transform[] levels;
    [SerializeField] private Vector3 nextLevelPosition;
    [SerializeField] private GameObject player;
    [SerializeField] private float levelDrawDistance;
    [SerializeField] private float LevelDeleteDistance;
    
    // Update is called once per frame
    void Update()
    { 
        RemoveTiles();
        LoadTiles();
    }

    void LoadTiles()
    {
        while ((nextLevelPosition - player.transform.position).x < levelDrawDistance)
        {
            //load random tilelevels at a specific distance 
            Transform level = levels[Random.Range(0, levels.Length)];
            Transform newPart = Instantiate(level, nextLevelPosition - level.Find("Start_point").position, level.rotation,
                transform);

            nextLevelPosition = newPart.Find("End_point").position;
        }
    }

    void RemoveTiles()
    {
        if (transform.childCount>1)
        {
            //remove the tile in position 0 when the player is a specific amount of lenght away from it
            Transform level = transform.GetChild(0);
            Vector3 diff = player.transform.position - level.position;

            if (diff.x > LevelDeleteDistance)
            {
                Destroy(level.gameObject);
            }
        }
    }
}
