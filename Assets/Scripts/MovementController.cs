using UnityEngine;

public class MovementController : MonoBehaviour
{
    public static MovementController Instance;

    private const float SENSIVITY = 0.04f;
    [SerializeField] private float _speed;
   
    [SerializeField] private CameraMovement _cameraMovement;
    private float _mouseStart;
    private float _mouseCurrent => Input.mousePosition.x;
    private float _mouseDelta;

    private Vector3 _direction;

    private bool isMove = false;

    private void Awake()
    {
        Instance = this;
        enabled = false;
    }

    public void Enable()
    {
        enabled = true;
    }

    private void Update()
    {
        
        if (isMove)
        {
            AlliesGroup.Instance.Run();
            if (Input.GetMouseButtonDown(0))
            {
                _mouseStart = Input.mousePosition.x;
            }

            if (Input.GetMouseButton(0))
            {
                _mouseDelta = _mouseCurrent - _mouseStart;
                if (_mouseDelta > 25f)
                {
                    _mouseDelta = 25f;
                }
                if (_mouseDelta < -25f)
                {
                    _mouseDelta = -25f;
                }
                _mouseStart = Input.mousePosition.x;
                _direction = _mouseDelta > 0 ? Vector3.right * _mouseDelta : Vector3.left * Mathf.Abs(_mouseDelta);
                
                if (transform.position.x + _direction.x * _speed * Time.smoothDeltaTime * SENSIVITY < AlliesGroup.Instance.GetBound(BoundBorder.Right))
                {
                    transform.position += _direction * _speed * Time.smoothDeltaTime * SENSIVITY;
                }
                else
                {
                    transform.position = new Vector3(AlliesGroup.Instance.GetBound(BoundBorder.Right), transform.position.y, transform.position.z);
                }

                if (transform.position.x + _direction.x * _speed * Time.smoothDeltaTime * SENSIVITY > AlliesGroup.Instance.GetBound(BoundBorder.Left))
                {
                    transform.position += _direction * _speed * Time.smoothDeltaTime * SENSIVITY;
                }
                else
                {
                    transform.position = new Vector3(AlliesGroup.Instance.GetBound(BoundBorder.Left), transform.position.y, transform.position.z);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _direction = Vector3.zero;
            }

            Vector3 moveVector = transform.forward + _direction * SENSIVITY;
            transform.position += Vector3.forward * _speed * Time.smoothDeltaTime;
        }else
        {
            
        }
    }

    public void SetControllerState(bool _bool)
    {
        isMove = _bool;
        _cameraMovement.SetState(!_bool);
    }

    public void ChangeControllerState()
    {
        print("ChangeController");
        isMove = isMove ? false : true;
        _cameraMovement.ChangeState();
    }
}
