using UnityEngine;

[CreateAssetMenu(fileName = "New Player Config", menuName = "Custom/Entity/Player/Config")]
public class PlayerConfig : ScriptableObject
{
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
    public Vector3 standingColliderCenter = new Vector3(0f, 1.2f, 0f);
    public Vector3 crouchColliderCenter = new Vector3(0f, 0.9f, 0f);
    public float standingColliderHeight = 2.2f;
    public float crouchColliderHeight = 1.7f;
    public float ceilingDistance = 0.5f;
    public LayerMask ceilingLayer;
    public float groundDistance = 0.1f;
    public LayerMask groundLayer;
}
