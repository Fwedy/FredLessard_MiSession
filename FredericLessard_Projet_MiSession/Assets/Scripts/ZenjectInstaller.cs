using UnityEngine;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {

        Container.Bind<ObjectPool_Bullets>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}