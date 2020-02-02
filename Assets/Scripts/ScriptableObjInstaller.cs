using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ScriptableObjInstaller", menuName = "Installers/ScriptableObjInstaller")]
public class ScriptableObjInstaller : ScriptableObjectInstaller<ScriptableObjInstaller>
{
    [SerializeField]
    private GameConfig _config;

    public override void InstallBindings()
    {
        Container.BindInstance(_config);
    }
}