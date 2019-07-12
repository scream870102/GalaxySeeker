using System.Collections.Generic;

/// <summary>A base class for all character </summary>
public class Enemy : Character {
    // if this character enable or disable
    bool bEnable = true;
    /// <summary>Property for enable or disable all characterComponents on this character</summary>
    public bool IsEnable { set { if(value==false)DisableAllComponents();bEnable = value; } protected get { return bEnable; } }

    // field to store all component on this character;
    protected List<CharacterComponent> components = new List<CharacterComponent> ( );
    // call Init when this monoBehavior been spawned
    void Awake ( ) {
        Init ( );
    }

    // if this character is enable call Tick
    void Update ( ) {
        if (!IsEnable)
            return;
        Tick ( );
    }

    // if this character is enable call FixedTick
    void FixedUpdate ( ) {
        if (!IsEnable)
            return;
        FixedTick ( );
    }

    /// <summary>define what action should this monoClass do when it awaked</summary>
    protected virtual void Init ( ) {
        stats.Init ( );
        stats.OnHealthReachedZero += Dead;
    }

    /// <summary>define action should do each frame</summary>
    /// <remarks>sub class MUST override it and also call base.Tick</remarks>
    protected void Tick ( ) {
        foreach (CharacterComponent item in components)
            item.Update ( );
    }

    /// <summary>define action should do each fixedFrame</summary>
    /// <remarks>sub class MUST override it and also call base.Tick</remarks>
    protected void FixedTick ( ) {
        foreach (CharacterComponent item in components)
            item.FixedUpdate ( );
    }

    /// <summary>define action when character dead</summary>
    /// <remarks>sub class can override </remarks>
    protected virtual void Dead ( ) { }

    /// <summary>Add component to components</summary>
    public void AddComponent (CharacterComponent component) {
        components.Add (component);
    }
    void DisableAllComponents ( ) {
        foreach (CharacterComponent component in components) {
            component.IsEnable = false;
        }
    }
}
