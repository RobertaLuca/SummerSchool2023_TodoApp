using CommunityToolkit.Mvvm.ComponentModel;

namespace Core;

public sealed partial class NavigationService : ObservableObject
{
	[ObservableProperty]
	private bool _isNavigationAllowed = true;

	private PageData? _currentPageType;

	public event Action<PageData>? CurrentPageChanged;

	public PageData? CurrentPageType
	{
		get => _currentPageType;

		set
		{
			if (value is null)
			{
				return;
			}

			_currentPageType = value;

			CurrentPageChanged?.Invoke(value);
		}
	}
}
