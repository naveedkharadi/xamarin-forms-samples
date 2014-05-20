﻿using System;
using Xamarin.Forms;
using EmployeeDirectory.Data;
using System.Threading.Tasks;
using System.Linq;

namespace EmployeeDirectory
{
	public static class App
	{
		/// <summary>
		/// service used to supply data to the app
		/// </summary>
		/// <remarks>
		/// * Memory (uses CSV file)
		/// * LDAP (requires network)
		/// </remarks>
		public static IDirectoryService service;

		public static IPhoneFeatureService PhoneFeatureService;

		/// <summary>
		/// last time the device was used
		/// </summary>
		public static DateTime LastUseTime { get; set; }

		public static Page GetMainPage ()
		{
			service = MemoryDirectoryService.FromCsv ("XamarinDirectory.csv").Result;

			var employeeListView = new EmployeeListView ();
			var mainNav = new NavigationPage (employeeListView);
			return mainNav;
		}
	}
}
