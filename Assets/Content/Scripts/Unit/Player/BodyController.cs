using UnityEngine;

namespace Player
{
    public class BodyController
    {
        private readonly Rigidbody2D _rb;
        private readonly Vector2 _worldSize;

        private readonly float _movementSpeed;
        private readonly float _rotationSpeed;
        
        private Vector2 UpVector => _rb.transform.up;

        public BodyController(Rigidbody2D rigidbody2D, PlayerSettings playerSettings, Vector2 worldSize)
        {
            _rb = rigidbody2D;
            _worldSize = worldSize;

            _movementSpeed = playerSettings.MovementSpeed;
            _rotationSpeed = playerSettings.RotationSpeed;
        }

        public void ManualFixedUpdate(float fixedDeltaTime)
        {
            var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");

            var nextPosition = _rb.position + UpVector * (_movementSpeed * vertical * fixedDeltaTime);
            nextPosition.x = Mathf.Clamp(nextPosition.x, -_worldSize.x, _worldSize.x);
            nextPosition.y = Mathf.Clamp(nextPosition.y, -_worldSize.y, _worldSize.y);
            
            _rb.MovePosition(nextPosition);
            _rb.MoveRotation(_rb.rotation - horizontal * _rotationSpeed * fixedDeltaTime);
        }
    }
}
