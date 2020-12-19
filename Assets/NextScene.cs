using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject lookAtObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckMainCameraPosition();
    }

    void CheckMainCameraPosition()
    {
        if(Vector3.Distance(mainCamera.transform.position, lookAtObject.transform.position) <= 2)
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
