using Microsoft.Extensions.Configuration;

namespace TodoApp.Services;

public class Configs
{
	private IConfigurationRoot _configurationRoot;

    public Configs()
    {
        _configurationRoot = new ConfigurationBuilder()
			.AddUserSecrets("684c11c1-0530-41f6-ae46-7f00bc4441f5")
			.Build();
	}

    public string? GetSetting(string key)
	{
		return _configurationRoot.GetSection(key).Value;
	}
}
