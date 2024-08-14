using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] private float _spacing;
    [SerializeField] private float _speed;
    [SerializeField] private ETypeStick _typeStick;

    private float _currentSpeed;

    private void Move()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down);

        if(_typeStick == ETypeStick.Horizontal)
        {
            if (hitRight && hitRight.transform.TryGetComponent<Barriers>(out var barriers1)) _currentSpeed = _speed; 
            else if (hitLeft && hitLeft.transform.TryGetComponent<Barriers>(out var barriers2)) _currentSpeed = -_speed;
            else if (hitRight) _currentSpeed = _speed;
            else if (hitLeft) _currentSpeed = -_speed;
        }
        else if(_typeStick == ETypeStick.Vertical)
        {
            if (hitUp && hitUp.transform.TryGetComponent<Barriers>(out var barriers1)) _currentSpeed = _speed;
            else if (hitDown && hitDown.transform.TryGetComponent<Barriers>(out var barriers2)) _currentSpeed = -_speed;
            else if (hitUp) _currentSpeed = _speed;
            else if (hitDown) _currentSpeed = -_speed;
        }
    }

    private void Update()
    {
        if (_typeStick == ETypeStick.Horizontal)
        {
            transform.position = new Vector2(transform.position.x + _currentSpeed * Time.deltaTime, transform.position.y);
        }
        else if (_typeStick == ETypeStick.Vertical)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + _currentSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _currentSpeed = 0;
    }


    private void OnMouseDown()
    {
        Move();
    }
}
