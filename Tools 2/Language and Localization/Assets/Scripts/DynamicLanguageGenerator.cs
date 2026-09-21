using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class DynamicLanguageGenerator
{
	public static void RegenerateMenu(string[] languages)
	{
		var folderPath = Application.dataPath + "/Scripts";
		if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

		var sb = new StringBuilder();
		sb.AppendLine("// AUTOMATICALLY GENERATED FILE - DO NOT EDIT MANUALLY");
		sb.AppendLine("using UnityEditor;");
		sb.AppendLine();
		sb.AppendLine("public static class GeneratedLanguages");
		sb.AppendLine("{");

		for (var i = 0; i < languages.Length; i++)
		{
			var lang = languages[i];
			sb.AppendLine($"\t[MenuItem(\"Window/Languages/{lang}\")]");
			sb.AppendLine($"\tprivate static void Select_Lang_{i}() => LanguageMenu.HandleSelection(\"{lang}\");");
			if (i != languages.Length - 1) sb.AppendLine();
		}
		sb.AppendLine("}");

		var filePath = folderPath + "/GeneratedLanguages.cs";
		File.WriteAllText(filePath, sb.ToString());

		AssetDatabase.Refresh();
		Debug.Log("Hover menu updated with latest dynamic changes!");
	}
}
