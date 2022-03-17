using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using osu.Framework;
using osu.Framework.Logging;

namespace ChessX.Game.Rulesets
{
    public class RulesetStore : IDisposable
    {
        private const string ruleset_library_prefix = @"ChessX.Game.Rulesets";

        private readonly Dictionary<Assembly, Type> loadedAssemblies = new();

        /// <summary>
        /// All available rulesets.
        /// </summary>
        public IEnumerable<RulesetInfo> Rulesets => rulesets;

        private readonly List<RulesetInfo> rulesets = new();

        public RulesetStore()
        {
            // On android in release configuration assemblies are loaded from the apk directly into memory.
            // We cannot read assemblies from cwd, so should check loaded assemblies instead.
            loadFromAppDomain();

            // This null check prevents Android from attempting to load the rulesets from disk,
            // as the underlying path "AppContext.BaseDirectory", despite being non-nullable, it returns null on android.
            // See https://github.com/xamarin/xamarin-android/issues/3489.
            if (RuntimeInfo.StartupDirectory != null)
                loadFromDisk();

            // the event handler contains code for resolving dependency on the game assembly for rulesets located outside the base game directory.
            // It needs to be attached to the assembly lookup event before the actual call to loadUserRulesets() else rulesets located out of the base game directory will fail
            // to load as unable to locate the game core assembly.
            AppDomain.CurrentDomain.AssemblyResolve += resolveRulesetDependencyAssembly;

            gatherRulesetInfos();
        }

        /// <summary>
        /// Retrieve a ruleset using a known name.
        /// </summary>
        /// <param name="name">The ruleset's name.</param>
        /// <returns>A ruleset, if available, else null.</returns>
        public RulesetInfo GetRuleset(string name) => Rulesets.FirstOrDefault(r => r.Name == name);

        private Assembly resolveRulesetDependencyAssembly(object sender, ResolveEventArgs args)
        {
            var asm = new AssemblyName(args.Name);

            // the requesting assembly may be located out of the executable's base directory, thus requiring manual resolving of its dependencies.
            // this attempts resolving the ruleset dependencies on game core and framework assemblies by returning assemblies with the same assembly name
            // already loaded in the AppDomain.
            var domainAssembly = AppDomain.CurrentDomain.GetAssemblies()
                                          // Given name is always going to be equally-or-more qualified than the assembly name.
                                          .Where(a =>
                                          {
                                              string name = a.GetName().Name;
                                              if (name == null)
                                                  return false;

                                              return args.Name.Contains(name, StringComparison.Ordinal);
                                          })
                                          // Pick the greatest assembly version.
                                          .OrderByDescending(a => a.GetName().Version)
                                          .FirstOrDefault();

            if (domainAssembly != null)
                return domainAssembly;

            return loadedAssemblies.Keys.FirstOrDefault(a => a.FullName == asm.FullName);
        }

        private void loadFromAppDomain()
        {
            foreach (var ruleset in AppDomain.CurrentDomain.GetAssemblies())
            {
                string rulesetName = ruleset.GetName().Name;

                if (rulesetName == null)
                    continue;

                if (!rulesetName.StartsWith(ruleset_library_prefix, StringComparison.InvariantCultureIgnoreCase) || rulesetName.Contains(@"Tests"))
                    continue;

                addRuleset(ruleset);
            }
        }

        private void loadFromDisk()
        {
            try
            {
                string[] files = Directory.GetFiles(RuntimeInfo.StartupDirectory, @$"{ruleset_library_prefix}.*.dll");

                foreach (string file in files.Where(f => !Path.GetFileName(f).Contains("Tests")))
                    loadRulesetFromFile(file);
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Could not load rulesets from directory {RuntimeInfo.StartupDirectory}");
            }
        }

        private void loadRulesetFromFile(string file)
        {
            string filename = Path.GetFileNameWithoutExtension(file);

            if (loadedAssemblies.Values.Any(t => Path.GetFileNameWithoutExtension(t.Assembly.Location) == filename))
                return;

            try
            {
                addRuleset(Assembly.LoadFrom(file));
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Failed to load ruleset {filename}");
            }
        }

        private void addRuleset(Assembly assembly)
        {
            if (loadedAssemblies.ContainsKey(assembly))
                return;

            // the same assembly may be loaded twice in the same AppDomain (currently a thing in certain Rider versions https://youtrack.jetbrains.com/issue/RIDER-48799).
            // as a failsafe, also compare by FullName.
            if (loadedAssemblies.Any(a => a.Key.FullName == assembly.FullName))
                return;

            try
            {
                loadedAssemblies[assembly] = assembly.GetTypes().First(t => t.IsPublic && t.IsSubclassOf(typeof(Ruleset)));
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Failed to add ruleset {assembly}");
            }
        }

        private void gatherRulesetInfos()
        {
            var rulesets = loadedAssemblies.Values
                                           .Select(r => Activator.CreateInstance(r) as Ruleset)
                                           .Where(r => r != null)
                                           .Select(r => r.RulesetInfo);

            foreach (var r in rulesets.OrderBy(r => r))
            {
                try
                {
                    var resolvedType = Type.GetType(r.InstantiationInfo)
                                       ?? throw new RulesetLoadException(@"Type could not be resolved");

                    var instanceInfo = (Activator.CreateInstance(resolvedType) as Ruleset)?.RulesetInfo
                                       ?? throw new RulesetLoadException(@"Instantiation failure");

                    // If a ruleset isn't up-to-date with the API, it could cause a crash at an arbitrary point of execution.
                    // To eagerly handle cases of missing implementations, enumerate all types here and mark as non-available on throw.
                    resolvedType.Assembly.GetTypes();

                    r.Name = instanceInfo.Name;
                    r.InstantiationInfo = instanceInfo.InstantiationInfo;

                    this.rulesets.Add(r.Clone());
                }
                catch (Exception ex)
                {
                    Logger.Log($"Could not load ruleset {r}: {ex.Message}");
                }
            }

            this.rulesets.AddRange(this.rulesets.OrderBy(r => r));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            AppDomain.CurrentDomain.AssemblyResolve -= resolveRulesetDependencyAssembly;
        }
    }
}
