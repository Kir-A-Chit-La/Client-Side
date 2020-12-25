using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class CameraController : NetworkedBehaviour
{
    [SerializeField] private Transform _targetPlayer;
    [SerializeField] private float _smoothness = 10f;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _cameraRotation;
    private Vector3 _velocity = Vector3.zero;

    private void Start()
    {
        if(IsLocalPlayer)
        {
            gameObject.transform.SetParent(null);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _targetPlayer.position + _offset, ref _velocity, _smoothness * Time.deltaTime);
        transform.rotation = Quaternion.Euler(_cameraRotation);
    }
}
