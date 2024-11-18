using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

///<summary>
///Initializable接口被用于任何可能需要进行初始化，并且对于初始化有先后顺序要求的对象。<br/>
///在脚本组件扩展了该接口后，需要将带有该接口的游戏物体放入GameManager的initializableList列表。<br/>
///或者在其他地方主动调用Initialize方法。
///</summary>
public interface Initializable
{
    public abstract void Initialize();
}
