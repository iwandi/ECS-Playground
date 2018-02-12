using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Playground
{
    public struct Entity
    {

    }

    public struct Name
    {
        
    }

    public class EntitySystem : ISystem
    {
        public void Init(Context context)
        {

        }

        public void DeInit()
        {

        }

        public Entity CreateEntity()
        {
            return new Entity();
        }
    }
}
