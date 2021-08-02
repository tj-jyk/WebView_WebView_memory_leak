using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebView2UWPApp
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			InitializeComponent();
			AddNewWebView2();
			var timer = new DispatcherTimer
			{
				Interval = TimeSpan.FromSeconds(1)
			};
			timer.Tick += Timer_Tick;
			timer.Start();
		}

		private void AddNewWebView2()
		{
			var webView = new WebView2
			{
				HorizontalAlignment = HorizontalAlignment.Stretch,
				VerticalAlignment = VerticalAlignment.Stretch,
				Source = new Uri("https://github.com/")
			};
			Grid.SetColumnSpan(webView, 2);
			Grid.SetRow(webView, 1);
			rootGrid.Children.Add(webView);
		}

		private void Timer_Tick(object sender, object e)
		{
			var allWebViews = new List<WebView2>();
			CollectWebViews(rootGrid, allWebViews);
			VisualTreeHelper.DisconnectChildrenRecursive(rootGrid);
			for (var i = 0; i < allWebViews.Count; i++)
			{
				//allWebViews[i].Source = new Uri("about:blank");
				allWebViews[i].Close();
				GC.Collect();
			}

			AddNewWebView2();
		}

		private static void CollectWebViews(DependencyObject parent, List<WebView2> list)
		{
			var count = VisualTreeHelper.GetChildrenCount(parent);
			for (var i = 0; i < count; i++)
			{
				var currChild = VisualTreeHelper.GetChild(parent, i);
				if (currChild is WebView2 webView)
				{
					list.Add(webView);
				}
				else
				{
					CollectWebViews(currChild, list);
				}
			}
		}
	}
}
