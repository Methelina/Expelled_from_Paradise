// ------------------------------------------------------------------------------
//  _______   _____ ___ ___   _   ___ ___ 
// |_   _\ \ / / _ \ __/ __| /_\ | __| __|
//   | |  \ V /|  _/ _|\__ \/ _ \| _|| _| 
//   |_|   |_| |_| |___|___/_/ \_\_| |___|
// 
// This file has been generated automatically by TypeSafe.
// Any changes to this file may be lost when it is regenerated.
// https://www.stompyrobot.uk/tools/typesafe
// 
// TypeSafe Version: 1.1.1-Unity5
// 
// ------------------------------------------------------------------------------



public sealed class SRResources {
    
    private const string _tsInternal = "1.1.1-Unity5";
    
    private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[] {
                new global::TypeSafe.Resource<global::UnityEngine.TextAsset>("LineBreaking Leading Characters", "LineBreaking Leading Characters"),
                new global::TypeSafe.Resource<global::UnityEngine.Material>("Shadows", "Shadows"),
                new global::TypeSafe.PrefabResource("point_arrow", "point_arrow"),
                new global::TypeSafe.PrefabResource("gib_pack_big", "gib_pack_big"),
                new global::TypeSafe.PrefabResource("SoundManager", "SoundManager"),
                new global::TypeSafe.Resource<global::UnityEngine.Texture2D>("floor", "floor"),
                new global::TypeSafe.Resource<global::UnityEngine.Texture2D>("condition", "condition"),
                new global::TypeSafe.Resource<global::UnityEngine.TextAsset>("LineBreaking Following Characters", "LineBreaking Following Characters"),
                new global::TypeSafe.Resource<global::UnityEngine.TextAsset>("PlayMakerAssemblies", "PlayMakerAssemblies"),
                new global::TypeSafe.Resource<global::UnityEngine.Texture2D>("gears", "gears"),
                new global::TypeSafe.Resource<global::TMPro.TMP_Settings>("TMP_Settings", "TMP_Settings"),
                new global::TypeSafe.PrefabResource("gib_pack_small", "gib_pack_small"),
                new global::TypeSafe.PrefabResource("GlobalBlackboard", "GlobalBlackboard"),
                new global::TypeSafe.Resource<global::TMPro.TMP_StyleSheet>("TMP_DefaultStyleSheet", "TMP_DefaultStyleSheet")});
    
    private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
    
    private SRResources() {
    }
    
    public static global::TypeSafe.Resource<global::UnityEngine.TextAsset> LineBreaking_Leading_Characters {
        get {
            return ((global::TypeSafe.Resource<global::UnityEngine.TextAsset>)(__ts_internal_resources[0]));
        }
    }
    
    public static global::TypeSafe.Resource<global::UnityEngine.Material> Shadows {
        get {
            return ((global::TypeSafe.Resource<global::UnityEngine.Material>)(__ts_internal_resources[1]));
        }
    }
    
    public static global::TypeSafe.PrefabResource point_arrow {
        get {
            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[2]));
        }
    }
    
    public static global::TypeSafe.PrefabResource gib_pack_big {
        get {
            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[3]));
        }
    }
    
    public static global::TypeSafe.PrefabResource SoundManager {
        get {
            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[4]));
        }
    }
    
    public static global::TypeSafe.Resource<global::UnityEngine.Texture2D> floor {
        get {
            return ((global::TypeSafe.Resource<global::UnityEngine.Texture2D>)(__ts_internal_resources[5]));
        }
    }
    
    public static global::TypeSafe.Resource<global::UnityEngine.Texture2D> condition {
        get {
            return ((global::TypeSafe.Resource<global::UnityEngine.Texture2D>)(__ts_internal_resources[6]));
        }
    }
    
    public static global::TypeSafe.Resource<global::UnityEngine.TextAsset> LineBreaking_Following_Characters {
        get {
            return ((global::TypeSafe.Resource<global::UnityEngine.TextAsset>)(__ts_internal_resources[7]));
        }
    }
    
    public static global::TypeSafe.Resource<global::UnityEngine.TextAsset> PlayMakerAssemblies {
        get {
            return ((global::TypeSafe.Resource<global::UnityEngine.TextAsset>)(__ts_internal_resources[8]));
        }
    }
    
    public static global::TypeSafe.Resource<global::UnityEngine.Texture2D> gears {
        get {
            return ((global::TypeSafe.Resource<global::UnityEngine.Texture2D>)(__ts_internal_resources[9]));
        }
    }
    
    public static global::TypeSafe.Resource<global::TMPro.TMP_Settings> TMP_Settings {
        get {
            return ((global::TypeSafe.Resource<global::TMPro.TMP_Settings>)(__ts_internal_resources[10]));
        }
    }
    
    public static global::TypeSafe.PrefabResource gib_pack_small {
        get {
            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[11]));
        }
    }
    
    public static global::TypeSafe.PrefabResource GlobalBlackboard {
        get {
            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[12]));
        }
    }
    
    public static global::TypeSafe.Resource<global::TMPro.TMP_StyleSheet> TMP_DefaultStyleSheet {
        get {
            return ((global::TypeSafe.Resource<global::TMPro.TMP_StyleSheet>)(__ts_internal_resources[13]));
        }
    }
    
    /// <summary>
    /// Return a list of all resources in this folder.
    /// This method has a very low performance cost, no need to cache the result.
    /// </summary>
    /// <returns>A list of resource objects in this folder.</returns>
    public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
        return __ts_internal_resources;
    }
    
    /// <summary>
    /// Return a list of all resources in this folder and all sub-folders.
    /// The result of this method is cached, so subsequent calls will have very low performance cost.
    /// </summary>
    /// <returns>A list of resource objects in this folder and sub-folders.</returns>
    public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
        if ((__ts_internal_recursiveLookupCache != null)) {
            return __ts_internal_recursiveLookupCache;
        }
        global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
        tmp.AddRange(GetContents());
        tmp.AddRange(Shaders.GetContentsRecursive());
        tmp.AddRange(UI.GetContentsRecursive());
        tmp.AddRange(player.GetContentsRecursive());
        tmp.AddRange(Fonts_Materials.GetContentsRecursive());
        __ts_internal_recursiveLookupCache = tmp;
        return __ts_internal_recursiveLookupCache;
    }
    
    /// <summary>
    /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
    /// This method does not cache the result, so you should cache the result yourself if you will use it often.
    /// </summary>
    /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
    public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
        where TResource : global::UnityEngine.Object {
        return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
    }
    
    /// <summary>
    /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
    /// This method does not cache the result, so you should cache the result yourself if you will use it often.
    /// </summary>
    /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
    public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
        where TResource : global::UnityEngine.Object {
        return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
    }
    
    /// <summary>
    /// Call Unload() on every loaded resource in this folder.
    /// </summary>
    public static void UnloadAll() {
        global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
    }
    
    /// <summary>
    /// Call Unload() on every loaded resource in this folder and subfolders.
    /// </summary>
    private void UnloadAllRecursive() {
        global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
    }
    
    public sealed class Shaders {
        
        private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[] {
                    new global::TypeSafe.Resource<global::UnityEngine.Shader>("TMPro_Bitmap-Mobile", "Shaders/TMPro_Bitmap-Mobile"),
                    new global::TypeSafe.Resource<global::UnityEngine.TextAsset>("TMPro_Surface", "Shaders/TMPro_Surface"),
                    new global::TypeSafe.Resource<global::UnityEngine.Shader>("TMPro_SDF-Surface", "Shaders/TMPro_SDF-Surface"),
                    new global::TypeSafe.Resource<global::UnityEngine.Shader>("TMPro_SDF", "Shaders/TMPro_SDF"),
                    new global::TypeSafe.Resource<global::UnityEngine.Shader>("TMPro_SDF-Mobile Overlay", "Shaders/TMPro_SDF-Mobile Overlay"),
                    new global::TypeSafe.Resource<global::UnityEngine.TextAsset>("TMPro_Properties", "Shaders/TMPro_Properties"),
                    new global::TypeSafe.Resource<global::UnityEngine.Shader>("TMPro_SDF-Surface-Mobile", "Shaders/TMPro_SDF-Surface-Mobile"),
                    new global::TypeSafe.Resource<global::UnityEngine.Shader>("TMPro_SDF Overlay", "Shaders/TMPro_SDF Overlay"),
                    new global::TypeSafe.Resource<global::UnityEngine.TextAsset>("TMPro", "Shaders/TMPro"),
                    new global::TypeSafe.Resource<global::UnityEngine.Shader>("TMPro_Bitmap", "Shaders/TMPro_Bitmap"),
                    new global::TypeSafe.Resource<global::UnityEngine.Shader>("TMPro_SDF-Mobile", "Shaders/TMPro_SDF-Mobile")});
        
        private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
        
        private Shaders() {
        }
        
        public static global::TypeSafe.Resource<global::UnityEngine.Shader> TMPro_BitmapMobile {
            get {
                return ((global::TypeSafe.Resource<global::UnityEngine.Shader>)(__ts_internal_resources[0]));
            }
        }
        
        public static global::TypeSafe.Resource<global::UnityEngine.TextAsset> TMPro_Surface {
            get {
                return ((global::TypeSafe.Resource<global::UnityEngine.TextAsset>)(__ts_internal_resources[1]));
            }
        }
        
        public static global::TypeSafe.Resource<global::UnityEngine.Shader> TMPro_SDFSurface {
            get {
                return ((global::TypeSafe.Resource<global::UnityEngine.Shader>)(__ts_internal_resources[2]));
            }
        }
        
        public static global::TypeSafe.Resource<global::UnityEngine.Shader> TMPro_SDF {
            get {
                return ((global::TypeSafe.Resource<global::UnityEngine.Shader>)(__ts_internal_resources[3]));
            }
        }
        
        public static global::TypeSafe.Resource<global::UnityEngine.Shader> TMPro_SDFMobile_Overlay {
            get {
                return ((global::TypeSafe.Resource<global::UnityEngine.Shader>)(__ts_internal_resources[4]));
            }
        }
        
        public static global::TypeSafe.Resource<global::UnityEngine.TextAsset> TMPro_Properties {
            get {
                return ((global::TypeSafe.Resource<global::UnityEngine.TextAsset>)(__ts_internal_resources[5]));
            }
        }
        
        public static global::TypeSafe.Resource<global::UnityEngine.Shader> TMPro_SDFSurfaceMobile {
            get {
                return ((global::TypeSafe.Resource<global::UnityEngine.Shader>)(__ts_internal_resources[6]));
            }
        }
        
        public static global::TypeSafe.Resource<global::UnityEngine.Shader> TMPro_SDF_Overlay {
            get {
                return ((global::TypeSafe.Resource<global::UnityEngine.Shader>)(__ts_internal_resources[7]));
            }
        }
        
        public static global::TypeSafe.Resource<global::UnityEngine.TextAsset> TMPro {
            get {
                return ((global::TypeSafe.Resource<global::UnityEngine.TextAsset>)(__ts_internal_resources[8]));
            }
        }
        
        public static global::TypeSafe.Resource<global::UnityEngine.Shader> TMPro_Bitmap {
            get {
                return ((global::TypeSafe.Resource<global::UnityEngine.Shader>)(__ts_internal_resources[9]));
            }
        }
        
        public static global::TypeSafe.Resource<global::UnityEngine.Shader> TMPro_SDFMobile {
            get {
                return ((global::TypeSafe.Resource<global::UnityEngine.Shader>)(__ts_internal_resources[10]));
            }
        }
        
        /// <summary>
        /// Return a list of all resources in this folder.
        /// This method has a very low performance cost, no need to cache the result.
        /// </summary>
        /// <returns>A list of resource objects in this folder.</returns>
        public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
            return __ts_internal_resources;
        }
        
        /// <summary>
        /// Return a list of all resources in this folder and all sub-folders.
        /// The result of this method is cached, so subsequent calls will have very low performance cost.
        /// </summary>
        /// <returns>A list of resource objects in this folder and sub-folders.</returns>
        public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
            if ((__ts_internal_recursiveLookupCache != null)) {
                return __ts_internal_recursiveLookupCache;
            }
            global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
            tmp.AddRange(GetContents());
            __ts_internal_recursiveLookupCache = tmp;
            return __ts_internal_recursiveLookupCache;
        }
        
        /// <summary>
        /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
        /// This method does not cache the result, so you should cache the result yourself if you will use it often.
        /// </summary>
        /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
        public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
            where TResource : global::UnityEngine.Object {
            return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
        }
        
        /// <summary>
        /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
        /// This method does not cache the result, so you should cache the result yourself if you will use it often.
        /// </summary>
        /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
        public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
            where TResource : global::UnityEngine.Object {
            return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
        }
        
        /// <summary>
        /// Call Unload() on every loaded resource in this folder.
        /// </summary>
        public static void UnloadAll() {
            global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
        }
        
        /// <summary>
        /// Call Unload() on every loaded resource in this folder and subfolders.
        /// </summary>
        private void UnloadAllRecursive() {
            global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
        }
    }
    
    public sealed class UI {
        
        private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[] {
                    new global::TypeSafe.PrefabResource("level_clear", "UI/level_clear"),
                    new global::TypeSafe.PrefabResource("stage_clear", "UI/stage_clear")});
        
        private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
        
        private UI() {
        }
        
        public static global::TypeSafe.PrefabResource level_clear {
            get {
                return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[0]));
            }
        }
        
        public static global::TypeSafe.PrefabResource stage_clear {
            get {
                return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[1]));
            }
        }
        
        /// <summary>
        /// Return a list of all resources in this folder.
        /// This method has a very low performance cost, no need to cache the result.
        /// </summary>
        /// <returns>A list of resource objects in this folder.</returns>
        public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
            return __ts_internal_resources;
        }
        
        /// <summary>
        /// Return a list of all resources in this folder and all sub-folders.
        /// The result of this method is cached, so subsequent calls will have very low performance cost.
        /// </summary>
        /// <returns>A list of resource objects in this folder and sub-folders.</returns>
        public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
            if ((__ts_internal_recursiveLookupCache != null)) {
                return __ts_internal_recursiveLookupCache;
            }
            global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
            tmp.AddRange(GetContents());
            __ts_internal_recursiveLookupCache = tmp;
            return __ts_internal_recursiveLookupCache;
        }
        
        /// <summary>
        /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
        /// This method does not cache the result, so you should cache the result yourself if you will use it often.
        /// </summary>
        /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
        public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
            where TResource : global::UnityEngine.Object {
            return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
        }
        
        /// <summary>
        /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
        /// This method does not cache the result, so you should cache the result yourself if you will use it often.
        /// </summary>
        /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
        public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
            where TResource : global::UnityEngine.Object {
            return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
        }
        
        /// <summary>
        /// Call Unload() on every loaded resource in this folder.
        /// </summary>
        public static void UnloadAll() {
            global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
        }
        
        /// <summary>
        /// Call Unload() on every loaded resource in this folder and subfolders.
        /// </summary>
        private void UnloadAllRecursive() {
            global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
        }
    }
    
    public sealed class player {
        
        private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[] {
                    new global::TypeSafe.PrefabResource("GoatMan", "player/GoatMan")});
        
        private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
        
        private player() {
        }
        
        public static global::TypeSafe.PrefabResource GoatMan {
            get {
                return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[0]));
            }
        }
        
        /// <summary>
        /// Return a list of all resources in this folder.
        /// This method has a very low performance cost, no need to cache the result.
        /// </summary>
        /// <returns>A list of resource objects in this folder.</returns>
        public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
            return __ts_internal_resources;
        }
        
        /// <summary>
        /// Return a list of all resources in this folder and all sub-folders.
        /// The result of this method is cached, so subsequent calls will have very low performance cost.
        /// </summary>
        /// <returns>A list of resource objects in this folder and sub-folders.</returns>
        public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
            if ((__ts_internal_recursiveLookupCache != null)) {
                return __ts_internal_recursiveLookupCache;
            }
            global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
            tmp.AddRange(GetContents());
            __ts_internal_recursiveLookupCache = tmp;
            return __ts_internal_recursiveLookupCache;
        }
        
        /// <summary>
        /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
        /// This method does not cache the result, so you should cache the result yourself if you will use it often.
        /// </summary>
        /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
        public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
            where TResource : global::UnityEngine.Object {
            return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
        }
        
        /// <summary>
        /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
        /// This method does not cache the result, so you should cache the result yourself if you will use it often.
        /// </summary>
        /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
        public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
            where TResource : global::UnityEngine.Object {
            return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
        }
        
        /// <summary>
        /// Call Unload() on every loaded resource in this folder.
        /// </summary>
        public static void UnloadAll() {
            global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
        }
        
        /// <summary>
        /// Call Unload() on every loaded resource in this folder and subfolders.
        /// </summary>
        private void UnloadAllRecursive() {
            global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
        }
    }
    
    public sealed class Fonts_Materials {
        
        private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[] {
                    new global::TypeSafe.Resource<global::TMPro.TextMeshProFont>("ARIAL SDF", "Fonts & Materials/ARIAL SDF")});
        
        private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
        
        private Fonts_Materials() {
        }
        
        public static global::TypeSafe.Resource<global::TMPro.TextMeshProFont> ARIAL_SDF {
            get {
                return ((global::TypeSafe.Resource<global::TMPro.TextMeshProFont>)(__ts_internal_resources[0]));
            }
        }
        
        /// <summary>
        /// Return a list of all resources in this folder.
        /// This method has a very low performance cost, no need to cache the result.
        /// </summary>
        /// <returns>A list of resource objects in this folder.</returns>
        public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
            return __ts_internal_resources;
        }
        
        /// <summary>
        /// Return a list of all resources in this folder and all sub-folders.
        /// The result of this method is cached, so subsequent calls will have very low performance cost.
        /// </summary>
        /// <returns>A list of resource objects in this folder and sub-folders.</returns>
        public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
            if ((__ts_internal_recursiveLookupCache != null)) {
                return __ts_internal_recursiveLookupCache;
            }
            global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
            tmp.AddRange(GetContents());
            __ts_internal_recursiveLookupCache = tmp;
            return __ts_internal_recursiveLookupCache;
        }
        
        /// <summary>
        /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
        /// This method does not cache the result, so you should cache the result yourself if you will use it often.
        /// </summary>
        /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
        public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
            where TResource : global::UnityEngine.Object {
            return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
        }
        
        /// <summary>
        /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
        /// This method does not cache the result, so you should cache the result yourself if you will use it often.
        /// </summary>
        /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
        public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
            where TResource : global::UnityEngine.Object {
            return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
        }
        
        /// <summary>
        /// Call Unload() on every loaded resource in this folder.
        /// </summary>
        public static void UnloadAll() {
            global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
        }
        
        /// <summary>
        /// Call Unload() on every loaded resource in this folder and subfolders.
        /// </summary>
        private void UnloadAllRecursive() {
            global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
        }
    }
}
