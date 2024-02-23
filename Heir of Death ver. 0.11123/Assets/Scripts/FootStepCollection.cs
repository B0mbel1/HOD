using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Footsteps Collections", menuName = "Create Footstep Collection")]

public class FootStepCollection : ScriptableObject
{
    public List<AudioClip> footstepSound = new List<AudioClip>();
    public AudioClip jumpSound;
    public AudioClip landSound;
}
