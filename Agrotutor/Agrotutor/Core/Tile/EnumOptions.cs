// <copyright file="EnumOptions.cs" company="IIASA">
// Copyright (c) IIASA. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using Agrotutor.Core.Tile.NamingConfiguration;

namespace Agrotutor.Core.Tile
{
    public class EnumOptions
	{
		public static EnumOptions Default
		{
			get
			{
				EnumOptions enumOptions = new EnumOptions();
				enumOptions.SetNamingScheme(NamingScheme.SnakeCase);

				return enumOptions;
			}
		}

		public Func<string, string> NamingFuction { get; set; }

		public EnumOptions SetNamingScheme(Func<string, string> namingFunc)
		{
			NamingFuction = namingFunc;

			return this;
		}
	}
}