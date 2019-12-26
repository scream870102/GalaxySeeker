using Eccentric.Utils;

using UnityEngine;
namespace GalaxySeeker.Item {
	/// <summary>class define what an item has</summary>
	/// <remarks>include icon name and which inventory belongs</remarks>
	[RequireComponent (typeof (SpriteRenderer))]
	public class Item : MonoBehaviour {
		protected EItemType type = EItemType.NONE;
		SpriteRenderer sr = null;
		PlayerEquipment inventory = null;
		[SerializeField] Sprite icon = null;
		[SerializeField] Vector3 initPos = Vector3.zero;
		[SerializeField] float initRot = 0f;
		bool bItemCanUse = false;
		Player owner = null;
		protected Timer timer = null;
		new public string name = "New Item";
		public Player Owner => owner;
		public Vector3 InitPos => initPos;
		public float InitRot => initRot;
		public bool IsItemCanUse { set => bItemCanUse = value; }
		public PlayerEquipment Inventory { set { if (inventory == null) { inventory = value; owner = inventory.Parent; Init ( ); } } get => inventory; }
		public SpriteRenderer SpriteRenderer => sr;
		public Sprite Icon => icon;
		public EItemType Type => type;
		public float CD {
			get {
				if (timer != null)return timer.Remain01;
				else return 1.0f;
			}
		}

		/// <summary>if Item already used this event will invoke</summary>
		/// <param name="string">item name</param>
		public event System.Action<string> OnItemUsed;
		//when enable reset Item
		void OnEnable ( ) {
			sr = GetComponent<SpriteRenderer> ( );
			bItemCanUse = false;
		}

		//if item state is using keep call UsingItem()
		void Update ( ) {
			if (!bItemCanUse)
				return;
			UsingItem ( );
		}

		//define action when item is being used update every frame
		//child class must override this method
		protected virtual void UsingItem ( ) {
			// Use the item
			// Something may happen
			//must call AlreadyUsed when finish using item
			//And if player is using item must call BeginUsing()
		}

		///<summary>if item finish its action call this method to reset all field and invoke event OnItemUsed</summary>
		protected void AlreadyUsed ( ) {
			Reset ( );
			if (OnItemUsed != null)
				OnItemUsed (name);
		}
		/// <summary>Item is being used and going to remove from hierarchy</summary>
		public void RemoveFromInventory ( ) {
			if (inventory == null) {
				Debug.LogWarning ("No Inventory");
				return;
			}
			inventory.RemoveItem (this);
			RemoveItem ( );
			Destroy (this);
		}

		//define reset action 
		//child class can override this method to add something need to reset
		protected virtual void Reset ( ) { }
		//define the action before remove item from inventory
		//can override
		protected virtual void RemoveItem ( ) { }
		//define what action should do when item is being pick up by player 
		//can override
		protected virtual void Init ( ) { }
		//when player is using item must call this method 
		//this is to prevent player using over one item at the same time
		//must call
		protected void BeginUsing ( ) {
			inventory.IsItemUsing = true;
		}

	}

	[System.Serializable]
	public enum EItemType {
		JETPACK,
		SHOES,
		ROPE,
		NONE,
	}

}
