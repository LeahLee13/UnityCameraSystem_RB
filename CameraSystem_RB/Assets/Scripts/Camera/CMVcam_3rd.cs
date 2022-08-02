using Cinemachine;
using UnityEngine;

public class CMVcam_3rd : MonoBehaviour
{
    #region �ֶ�
    private CMVcamManager cmvCamManager;

    [Header("Cinemachine")]
    private CinemachineVirtualCamera virtualCamera;

    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;

    [Header("Cinemachine_1st")]
    [Tooltip("����������ƶ����Ƕ�")]
    public float topClamp_3rd = 80.0f;
    [Tooltip("����������ƶ����Ƕ�")]
    public float bottomClamp_3rd = -40.0f;
    [Tooltip("��ת�ٶ�")]
    public float rotationSpeed = 1.0f;
    [Tooltip("����Ķ�������������ͷ������ʱ����΢�����λ��")]
    public float cameraAngleOverride = 0.0f;

    #region ���
    private float mouseY;
    #endregion

    #region ��ת
    [Header("Controller Input")]
    public bool isMoveCamera_ = false;

    public KeyCode isMouseBtnControlCamera = KeyCode.RightControl;
    public bool isMouseBtnControlCamera_ = true;

    public bool IsMouseBtnControlCamera
    {
        get
        {
            if (isMouseBtnControlCamera_)
            {
                if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
    #endregion
    #endregion

    #region �������ں���
    void Awake()
    {
        cmvCamManager = CMVcamManager.Instance;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void LateUpdate()
    {
        if (cmvCamManager.followTarget_3rd == null)
        {
            if (this.virtualCamera.Follow == null)
            {
                this.virtualCamera.Follow = null;
            }

            if (this.virtualCamera.LookAt == null)
            {
                this.virtualCamera.LookAt = null;
            }

            return;
        }

        if (this.virtualCamera.Follow == null)
        {
            this.virtualCamera.Follow = cmvCamManager.followTarget_3rd;
        }

        if (this.virtualCamera.LookAt == null)
        {
            this.virtualCamera.LookAt = cmvCamManager.followTarget_3rd;
        }

        mouseY = Input.GetAxis("Mouse Y");

        SetInput();
        RotationCamera();
    }
    #endregion

    #region ����
    #region ����
    /// <summary>
    /// �Ƿ񼤻�
    /// </summary>
    /// <param name="isEnabled">�Ƿ񼤻�</param>
    public void SetEnable(bool isEnabled = false)
    {
        virtualCamera.enabled = isEnabled;
        enabled = isEnabled;
    }
    #endregion

    #region �����л�
    private void SetInput()
    {
        //�����Ҳ�Ctrl���л�����������/����ƶ���ת
        if (Input.GetKeyDown(isMouseBtnControlCamera))
        {
            isMouseBtnControlCamera_ = !isMouseBtnControlCamera_;
        }
    }
    #endregion

    #region ��ת
    /// <summary>
    /// ��ת�����
    /// </summary>
    private void RotationCamera()
    {
        Vector2 look = new Vector2(0, -mouseY * rotationSpeed);

        if (IsMouseBtnControlCamera)
        {
            if (look.sqrMagnitude >= _threshold)
            {
                _cinemachineTargetPitch += look.y;
                _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, bottomClamp_3rd, topClamp_3rd);
                cmvCamManager.followTarget_3rd.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0, 0);
            }
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) { angle += 360f; }
        if (angle > 360f) { angle -= 360f; }
        return Mathf.Clamp(angle, min, max);
    }
    #endregion
    #endregion
}
