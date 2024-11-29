using System.ComponentModel;

namespace HardwareHero.Shared.Exceptions
{
    [Serializable]
    public class DependDeleteException : Exception
    {
        public DependDeleteException()
            : base("Cannot delete an object because something depend on it")
        { }

        public DependDeleteException(string message)
            : base(message)
        { }

        public static void ThrowIfConflict(int refCount, string objName)
        {
            if (refCount > 0)
            {
                throw new DependDeleteException(
                    $"Cannot delete an object because {refCount} '{objName}' objects depend on it");
            }
        }
    }
}
