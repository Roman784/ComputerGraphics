using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float sensitivity;

    private float xRotation = 0f; // Угол поворота по горизонтали
    private float yRotation = 0f; // Угол поворота по вертикали

    public Vector3 initialRotation;

    private void Awake()
    {
        initialRotation = transform.localEulerAngles;   
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            transform.position = transform.position + Vector3.up * _moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.LeftShift))
            transform.position = transform.position + Vector3.down * _moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
            transform.position = transform.position - transform.right * _moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.position = transform.position + transform.right * _moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
            transform.position = transform.position + transform.forward * _moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            transform.position = transform.position - transform.forward * _moveSpeed * Time.deltaTime;

        // Получаем ввод с клавиатуры
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            yRotation -= sensitivity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            yRotation += sensitivity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            xRotation -= sensitivity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            xRotation += sensitivity * Time.deltaTime;
        }

        // Ограничиваем вертикальный поворот
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Поворачиваем камеру
        transform.localRotation = Quaternion.Euler(xRotation + initialRotation.x,
                                                  yRotation + initialRotation.y,
                                                  initialRotation.z);
    }
}
