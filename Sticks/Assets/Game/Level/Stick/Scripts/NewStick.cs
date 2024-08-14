using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class NewStick : MonoBehaviour
{
    [SerializeField] private EStickType _type = EStickType.None;
    [SerializeField] private EStickMovementDirection _movementDirection = EStickMovementDirection.Positive;
    [SerializeField] private float _movementSpeed = 20f;

    [Space]
    [SerializeField] private float _otherSticksCheckDistance = 5f;
    [SerializeField] private float _raycastOffset = 0.15f;

    private Vector2 _velocity;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnMouseDown()
    {
        StartMovement();
    }


    private void StartMovement()
    {
        if (CanMove())
            Debug.Log("Can");
        else
            Debug.Log("can't");
    }

    private bool CanMove()
    {
        switch (_type)
        {
            case EStickType.None:
                return false;

            case EStickType.Horizontal:
                return Raycast(Vector2.right, new Vector2(_collider.size.x + _raycastOffset, 0f));

            case EStickType.Vertical:
                return Raycast(Vector2.up, new Vector2(0f, _collider.size.y + _raycastOffset));

            default:
                break;
        }

        return false;
    }

    private bool Raycast(Vector2 positiveDirection, Vector2 colliderOffset)
    {
        var origin = (Vector2)transform.position + colliderOffset;                   // Где-то на этих строчках и есть весь фарш, который я не успеваю сделать,
        var direction = positiveDirection * (int)_movementDirection;                 // я пытался посылать рейкаст с конца коллайдера, чтоб луч не
        var hit = Physics2D.Raycast(origin, direction, _otherSticksCheckDistance);   // касался объекта-отправителя. Но чет мозги у меня совсем не работают

        Debug.DrawRay(origin, direction * _otherSticksCheckDistance, Color.red, 1f);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<NewStick>(out var stick))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
            
    }
}