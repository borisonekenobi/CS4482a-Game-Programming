public record Settings
{
	public static readonly Settings Instance = new();

	public string Language { get; set; } = "English";
	public string[] Languages { get; set; } = {
		"Bulgarian",
		"Chinese (Simplified)",
		"Chinese (Traditional)",
		"English",
		"French",
		"German",
		"Japanese",
		"Korean",
		"Polish",
		"Russian"
	};
}
