using CommunityToolkit.Mvvm.ComponentModel;

namespace Core;

public sealed partial class NavigationService : ObservableObject
{
	[ObservableProperty]
	private bool _isNavigationAllowed = true;

	private PageData? _currentPageData;

	public event Action<PageData>? CurrentPageChanged;

	public PageData? CurrentPageData
	{
		get => _currentPageData;

		set
		{
			if (value is null)
			{
				return;
			}

			_currentPageData = value;

			CurrentPageChanged?.Invoke(value);
		}
	}
}
