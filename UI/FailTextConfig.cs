

namespace CustomFailText.UI
{
    public class FailTextConfig
    {
        internal static BS_Utils.Utilities.Config FailConfig = new BS_Utils.Utilities.Config("CustomFailText/CustomFailText");
        internal static bool italicText;
        internal static bool enablePlugin;

        /// <summary>
        /// This is called whenever BSIPA reads the config from disk (including when file changes are detected).
        /// </summary>
        public virtual void OnReload()
        { }

        /// <summary>
        /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
        /// </summary>
        public virtual void Changed()
        { }

        /// <summary>
        /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
        /// </summary>
        public virtual void CopyFrom(FailTextConfig other)
        { }
    }
}
