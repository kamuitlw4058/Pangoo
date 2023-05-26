using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pangoo{

    // [ExecuteAlways]
    public class EntitySimpleCreater : MonoBehaviour
    {
        [SerializeField] EntityInfo Info;
        // Start is called before the first frame update
        EntityBase Entity;

        EntityLoader Loader;

        bool Created;


        // Update is called once per frame
        void Update()
        {
            if(Loader == null){
                Loader = EntityLoader.Create(this);
            }
            
            if(Loader != null &&  !Created && Info != null){
                Debug.Log($"Show Entity:{Info}");
                Loader.ShowEntity(EnumEntity.StaticScene,
                    (o)=>{
                    Entity = o.Logic as EntityBase;
                    },
                    Info,
                    EntityData.Create());  
                Created = true;
            }
        }
    }
}
