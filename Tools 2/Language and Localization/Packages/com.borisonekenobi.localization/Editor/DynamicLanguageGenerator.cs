using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Borisonekenobi.Localization.Editor
{
    public static class DynamicLanguageGenerator
    {
        public static void RegenerateMenu(string[] languages)
        {
            var folderPath = Application.dataPath + "/Scripts/Editor";
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var sb = new StringBuilder();
            sb.AppendLine("// AUTOMATICALLY GENERATED FILE - DO NOT EDIT MANUALLY");
            sb.AppendLine();
            sb.AppendLine("using Borisonekenobi.Localization.Editor;");
            sb.AppendLine("using UnityEditor;");
            sb.AppendLine();
            sb.AppendLine("namespace Editor");
            sb.AppendLine("{");
            sb.AppendLine("\tpublic static class GeneratedLanguages");
            sb.AppendLine("\t{");

            for (var i = 0; i < languages.Length; i++)
            {
                var lang = languages[i];
                sb.AppendLine($"\t\t[MenuItem(\"Window/Languages/{lang}\")]");
                sb.AppendLine($"\t\tprivate static void Select_Lang_{i}() => LanguageMenu.HandleSelection(\"{lang}\");");
                if (i != languages.Length - 1) sb.AppendLine();
            }

            sb.AppendLine("\t}");
            sb.AppendLine("}");

            var filePath = folderPath + "/GeneratedLanguages.cs";
            File.WriteAllText(filePath, sb.ToString());

            AssetDatabase.Refresh();
            Debug.Log("Hover menu updated with latest dynamic changes!");
        }
    }
}
