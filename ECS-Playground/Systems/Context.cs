using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Playground
{
    public interface IComponent
    {

    }

    public interface ISystem
    {
        void Init(Context context);
        void DeInit();
    }

    public class Context
    {
        protected TypeInstanceRegestry<ISystem> systems = new TypeInstanceRegestry<ISystem>();

        public void Configure(object config)
        {
            // TODO Setup some base Systems based on config
        }

        public void RegsiterTypesInDomain(System.AppDomain domain = null)
        {
            systems.RegsiterTypesInDomain(domain);
        }

        public void RegsiterTypesInAssambly(System.Reflection.Assembly asm = null)
        {
            systems.RegsiterTypesInAssambly(asm);
        }

        public T AddSystem<T>() where T : new()
        {
            return AddSystem(Activator.CreateInstance<T>());
        }

        public T AddSystem<T>(T sys)
        {
            systems.RegisterInstance(sys);
            return sys;
        }

        public bool TryGetSystem<T>(out T sys)
        {
            return systems.TryGetInstanceByType<T>(out sys);
        }

        public T GetSystem<T>()
        {
            T sys;
            if(!TryGetSystem<T>(out sys))
            {
                sys = systems.GetInstanceByType<T>(true);
                AddSystem<T>(sys);
            }
            return sys;
        }
    }
}
