using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Core;

namespace TodoApp.ViewModels;

public sealed partial class MainWindowViewModel : ViewModelBase
{
	private readonly NavigationService _navigationService;
	private readonly ServiceCollection _serviceCollection;

	[ObservableProperty]
	private UserControl? _content;

    public MainWindowViewModel(NavigationService navigationService, ServiceCollection serviceCollection)
    {
		_serviceCollection = serviceCollection;
		_navigationService = navigationService;
		_navigationService.CurrentPageChanged += CurrentPageChanged;

		if (_navigationService.CurrentPageType is not null)
		{
			CurrentPageChanged(_navigationService.CurrentPageType);
		}
	}

	private void CurrentPageChanged(PageData pageData)
	{
		if (Content is not null)
		{
			IsActiveChanged(false, Content.DataContext);
		}

		var control = _serviceCollection.GetService(pageData.Type!) as UserControl ?? throw new Exception("null control");
		control.DataContext = _serviceCollection.GetService(pageData.ViewModelType!);
		Content = control;

		IsActiveChanged(true, control.DataContext);
	}

	public static void IsActiveChanged(bool isActive, object? dataContext)
	{
		if (dataContext is not IActiveAware activeAware)
		{
			return;
		}

		if (isActive)
		{
			activeAware.OnActivated();
		}
		else
		{
			activeAware.OnDeactivated();
		}
	}
}
