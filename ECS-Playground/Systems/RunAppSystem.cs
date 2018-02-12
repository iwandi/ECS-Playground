using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Playground
{
    public interface IRunApp
    {
        void Run();
    }

    public class RunAppSystem : ISystem, IRunApp
    {
        public void Init(Context context)
        {

        }

        public void DeInit()
        {

        }

        public void Run()
        {
            while(true)
            {

            }
        }
    }
}
