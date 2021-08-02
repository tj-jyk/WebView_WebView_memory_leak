﻿using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


namespace WebView1UWPApp
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			InitializeComponent();
			AddNewWebView();

			var timer = new DispatcherTimer
			{
				Interval = TimeSpan.FromSeconds(1)
			};
			timer.Tick += Timer_Tick;
			timer.Start();
		}

		private void AddNewWebView()
		{
			var webView = new WebView
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
			var allWebViews = new List<WebView>();
			CollectWebViews(rootGrid, allWebViews);
			VisualTreeHelper.DisconnectChildrenRecursive(rootGrid);
			for (var i = 0; i < allWebViews.Count; i++)
			{
				allWebViews[i].Navigate(new Uri("about:blank"));
			}

			AddNewWebView();
		}

		private static void CollectWebViews(DependencyObject parent, List<WebView> list)
		{
			var count = VisualTreeHelper.GetChildrenCount(parent);
			for (var i = 0; i < count; i++)
			{
				var currChild = VisualTreeHelper.GetChild(parent, i);
				if (currChild is WebView webView)
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
