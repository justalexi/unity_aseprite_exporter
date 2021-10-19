using System.Diagnostics;
using System.IO;
using Debug = UnityEngine.Debug;

namespace Justalexi.AsepriteExporter.Editor
{
    public static class AsepriteExporterCommon
    {
        /// <license>
        /// MIT License

        /// Copyright (c) 2021 justalexi

        /// Permission is hereby granted, free of charge, to any person obtaining a copy
        /// of this software and associated documentation files (the "Software"), to deal
        /// in the Software without restriction, including without limitation the rights
        /// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
        /// copies of the Software, and to permit persons to whom the Software is
        /// furnished to do so, subject to the following conditions:

        /// The above copyright notice and this permission notice shall be included in all
        /// copies or substantial portions of the Software.
        /// 
        /// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
        /// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
        /// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
        /// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
        /// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
        /// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
        /// SOFTWARE.
        /// </license>
        /// <summary>
        /// See https://www.aseprite.org/docs/cli/ for Aseprite installation directories,
        /// Also can add Aseprite's directory to OS's path variables, so that it can be launched with just a name
        /// </summary>
        private static string asepritePath = "aseprite";


        public static bool ExportAsepriteFile(FileInfo asepriteFile)
        {
            string resultPngFileName;
            var asepriteFileName = asepriteFile.Name;
            if (asepriteFileName.EndsWith(".ase"))
            {
                resultPngFileName = asepriteFileName.Replace(".ase", ".png");
            }
            else if (asepriteFileName.EndsWith(".aseprite"))
            {
                resultPngFileName = asepriteFileName.Replace(".aseprite", ".png");
            }
            else
            {
                Debug.Log("AsepriteExporterCommon.ExportAsepriteFile: WRONG FILE TYPE. " + asepriteFileName);
                return false;
            }

            var resultPngFullPath = Path.Combine(asepriteFile.Directory.FullName, resultPngFileName);

            // See https://www.aseprite.org/docs/cli/ for other useful CLI examples
            string arguments = "-b " + asepriteFile + " --save-as " + resultPngFullPath;

            Process process = Process.Start(asepritePath, arguments);

            if (process != null)
            {
                process.WaitForExit();
                process.Close();
            }
            else
            {
                Debug.LogError("AsepriteExporterCommon.ExportAsepriteFile: ERROR! 'process' is null");
                return false;
            }

            return true;
        }
    }
}