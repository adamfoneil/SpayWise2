﻿using Hydro;

namespace HydroApp.Components;

// ~/Pages/Components/PageCounter.cshtml.cs

public class PageCounter : HydroComponent
{
	public int Count { get; set; }

	public void Add()
	{
		Count++;
	}
}