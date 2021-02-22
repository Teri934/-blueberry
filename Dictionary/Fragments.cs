﻿using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Media;
using Android.Util;
using Language;

namespace Fragments
{
	class MainFragment : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup parent, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Dictionary.Resource.Layout.content_main, parent, false);
			Button recordings = view.FindViewById<Button>(Dictionary.Resource.Id.recordings_button);
			recordings.Click += Recordings_Click;
			return view;
		}

		void Recordings_Click(object sender, EventArgs e)
		{
			FragmentTransaction ft = FragmentManager.BeginTransaction();
			ft.Replace(Dictionary.Resource.Id.place_holder, new RecordingsFragment());
			ft.AddToBackStack(null);
			ft.Commit();
		}
	}

	class RecordingsFragment : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup parent, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Dictionary.Resource.Layout.content_recordings, parent, false);
			Log.Debug("f", "recordings");

			FragmentTransaction ft = FragmentManager.BeginTransaction();
			for (int i = 0; i < English.Dictionary.Count; i++)
			{
				ft.Add(Dictionary.Resource.Id.sounds_list, new SoundsFragment(i));
			}
			ft.Commit();

			return view;
		}
	}

	class SoundsFragment : Fragment
	{
		static MediaPlayer player = new MediaPlayer();
		int index;

		public SoundsFragment(int index)
		{
			this.index = index;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup parent, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Dictionary.Resource.Layout.sound_button, parent, false);
			ImageButton b = (ImageButton)((ViewGroup)view).GetChildAt(0);
			b.Click += Sound_Click;
			return view;
		}   

		void Sound_Click(object sender, EventArgs e)
		{
			player.Release();
			player = MediaPlayer.Create(Application.Context, Application.Context.Resources.GetIdentifier(English.Dictionary[index].Filename, "raw", Application.Context.PackageName));
			player.Start();
		}
	}
}