using System;

namespace ChessX.Game.Rulesets
{
    public class RulesetInfo : IEquatable<RulesetInfo>, IComparable<RulesetInfo>
    {
        public string Name { get; set; }

        public string InstantiationInfo { get; set; }

        public RulesetInfo(string name, string instantiationInfo)
        {
            Name = name;
            InstantiationInfo = instantiationInfo;
        }

        public bool Equals(RulesetInfo other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other == null) return false;

            return Name == other.Name;
        }

        public int CompareTo(RulesetInfo other)
        {
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            hashCode.Add(Name);
            return hashCode.ToHashCode();
        }

        public override string ToString() => Name;

        public RulesetInfo Clone() => new RulesetInfo(Name, InstantiationInfo);

        public Ruleset CreateInstance()
        {
            var type = Type.GetType(InstantiationInfo);

            if (type == null)
                throw new RulesetLoadException(@"Type lookup failure");

            var ruleset = Activator.CreateInstance(type) as Ruleset;

            if (ruleset == null)
                throw new RulesetLoadException(@"Instantiation failure");

            return ruleset;
        }
    }
}
