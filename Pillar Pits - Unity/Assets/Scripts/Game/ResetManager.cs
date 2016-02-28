using UnityEngine;
using System.Collections;

public class ResetManager : MonoBehaviour {

    public delegate void ResetDelegate();
    public static ResetDelegate ResetLevel;
}
