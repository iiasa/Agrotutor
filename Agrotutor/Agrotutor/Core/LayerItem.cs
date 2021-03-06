﻿// <copyright file="LayerItemViewModel.cs" company="IIASA">
// Copyright (c) IIASA. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Agrotutor.Core
{
    public class LayerItem : Item
	{
		public string Icon { get; set; }

		public bool IsEnabled { get; set; }

		public bool IsRaster { get; set; }

		public string FileName { get; set; }

		public string TextColor
		{
			get
			{
				if (IsEnabled)
				{
					return "#000000";
				}
				else
				{
					return "#CCCCCC";
				}
			}

			set
			{
			}
		}
	}
}