using Magnetify.ViewModels;

namespace Magnetify.Views;

public partial class RecentPage : ContentPage
{
	public RecentPage(RecentViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}