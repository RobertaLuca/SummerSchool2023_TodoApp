using CommunityToolkit.Mvvm.ComponentModel;

namespace Core;

public sealed partial class NavigationService : ObservableObject
{
	[ObservableProperty]
	private bool _isNavigationAllowed = true;

	private PageData? _currentPageData;

	public event Action<PageData>? CurrentPageChanged;

	public Dictionary<Type, PageData> Pages { get; } = new();

	public PageData? CurrentPageData
	{
		get => _currentPageData;

		private set
		{
			if (value is null)
			{
				return;
			}

			_currentPageData = value;

			CurrentPageChanged?.Invoke(value);
		}
	}

	public NavigationService RegisterPage<P, VM>(string pageName, string? icon = null, bool showSidePanel = true)
	{
		Pages.Add(typeof(VM), new PageData()
		{
			Name = pageName,
			ViewModelType = typeof(VM),
			ViewType = typeof(P),
			Icon = icon,
			ShowSidePanel = showSidePanel
		});

		return this;
	}

	public void Navigate<T>()
	{
		CurrentPageData = Pages[typeof(T)];
	}
}
