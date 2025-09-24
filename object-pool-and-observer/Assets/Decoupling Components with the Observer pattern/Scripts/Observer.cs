using UnityEngine;

//try to get stuff from school machine

namespace Chapter.Observer
{
    public abstract class Observer : MonoBehaviour
    {
        public abstract void Notify(Subject subject);
    }
}