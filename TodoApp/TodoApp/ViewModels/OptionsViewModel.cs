using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core;

namespace TodoApp.ViewModels;

public sealed partial class OptionsViewModel : ObservableObject
{
	private readonly NavigationService _navigationService;

	[ObservableProperty]
	private bool _theme;

	public OptionsViewModel(NavigationService navigationService)
	{
		_navigationService = navigationService;
	}

	partial void OnThemeChanged(bool value)
	{
		Application.Current!.RequestedThemeVariant = value ? ThemeVariant.Dark : ThemeVariant.Light;
	}

	[RelayCommand]
	private void GoBack()
	{
		_navigationService.Navigate<TodoItemsViewModel>();
	}
}
