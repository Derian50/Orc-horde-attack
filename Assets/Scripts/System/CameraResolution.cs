
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    [SerializeField] float ratio;
    void Start()
    {
        Camera.main.aspect = ratio;
    }
}
