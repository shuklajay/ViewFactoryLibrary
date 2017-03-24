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

		public async void Push (Page page)
		{

			await implementor.Navigation.PushAsync (page);
		}

		public void Push<TViewModel> ()
			where TViewModel : BaseViewModel
		{
			Push (ViewFactory.CreatePage<TViewModel> ());
		}

		public async void Pop ()
		{
			await implementor.Navigation.PopAsync ();
		}

		public async Task PopToRoot ()
		{
			await implementor.Navigation.PopToRootAsync ();
		}


		public void InsertPageBfor<TViewModel, TViewModel1> () where TViewModel : BaseViewModel
		where TViewModel1 : BaseViewModel
		{
			implementor.Navigation.InsertPageBefore (ViewFactory.CreatePage<TViewModel> (), ViewFactory.CreatePage<TViewModel1> ());
		}

		public async void PushModal (Page page)
		{
			await implementor.Navigation.PushModalAsync (page);
		}

		public void PushModal<TViewModel> ()
			where TViewModel : BaseViewModel
		{
			PushModal (ViewFactory.CreatePage<TViewModel> ());
		}

		public async void PopModal ()
		{
			var modalParent = implementor;
			while (modalParent.Parent as Page != null)
				modalParent = (Page)modalParent.Parent;
			await implementor.Navigation.PopModalAsync ();
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
			return this.implementor.Navigation.NavigationStack;
		}

		public async Task<bool> FindPageFromStack (Page page)
		{
			bool t = false;
			foreach (var item in this.implementor.Navigation.NavigationStack) {
				if (Convert.ToString (item.BindingContext.GetType ().Name) == page.BindingContext.GetType ().Name) {
					t = true;
					break;
				}
			}
			return t;
		}
	}
}