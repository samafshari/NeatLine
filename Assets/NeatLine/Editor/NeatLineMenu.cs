using UnityEditor;
using UnityEngine;

public class NeatLineMenu : MonoBehaviour
{
    [MenuItem("GameObject/2D Object/NeatLine")]
    public static void Create()
    {
        var obj = new GameObject("NeatLine");
        obj.AddComponent<NeatLine>();

        if (Selection.activeGameObject != null)
        {
            var assetPath = AssetDatabase.GetAssetPath(Selection.activeGameObject);
            if (string.IsNullOrEmpty(assetPath))
            {
                obj.transform.SetParent(Selection.activeGameObject.transform);
            }
        }

        obj.transform.localScale = Vector3.one;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localPosition = Vector3.zero;
        if (obj.transform.parent != null)
        {
            obj.layer = obj.transform.parent.gameObject.layer;
        }
        Selection.activeGameObject = obj;
    }
}
