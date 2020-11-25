// <copyright file="FileManager.cs" company="IIASA">
// Copyright (c) IIASA. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.IO;

namespace Agrotutor.Core
{
    public class FileManager
	{
	
		public static string SavingPath { get; set; }
	

		// Check if the cache for a specified url is saved on cache or not
		public static bool CacheFileExists(string url)
		{
			string fileName = url.GetHashCode().ToString(); 
			string path = Path.Combine(SavingPath, fileName);
         
		    if (File.Exists(path) )
		    {
		        FileInfo fileInfo = new FileInfo(path);
		        if (fileInfo.Length>0)
		        {
		            return true;
		        }
		    }

		    return false;

        }

		public static void DeleteOfflineCache(string url)
		{
		  
		        string fileName = url.GetHashCode().ToString();
            string path = Path.Combine(SavingPath, fileName);
		        File.Delete(path);
      
        }

		public static string GetCacheFilePath(string url)
		{
			string fileName = url.GetHashCode().ToString();
			string path = Path.Combine(SavingPath, fileName);
			return path;
		}

		public static string GetFullPath(string fileName)
		{
			string path = Path.Combine(SavingPath, fileName);
			return path;
		}

		public static void SaveFileToDirectory(string layerName, byte[] bytesArray)
		{
			string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), layerName);
			Console.WriteLine("Filename " + fileName);
			try
			{
				File.WriteAllBytes(fileName, bytesArray);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}
	}
}