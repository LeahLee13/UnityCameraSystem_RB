using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameObject playerPrefab;
    [HideInInspector]
    public GameObject playerObj;

    void Start()
    {
        playerObj = GameObject.Instantiate<GameObject>(playerPrefab, new Vector3(25, 0, 28), new Quaternion(0, 180, 0, 0));
        playerObj.name = playerPrefab.name;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (playerObj == null)
            {
                playerObj = GameObject.Instantiate<GameObject>(playerPrefab, new Vector3(25, 0, 28), new Quaternion(0, 180, 0, 0));
                playerObj.name = playerPrefab.name;
            }
            else
            {
                Destroy(playerObj);
            }
        }
    }
}
