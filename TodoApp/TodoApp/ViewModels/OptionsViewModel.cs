using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.ViewModels
{
	public sealed partial class OptionsViewModel : ObservableObject
	{
		private readonly NavigationService _navigationService;
		private readonly PageService _pageService;

		[ObservableProperty]
		private bool _theme;

        public OptionsViewModel(NavigationService navigationService, PageService pageService)
        {
			_navigationService = navigationService;
			_pageService = pageService;
		}

        partial void OnThemeChanged(bool value)
		{
			Application.Current!.RequestedThemeVariant = value ? ThemeVariant.Dark : ThemeVariant.Light;
		}

		[RelayCommand]
		private void GoBack()
		{
			_navigationService.CurrentPageData = _pageService.Pages[typeof(TodoItemsViewModel)];
		}
	}
}
