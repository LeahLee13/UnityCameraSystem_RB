using UnityEngine;
using Invector.vCharacterController;

public class CMVcamManager : MonoSingleton<CMVcamManager>
{
    #region �ֶ�
    #region ��ɫ
    [HideInInspector]
    public PlayerController_RB playerController;
    [HideInInspector]
    public PlayerInputController_RB inputController;
    #endregion

    #region �����
    [Header("������ϵĸ���Ŀ��")]
    [HideInInspector]
    public Transform followTarget_1st;
    [HideInInspector]
    public Transform followTarget_3rd;

    [Header("�������")]
    public CMVcam_1st cmvCam_1st;
    public CMVcam_3rd cmvCam_3rd;
    public CMVcam_Free cmvCam_Free;
    public CMVcam_Quarter CMVcam_Quarter;
    public CMVcam_OverLook CMVcam_OverLook;

    [Header("�л��ӽ�")]
    public KeyCode is1st = KeyCode.V;
    public bool is1st_ = false;

    public KeyCode is3rd = KeyCode.B;
    public bool is3rd_ = false;

    public KeyCode isQuarter = KeyCode.N;
    public bool isQuarter_ = false;

    public KeyCode isOverLook = KeyCode.M;
    public bool isOverLook_ = false;
    #endregion

    #region ���
    [Header("�����ʾ/����")]
    public KeyCode isCursorLocked = KeyCode.Alpha0;
    public bool isCursorLocked_ = false;
    #endregion
    #endregion

    #region �������ں���
    void Start()
    {
        is1st_ = false;
        is3rd_ = false;
        isQuarter_ = false;
        isOverLook_ = false;

        cmvCam_Free.SetEnable(true);
        cmvCam_1st.SetEnable(false);
        cmvCam_3rd.SetEnable(false);
        CMVcam_Quarter.SetEnable(false);
        CMVcam_OverLook.SetEnable(false);
    }

    void Update()
    {
        if (playerController == null)
        {
            GameObject playerObj = GameManager.Instance.playerObj;

            if (playerObj == null)
            {
                return;
            }
            else
            {
                playerController = playerObj.GetComponent<PlayerController_RB>();
                inputController = playerObj.GetComponent<PlayerInputController_RB>();
            }
        }

        if (playerController != null)
        {
            if (followTarget_1st == null)
            {
                followTarget_1st = playerController.followTarget_1st;
            }

            if (followTarget_3rd == null)
            {
                followTarget_3rd = playerController.followTarget_3rd;
            }
        }

        Input();
    }
    #endregion

    #region ����
    private void Input()
    {
        //����0���л��Ƿ���ʾ���
        if (UnityEngine.Input.GetKeyDown(isCursorLocked))
        {
            isCursorLocked_ = !isCursorLocked_;
            SetCursorState(isCursorLocked_);
        }

        #region �л��ӽ�
        //��һ�˳��ӽ�/�����ӽ��л�
        if (UnityEngine.Input.GetKeyDown(is1st))
        {
            is1st_ = !is1st_;
            is3rd_ = false;
            isQuarter_ = false;
            isOverLook_ = false;

            cmvCam_Free.SetEnable(!is1st_);
            cmvCam_1st.SetEnable(is1st_);
            cmvCam_3rd.SetEnable(is3rd_);
            CMVcam_Quarter.SetEnable(isQuarter_);
            CMVcam_OverLook.SetEnable(isOverLook_);

            playerController.mesh.SetActive(!is1st_);
            playerController.mesh_CutOut.SetActive(is1st_);

            playerController.isStrafing = is1st_;
            playerController.canRotate = false;
        }

        //�����˳ƹ����ӽ�/�����ӽ��л�
        if (UnityEngine.Input.GetKeyDown(is3rd))
        {
            is3rd_ = !is3rd_;
            is1st_ = false;
            isQuarter_ = false;
            isOverLook_ = false;

            cmvCam_Free.SetEnable(!is3rd_);
            cmvCam_1st.SetEnable(is1st_);
            cmvCam_3rd.SetEnable(is3rd_);
            CMVcam_Quarter.SetEnable(isQuarter_);
            CMVcam_OverLook.SetEnable(isOverLook_);

            playerController.mesh.SetActive(true);
            playerController.mesh_CutOut.SetActive(false);

            playerController.isStrafing = is3rd_;
            playerController.canRotate = is3rd_;
        }

        //б45���ӽ�/�����ӽ��л�
        if (UnityEngine.Input.GetKeyDown(isQuarter))
        {
            isQuarter_ = !isQuarter_;
            is1st_ = false;
            is3rd_ = false;
            isOverLook_ = false;

            cmvCam_Free.SetEnable(!isQuarter_);
            cmvCam_1st.SetEnable(is1st_);
            cmvCam_3rd.SetEnable(is3rd_);
            CMVcam_Quarter.SetEnable(isQuarter_);
            CMVcam_OverLook.SetEnable(isOverLook_);

            playerController.mesh.SetActive(true);
            playerController.mesh_CutOut.SetActive(false);

            playerController.isStrafing = false;
            playerController.canRotate = false;
        }

        //���ӽ�/�����ӽ��л�
        if (UnityEngine.Input.GetKeyDown(isOverLook))
        {
            isOverLook_ = !isOverLook_;
            is1st_ = false;
            is3rd_ = false;
            isQuarter_ = false;

            cmvCam_Free.SetEnable(!isOverLook_);
            cmvCam_1st.SetEnable(is1st_);
            cmvCam_3rd.SetEnable(is3rd_);
            CMVcam_Quarter.SetEnable(isQuarter_);
            CMVcam_OverLook.SetEnable(isOverLook_);

            playerController.mesh.SetActive(true);
            playerController.mesh_CutOut.SetActive(false);

            playerController.isStrafing = false;
            playerController.canRotate = false;
        }
        #endregion
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
    #endregion
}
