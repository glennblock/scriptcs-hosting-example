using System;
using ScriptCs;
using ScriptCs.Contracts;
using ScriptCs.Hosting;
using ScriptCs.Engine.Mono;
using System.Linq;
using ScriptCs.Engine.Roslyn;

namespace ScriptsHostingSample
{
    class MainClass
    {
        public static void Main (string[] args)
        {
 
            var console = (IConsole) new ScriptConsole();
            var logProvider = new ColoredConsoleLogProvider (LogLevel.Info, console);

            var builder = new ScriptServicesBuilder (console, logProvider);

            SetEngine (builder);
            var services = builder.Build ();
        
            var executor = (ScriptExecutor) services.Executor;
            executor.Initialize (Enumerable.Empty<string>(), Enumerable.Empty<IScriptPack>());
            ExecuteLooseScript (executor);
            ExecuteFile (executor);
        }

        public static void ExecuteLooseScript(ScriptExecutor executor) {
            var script = @"Console.WriteLine(""Hello from scriptcs"")";
            executor.ExecuteScript (script);
        }

        public static void ExecuteFile(ScriptExecutor executor) {
            executor.Execute ("HelloWorld.csx");
        }

        static void SetEngine (ScriptServicesBuilder builder)
        {
            var useMono = Type.GetType ("Mono.Runtime") != null;
            if (useMono) {
                builder.ScriptEngine<MonoScriptEngine> ();
            }
            else {
                builder.ScriptEngine<RoslynScriptEngine> ();
            }
        }
    }
}

