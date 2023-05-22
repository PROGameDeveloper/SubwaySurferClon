using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject m_camera;
    public GameObject player;
    // Start is called before the first frame update

    private void Update()
    {
        if (player.transform.position.z >= transform.position.z)
        {
            player.GetComponent<CharacterController>().Move(new Vector3(player.transform.position.x,0, -40));
        }
    }
}
