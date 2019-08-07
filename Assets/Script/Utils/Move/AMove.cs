using UnityEngine;
namespace GalaxySeeker.Move {
    /// <summary>this is the abstract class for every movement</summary>
    /// <remarks>child class must override GetNextPos and define bFacingRight</remarks>
    public abstract class AMove {
        //true calculate with deltaTime false calculate with fixedDeltaTime
        protected bool bUseDeltaTime;
        protected bool bFacingRight;
        /// <summary>define is this movement now face to right direction</summary>
        public bool IsFacingRight { get { return bFacingRight; } set { bFacingRight = value; } }
        /// <summary>Outer class call this method to get the next position they should go</summary>
        /// <param name="refPos">the current position of owner READONLY</param>
        public abstract Vector2 GetNextPos (in Vector2 refPos);
        protected Vector2 initPos;
        public AMove (Vector2 InitPos, bool IsUseDeltaTime = true) {
            this.initPos = InitPos;
            this.bUseDeltaTime = IsUseDeltaTime;
        }
    }

}
