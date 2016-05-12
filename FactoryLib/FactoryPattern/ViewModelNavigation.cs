using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FactoryLib
{
	public class ViewModelNavigation
	{
		readonly Page implementor;

		public ViewModelNavigation (Page implementor)
		{
			this.implementor = implementor;
		}

		public void Push (Page page)
		{

			implementor.Navigation.PushAsync (page);
		}

		public void Push<TViewModel> ()
			where TViewModel : BaseViewModel
		{
			Push (ViewFactory.CreatePage<TViewModel> ());
		}

		public void Pop ()
		{
			implementor.Navigation.PopAsync ();
		}

		public void PopToRoot ()
		{
			implementor.Navigation.PopToRootAsync ();
		}

		public void PushModal (Page page)
		{
			implementor.Navigation.PushModalAsync (page);
		}

		public void PushModal<TViewModel> ()
			where TViewModel : BaseViewModel
		{
			PushModal (ViewFactory.CreatePage<TViewModel> ());
		}

		public void PopModal ()
		{
			var modalParent = implementor;
			while (modalParent.Parent as Page != null)
				modalParent = (Page)modalParent.Parent;
			implementor.Navigation.PopModalAsync ();
		}

		public async Task RemoveAsync<TViewModel> (TViewModel viewModel, bool animated = true)
			where TViewModel : BaseViewModel
		{
			try {               
				foreach (var page in this.implementor.Navigation.NavigationStack) {
					if (Convert.ToString (page.BindingContext) == Convert.ToString (viewModel)) {
						// If the page is on top of the stack it must be popped first
						if (this.implementor.Navigation.NavigationStack [this.implementor.Navigation.NavigationStack.Count - 1] == page) {
							await this.implementor.Navigation.PopAsync ();
						}
						// Clear the view model/bindings
						page.BindingContext = null;                     
						// Remove the page from the stack
						this.implementor.Navigation.RemovePage (page);
						return;
					}
				}
			} catch (Exception ex) {
				ex.StackTrace.ToString ();
			}
		}

		public IReadOnlyList<Page> NavigationStack ()
		{
			return  this.implementor.Navigation.NavigationStack;
		}
	}
}