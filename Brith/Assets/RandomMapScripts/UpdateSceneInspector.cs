using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace V
{
    public class UpdateSceneInspector : MonoBehaviour
    {
        void Start()
        {
            Save();
        }
        public static void Save()
        {
            var scene = SceneManager.GetActiveScene();
            EditorSceneManager.SaveScene(scene);
        }
    }
}
