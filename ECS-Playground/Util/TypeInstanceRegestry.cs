using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Playground
{
    public class TypeInstanceRegestry<TBase>
    {
        readonly Type tBase = typeof(TBase);

        Dictionary<Type, TypeEntry> regestry = new Dictionary<Type, TypeEntry>();

        public void RegsiterTypesInDomain(System.AppDomain domain = null)
        {
            if (domain == null)
            {
                domain = AppDomain.CurrentDomain;
            }
            foreach (System.Reflection.Assembly asm in domain.GetAssemblies())
            {
                RegsiterTypesInAssambly(asm);
            }
        }

        public void RegsiterTypesInAssambly(System.Reflection.Assembly asm = null)
        {
            if (asm == null)
            {
                asm = System.Reflection.Assembly.GetExecutingAssembly();
            }
            foreach (Type t in asm.GetTypes())
            {
                RegsiterType(t);
            }
        }

        public void RegsiterType(Type t)
        {
            foreach (Type subType in GetSubTypes(t))
            {
                TypeEntry entry = GetEntry(subType);
                entry.AddType(t);
                System.Diagnostics.Debug.WriteLine("AddType {0} : {1}", t.Name, subType.Name);
            }
        }

        public void RegisterInstance(object obj, bool overrideExistring = false)
        {
            Type t = obj.GetType();

            foreach(Type subType in GetSubTypes(t))
            {
                TypeEntry entry = GetEntry(subType);
                entry.AddInstance(obj, overrideExistring);
            }
        }

        Type[] GetSubTypes(Type t)
        {
            return t.FindInterfaces(TypeFilter, tBase);
        }

        bool TypeFilter(Type m, object filterCriteria)
        {
            //return m.Equals(tBase);
            // TODO : check if we need to filter something 
            return true;
        }

        public void UnRegisterInstance(object obj)
        {
            // TODO check all registrations for thisy instance and remove it
        }
        
        public T GetInstanceByType<T>(bool create)
        {
            Type t = typeof(T);
            TypeEntry entry;
            if (regestry.TryGetValue(t, out entry))
            {
                return (T)entry.GetPrimary(create);
            }
            return default(T);
        }

        public bool TryGetInstanceByType<T>(out T value)
        {
            Type t = typeof(T);
            TypeEntry entry;
            if (regestry.TryGetValue(t, out entry))
            {
                value = (T)entry.GetPrimary();
                return true;
            }
            value = default(T);
            return false;
        }

        TypeEntry GetEntry(Type t)
        {
            TypeEntry entry;
            if(!regestry.TryGetValue(t, out entry))
            {
                entry = new TypeEntry();
                regestry.Add(t, entry);
            }
            return entry;
        }

        class TypeEntry
        {
            Type PrimeryType;
            List<Type> Options = new List<Type>();

            object Primary;
            List<object> Instances = new List<object>();

            public void AddType(Type t)
            {
                if(t != PrimeryType)
                {
                    if(!Options.Contains(t))
                    {
                        Options.Add(t);
                    }
                    if(PrimeryType == null)
                    {
                        PrimeryType = t;
                    }
                }
            }

            public void AddInstance(object obj, bool overrideExistring = false)
            {
                if(Primary != obj)
                {
                    if(!Instances.Contains(obj))
                    {
                        Instances.Add(obj);
                    }
                    if(Primary == null || overrideExistring)
                    {
                        Primary = obj;
                    }
                }
            }

            public object GetPrimary(bool create = false)
            {
                if (Primary == null)
                {
                    if (PrimeryType != null)
                    {
                        object obj = System.Activator.CreateInstance(PrimeryType);
                        AddInstance(obj);
                    }
                }
                return Primary;
            }
        }
    }
}
