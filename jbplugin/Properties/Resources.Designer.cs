﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace jbplugin.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("jbplugin.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Auto Import Failed.
        /// </summary>
        internal static string AUTOIMPORTFAILED {
            get {
                return ResourceManager.GetString("AUTOIMPORTFAILED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error Connect to Device.
        /// </summary>
        internal static string ERRORCONNECT {
            get {
                return ResourceManager.GetString("ERRORCONNECT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error loading EANs/UPCs from Device.
        /// </summary>
        internal static string ERRORLOADEAN {
            get {
                return ResourceManager.GetString("ERRORLOADEAN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exporting ....
        /// </summary>
        internal static string EXPORTING {
            get {
                return ResourceManager.GetString("EXPORTING", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Header1 {
            get {
                object obj = ResourceManager.GetObject("Header1", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No itunes found, please put IP and Port manually.
        /// </summary>
        internal static string NOITUNES {
            get {
                return ResourceManager.GetString("NOITUNES", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Read EANs/UPCs finished.
        /// </summary>
        internal static string READEANDONE {
            get {
                return ResourceManager.GetString("READEANDONE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WiFi Mode: The DVD-Profiler will update all Profiles completly unnatended in your local WiFi..
        /// </summary>
        internal static string WIFIMODE {
            get {
                return ResourceManager.GetString("WIFIMODE", resourceCulture);
            }
        }
    }
}
