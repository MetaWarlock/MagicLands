using UnityEngine;
using UnityEngine.InputSystem;

public class LSPlayer : MonoBehaviour
{
    public MapPoint currentPoint;

    public float moveSpeed;

    private float moveInput;
    private float moveUpDown;

    private bool levelLoading;

    public LSManager theManager;


    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentPoint.transform.position) < 0.01f && !levelLoading)
        {
            if (moveUpDown < 0.5f && moveUpDown > -0.5f)
            {            
                if (moveInput > 0.5f) if (currentPoint.right != null) SetNextPoint(currentPoint.right);
                if (moveInput < -0.5f) if (currentPoint.left != null) SetNextPoint(currentPoint.left);
            }

            if (moveInput < 0.5f && moveInput > -0.5f)
            {
                if (moveUpDown > 0.5f) if (currentPoint.up != null) SetNextPoint(currentPoint.up);
                if (moveUpDown < -0.5f) if (currentPoint.down != null) SetNextPoint(currentPoint.down);
            }
        }

        if (currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked)
        {
            LSUIController.instance.ShowInfo(currentPoint);
        }
    }

    public void SetNextPoint(MapPoint nextPoint)
    {
        currentPoint = nextPoint;
        LSUIController.instance.HideInfo();

        AudioManager.instance.PlaySFX(5);
    }

    public void SetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>();

    }
    public void SetMoveUpDown(InputAction.CallbackContext context)
    {
        moveUpDown = context.ReadValue<float>();

    }



    public void Jump(InputAction.CallbackContext context)
    {
        if (currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked)
        {
            levelLoading = true;
            theManager.LoadLevel();
        }
    }
   
    }
