/// <summary>basic component of all characterComponent</summary>
/// <remarks>sub class must override Tick FixedTick and also inherit constructor</remarks>
public class CharacterComponent {
    // ref for component parent
    Character parent = null;
    /// <summary>ref for character parent READONLY</summary>
    public Character Parent { get { return parent; } }
    //bool to determine component be enable or disable
    bool bEnable = true;
    /// <summary>Property to enable or disable this component</summary>
    public bool IsEnable { get { return bEnable; } set { if (value == false) Disable ( ); bEnable = value; } }
    /// <summary>set parent and decide if enable this component when it's been spawned</summary>
    public CharacterComponent (Enemy parent, bool IsEnable = true) {
        this.parent = parent;
        this.bEnable = IsEnable;
        parent.AddComponent (this);
    }
    /// <summary>Character keep call this method to Update component</summary>
    /// <remarks>if component has parent and also enable keep Update</remarks>
    public void Update ( ) {
        if (!bEnable || parent == null)
            return;
        Tick ( );
    }
    /// <summary>Character keep call this method to FixedUpdate component</summary>
    /// <remarks>if component has parent and also enable keep Update</remarks>
    public void FixedUpdate ( ) {
        if (!bEnable || parent == null)
            return;
        FixedTick ( );
    }
    // sub class override this method which define action each frame
    protected virtual void Tick ( ) { }
    // sub class override this method which define action each fixedFrame
    protected virtual void FixedTick ( ) { }
    protected virtual void Disable ( ) { }
}
