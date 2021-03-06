using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ZigMenu : MonoBehaviour
{

    public GameObject[] menuCanvas;

    private static float TIME_TO_SELECT = 1.5F;
    public Button[] buttons;

    private int selected = -1;
    private float selecttimer = TIME_TO_SELECT;
    private float selectedtimercomparator = -1;

    private bool prepareLeftSwipe = false;

    /*
    */
    public Transform Head;
    public Transform Neck;
    public Transform Torso;
    public Transform Waist;

    public Transform LeftCollar;
    public Transform LeftShoulder;
    public Transform LeftElbow;
    public Transform LeftWrist;
    public Transform LeftHand;
    public Transform LeftFingertip;

    public Transform RightCollar;
    public Transform RightShoulder;
    public Transform RightElbow;
    public Transform RightWrist;
    public Transform RightHand;
    public Transform RightFingertip;

    public Transform LeftHip;
    public Transform LeftKnee;
    public Transform LeftAnkle;
    public Transform LeftFoot;

    public Transform RightHip;
    public Transform RightKnee;
    public Transform RightAnkle;
    public Transform RightFoot;
    public bool mirror = false;
    public bool UpdateJointPositions = false;
    public bool UpdateRootPosition = false;
    public bool UpdateOrientation = true;
    public bool RotateToPsiPose = false;
    public float RotationDamping = 30.0f;
    public float Damping = 30.0f;
    public Vector3 Scale = new Vector3(0.001f, 0.001f, 0.001f);

    public Vector3 PositionBias = Vector3.zero;

    private Transform[] transforms;
    private Quaternion[] initialRotations;
    private Vector3 rootPosition;


    ZigJointId mirrorJoint(ZigJointId joint)
    {
        switch (joint)
        {
            case ZigJointId.LeftCollar:
                return ZigJointId.RightCollar;
            case ZigJointId.LeftShoulder:
                return ZigJointId.RightShoulder;
            case ZigJointId.LeftElbow:
                return ZigJointId.RightElbow;
            case ZigJointId.LeftWrist:
                return ZigJointId.RightWrist;
            case ZigJointId.LeftHand:
                return ZigJointId.RightHand;
            case ZigJointId.LeftFingertip:
                return ZigJointId.RightFingertip;
            case ZigJointId.LeftHip:
                return ZigJointId.RightHip;
            case ZigJointId.LeftKnee:
                return ZigJointId.RightKnee;
            case ZigJointId.LeftAnkle:
                return ZigJointId.RightAnkle;
            case ZigJointId.LeftFoot:
                return ZigJointId.RightFoot;

            case ZigJointId.RightCollar:
                return ZigJointId.LeftCollar;
            case ZigJointId.RightShoulder:
                return ZigJointId.LeftShoulder;
            case ZigJointId.RightElbow:
                return ZigJointId.LeftElbow;
            case ZigJointId.RightWrist:
                return ZigJointId.LeftWrist;
            case ZigJointId.RightHand:
                return ZigJointId.LeftHand;
            case ZigJointId.RightFingertip:
                return ZigJointId.LeftFingertip;
            case ZigJointId.RightHip:
                return ZigJointId.LeftHip;
            case ZigJointId.RightKnee:
                return ZigJointId.LeftKnee;
            case ZigJointId.RightAnkle:
                return ZigJointId.LeftAnkle;
            case ZigJointId.RightFoot:
                return ZigJointId.LeftFoot;


            default:
                return joint;
        }
    }


    public void Awake()
    {
        int jointCount = Enum.GetNames(typeof(ZigJointId)).Length;

        transforms = new Transform[jointCount];
        initialRotations = new Quaternion[jointCount];

        transforms[(int)ZigJointId.Head] = Head;
        transforms[(int)ZigJointId.Neck] = Neck;
        transforms[(int)ZigJointId.Torso] = Torso;
        transforms[(int)ZigJointId.Waist] = Waist;
        transforms[(int)ZigJointId.LeftCollar] = LeftCollar;
        transforms[(int)ZigJointId.LeftShoulder] = LeftShoulder;
        transforms[(int)ZigJointId.LeftElbow] = LeftElbow;
        transforms[(int)ZigJointId.LeftWrist] = LeftWrist;
        transforms[(int)ZigJointId.LeftHand] = LeftHand;
        transforms[(int)ZigJointId.LeftFingertip] = LeftFingertip;
        transforms[(int)ZigJointId.RightCollar] = RightCollar;
        transforms[(int)ZigJointId.RightShoulder] = RightShoulder;
        transforms[(int)ZigJointId.RightElbow] = RightElbow;
        transforms[(int)ZigJointId.RightWrist] = RightWrist;
        transforms[(int)ZigJointId.RightHand] = RightHand;
        transforms[(int)ZigJointId.RightFingertip] = RightFingertip;
        transforms[(int)ZigJointId.LeftHip] = LeftHip;
        transforms[(int)ZigJointId.LeftKnee] = LeftKnee;
        transforms[(int)ZigJointId.LeftAnkle] = LeftAnkle;
        transforms[(int)ZigJointId.LeftFoot] = LeftFoot;
        transforms[(int)ZigJointId.RightHip] = RightHip;
        transforms[(int)ZigJointId.RightKnee] = RightKnee;
        transforms[(int)ZigJointId.RightAnkle] = RightAnkle;
        transforms[(int)ZigJointId.RightFoot] = RightFoot;



        // save all initial rotations
        // NOTE: Assumes skeleton model is in "T" pose since all rotations are relative to that pose
        foreach (ZigJointId j in Enum.GetValues(typeof(ZigJointId)))
        {
            if (transforms[(int)j])
            {
                // we will store the relative rotation of each joint from the gameobject rotation
                // we need this since we will be setting the joint's rotation (not localRotation) but we 
                // still want the rotations to be relative to our game object
                initialRotations[(int)j] = Quaternion.Inverse(transform.rotation) * transforms[(int)j].rotation;
            }
        }
    }

    void Start()
    {
        // start out in calibration pose
        if (RotateToPsiPose)
        {
            RotateToCalibrationPose();
        }
    }

    void UpdateRoot(Vector3 skelRoot)
    {
        // +Z is backwards in OpenNI coordinates, so reverse it
        rootPosition = Vector3.Scale(new Vector3(skelRoot.x, skelRoot.y, skelRoot.z), doMirror(Scale)) + PositionBias;
        if (UpdateRootPosition)
        {
            transform.localPosition = (transform.rotation * rootPosition);
        }
    }

    void UpdateRotation(ZigJointId joint, Quaternion orientation)
    {
        joint = mirror ? mirrorJoint(joint) : joint;
        // make sure something is hooked up to this joint
        if (!transforms[(int)joint])
        {
            return;
        }

        if (UpdateOrientation)
        {
            Quaternion newRotation = transform.rotation * orientation * initialRotations[(int)joint];
            if (mirror)
            {
                newRotation.y = -newRotation.y;
                newRotation.z = -newRotation.z;
            }
           // transforms[(int)joint].rotation = Quaternion.Slerp(transforms[(int)joint].rotation, newRotation, Time.deltaTime * RotationDamping);

            if (menuCanvas[0].active)
                trackGestures();
            else
                swipeGesture();
        }
    }
    Vector3 doMirror(Vector3 vec)
    {
        return new Vector3(mirror ? -vec.x : vec.x, vec.y, vec.z);
    }
    void UpdatePosition(ZigJointId joint, Vector3 position)
    {
        joint = mirror ? mirrorJoint(joint) : joint;
        // make sure something is hooked up to this joint
        if (!transforms[(int)joint])
        {
            return;
        }

        if (UpdateJointPositions)
        {
            Vector3 dest = Vector3.Scale(position, doMirror(Scale)) - rootPosition;
            transforms[(int)joint].localPosition = Vector3.Lerp(transforms[(int)joint].localPosition, dest, Time.deltaTime * Damping);
        }
    }

    public void RotateToCalibrationPose()
    {
        foreach (ZigJointId j in Enum.GetValues(typeof(ZigJointId)))
        {
            if (null != transforms[(int)j])
            {
                transforms[(int)j].rotation = transform.rotation * initialRotations[(int)j];
            }
        }

        // calibration pose is skeleton base pose ("T") with both elbows bent in 90 degrees
        if (null != RightElbow)
        {
            RightElbow.rotation = transform.rotation * Quaternion.Euler(0, -90, 90) * initialRotations[(int)ZigJointId.RightElbow];
        }
        if (null != LeftElbow)
        {
            LeftElbow.rotation = transform.rotation * Quaternion.Euler(0, 90, -90) * initialRotations[(int)ZigJointId.LeftElbow];
        }
    }

    public void SetRootPositionBias()
    {
        this.PositionBias = -rootPosition;
    }

    public void SetRootPositionBias(Vector3 bias)
    {
        this.PositionBias = bias;
    }

    void Zig_UpdateUser(ZigTrackedUser user)
    {
        UpdateRoot(user.Position);
        if (user.SkeletonTracked)
        {
            foreach (ZigInputJoint joint in user.Skeleton)
            {
                if (joint.GoodPosition) UpdatePosition(joint.Id, joint.Position);
                if (joint.GoodRotation) UpdateRotation(joint.Id, joint.Rotation);
            }
        }
    }


    private void trackGestures()
    {
       // print("Right hand y position: " + transforms[(int)ZigJointId.RightHand].transform.localPosition.y);
        float ypos = transforms[(int)ZigJointId.RightHand].transform.localPosition.y;
        
        if (ypos < 0.4 && ypos > 0.3)
        {
            selected = 0;
            buttons[selected].Select();
            //print("selected: " + selected + " old selected: " + selectedtimercomparator);

            if (selected != selectedtimercomparator)
            {
                //print("fire new timer");
                selectedtimercomparator = selected;
                selecttimer = TIME_TO_SELECT;
                InvokeRepeating("checker", 0.1f, 0.1f);
            }
        } else if (ypos < 0.15 && ypos > 0.05)
        {
            selected = 1;
            buttons[selected].Select();
            //print("selected: " + selected + " old selected: " + selectedtimercomparator);

            if (selected != selectedtimercomparator)
            {
                //print("fire new timer");
                selectedtimercomparator = selected;
                selecttimer = TIME_TO_SELECT;
                InvokeRepeating("checker", 0.1f, 0.1f);
            }
        }
        else if (ypos < -0.1 && ypos > -0.2)
        {
            selected = 2;
            buttons[selected].Select();
            //print("selected: " + selected + " old selected: " + selectedtimercomparator);

            if (selected != selectedtimercomparator)
            {
                print("fire new timer");
                selectedtimercomparator = selected;
                selecttimer = TIME_TO_SELECT;
                InvokeRepeating("checker", 0.1f, 0.1f);
            }
        }
        else if (ypos < -0.35 && ypos > -0.45)
        {
            selected = 3;
            buttons[selected].Select();
            //print("selected: " + selected + " old selected: " + selectedtimercomparator);

            if (selected != selectedtimercomparator)
            {
                print("fire new timer");
                selectedtimercomparator = selected;
                selecttimer = TIME_TO_SELECT;
                InvokeRepeating("checker", 0.1f, 0.1f);
            }
        }

        else
        {
            GameObject myEventSystem = GameObject.Find("EventSystem");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            selected = -1;
            selectedtimercomparator = -1;
            CancelInvoke("checker");
            selecttimer = TIME_TO_SELECT;
          //  print("timer canceled");
        }

    }

    private void checker()
    {
        print("timer: " + selecttimer + "selected: " + selected + " old selected: " + selectedtimercomparator);
        if (selected == selectedtimercomparator)
        {
            selecttimer -= 0.1f;

            if (selecttimer <= 0)
            {
                // fire selected button
                print("fire selected button");

                switch (selected)
                {
                    case 0:
                        fireNewGame();
                        break;
                    case 1:
                        _openOptionsTab();
                        break;
                    case 2:
                        _openScoresTab();
                        break;
                    case 3:
                        fireExit();
                        break;
                }

                // restarting the timer
                

                CancelInvoke("checker");
                selecttimer = TIME_TO_SELECT;
            }
        }
    }

    /*
    button on click events
    */

    private void fireNewGame()
    {
        SceneManager.LoadScene("FlappyBird", LoadSceneMode.Single);
    }

    private void fireExit()
    {
        print("exit");
        Application.Quit();
    }

    /*
    menu switch
    */

    private void changeMenu(int id)
    {
        for (int i = 0; i < menuCanvas.Length; i++)
        {
            menuCanvas[i].SetActive(false);
        }

        menuCanvas[id].SetActive(true);
    }

    public void _openMainTab()
    {
        changeMenu(0);
    }

    private void _openScoresTab()
    {
        changeMenu(1);
    }

    private void _openOptionsTab()
    {
        changeMenu(2);
    }


    /*
    Track for a left hand swipe
    */

    private void swipeGesture()
    {
        float xpos = transforms[(int)ZigJointId.LeftHand].transform.localPosition.x;

        //print("Left hand x pos" + xpos);

        if (xpos < -0.2 && !prepareLeftSwipe)
        {
            prepareLeftSwipe = true;

        } else if ( xpos > 0.1 && prepareLeftSwipe)
        {
            print("fire back event");
            _openMainTab();
            prepareLeftSwipe = false;
        }

    }


}
