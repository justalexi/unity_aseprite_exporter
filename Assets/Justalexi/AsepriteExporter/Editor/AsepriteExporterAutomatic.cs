using System.IO;
using UnityEditor;
using UnityEngine;

namespace Justalexi.AsepriteExporter.Editor
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
    public class AsepriteExporterAutomatic : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (var importedAssetPath in importedAssets)
            {
                if (importedAssetPath.EndsWith(".ase") || importedAssetPath.EndsWith(".aseprite"))
                {
                    var asepriteFilePath = importedAssetPath.StartsWith("Assets/") ? importedAssetPath.Substring(7) : importedAssetPath;

                    var asepriteFileFullPath = Path.Combine(Application.dataPath, asepriteFilePath);

                    FileInfo asepriteFile = new FileInfo(asepriteFileFullPath);

                    if (!asepriteFile.Exists)
                    {
                        Debug.LogError("AsepriteExporterAutomatic.OnPostprocessAllAssets: ERROR! Non-existent file: " + asepriteFileFullPath);
                        return;
                    }

                    var success = AsepriteExporterCommon.ExportAsepriteFile(asepriteFile);
                    if (success)
                    {
                        // 'ImportAsset' expects path starting from "Assets/"
                        var typeLength = importedAssetPath.EndsWith(".ase") ? 4 : (importedAssetPath.EndsWith(".aseprite") ? 9 : 0);
                        var relativeAsepriteFilePath = importedAssetPath.Substring(0, importedAssetPath.Length - typeLength) + ".png";
                        AssetDatabase.ImportAsset(relativeAsepriteFilePath);
                    }
                }
            }
        }
    }
}