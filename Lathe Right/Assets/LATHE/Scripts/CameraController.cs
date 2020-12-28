using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    
    protected Transform _XForm_Camera;
    protected Transform _XForm_Parent;

    protected Vector3 _LocalRotation; // Stores rotation of camera pivot every frame, the final target rotation
    protected float _CameraDistance = 10f; 
    protected Vector3 _PivotMove; // Stores translational movement of pivot in x-y plane every frame

    public float MouseSensitivity = 4f;
    public float ScrollSensitvity = 4f;
    public float OrbitDampening = 25f;
    public float ScrollDampening = 6f;

    void Start() {
        this._XForm_Camera = this.transform;
        this._XForm_Parent = this.transform.parent;
    }

    void LateUpdate() {

        if (!EventSystem.current.IsPointerOverGameObject())
        {

            if (Input.GetMouseButton(0) ) // Translational movement of camera pivot in local XY plane
            {
                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) //Only triggers when mouse is not stationary
                {
                    _PivotMove = Vector3.zero;
                    _PivotMove.x += (-Input.GetAxis("Mouse X"));
                    _PivotMove.y += (-Input.GetAxis("Mouse Y"));
                    MovePivotPoint(_PivotMove);
                }
            }
            
            // Rotate camera relative to cameraPivot
            if (Input.GetMouseButton(1) )
            {
                //Rotation of the Camera based on Mouse Coordinates
                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) //Only triggers when mouse is not stationary
                {
                    _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                    _LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity; // Prevent inverted controls

                    //Clamp the y Rotation to horizon and not flipping over at the top
                    _LocalRotation.y = Mathf.Clamp(_LocalRotation.y, 0f, 90f);
                }
            }

            //Zooming Input from Mouse Scroll Wheel
            if ((Input.GetAxis("Mouse ScrollWheel") != 0f) && Input.GetKey(KeyCode.LeftShift))
            {
                float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;
                ScrollAmount *= (this._CameraDistance * 0.3f);
                this._CameraDistance += ScrollAmount * -1f;

                //This makes camera go no closer than 1.5 meters from target, and no further than 10 meters.
                this._CameraDistance = Mathf.Clamp(this._CameraDistance, 1.5f, 10f);
            }
            
            //
            //Actual Camera Rig Transformations

            Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
            this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);

            if ( this._XForm_Camera.localPosition.z != this._CameraDistance * -1f )
            {
                this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ScrollDampening));
            }
        }
        
    }

    private void MovePivotPoint(Vector3 movement){
        this._XForm_Parent.transform.Translate(_PivotMove * MouseSensitivity * 3 * Time.deltaTime, Space.Self);
        this._XForm_Parent.transform.position = new Vector3(
            Mathf.Clamp(this._XForm_Parent.transform.position.x, -15, 15),
            Mathf.Clamp(this._XForm_Parent.transform.position.y, 2, 15),
            Mathf.Clamp(this._XForm_Parent.transform.position.z, -8, 8)
        );
    }

}
