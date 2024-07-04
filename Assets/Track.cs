using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class Track : MonoBehaviour
{
    public ARTrackedImageManager manager;
    public List<GameObject> list1 = new List<GameObject>();
    public List<AudioClip> list2 = new List<AudioClip>();

    private Dictionary<string, GameObject> dict1 = new Dictionary<string, GameObject>();
    private Dictionary<string, AudioClip> dict2 = new Dictionary<string, AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject o in list1)
        {
            dict1.Add(o.name, o);
        }

        foreach(AudioClip o in list2)
        {
            dict2.Add(o.name, o);
        }
    }

    void UpdateImage(ARTrackedImage t)
    {
        string name = t.referenceImage.name;

        GameObject o = dict1[name];

        o.transform.position = t.transform.position;
        o.transform.rotation = t.transform.rotation;
        o.SetActive(true);
    }

    void UpdateSound(ARTrackedImage t)
    {
        string name = t.referenceImage.name;

        AudioClip sound = dict2[name];
        GetComponent<AudioSource>().PlayOneShot(sound);
    }

    private void OnChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach(ARTrackedImage t in args.added)
        {
            UpdateImage(t);
            UpdateSound(t);
        }

        foreach(ARTrackedImage t in args.updated)
        {
            UpdateImage(t);
        }
    }

    private void OnEnable()
    {
        manager.trackedImagesChanged += OnChanged;
    }

    void OnDisable()
    {
        manager.trackedImagesChanged -= OnChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
