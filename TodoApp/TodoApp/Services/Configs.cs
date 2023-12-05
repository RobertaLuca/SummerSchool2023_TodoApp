using Microsoft.Extensions.Configuration;

namespace TodoApp.Services;

// TODO: maybe it would better for pages to inject directly the IConfiguration interface and to create this in the when the app starts?
public class Configs
{
	private readonly IConfigurationRoot _configurationRoot;

    public Configs()
    {
        _configurationRoot = new ConfigurationBuilder()
			.AddUserSecrets("684c11c1-0530-41f6-ae46-7f00bc4441f5")
			.Build();
	}

    public string? GetSetting(string key)
	{
		return _configurationRoot[key];
	}
}
