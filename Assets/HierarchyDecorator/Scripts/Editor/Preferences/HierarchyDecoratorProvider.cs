﻿using System.IO;
using UnityEditor;
using UnityEngine;

namespace HierarchyDecorator
    {
    // Register the Settings for the HiearchyDecorator
    internal static class HierarchyDecoratorProvider
        {
        public static SettingsProvider[] tabs =
            {
            new SettingsPreferences (Constants.SETTINGS_PATH, SettingsScope.User),
            };

        private static bool SettingsExist()
            {
            return File.Exists (Constants.SETTINGS_ASSET_PATH);
            }

        [SettingsProviderGroup]
        public static SettingsProvider[] Load()
            {
            //if (SettingsExist ())
            //    return tabs;
            //else
            //    return (HierarchyDecoratorSettings.GetOrCreateSettings ()) ? tabs : null;
            return tabs;
            }

        }

    }

