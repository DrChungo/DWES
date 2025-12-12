using System.Diagnostics;

namespace CrearApi.Functions
{
    public static class NodeLauncher
    {
        private static Process? nodeProcess;

        public static void StartNodeApi()
        {
            if (nodeProcess != null && !nodeProcess.HasExited)
                return;

            var psi = new ProcessStartInfo
            {
                FileName = "node",
                Arguments = "api.js",   // archivo de tu API Node
                WorkingDirectory = @"C:\Users\Usuario\Desktop\clase\2 DAW\DWES\CrearApi\Node", // ruta a la carpeta Node
                CreateNoWindow = true,
                UseShellExecute = false
            };

            nodeProcess = Process.Start(psi);
        }

        public static void StopNodeApi()
        {
            if (nodeProcess != null && !nodeProcess.HasExited)
            {
                nodeProcess.Kill();
                nodeProcess.Dispose();
            }
        }
    }
}
