using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private IPlayerState currentState;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float switchTableDistance = 1.5f;

    [Header("Pouring Settings")]
    public float fillRate = 0.3f;

    [Header("References")]
    public Transform[] tables;
    private int currentTableIndex = 0;

    private float fillLevel = 0;

    private void Start()
    {
        SetState(new PlayerMoveState());
    }

    // Ensure Update calls currentState.UpdateState(this)
    private void Update()
    {
        currentState?.UpdateState(this);
    }

    public void SetState(IPlayerState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    #region Input-Driven Actions

    public void MoveLeft()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    public void MoveRight()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    public void SwitchTable(int direction)
    {
        // Update the current table index with wrap-around logic
        currentTableIndex += direction;

        if (currentTableIndex < 0)
        {
            currentTableIndex = tables.Length - 1; // Wrap to the last table
        }
        else if (currentTableIndex >= tables.Length)
        {
            currentTableIndex = 0; // Wrap to the first table
        }

        // Update the player's position to the new table's position while keeping x and z coordinates
        Vector3 newPosition = new Vector3(transform.position.x, tables[currentTableIndex].position.y, transform.position.z);
        transform.position = newPosition;
    }



    #endregion

    #region Pouring and Throwing Mechanics

    public void StartPouring()
    {
        fillLevel = 0;
    }

    public bool PourBeer()
    {
        fillLevel += Time.deltaTime * fillRate;
        fillLevel = Mathf.Clamp(fillLevel, 0, 1);
        UpdateFillUI(fillLevel);

        return fillLevel >= 1;
    }

    public void ThrowGlass()
    {
        string quality = DetermineQuality(fillLevel);
        Debug.Log($"Glass thrown with fill level {fillLevel} ({quality}) quality.");
    }

    private string DetermineQuality(float fill)
    {
        if (fill >= 0.65f && fill <= 0.75f) return "Perfect";
        if ((fill >= 0.55f && fill < 0.65f) || (fill > 0.75f && fill <= 0.85f)) return "Good";
        return "Bad";
    }

    private void UpdateFillUI(float fillLevel)
    {
        Debug.Log($"Fill Level: {fillLevel}");
    }

    #endregion
}
