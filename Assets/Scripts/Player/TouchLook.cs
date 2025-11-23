using UnityEngine;
using UnityEngine.EventSystems;

public class TouchLook : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Transform cameraRig;         // Pivot contenant la caméra
    public float sensitivity = 0.2f;
    public float minPitch = -60f;       // Limite vers le bas
    public float maxPitch = 60f;        // Limite vers le haut

    private bool isDragging = false;
    private float pitch = 0f;           // Rotation verticale cumulée

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging || cameraRig == null) return;

        float deltaX = eventData.delta.x;
        float deltaY = eventData.delta.y;

        // Rotation horizontale (autour de Y)
        cameraRig.Rotate(Vector3.up, deltaX * sensitivity, Space.World);

        // Rotation verticale (autour de X) avec clamp
        pitch -= deltaY * sensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Vector3 currentEuler = cameraRig.localEulerAngles;
        currentEuler.x = pitch;
        cameraRig.localEulerAngles = currentEuler;
    }
}