using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FactoryLib
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler == null)
				return;
			handler (this, new PropertyChangedEventArgs (propertyName));
		}

		#endregion

		private string m_title;

		public string Title {
			get {
				return m_title;
			}
			set {
				m_title = value;
				OnPropertyChanged ();
			}
		}

		private string m_ImageBackGround;

		public string ImageBackGround {
			get {
				return m_ImageBackGround;
			}
			set {
				m_ImageBackGround = value;
				OnPropertyChanged ();
			}
		}

		public BaseViewModel ()
		{
			this.m_title = string.Empty;
			this.m_ImageBackGround = string.Empty;

		}

		public ViewModelNavigation Navigation { get; set; }
	}
}