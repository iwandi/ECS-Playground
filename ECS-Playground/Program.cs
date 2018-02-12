using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            Context ctx = new Context();

            ctx.RegsiterTypesInAssambly();
            ctx.AddSystem<Example.PongGameSystem>();

            IRunApp runApp = ctx.GetSystem<IRunApp>();
            runApp.Run();
        }
    }
}
